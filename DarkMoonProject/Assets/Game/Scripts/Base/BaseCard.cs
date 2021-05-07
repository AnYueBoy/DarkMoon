/*
 * @Author: l hy 
 * @Date: 2020-11-18 16:44:09 
 * @Description: 卡牌
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseCard : MonoBehaviour {
    protected int currentLevel = 1;

    protected CardData cardData = null;

    #region ui相关

    [Header ("立绘图片")]
    public Image drawImage = null;

    [Header ("背景图片")]
    public Image cardBgImage = null;

    [Header ("描述")]
    public Text describeText = null;

    #endregion

    protected void buildCard () {

    }

    private void initAbilityCardData () {
        foreach (int id in this.cardData.abilityDataDic.Keys) {
            // BaseAbility targetAbility = this.cardData.abilityDataDic[id];
        }
    }

    protected bool consumeCheck () {
        CampEnum targetCamp = cardData.camp;
        BaseRoleData targetData = AppContext.instance.dataManager.getCampRoleData (targetCamp);
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
        Dictionary<int, BaseAbility> abilityDic = AppContext.instance.abilityManager.abilityDic;
        if (abilityDic == null) {
            Debug.LogWarning ("abilityDic not exist in turnBegin");
            return;
        }
        foreach (int id in cardData.abilityDataDic.Keys) {
            BaseAbility targetAbility = abilityDic[id];
            targetAbility.turnBeginEffect ();
        }
    }

    protected void playerTrigger () {
        Dictionary<int, AbilityData> abilityDataDic = cardData.abilityDataDic;
        if (abilityDataDic == null) {
            Debug.LogWarning ("abilityDic not exist in playerTrigger");
            return;
        }
        Dictionary<int, BaseAbility> abilityDic = AppContext.instance.abilityManager.abilityDic;
        if (abilityDic == null) {
            Debug.LogWarning ("abilityDic not exist in playerTrigger");
            return;
        }
        foreach (int id in cardData.abilityDataDic.Keys) {
            BaseAbility targetAbility = abilityDic[id];
            targetAbility.effect ();
        }
    }

    protected void turnEnd () {
        Dictionary<int, AbilityData> abilityDataDic = cardData.abilityDataDic;
        if (abilityDataDic == null) {
            Debug.LogWarning ("abilityDic not exist in turnEnd");
            return;
        }
        Dictionary<int, BaseAbility> abilityDic = AppContext.instance.abilityManager.abilityDic;
        if (abilityDic == null) {
            Debug.LogWarning ("abilityDic not exist in turnEnd");
            return;
        }
        foreach (int id in cardData.abilityDataDic.Keys) {
            BaseAbility targetAbility = abilityDic[id];
            targetAbility.turnEndEffect ();
        }
    }
}