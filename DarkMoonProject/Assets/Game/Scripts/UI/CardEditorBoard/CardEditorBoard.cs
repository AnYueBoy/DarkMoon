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

    [Header ("插槽能力列表")]
    public ScrollRect scrollList = null;

    [Header ("列表节点")]
    public GameObject content = null;

    [Header ("能力item")]
    public GameObject abilityItemPrefab = null;

    [Header ("卡片预览区")]
    public PreviewCard cardPreview = null;

    private List<AbilityItem> abilityItemList = new List<AbilityItem> ();

    private CustomCardData previewData = null;

    private void OnEnable () {
        this.init ();
    }

    private void init () {
        this.previewData = new CustomCardData ();
        this.cardPreview.init (this.previewData);
    }

    public void loadAbilityListClick () {
        this.recycleAllAbilityItem ();
        // 加载能力插槽
        this.loadAbilityItems ();
    }

    public void resetCardClick () {
        this.loadAbilityListClick ();
        this.init ();
    }

    private void loadAbilityItems () {
        // 加载能力插槽item
        int abilityCount = CustomDataManager.abilityPoolDataDic.Count;
        int startIndex = this.abilityItemList.Count;
        for (var i = startIndex; i < abilityCount; i++) {
            GameObject abilityItemNode = ObjectPool.instance.requestInstance (this.abilityItemPrefab);
            abilityItemNode.transform.SetParent (this.content.transform, false);
            AbilityItem abilityItem = abilityItemNode.GetComponent<AbilityItem> ();
            this.abilityItemList.Add (abilityItem);
        }

        this.refreshAbilityData ();
    }

    private void refreshAbilityData () {
        // 刷新插槽数据
        Dictionary<int, AbilityData> abilityDic = CustomDataManager.abilityPoolDataDic;
        int index = 0;
        foreach (AbilityData itemData in abilityDic.Values) {
            AbilityItem abilityItem = this.abilityItemList[index];
            abilityItem.gameObject.SetActive (true);
            abilityItem.init (itemData);
            index++;
        }
    }

    private void recycleAllAbilityItem () {
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
                this.previewData.abilities.Add (abilityItem.AbilityData);
                this.cardPreview.init (this.previewData);
            }
        }
    }

    public void buildCardCompleted () {
        CardPoolData cardPoolData = CustomDataManager.cardPoolData;
        // TODO: id 改变需要确认
        this.previewData.id = cardPoolData.cards.Count + 1;
        cardPoolData.cards.Add (previewData);

        string cardPoolStr = JsonMapper.ToJson (cardPoolData);

        string filePath = Application.dataPath + "/Game/Resources/" + CustomUrlString.cardJsonUrl + ".json";
        Debug.Log ("filePath: " + filePath);
        if (!File.Exists (filePath)) {
            Debug.LogError ("target file not exist");
            return;
        }

        StreamWriter sw = new StreamWriter (filePath);
        sw.Write (cardPoolStr);
        sw.Close ();
    }

    public void close_Click () {
        AppContext.instance.uIManager.showBoard (UIPath.HallBoard);
    }
}