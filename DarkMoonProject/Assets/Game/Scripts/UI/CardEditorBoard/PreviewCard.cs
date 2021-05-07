/*
 * @Author: l hy 
 * @Date: 2020-12-21 21:24:52 
 * @Description: 预览区卡牌
 * @Last Modified by: l hy
 * @Last Modified time: 2021-05-07 22:38:47
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UFramework;
using UnityEngine;
using UnityEngine.UI;

public class PreviewCard : MonoBehaviour {

    #region ui相关

    public Image iconImage = null;

    public Image cardColorBgImage = null;

    public Text describeText = null;

    public InputField cardName = null;

    public InputField cardConsume = null;

    #endregion

    private CustomCardData previewData = null;

    private void OnEnable () {
        this.addChangeListener ();
    }

    public void init (CustomCardData previewData) {
        this.previewData = previewData;
        this.showCardInfo ();
    }

    private void addChangeListener () {
        this.cardName.onValueChanged.AddListener ((string cardNameValue) => {
            this.previewData.cardName = cardNameValue;
            this.showCardInfo ();
        });

        this.cardConsume.onValueChanged.AddListener ((String consumeValue) => {
            if (String.IsNullOrEmpty (consumeValue)) {
                this.previewData.consumeEnergy = 0;
            } else {
                this.previewData.consumeEnergy = int.Parse (consumeValue);
            }
            this.showCardInfo ();
        });
    }

    private void showCardInfo () {
        // 显示卡牌描述
        string describeStr = "";
        foreach (AbilityData abilityData in this.previewData.abilities) {
            string replaceStr = abilityData.abilityEffect.Replace ("X", abilityData.baseValue.ToString ());
            describeStr += replaceStr + "\n";
        }
        this.describeText.text = describeStr;

        // TODO: 根据卡牌类型替换卡牌bg2，并决定是否显示卡牌耗费
        // 如果显示耗费要决定耗费的样式

        // 显示卡牌耗费
        this.cardConsume.text = this.previewData.consumeEnergy.ToString ();

        // 显示卡牌名称
        this.cardName.text = this.previewData.cardName;

        // 显示卡牌颜色背景
        this.cardColorBgImage.color = Util.getColorByCardType (this.previewData.cardType);
    }

    private void OnDisable () {
        this.cardName.onValueChanged.RemoveAllListeners ();
        this.cardConsume.onValueChanged.RemoveAllListeners ();
    }
}