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

public class BattleSelectBoard : BaseUI {

    public Transform itemContent;

    private List<BattleSelectItem> battleItemList = new List<BattleSelectItem> ();

    private readonly int itemCount = 3;

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
        for (int i = 0; i < this.itemCount; i++) {
            GameObject itemNode = ObjectPool.instance.requestInstance (itemPrefab);
            itemNode.transform.SetParent (this.itemContent);
            itemNode.transform.localPosition = new Vector3 (this.itemStartX + itemInterval * i, this.itemStartY, 0);

            BattleSelectItem battleSelectItem = itemNode.GetComponent<BattleSelectItem> ();
            this.battleItemList.Add (battleSelectItem);
        }

    }

    private void refreshBattleItemsData () {
        if (this.battleItemList.Count <= 0) {
            return;
        }

        Dictionary<int, BattleItemData> battleItemDataDic = AppContext.instance.customDataManager.battleItemDataDic;
        int index = 0;
        foreach (var battleItemData in battleItemDataDic.Values) {
            BattleSelectItem battleSelectItem = this.battleItemList[index];
            if (battleSelectItem == null) {
                continue;
            }

            battleSelectItem.init (battleItemData, () => {
                this.unSelectedAllItems ();
            });
            index++;
        }
    }

    private void unSelectedAllItems () {
        foreach (BattleSelectItem battleSelectItem in this.battleItemList) {
            battleSelectItem.unSelectedItem ();
        }
    }

    public void returnHallBoard () {
        AppContext.instance.uIManager.showBoard (UIPath.HallBoard);
    }
}