using System.Collections.Generic;
/*
 * @Author: l hy 
 * @Date: 2021-05-17 21:51:57 
 * @Description: 战斗界面
 * @Last Modified by: l hy
 * @Last Modified time: 2021-05-17 22:21:14
 */

using UFramework.GameCommon;
using UnityEngine;

public class BattleBoard : BaseUI {

    public Transform itemContent;

    private List<BattleItem> battleItemList = new List<BattleItem> ();

    private readonly int createCount = 3;

    private readonly float itemStartX = -206;

    private readonly float itemStartY = 17;

    private readonly float itemInterval = 214;

    public override void onShow (params object[] args) {
        this.createBattleItems ();
        this.refreshBattleItemsData ();
    }

    private void createBattleItems () {
        if (this.battleItemList.Count > 0) {
            return;
        }
        GameObject itemPrefab = AppContext.instance.assetsManager.getAssetByUrlSync<GameObject> (CustomUrlString.battleItemPrefab);
        for (int i = 0; i < this.createCount; i++) {
            GameObject itemNode = ObjectPool.instance.requestInstance (itemPrefab);
            itemNode.transform.SetParent (this.itemContent);
            itemNode.transform.localPosition = new Vector3 (this.itemStartX + itemInterval * i, this.itemStartY, 0);

            BattleItem battleItem = itemNode.GetComponent<BattleItem> ();
            this.battleItemList.Add (battleItem);
        }

    }

    private void refreshBattleItemsData () {

    }

    public void returnHallBoard () {
        AppContext.instance.uIManager.showBoard (UIPath.HallBoard);
    }
}