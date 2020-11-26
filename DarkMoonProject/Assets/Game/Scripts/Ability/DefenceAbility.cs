/*
 * @Author: l hy 
 * @Date: 2020-11-26 15:11:36 
 * @Description: 防御能力插槽
 */

public class DefenceAbility : BaseAbility {
    protected override void effect (AbilityData abilityData, BaseRoleData targetRole) {
        targetRole.armor += abilityData.baseValue;
    }
}