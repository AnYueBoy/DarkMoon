/*
 * @Author: l hy 
 * @Date: 2020-11-26 15:52:53 
 * @Description: 虚无插槽
 */

public class NotingAbility : BaseAbility {

    public int id = 3;

    protected override void turnBeginEffect (CardData cardData) { }
    protected override void effect (CardData cardData) { }

    protected override void turnEndEffect (CardData cardData) {
        cardData.isRemove = true;
    }
}