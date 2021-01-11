/*
 * @Author: l hy 
 * @Date: 2020-11-26 15:11:36 
 * @Description: 防御能力插槽
 */

public class DefenceAbility : BaseAbility {

    public new int id = 2;

    public override void refreshCardData (CardData cardData) {
        this.cardData = cardData;
    }

    public override void turnBeginEffect () { }

    public override void effect () {
        AbilityData abilityData = this.cardData.abilityDataDic[id];
        BaseRoleData targetData = DataManager.getInstance ().getCampRoleData (this.cardData.camp);
        targetData.armor += abilityData.baseValue;
    }

    public override void turnEndEffect () { }
}