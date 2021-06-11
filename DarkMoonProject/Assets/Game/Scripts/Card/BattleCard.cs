/*
 * @Author: l hy 
 * @Date: 2021-06-05 17:09:30 
 * @Description: 战斗卡牌
 */

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BattleCard : BaseCard, IPointerDownHandler, IPointerUpHandler, IDragHandler, IEndDragHandler {

    private Vector3 localPos;

    private Vector3 localEulerAngles;

    private Vector3 localScale;

    private RectTransform childRectTransform;

    public override void init (CustomCardData cardData) {
        base.init (cardData);
        this.childRectTransform = this.rectTransform.GetChild (0).GetComponent<RectTransform> ();
    }

    public void setCardInfo () {
        this.localPos = this.rectTransform.localPosition;
        this.localEulerAngles = this.rectTransform.localEulerAngles;
        this.localScale = this.rectTransform.localScale;
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
        Vector2 localPos = new Vector2 ();
        // 需要注意的是，eventData中的position返回的是屏幕坐标，从左边下角(0,0),需要转换到UGUI的坐标
        RectTransformUtility.ScreenPointToLocalPointInRectangle (this.parentRectTransform, eventData.position, AppContext.instance.uiCamera, out localPos);
        if (localPos.y < 0) {
            localPos.y = 0;
        }
        this.rectTransform.localPosition = new Vector3 (this.rectTransform.localPosition.x, localPos.y, this.rectTransform.localPosition.z);
        this.rectTransform.localEulerAngles = Vector3.zero;

        this.resetChildState ();
    }

    public void OnEndDrag (PointerEventData eventData) {
        this.resetParentState ();
        this.resetChildState ();
    }

    private readonly float scaleOffset = 1.5f;
    public void OnPointerDown (PointerEventData eventData) {
        float enterOffset = this.rectTransform.rect.height * this.scaleOffset;

        this.childRectTransform.localPosition = new Vector3 (-this.rectTransform.position.x, enterOffset, 0);
        this.childRectTransform.localEulerAngles = new Vector3 (0, 0, -this.rectTransform.localEulerAngles.z);
        this.childRectTransform.localScale = Vector3.one * scaleOffset;
    }

    public void OnPointerUp (PointerEventData eventData) {
        this.resetParentState ();
        this.resetChildState ();
    }

    private void resetParentState () {
        this.rectTransform.localPosition = this.localPos;
        this.rectTransform.localEulerAngles = this.localEulerAngles;
        this.rectTransform.localScale = this.localScale;
    }

    private void resetChildState () {
        this.childRectTransform.localEulerAngles = Vector3.zero;
        this.childRectTransform.localPosition = Vector3.zero;
        this.childRectTransform.localScale = Vector3.one;
    }
}