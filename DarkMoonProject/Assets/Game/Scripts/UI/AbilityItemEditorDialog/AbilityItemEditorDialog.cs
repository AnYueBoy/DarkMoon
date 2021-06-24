/*
 * @Author: l hy 
 * @Date: 2021-06-24 09:38:22 
 * @Description: 能力编辑界面
 */

using System;
using UFramework.GameCommon;
using UnityEngine.UI;
public class AbilityItemEditorDialog : BaseUI {

	public Text title;

	public Text effectDescribe;

	public InputField customerValue;

	private int abilityItemId;

	private AbilityData abilityData;

	private void OnEnable () {
		this.customerValue.onValueChanged.AddListener ((string changeValue) => {
			if (String.IsNullOrEmpty (changeValue)) {
				this.abilityData.baseValue = 0;
			} else {
				this.abilityData.baseValue = int.Parse (changeValue);
			}

			this.refreshItemInfo ();
		});
	}

	public override void onShow (params object[] args) {
		base.onShow (args);
		this.abilityItemId = (int) args[0];
		this.abilityData = AppContext.instance.customDataManager.abilityPoolDataDic[this.abilityItemId];
	}

	private void refreshItemInfo () {
		// 显示能力信息
		this.title.text = this.abilityData.abilityName;
		this.effectDescribe.text = this.abilityData.abilityEffect.Replace ("X", this.abilityData.baseValue.ToString ());
	}

	public void close () {
		AppContext.instance.configManager.saveAbilityPoolConfig ();
		AppContext.instance.uIManager.closeDialog (UIPath.AbilityItemEditorDialog);
	}

}