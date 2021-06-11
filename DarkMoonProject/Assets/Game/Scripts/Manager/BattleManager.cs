/*
 * @Author: l hy 
 * @Date: 2021-05-26 19:16:02 
 * @Description: 战斗管理
 */
using System.Collections.Generic;
using UFramework.FrameUtil;
using UnityEngine;
public class BattleManager {

    private TurnEnum curTurn;

    private Monster battleMonster;

    private Transform cardParent;

    private int curCardIndex = 0;

    private PlayerData _battlePlayerData;

    public PlayerData battlePlayerData {
        get {
            return this._battlePlayerData;
        }
    }

    public void battlePrepare (Monster monster, Transform cardParent) {
        this.curTurn = TurnEnum.PLAYER;
        this.battleMonster = monster;
        this.cardParent = cardParent;

        this.curCardIndex = 0;

        // 复制玩家数据
        this._battlePlayerData = AppContext.instance.playerDataManager.playerData.clone<PlayerData> ();

        // 混乱玩家卡牌
        CommonUtil.confusionElement<int> (this._battlePlayerData.cardList);

        // 播放敌人动画
        this.battleMonster.playAnimation ();

        // 生成玩家卡牌
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

    private List<BattleCard> battleCardList = new List<BattleCard> ();

    private readonly float fixedWidth = 640;

    private readonly float leftBound = -320;

    private readonly float cardInterval = 70f;

    private readonly float angleValue = -1.5f;

    private void spawnPlayerCards () {
        int drawCardCount = AppContext.instance.playerDataManager.playerData.drawCardCount;
        List<int> playerCardList = this._battlePlayerData.cardList;
        int leftCardCount = playerCardList.Count;
        for (int i = 0;
            (i < drawCardCount && i < leftCardCount); i++) {
            BattleCard battleCard = AppContext.instance.spawnManager.createBattleCard (this.cardParent);
            this.battleCardList.Add (battleCard);
            int cardId = playerCardList[i];
            CustomCardData cardData = AppContext.instance.customDataManager.cardDataDic[cardId];

            battleCard.init (cardData);
            this.curCardIndex++;
        }

        this.sectorArrayCard (this.battleCardList);
    }

    private void sectorArrayCard (List<BattleCard> battleCards) {
        // 卡牌扇形排布
        int battleCardCount = battleCards.Count;
        float interval = this.fixedWidth / (battleCardCount + 1);

        bool isDouble = battleCardCount % 2 == 0;
        int startIndex = -Mathf.FloorToInt (battleCardCount / 2);

        for (int i = 0; i < battleCardCount; i++) {
            BattleCard battleCard = battleCards[i];
            float cardX = 0;
            if (interval > this.cardInterval) {
                // 使用卡牌间距
                cardX = startIndex * this.cardInterval;
            } else {
                // 使用计算间距
                cardX = this.leftBound + (i + 1) * interval;
            }
            battleCard.transform.localPosition = new Vector3 (cardX, 0, 0);

            battleCard.transform.localEulerAngles = new Vector3 (0, 0, startIndex * this.angleValue);
            startIndex++;
            if (isDouble && startIndex == 0) {
                startIndex++;
            }

            battleCard.setCardInfo ();
        }
    }

    public void removeBatteleCard (BattleCard battleCard) {
        this.battleCardList.Remove (battleCard);
        // TODO: 转移到弃牌堆

        this.sectorArrayCard (this.battleCardList);
    }
}