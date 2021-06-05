/*
 * @Author: l hy 
 * @Date: 2020-12-21 21:24:52 
 * @Description: 预览区卡牌
 * @Last Modified by: l hy
 * @Last Modified time: 2021-05-27 11:28:51
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

    public Image cardConsumeBg = null;

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

        // 显示卡牌耗费
        this.showCardConsume ();

        // 显示卡牌名称
        this.cardName.text = this.previewData.cardName;

        // 显示卡牌颜色背景
        this.cardColorBgImage.color = Util.getColorByCardType (this.previewData.cardType);

        // 显示卡牌icon
        this.showCardIcon ();
    }

    private void showCardConsume () {
        // 显示卡牌耗费背景
        this.cardConsumeBg.gameObject.SetActive (true);
        CardTypeEnum cardType = this.previewData.cardType;
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
        this.cardConsume.text = this.previewData.consumeEnergy.ToString ();
    }

    private void showCardIcon () {
        string textureUrl = this.previewData.textureUrl;
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

    private void OnDisable () {
        this.cardName.onValueChanged.RemoveAllListeners ();
        this.cardConsume.onValueChanged.RemoveAllListeners ();
    }
}