/*
 * @Author: l hy 
 * @Date: 2021-05-26 19:16:02 
 * @Description: 战斗管理
 */
public class BattleManager {

    private TurnEnum curTurn;

    private Monster battleMonster;

    public void battlePrepare (Monster monster) {
        this.curTurn = TurnEnum.PLAYER;
        this.battleMonster = monster;
    }

    public void switchTurn () {
        this.curTurn = this.curTurn == TurnEnum.PLAYER?TurnEnum.MONSTER : TurnEnum.MONSTER;
        // 对敌方或是怪物方数据进行重置
    }
}

public enum TurnEnum {
    MONSTER,

    PLAYER,
}