/*
 * @Author: l hy 
 * @Date: 2020-11-18 16:44:09 
 * @Description: 卡牌基类
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCard : MonoBehaviour {

    protected int currentLevel = 1;

    protected CardData cardData = null;

    protected void buildCard () {

    }

    protected bool consumeCheck () {
        CampEnum targetCamp = cardData.camp;
        BaseRoleData targetData = DataManager.getInstance ().getTargetData (targetCamp);
        if (targetData.energy < cardData.energyConsume) {
            return false;
        }
        return true;
    }

    protected void turnBegin () {
        Dictionary<int, AbilityData> abilityDataDic = cardData.abilityDataDic;
        if (abilityDataDic == null) {
            Debug.LogWarning ("abilityDic not exist in turnBegin");
            return;
        }
        Dictionary<int, BaseAbility> abilityDic = AbilityManager.getInstance ().abilityDic;
        if (abilityDic == null) {
            Debug.LogWarning ("abilityDic not exist in turnBegin");
            return;
        }
        foreach (int id in cardData.abilityDataDic.Keys) {
            BaseAbility targetAbility = abilityDic[id];
            // FIXME: 感觉应该传入的是abilityData cardData不属于能力插槽操作的数据,纠结ing
            targetAbility.turnBeginEffect (cardData);
        }
    }

    protected void playerTrigger () {
        Dictionary<int, AbilityData> abilityDataDic = cardData.abilityDataDic;
        if (abilityDataDic == null) {
            Debug.LogWarning ("abilityDic not exist in playerTrigger");
            return;
        }
        Dictionary<int, BaseAbility> abilityDic = AbilityManager.getInstance ().abilityDic;
        if (abilityDic == null) {
            Debug.LogWarning ("abilityDic not exist in playerTrigger");
            return;
        }
        foreach (int id in cardData.abilityDataDic.Keys) {
            BaseAbility targetAbility = abilityDic[id];
            targetAbility.effect (cardData);
        }
    }

    protected void turnEnd () {
        Dictionary<int, AbilityData> abilityDataDic = cardData.abilityDataDic;
        if (abilityDataDic == null) {
            Debug.LogWarning ("abilityDic not exist in turnEnd");
            return;
        }
        Dictionary<int, BaseAbility> abilityDic = AbilityManager.getInstance ().abilityDic;
        if (abilityDic == null) {
            Debug.LogWarning ("abilityDic not exist in turnEnd");
            return;
        }
        foreach (int id in cardData.abilityDataDic.Keys) {
            BaseAbility targetAbility = abilityDic[id];
            targetAbility.turnEndEffect (cardData);
        }
    }
}