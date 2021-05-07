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

    private CustomCardData previewData = new CustomCardData ();

    public void loadAbilityListClick () {
        this.recycleAllAbilityItem ();
        // 加载能力插槽
        this.loadAbilities ();
    }

    public void resetCardClick () {
        // 重置卡牌数据
        this.previewData = new CustomCardData ();

        this.loadAbilityListClick ();
    }

    private void loadAbilities () {
        Dictionary<int, AbilityData> abilityDic = CustomDataManager.abilityPoolDataDic;
        foreach (AbilityData abilityData in abilityDic.Values) {
            GameObject abilityItemNode = ObjectPool.instance.requestInstance (this.abilityItemPrefab);
            abilityItemNode.transform.SetParent (this.content.transform, false);
            AbilityItem abilityItem = abilityItemNode.GetComponent<AbilityItem> ();

            abilityItem.init (abilityData);
            this.abilityItemList.Add (abilityItem);
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
            if (!abilityItem) {
                continue;
            }

            if (abilityItem.getJoinedState ()) {
                ObjectPool.instance.returnInstance (abilityItem.gameObject);
                this.abilityItemList.Remove (abilityItem);
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