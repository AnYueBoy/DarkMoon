/*
 * @Author: l hy 
 * @Date: 2020-11-18 16:44:09 
 * @Description: 卡牌
 */

using System;
using UFramework;
using UnityEngine;
using UnityEngine.UI;

public class BaseCard : MonoBehaviour {
    protected int currentLevel = 1;

    #region ui相关

    protected RectTransform rectTransform;

    public Image iconImage = null;

    public Image cardColorBgImage = null;

    public Text describeText = null;

    public InputField cardName = null;

    public InputField cardConsume = null;

    public Image cardConsumeBg = null;
    #endregion

    protected CustomCardData cardData;

    public virtual void init (CustomCardData cardData) {
        this.rectTransform = this.GetComponent<RectTransform> ();
        this.cardData = cardData;
        this.showCardInfo ();
    }

    protected void showCardInfo () {
        // 显示卡牌描述
        string describeStr = "";
        foreach (AbilityData abilityData in this.cardData.abilities) {
            string replaceStr = abilityData.abilityEffect.Replace ("X", abilityData.baseValue.ToString ());
            describeStr += replaceStr + "\n";
        }
        this.describeText.text = describeStr;

        // 显示卡牌耗费
        this.showCardConsume ();

        // 显示卡牌名称
        this.cardName.text = this.cardData.cardName;

        // 显示卡牌颜色背景
        this.cardColorBgImage.color = Util.getColorByCardType (this.cardData.cardType);

        // 显示卡牌icon
        this.showCardIcon ();
    }

    private void showCardConsume () {
        // 显示卡牌耗费背景
        this.cardConsumeBg.gameObject.SetActive (true);
        CardTypeEnum cardType = this.cardData.cardType;
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
        this.cardConsume.text = this.cardData.consumeEnergy.ToString ();
    }

    private void showCardIcon () {
        string textureUrl = this.cardData.textureUrl;
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
}