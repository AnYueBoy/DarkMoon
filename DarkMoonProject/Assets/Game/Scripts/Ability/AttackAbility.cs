/*
 * @Author: l hy 
 * @Date: 2020-11-19 09:59:09 
 * @Description: 攻击能力 
 */

public class AttackAbility : BaseAbility {

    protected int id = 1;

    protected override void effect (CardData cardData) {
        AbilityData abilityData = cardData.abilityDic[id];
        BaseRoleData targetData = DataManager.getInstance ().getTargetData (cardData.camp);

        if (abilityData.baseValue <= targetData.armor) {
            targetData.armor -= abilityData.baseValue;
            return;
        }
        double extendValue = abilityData.baseValue - targetData.armor;
        targetData.armor = 0;
        targetData.roleHp -= extendValue;
    }
}