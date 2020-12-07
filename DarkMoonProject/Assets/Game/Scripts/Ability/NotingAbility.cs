/*
 * @Author: l hy 
 * @Date: 2020-11-26 15:52:53 
 * @Description: 虚无插槽
 */

public class NotingAbility : BaseAbility {

    public int id = 3;

    public override void turnBeginEffect (CardData cardData) { }
    public override void effect (CardData cardData) { }

    public override void turnEndEffect (CardData cardData) {
        cardData.isRemove = true;
    }

    public override string title () {
        return "虚无";
    }

    public override string describe () {
        return "在回合结束时，移出卡组";
    }
}