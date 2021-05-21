/*
 * @Author: l hy 
 * @Date: 2021-05-13 14:45:06 
 * @Description: 战斗item
 */

using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class BattleSelectItem : MonoBehaviour {
    public Text itemName;

    public Image iconImage;

    public Text itemDescribe;

    public GameObject enterBtnNode;

    private BattleItemData battleItemData;

    private Action selectedCallback;

    private Tween enterBtnTween;

    public void init (BattleItemData battleItemData, Action selectedCallback) {
        this.battleItemData = battleItemData;
        this.selectedCallback = selectedCallback;
        this.showItemInfo ();
        this.enterBtnNode.transform.localScale = Vector3.zero;
    }

    private void showItemInfo () {
        this.itemName.text = this.battleItemData.itemName;

        this.iconImage.sprite = AppContext.instance.assetsManager.getAssetByUrlSync<Sprite> (this.battleItemData.iconUrl);

        this.itemDescribe.text = this.battleItemData.describe;
    }

    private readonly float tweenTime = 0.3f;

    public void selectedItem () {
        this.selectedCallback?.Invoke ();
        this.enterBtnTween?.Kill ();
        this.enterBtnTween = this.enterBtnNode.transform.DOScale (Vector3.one, this.tweenTime);
        this.enterBtnTween.Play ();
    }

    public void unSelectedItem () {
        this.enterBtnTween?.Kill ();
        this.enterBtnTween = this.enterBtnNode.transform.DOScale (Vector3.zero, this.tweenTime);
        this.enterBtnTween.Play ();
    }

    public void enterItem () {
        switch (this.battleItemData.itemType) {
            case ItemTypeEnum.BATTLE:
                //  进入战斗界面
                AppContext.instance.uIManager.showBoard (UIPath.BattleBoard, this.battleItemData.id);
                break;

            case ItemTypeEnum.BLESS:
                break;

            case ItemTypeEnum.SHOP:
                break;
            default:
                break;
        }

    }

}