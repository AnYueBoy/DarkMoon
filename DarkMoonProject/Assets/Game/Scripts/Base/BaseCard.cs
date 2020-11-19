/*
 * @Author: l hy 
 * @Date: 2020-11-18 16:44:09 
 * @Description: 卡牌基类
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCard : MonoBehaviour {

    protected int energyConsume = 1;

    protected int currentLevel = 1;

    protected CampEnum camp = CampEnum.PLAYER;

    protected List<int> abilityList = new List<int> ();

}