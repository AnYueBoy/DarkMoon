using System.Collections.Generic;
using UFramework.FrameUtil;
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

    private List<int> playerCardListClone = new List<int> ();

    private int curCardIndex = 0;

    public void battlePrepare (Monster monster, Transform cardParent) {
        this.curTurn = TurnEnum.PLAYER;
        this.battleMonster = monster;
        this.cardParent = cardParent;

        this.curCardIndex = 0;

        // 复制玩家数据中玩家卡牌并混乱
        this.copyPlayerCardList ();

        // 播放敌人动画
        this.battleMonster.playAnimation ();

        // 生成玩家卡牌
        this.spawnPlayerCards ();
    }

    private void copyPlayerCardList () {
        List<int> playerCardList = AppContext.instance.playerDataManager.playerData.cardList;
        foreach (int cardId in playerCardList) {
            this.playerCardListClone.Add (cardId);
        }

        CommonUtil.confusionElement<int> (this.playerCardListClone);
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

    private List<BattleCard> battleCardList = new List<BattleCard> ();

    private void spawnPlayerCards () {
        int drawCardCount = AppContext.instance.playerDataManager.playerData.drawCardCount;
        int leftCardCount = this.playerCardListClone.Count;
        for (int i = 0;
            (i < drawCardCount && i < leftCardCount); i++) {
            BattleCard battleCard = AppContext.instance.spawnManager.createBattleCard (this.cardParent);
            this.battleCardList.Add (battleCard);
            int cardId = this.playerCardListClone[i];
            CustomCardData cardData = AppContext.instance.customDataManager.cardDataDic[cardId];

            battleCard.init (cardData);
            this.curCardIndex++;
        }

        // TODO: 卡牌扇形排布


    }
}

public enum TurnEnum {
    MONSTER,

    PLAYER,
}