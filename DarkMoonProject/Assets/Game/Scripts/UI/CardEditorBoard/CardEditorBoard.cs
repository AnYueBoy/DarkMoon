﻿/*
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

    private readonly Vector2 itemStartPos = new Vector2 (0, 400);

    private List<AbilityItem> abilityItemList = new List<AbilityItem> ();

    public void createCards_Click () {
        this.loadAbilities ();
    }

    private void loadAbilities () {
        if (this.abilityItemList.Count > 0) {
            this.refreshAbilityData ();
            return;
        }

        Dictionary<int, BaseAbility> abilityDic = AppContext.instance.abilityManager.abilityDic;
        int index = 0;
        foreach (int id in abilityDic.Keys) {
            GameObject abilityItemNode = ObjectPool.getInstance ().requestInstance (this.abilityItemPrefab);
            abilityItemNode.transform.SetParent (this.content.transform);
            float itemPosX = this.itemStartPos.x;
            float itemPosY = this.itemStartPos.y - ConstValue.abilityItemInterval * index;
            abilityItemNode.transform.localPosition = new Vector3 (itemPosX, itemPosY, 0);
            AbilityItem abilityItem = abilityItemNode.GetComponent<AbilityItem> ();

            // FIXME: 数据获取有问题
            abilityItem.init ();
            this.abilityItemList.Add (abilityItem);
            index++;
        }
    }

    private void refreshAbilityData () {

    }
}