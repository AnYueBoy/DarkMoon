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

    }

    protected void playerTrigger () {

    }

    protected void turnEnd () {

    }
}