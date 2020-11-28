/*
 * @Author: l hy 
 * @Date: 2020-11-26 15:52:53 
 * @Description: 虚无插槽
 */

public class NotingAbility : BaseAbility {
    protected override void effect (CardData cardData) {
        cardData.isRemove = true;
    }
}