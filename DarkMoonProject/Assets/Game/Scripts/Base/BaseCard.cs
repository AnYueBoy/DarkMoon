/*
 * @Author: l hy 
 * @Date: 2020-11-18 16:44:09 
 * @Description: 卡牌
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UFramework;
using UnityEngine;
using UnityEngine.UI;

public class BaseCard : MonoBehaviour {
    protected int currentLevel = 1;

    /*即将废弃*/
    protected CardData cardData = null;

    #region ui相关

    public Image iconImage = null;

    public Image cardColorBgImage = null;

    public Text describeText = null;

    public InputField cardName = null;

    public InputField cardConsume = null;

    public Image cardConsumeBg = null;

    private CustomCardData customCardData;

    #endregion

    public void init (CustomCardData cardData) {
        this.customCardData = cardData;
    }

    private void showCardInfo () {
        // 显示卡牌描述
        string describeStr = "";
        foreach (AbilityData abilityData in this.customCardData.abilities) {
            string replaceStr = abilityData.abilityEffect.Replace ("X", abilityData.baseValue.ToString ());
            describeStr += replaceStr + "\n";
        }
        this.describeText.text = describeStr;

        // 显示卡牌耗费
        this.showCardConsume ();

        // 显示卡牌名称
        this.cardName.text = this.customCardData.cardName;

        // 显示卡牌颜色背景
        this.cardColorBgImage.color = Util.getColorByCardType (this.customCardData.cardType);

        // 显示卡牌icon
        this.showCardIcon ();
    }

    private void showCardConsume () {
        // 显示卡牌耗费背景
        this.cardConsumeBg.gameObject.SetActive (true);
        CardTypeEnum cardType = this.customCardData.cardType;
        switch (cardType) {
            case CardTypeEnum.ACTION:
                string actionUrl = CustomUrlString.consumePreTexture + cardType;
                this.cardConsumeBg.sprite = AppContext.instance.assetsManager.getAssetByUrlSync<Sprite> (actionUrl);
                break;

            case CardTypeEnum.SPELL:
                string spellUrl = CustomUrlString.consumePreTexture + cardType;
                this.cardConsumeBg.sprite = AppContext.instance.assetsManager.getAssetByUrlSync<Sprite> (spellUrl);
                break;

            case CardTypeEnum.BLEED:
                string bleedUrl = CustomUrlString.consumePreTexture + cardType;
                this.cardConsumeBg.sprite = AppContext.instance.assetsManager.getAssetByUrlSync<Sprite> (bleedUrl);
                break;

            case CardTypeEnum.EQUIPMENT:
            case CardTypeEnum.ATTACK:
            case CardTypeEnum.MAGIC:
            case CardTypeEnum.PRAY:
            case CardTypeEnum.REFLEX:
            case CardTypeEnum.SPECIAL:
                this.cardConsumeBg.gameObject.SetActive (false);
                break;

            default:
                Debug.LogWarning ("card type exceed value: " + cardType);
                break;
        }

        // 显示卡牌消耗数值
        this.cardConsume.text = this.customCardData.consumeEnergy.ToString ();
    }

    private void showCardIcon () {
        string textureUrl = this.customCardData.textureUrl;
        if (String.IsNullOrEmpty (textureUrl)) {
            this.iconImage.sprite = null;
            return;
        }

        this.splitUrl (textureUrl);
        if (!String.IsNullOrEmpty (this.iconUrl)) {
            this.iconImage.sprite = AppContext.instance.assetsManager.getAssetByUrlSync<Sprite> (this.iconUrl);
        } else {
            this.iconImage.sprite = AppContext.instance.assetsManager.getAssetByBundleSync<Sprite> (this.bundleName, this.assetName);
        }
    }

    private string iconUrl;
    private string bundleName;
    private string assetName;

    public void splitUrl (string assetUrl) {
        this.iconUrl = null;
        this.bundleName = null;
        this.assetName = null;
        string[] urls = assetUrl.Split (':');
        if (urls.Length > 1) {
            this.bundleName = urls[0];
            this.bundleName = urls[1];
        } else {
            this.iconUrl = urls[0];
        }
    }

    #region 战斗相关

    protected bool consumeCheck () {
        CampEnum targetCamp = cardData.camp;
        BaseRoleData targetData = AppContext.instance.dataManager.getCampRoleData (targetCamp);
        if (targetData.energy < cardData.energyConsume) {
            return false;
        }
        return true;
    }

    protected void turnBegin () {
        Dictionary<int, AbilityData> abilityDataDic = cardData.abilityDataDic;
        if (abilityDataDic == null) {
            Debug.LogWarning ("abilityDic not exist in turnBegin");
            return;
        }
        Dictionary<int, BaseAbility> abilityDic = AppContext.instance.abilityManager.abilityDic;
        if (abilityDic == null) {
            Debug.LogWarning ("abilityDic not exist in turnBegin");
            return;
        }
        foreach (int id in cardData.abilityDataDic.Keys) {
            BaseAbility targetAbility = abilityDic[id];
            targetAbility.turnBeginEffect ();
        }
    }

    protected void playerTrigger () {
        Dictionary<int, AbilityData> abilityDataDic = cardData.abilityDataDic;
        if (abilityDataDic == null) {
            Debug.LogWarning ("abilityDic not exist in playerTrigger");
            return;
        }
        Dictionary<int, BaseAbility> abilityDic = AppContext.instance.abilityManager.abilityDic;
        if (abilityDic == null) {
            Debug.LogWarning ("abilityDic not exist in playerTrigger");
            return;
        }
        foreach (int id in cardData.abilityDataDic.Keys) {
            BaseAbility targetAbility = abilityDic[id];
            targetAbility.effect ();
        }
    }

    protected void turnEnd () {
        Dictionary<int, AbilityData> abilityDataDic = cardData.abilityDataDic;
        if (abilityDataDic == null) {
            Debug.LogWarning ("abilityDic not exist in turnEnd");
            return;
        }
        Dictionary<int, BaseAbility> abilityDic = AppContext.instance.abilityManager.abilityDic;
        if (abilityDic == null) {
            Debug.LogWarning ("abilityDic not exist in turnEnd");
            return;
        }
        foreach (int id in cardData.abilityDataDic.Keys) {
            BaseAbility targetAbility = abilityDic[id];
            targetAbility.turnEndEffect ();
        }
    }

    #endregion
}