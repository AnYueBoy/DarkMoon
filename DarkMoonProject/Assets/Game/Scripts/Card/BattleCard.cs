/*
 * @Author: l hy 
 * @Date: 2021-06-05 17:09:30 
 * @Description: 战斗卡牌
 */

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BattleCard : BaseCard, IPointerEnterHandler, IPointerExitHandler, IDragHandler, IEndDragHandler {

    private float cardY;

    private RectTransform parentRect;

    public override void init (CustomCardData cardData) {
        base.init (cardData);
        this.cardY = this.rectTransform.localPosition.y;
        this.parentRect = this.transform.parent.GetComponent<RectTransform> ();
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
        RectTransformUtility.ScreenPointToLocalPointInRectangle (this.parentRect, eventData.position, AppContext.instance.uiCamera, out localPos);
        if (localPos.y < 0) {
            localPos.y = 0;
        }
        this.rectTransform.localPosition = new Vector3 (this.rectTransform.localPosition.x, localPos.y, this.rectTransform.localPosition.z);
    }

    private readonly float enterOffset = 15f;
    public void OnPointerEnter (PointerEventData eventData) {
        this.rectTransform.localPosition = new Vector3 (this.rectTransform.localPosition.x, this.rectTransform.localPosition.y + enterOffset, this.rectTransform.localPosition.z);
    }

    public void OnPointerExit (PointerEventData eventData) {
        this.rectTransform.localPosition = new Vector3 (this.rectTransform.localPosition.x, this.cardY, this.rectTransform.localPosition.z);
    }

    public void OnEndDrag (PointerEventData eventData) {
        this.rectTransform.localPosition = new Vector3 (this.rectTransform.localPosition.x, this.cardY, this.rectTransform.localPosition.z);;
    }
}