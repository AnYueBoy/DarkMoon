using System.Collections.Generic;
/*
 * @Author: l hy 
 * @Date: 2021-05-13 14:48:50 
 * @Description: 战斗item数据
 */

public class BattleItemData {

    public int id;

    public ItemTypeEnum itemType;

    public string itemName;

    public string iconUrl;

    public string describe;

    /*战斗独有*/
    public List<int> cardList;

    public float monsterHp;

    public float actionValue;

    public float magicValue;
}