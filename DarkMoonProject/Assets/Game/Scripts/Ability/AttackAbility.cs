/*
 * @Author: l hy 
 * @Date: 2020-11-19 09:59:09 
 * @Description: 攻击能力 
 */

public class AttackAbility : BaseAbility {
    protected override void effect (AbilityData abilityData, BaseRoleData targetData) {
        if (abilityData.baseValue <= targetData.armor) {
            targetData.armor -= abilityData.baseValue;
            return;
        }
        double extendValue = abilityData.baseValue - targetData.armor;
        targetData.armor = 0;
        targetData.roleHp -= extendValue;
    }
}