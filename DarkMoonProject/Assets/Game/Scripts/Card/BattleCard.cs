/*
 * @Author: l hy 
 * @Date: 2021-06-05 17:09:30 
 * @Description: 战斗卡牌
 */

using System.Collections.Generic;
using DG.Tweening;
using UFramework.GameCommon;
using UnityEngine;
using UnityEngine.EventSystems;

public class BattleCard : BaseCard, IPointerDownHandler, IPointerUpHandler, IDragHandler, IEndDragHandler {

    private Vector3 localPos;

    private Vector3 localEulerAngles;

    private Vector3 localScale;

    private RectTransform childRectTransform;

    public override void init (CustomCardData cardData) {
        base.init (cardData);
        this.childRectTransform = this._rectTransform.GetChild (0).GetComponent<RectTransform> ();
    }

    public void setCardInfo (Vector3 originPos, Vector3 originAngle, Vector3 originScale) {
        this.localPos = originPos;
        this.localEulerAngles = originAngle;
        this.localScale = originScale;
    }

    protected bool consumeCheck () {
        int consumeEnergy = this.cardData.consumeEnergy;

        if (consumeEnergy <= 0) {
            return true;
        }

        CardTypeEnum cardType = this.cardData.cardType;
        PlayerData battlePlayerData = AppContext.instance.battleManager.battlePlayerData;
        switch (cardType) {
            case CardTypeEnum.ACTION:
                return battlePlayerData.actionValue >= consumeEnergy;

            case CardTypeEnum.MAGIC:
                return battlePlayerData.magicValue >= consumeEnergy;

            case CardTypeEnum.BLEED:
                return battlePlayerData.hpValue >= consumeEnergy;

            default:
                Debug.LogError ("incorrect card type : " + consumeEnergy);
                return false;
        }
    }

    protected void turnBegin () {
        List<AbilityData> abilityDataList = this.cardData.abilities;
        foreach (AbilityData abilityData in abilityDataList) {
            int abilityId = abilityData.id;
            BaseAbility ability = AppContext.instance.abilityManager.abilityDic[abilityId];
            ability.turnBegin (abilityData);
        }
    }

    protected void playerTrigger () {
        List<AbilityData> abilityDataList = this.cardData.abilities;
        foreach (AbilityData abilityData in abilityDataList) {
            int abilityId = abilityData.id;
            BaseAbility ability = AppContext.instance.abilityManager.abilityDic[abilityId];
            ability.playerTrigger (abilityData);
        }
    }

    protected void turnEnd () {
        List<AbilityData> abilityDataList = this.cardData.abilities;
        foreach (AbilityData abilityData in abilityDataList) {
            int abilityId = abilityData.id;
            BaseAbility ability = AppContext.instance.abilityManager.abilityDic[abilityId];
            ability.turnEnd (abilityData);
        }
    }

    public void OnDrag (PointerEventData eventData) {
        this.isDrag = true;
        Vector2 localPos = new Vector2 ();
        // 需要注意的是，eventData中的position返回的是屏幕坐标，从左边下角(0,0),需要转换到UGUI的坐标
        RectTransformUtility.ScreenPointToLocalPointInRectangle (this.parentRectTransform, eventData.position, AppContext.instance.uiCamera, out localPos);
        if (localPos.y < 0) {
            localPos.y = 0;
        }
        this._rectTransform.localPosition = new Vector3 (localPos.x, localPos.y, this._rectTransform.localPosition.z);
        this._rectTransform.localEulerAngles = Vector3.zero;

        this.resetChildState (this.normalAnimationTime);
    }

    private readonly float triggerY = 300;

    private bool isDrag = false;
    public void OnEndDrag (PointerEventData eventData) {
        this.isDrag = false;
        if (this._rectTransform.localPosition.y < triggerY) {
            this.resetParentState (this.normalAnimationTime);
            this.resetChildState (0);
            return;
        }

        if (!this.consumeCheck ()) {
            this.resetParentState (this.normalAnimationTime);
            this.resetChildState (0);
            Debug.Log ("能量不足");
            return;
        }

        // 达到触发值
        this.playerTrigger ();
        ObjectPool.instance.returnInstance (this.transform.gameObject);

        AppContext.instance.battleManager.removeBatteleCard (this);
    }

    private readonly float scaleOffset = 1.5f;

    private readonly float normalAnimationTime = 0.25f;
    public void OnPointerDown (PointerEventData eventData) {
        float enterOffset = this._rectTransform.rect.height * this.scaleOffset;

        this.childRectTransform.DOLocalMove (new Vector3 (-this._rectTransform.position.x, enterOffset, 0), this.normalAnimationTime);
        this.childRectTransform.DOScale (Vector3.one * scaleOffset, this.normalAnimationTime);
        this.childRectTransform.localEulerAngles = new Vector3 (0, 0, -this._rectTransform.localEulerAngles.z);
    }

    public void OnPointerUp (PointerEventData eventData) {
        if (this.isDrag) {
            return;
        }
        this.resetChildState (this.normalAnimationTime);
    }

    private void resetParentState (float animationTime) {
        this._rectTransform.localEulerAngles = this.localEulerAngles;
        this._rectTransform.DOScale (this.localScale, animationTime);
        this._rectTransform.DOLocalMove (this.localPos, animationTime);
    }

    private void resetChildState (float animationTime) {
        this.childRectTransform.localEulerAngles = Vector3.zero;
        this.childRectTransform.DOLocalMove (Vector3.zero, this.normalAnimationTime);
        this.childRectTransform.DOScale (Vector3.one, this.normalAnimationTime);
    }
}