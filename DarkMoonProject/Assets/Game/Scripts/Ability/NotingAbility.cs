/*
 * @Author: l hy 
 * @Date: 2020-11-26 15:52:53 
 * @Description: 虚无插槽
 */

public class NotingAbility : BaseAbility {

    public int id = 3;

    private CardData cardData = null;

    public override void refreshCardData (CardData cardData) {
        this.cardData = cardData;
    }

    public override void turnBeginEffect () { }
    public override void effect () { }

    public override void turnEndEffect () {
        cardData.isRemove = true;
    }

    public override string title () {
        return "虚无";
    }

    public override string describe () {
        return "在回合结束时，移出卡组";
    }

}