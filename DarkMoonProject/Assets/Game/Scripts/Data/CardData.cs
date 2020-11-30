using System.Collections.Generic;
/*
 * @Author: l hy 
 * @Date: 2020-11-25 16:10:40 
 * @Description: 卡牌数据
 */

public class CardData {

    public int energyConsume = 0;

    public bool isRemove = false;

    public CampEnum camp = CampEnum.PLAYER;

    public Dictionary<int, AbilityData> abilityDataDic = new Dictionary<int, AbilityData> ();
}