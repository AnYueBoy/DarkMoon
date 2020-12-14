/*
 * @Author: l hy 
 * @Date: 2020-12-07 14:31:33 
 * @Description: 卡牌编辑界面
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardEditorBoard : BaseUI {

    [Header ("插槽能力列表")]
    public ScrollRect scrollList = null;

    [Header ("列表节点")]
    public GameObject content = null;

    [Header ("能力item")]
    public GameObject abilityItemPrefab = null;

    private List<AbilityItem> abilityItemList = new List<AbilityItem> ();

    public void createCards_Click () {
        this.loadAbilities ();
    }

    private void loadAbilities () {
        if (this.abilityItemList.Count > 0) {
            this.refreshAbilityData ();
            return;
        }

        Dictionary<int, AbilityData> abilityDic = CustomDataManager.abilityPoolDataDic;

        foreach (AbilityData abilityData in abilityDic.Values) {
            GameObject abilityItemNode = ObjectPool.getInstance ().requestInstance (this.abilityItemPrefab);
            abilityItemNode.transform.SetParent (this.content.transform, false);
            AbilityItem abilityItem = abilityItemNode.GetComponent<AbilityItem> ();

            abilityItem.init (abilityData);
            this.abilityItemList.Add (abilityItem);
        }
    }

    private void refreshAbilityData () {

    }
}