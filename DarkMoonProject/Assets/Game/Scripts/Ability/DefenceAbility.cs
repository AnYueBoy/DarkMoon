/*
 * @Author: l hy 
 * @Date: 2020-11-26 15:11:36 
 * @Description: 防御能力插槽
 */

public class DefenceAbility : BaseAbility {

    protected int id = 2;

    protected override void turnBeginEffect (CardData cardData) { }

    protected override void effect (CardData cardData) {
        AbilityData abilityData = cardData.abilityDic[id];
        BaseRoleData targetData = DataManager.getInstance ().getTargetData (cardData.camp);
        targetData.armor += abilityData.baseValue;
    }

    protected override void turnEndEffect (CardData cardData) { }
}