/*
 * @Author: l hy 
 * @Date: 2021-06-05 17:09:30 
 * @Description: 战斗卡牌
 */

using System.Collections.Generic;
using UnityEngine;

public class BattleCard : BaseCard {

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
}