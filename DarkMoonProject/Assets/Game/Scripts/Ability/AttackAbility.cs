/*
 * @Author: l hy 
 * @Date: 2020-11-19 09:59:09 
 * @Description: 攻击能力 
 */

public class AttackAbility : BaseAbility {
    public AttackAbility (int id) : base (id) { }

    public override void turnBegin (AbilityData abilityData) { }

    public override void playerTrigger (AbilityData abilityData) {
        TurnEnum curTurn = AppContext.instance.battleManager.curTurn;
        if (curTurn == TurnEnum.PLAYER) {
            // 作用于怪物
            AppContext.instance.battleManager.battleMonster.damage ((int) abilityData.baseValue);

        } else if (curTurn == TurnEnum.MONSTER) {
            //  作用于玩家
            AppContext.instance.battleManager.battlePlayer.damage ((int) abilityData.baseValue);
        }
    }

    public override void turnEnd (AbilityData abilityData) { }
}