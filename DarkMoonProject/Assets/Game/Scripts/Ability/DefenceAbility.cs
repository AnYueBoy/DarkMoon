/*
 * @Author: l hy 
 * @Date: 2020-11-26 15:11:36 
 * @Description: 防御能力插槽
 */

public class DefenceAbility : BaseAbility {

    protected int id = 3;

    protected override void effect (CardData cardData, BaseRoleData targetData) {
        AbilityData abilityData = cardData.abilityDic[id];
        targetData.armor += abilityData.baseValue;
    }
}