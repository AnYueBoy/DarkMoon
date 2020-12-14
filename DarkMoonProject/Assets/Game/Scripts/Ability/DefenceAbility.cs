/*
 * @Author: l hy 
 * @Date: 2020-11-26 15:11:36 
 * @Description: 防御能力插槽
 */

public class DefenceAbility : BaseAbility {

    public int id = 2;

    private CardData cardData = null;

    public override void refreshCardData (CardData cardData) {
        this.cardData = cardData;
    }

    public override void turnBeginEffect () { }

    public override void effect () {
        AbilityData abilityData = this.cardData.abilityDataDic[id];
        BaseRoleData targetData = DataManager.getInstance ().getTargetData (this.cardData.camp);
        targetData.armor += abilityData.baseValue;
    }

    public override void turnEndEffect () { }

    public override string describe () {
        return "获得防御";
    }

    public override string title () {
        return "防御";
    }

}