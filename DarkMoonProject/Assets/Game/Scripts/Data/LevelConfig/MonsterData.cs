/*
 * @Author: l hy 
 * @Date: 2021-05-21 13:16:11 
 * @Description: {} 
 */

using System.Collections.Generic;

public class MonsterData : BaseData {
    public int id;

    public List<int> cardList;

    public int monsterHp;

    public int maxMonsterHp;

    public int actionValue;

    public int magicValue;

    public int maxMagicValue;

    public int armor;

    public string url;

    public MonsterData () {
        this.armor = 0;
    }
}