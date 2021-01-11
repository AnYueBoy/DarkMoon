/*
 * @Author: l hy 
 * @Date: 2021-01-11 10:00:16 
 * @Description: 中毒
 */

public class PoisonAbility : BaseAbility {

    public int id = 3;

    public override void refreshCardData (CardData cardData) {
        this.cardData = cardData;
    }

    public override void turnBeginEffect () { }

    public override void effect () {
        AbilityData abilityData = this.cardData.abilityDataDic[id];
        BaseRoleData targetData = DataManager.getInstance ().getOtherCampRoleData (this.cardData.camp);
        targetData.addAbilityLayer (id, abilityData.baseValue);
    }
    public override void turnEndEffect () { }

    public override string title () {
        return "中毒";
    }
    public override string describe () {
        return "中毒方在回合开始时，受到中毒层数的伤害，并减少一层中毒";
    }
}