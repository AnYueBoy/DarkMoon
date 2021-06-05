/*
 * @Author: l hy 
 * @Date: 2020-12-07 14:31:33 
 * @Description: 卡牌编辑界面
 */

using System;
using System.Collections.Generic;
using System.IO;
using LitJson;
using UFramework.GameCommon;
using UnityEngine;
using UnityEngine.UI;

public class CardEditorBoard : BaseUI {

    public GameObject abilityContent = null;

    public GameObject cardIconContent = null;

    public EditorCard editorCard = null;

    public Dropdown cardTypeDropDown = null;

    private List<AbilityItem> abilityItemList = new List<AbilityItem> ();

    private List<CardIcon> cardIconList = new List<CardIcon> ();

    private CustomCardData editorCardData = null;

    private void OnEnable () {
        this.init ();

        // 添加监听
        this.cardTypeDropDown.onValueChanged.AddListener ((int selectedIndex) => {
            this.editorCardData.cardType = (CardTypeEnum) selectedIndex;
            this.editorCard.init (this.editorCardData);
        });
    }

    private void init () {
        // 创建要保存的卡牌数据
        this.editorCardData = new CustomCardData ();
        this.editorCardData.cardType = (CardTypeEnum) this.cardTypeDropDown.value;
        this.editorCard.init (this.editorCardData);
    }

    public void loadAbilityListClick () {
        this.recycleAllAbilityItem ();
        // 加载能力插槽
        this.loadAbilityItems ();
    }

    public void loadCardIconClick () {
        this.recycleAllCardIcon ();
        // 加载卡牌icon
        this.loadCardIcons ();
    }

    public void resetCardClick () {
        // 重置卡牌
        this.loadAbilityListClick ();
        // 重置卡牌icon
        this.loadCardIconClick ();
        this.init ();
    }

    private void loadAbilityItems () {
        // 加载能力插槽item
        int abilityCount = AppContext.instance.customDataManager.abilityPoolDataDic.Count;
        int startIndex = this.abilityItemList.Count;
        GameObject abilityItemPrefab = AppContext.instance.assetsManager.getAssetByUrlSync<GameObject> (CustomUrlString.abilityPrefab);
        for (int i = startIndex; i < abilityCount; i++) {
            GameObject abilityItemNode = ObjectPool.instance.requestInstance (abilityItemPrefab);
            abilityItemNode.transform.SetParent (this.abilityContent.transform, false);
            AbilityItem abilityItem = abilityItemNode.GetComponent<AbilityItem> ();
            this.abilityItemList.Add (abilityItem);
        }

        this.refreshAbilityData ();
    }

    private void refreshAbilityData () {
        // 刷新插槽数据
        Dictionary<int, AbilityData> abilityDic = AppContext.instance.customDataManager.abilityPoolDataDic;
        int index = 0;
        foreach (AbilityData itemData in abilityDic.Values) {
            AbilityItem abilityItem = this.abilityItemList[index];
            abilityItem.gameObject.SetActive (true);
            abilityItem.init (itemData);
            index++;
        }
    }

    private void recycleAllAbilityItem () {
        // 回收所有能力item
        foreach (AbilityItem abilityItem in this.abilityItemList) {
            if (abilityItem == null) {
                continue;
            }
            if (!abilityItem.gameObject.activeSelf) {
                continue;
            }

            ObjectPool.instance.returnInstance (abilityItem.gameObject);
        }
    }

    private void loadCardIcons () {
        List<PackAsset> spriteAssets = AppContext.instance.assetsManager.getAllAssetsByUrlSync<Sprite> (CustomUrlString.cardIconTexture);
        int startIndex = this.cardIconList.Count;
        // 卡牌头像预制
        GameObject cardIconPrefab = AppContext.instance.assetsManager.getAssetByUrlSync<GameObject> (CustomUrlString.cardIconPrefab);
        for (int i = startIndex; i < spriteAssets.Count; i++) {
            GameObject cardIconNode = ObjectPool.instance.requestInstance (cardIconPrefab);
            cardIconNode.transform.SetParent (this.cardIconContent.transform, false);
            CardIcon cardIconImage = cardIconNode.GetComponent<CardIcon> ();
            this.cardIconList.Add (cardIconImage);
        }

        this.refreshCardIconData ();
    }

    private void refreshCardIconData () {
        List<PackAsset> spriteAssets = AppContext.instance.assetsManager.getAllAssetsByUrlSync<Sprite> (CustomUrlString.cardIconTexture);
        int index = 0;
        foreach (PackAsset asset in spriteAssets) {
            CardIcon cardIcon = this.cardIconList[index];
            cardIcon.gameObject.SetActive (true);
            cardIcon.init (asset.targetAsset as Sprite, asset.assetUrl, (string iconUrl) => {
                this.selectedIcon (iconUrl);
            });
            index++;
        }
    }

    private void selectedIcon (string iconUrl) {
        foreach (CardIcon cardIcon in this.cardIconList) {
            if (cardIcon == null) {
                continue;
            }
            cardIcon.unSelected ();
        }

        this.editorCardData.textureUrl = iconUrl;
        this.editorCard.init (this.editorCardData);
    }

    private void recycleAllCardIcon () {
        // 回收所有能力item
        foreach (CardIcon cardIcon in this.cardIconList) {
            if (cardIcon == null) {
                continue;
            }
            if (!cardIcon.gameObject.activeSelf) {
                continue;
            }

            ObjectPool.instance.returnInstance (cardIcon.gameObject);
        }
    }

    private void Update () {
        this.refreshAbilityState ();
    }

    private void refreshAbilityState () {
        if (this.abilityItemList == null || this.abilityItemList.Count <= 0) {
            return;
        }

        for (int i = 0; i < this.abilityItemList.Count; i++) {
            AbilityItem abilityItem = this.abilityItemList[i];
            if (abilityItem == null) {
                continue;
            }

            if (abilityItem.getJoinedState ()) {
                ObjectPool.instance.returnInstance (abilityItem.gameObject);
                this.editorCardData.abilities.Add (abilityItem.AbilityData);
                this.editorCard.init (this.editorCardData);
            }
        }
    }

    public void buildCardCompleted () {
        // 构建卡牌
        CardPoolData cardPoolData = AppContext.instance.customDataManager.cardPoolData;
        // TODO: id 改变需要确认
        int cardId = cardPoolData.cards.Count + 1;
        this.editorCardData.id = cardId;
        cardPoolData.cards.Add (this.editorCardData);

        Dictionary<int, CustomCardData> cardDisc = AppContext.instance.customDataManager.cardDataDic;
        cardDisc.Add (cardId, this.editorCardData);

        string cardPoolStr = JsonMapper.ToJson (cardPoolData);

        string filePath = Application.dataPath + "/Game/Resources/" + ConfigPath.cardPoolConfig + ".json";
        Debug.Log ("filePath: " + filePath);

        StreamWriter sw = new StreamWriter (filePath);
        sw.Write (cardPoolStr);
        sw.Close ();

        // TODO: 临时修复逻辑，需求确认卡牌保存成功后的具体逻辑
        this.resetCardClick ();
    }

    public void close_Click () {
        AppContext.instance.uIManager.showBoard (UIPath.HallBoard);
    }
}