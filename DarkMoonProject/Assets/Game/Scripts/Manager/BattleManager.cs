/*
 * @Author: l hy 
 * @Date: 2021-05-26 19:16:02 
 * @Description: 战斗管理
 */
using UnityEngine;
public class BattleManager {

    private TurnEnum curTurn;

    private Monster battleMonster;

    private Transform cardParent;

    public void battlePrepare (Monster monster, Transform cardParent) {
        this.curTurn = TurnEnum.PLAYER;
        this.battleMonster = monster;
        this.cardParent = cardParent;

        this.spawnPlayerCards ();
    }

    public void localUpdate (float dt) {
        if (this.battleMonster == null || this.curTurn != TurnEnum.MONSTER) {
            return;
        }

        this.battleMonster.localUpdate (dt);
    }

    public void switchTurn () {
        if (this.curTurn == TurnEnum.MONSTER) {
            this.curTurn = TurnEnum.PLAYER;
            // TODO: 对玩家相关状态进行重置
        }

        if (this.curTurn == TurnEnum.PLAYER) {
            this.curTurn = TurnEnum.MONSTER;
            this.battleMonster.recoveryState ();
        }
    }

    private void spawnPlayerCards () {
        int drawCardCount = AppContext.instance.playerDataManager.playerData.drawCardCount;
        for (int i = 0; i < drawCardCount; i++) {
            BaseCard card = AppContext.instance.spawnManager.createCard (this.cardParent);
            // FIXME:
            card.init ();
        }
    }

}

public enum TurnEnum {
    MONSTER,

    PLAYER,
}