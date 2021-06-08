using System.Collections.Generic;
/*
 * @Author: l hy 
 * @Date: 2021-05-18 16:15:29 
 * @Description: 玩家数据
 */

public class PlayerData : BaseData {

    public bool isNewPlayer = true;

    public int drawCardCount = 3;

    // 玩家拥有的卡牌
    public List<int> cardList = new List<int> () { 1, 2, 3, 4, 5 };

    // 法力值
    public int magicValue = 10;

    // 行动力
    public int actionValue = 3;

    // 血量
    public int hpValue = 25;
}