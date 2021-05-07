/*
 * @Author: l hy 
 * @Date: 2020-11-19 09:59:09 
 * @Description: 攻击能力 
 */

public class AttackAbility : BaseAbility {
    public new int id = 1;

    public override void refreshCardData (CardData cardData) {
        this.cardData = cardData;
    }

    public override void turnBeginEffect () { }

    public override void effect () {
        AbilityData abilityData = this.cardData.abilityDataDic[id];
        BaseRoleData targetData = AppContext.instance.dataManager.getCampRoleData (this.cardData.camp);

        if (abilityData.baseValue <= targetData.armor) {
            targetData.armor -= abilityData.baseValue;
            return;
        }
        double extendValue = abilityData.baseValue - targetData.armor;
        targetData.armor = 0;
        targetData.roleHp -= extendValue;
    }

    public override void turnEndEffect () { }
}