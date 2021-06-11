/*
 * @Author: l hy 
 * @Date: 2020-11-26 15:52:53 
 * @Description: 虚无插槽
 */

public class NotingAbility : BaseAbility {
    public NotingAbility (int id) : base (id) { }

    public override void turnBegin (AbilityData abilityData) { }
    public override void playerTrigger (AbilityData abilityData) { }

    public override void turnEnd (AbilityData abilityData) { }
}