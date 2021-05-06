/*
 * @Author: l hy 
 * @Date: 2020-12-07 14:31:33 
 * @Description: 卡牌编辑界面
 */

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

    [Header ("能量消耗")]
    public InputField inputField = null;

    private List<AbilityItem> abilityItemList = new List<AbilityItem> ();

    private List<AbilityData> cardPreviewAbilityList = new List<AbilityData> ();

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
            GameObject abilityItemNode = ObjectPool.instance.requestInstance (this.abilityItemPrefab);
            abilityItemNode.transform.SetParent (this.content.transform, false);
            AbilityItem abilityItem = abilityItemNode.GetComponent<AbilityItem> ();

            abilityItem.init (abilityData);
            this.abilityItemList.Add (abilityItem);
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
                this.cardPreviewAbilityList.Add (abilityItem.AbilityData);
                this.refreshPreviewCard ();
            }
        }
    }

    private void refreshPreviewCard () {
        PreviewData previewData = new PreviewData (this.cardPreviewAbilityList);
        this.cardPreview.refreshCard (previewData);
    }

    public void buildCardCompleted () {
        CardPoolData cardPoolData = CustomDataManager.cardPoolData;
        CustomCardData customCardData = new CustomCardData ();

        customCardData.id = cardPoolData.cards.Count + 1;

        // FIXME: 卡牌背景图片
        customCardData.textureUrl = "";

        // 能力消耗
        string consumeEnergyStr = this.inputField.text;
        int consumeEnergy = int.Parse (consumeEnergyStr);
        customCardData.consumeEnergy = consumeEnergy;

        customCardData.cardName = "暗月";
        customCardData.abilities = this.cardPreviewAbilityList;
        cardPoolData.cards.Add (customCardData);

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

    private void refreshAbilityData () {
        // TODO: 刷新能力列表数据
    }

    public void close_Click () {
        AppContext.instance.uIManager.showBoard (UIPath.HallBoard);
    }
}