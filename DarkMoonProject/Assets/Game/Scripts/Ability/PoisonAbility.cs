/*
 * @Author: l hy 
 * @Date: 2021-01-11 10:00:16 
 * @Description: 中毒
 */

public class PoisonAbility : BaseAbility {

    public new int id = 4;

    public override void refreshCardData (CardData cardData) {
        this.cardData = cardData;
    }

    public override void turnBeginEffect () { }

    public override void effect () {
        AbilityData abilityData = this.cardData.abilityDataDic[id];
        BaseRoleData targetData = AppContext.instance.dataManager.getOtherCampRoleData (this.cardData.camp);
        targetData.addAbilityLayer (id, abilityData.baseValue);
    }
    public override void turnEndEffect () { }
}