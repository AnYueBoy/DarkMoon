/*
 * @Author: l hy 
 * @Date: 2020-11-26 15:11:36 
 * @Description: 防御能力插槽
 */

public class DefenceAbility : BaseAbility {

    public int id = 2;

    public override void turnBeginEffect (CardData cardData) { }

    public override void effect (CardData cardData) {
        AbilityData abilityData = cardData.abilityDataDic[id];
        BaseRoleData targetData = DataManager.getInstance ().getTargetData (cardData.camp);
        targetData.armor += abilityData.baseValue;
    }

    public override void turnEndEffect (CardData cardData) { }

    public override string describe () {
        return "获得防御";
    }

    public override string title () {
        return "防御";
    }
}