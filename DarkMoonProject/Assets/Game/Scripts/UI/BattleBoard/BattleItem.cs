/*
 * @Author: l hy 
 * @Date: 2021-05-13 14:45:06 
 * @Description: 战斗item
 */

using UnityEngine;
using UnityEngine.UI;

public class BattleItem : MonoBehaviour {
    public Text itemName;

    public Image iconImage;

    public Text itemDescribe;

    public GameObject enterBtnNode;

    private BattleItemData battleItemData;

    public void init (BattleItemData battleItemData) {
        this.battleItemData = battleItemData;
        this.showItemInfo ();
        this.enterBtnNode.SetActive (false);
    }

    private void showItemInfo () {
        this.itemName.text = this.battleItemData.itemName;

        this.iconImage.sprite = AppContext.instance.assetsManager.getAssetByUrlSync<Sprite> (this.battleItemData.iconUrl);

        this.itemDescribe.text = this.battleItemData.describe;
    }

}