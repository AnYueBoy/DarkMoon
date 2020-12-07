/*
 * @Author: l hy 
 * @Date: 2020-11-19 09:59:09 
 * @Description: 攻击能力 
 */

public class AttackAbility : BaseAbility {

    public int id = 1;

    private CardData cardData = null;

    public override void refreshCardData (CardData cardData) {
        this.cardData = cardData;
    }

    public override void turnBeginEffect () { }

    public override void effect () {
        AbilityData abilityData = this.cardData.abilityDataDic[id];
        BaseRoleData targetData = DataManager.getInstance ().getTargetData (this.cardData.camp);

        if (abilityData.baseValue <= targetData.armor) {
            targetData.armor -= abilityData.baseValue;
            return;
        }
        double extendValue = abilityData.baseValue - targetData.armor;
        targetData.armor = 0;
        targetData.roleHp -= extendValue;
    }

    public override void turnEndEffect () { }

    public override string describe () {
        return "造成伤害";
    }

    public override string title () {
        return "攻击";
    }

}