/*
 * @Author: l hy 
 * @Date: 2020-12-07 14:31:33 
 * @Description: 卡牌编辑界面
 */

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

    [Header ("卡片预览区")]
    public PreviewCard cardPreview = null;

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
            GameObject abilityItemNode = ObjectPool.getInstance ().requestInstance (this.abilityItemPrefab);
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
                ObjectPool.getInstance ().returnInstance (abilityItem.gameObject);
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

    private void refreshAbilityData () {
        // TODO: 刷新能力列表数据
    }
}