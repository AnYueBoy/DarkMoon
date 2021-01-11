/*
 * @Author: l hy 
 * @Date: 2020-11-26 15:52:53 
 * @Description: 虚无插槽
 */

public class NotingAbility : BaseAbility {

    public new int id = 3;

    public override void refreshCardData (CardData cardData) {
        this.cardData = cardData;
    }

    public override void turnBeginEffect () { }
    public override void effect () { }

    public override void turnEndEffect () {
        cardData.isRemove = true;
    }
}