/*
 * @Author: l hy 
 * @Date: 2021-05-26 19:16:02 
 * @Description: 战斗管理
 */
using System.Collections.Generic;
using DG.Tweening;
using UFramework.FrameUtil;
using UnityEngine;
public class BattleManager {

    public TurnEnum curTurn;

    public Monster battleMonster;

    public Player battlePlayer;

    private RectTransform cardParent;

    private int curCardIndex = 0;

    private PlayerData _battlePlayerData;

    public PlayerData battlePlayerData {
        get {
            return this._battlePlayerData;
        }
    }

    public void battlePrepare (Monster monster, RectTransform cardParent) {
        this.curTurn = TurnEnum.PLAYER;
        this.battleMonster = monster;
        this.battlePlayer = new Player ();
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

    private readonly float spawnCardInterval = 0.25f;

    private void spawnPlayerCards () {
        int drawCardCount = AppContext.instance.playerDataManager.playerData.drawCardCount;
        List<int> playerCardList = this._battlePlayerData.cardList;
        int leftCardCount = playerCardList.Count;
        for (int i = 0;
            (i < drawCardCount && i < leftCardCount); i++) {
            int index = i;
            AppContext.instance.promiseTimer.waitFor (index * this.spawnCardInterval).then (() => {
                BattleCard battleCard = AppContext.instance.spawnManager.createBattleCard (this.cardParent);
                this.battleCardList.Add (battleCard);
                int cardId = playerCardList[index];

                CustomCardData cardData = AppContext.instance.customDataManager.cardDataDic[cardId];

                battleCard.init (cardData);
                this.curCardIndex++;

                battleCard.rectTransform.localPosition = new Vector3 (-300, 0, 0);

                this.sectorArrayCard (this.battleCardList);
            });
        }
    }

    private readonly float cardAnimationTime = 0.2f;

    // 卡牌扇形排布
    private void sectorArrayCard (List<BattleCard> battleCards) {

        // 获取卡牌分布位置
        List<float> cardEndPosList = this.calculateCardPos (battleCardList);

        int startIndex = -Mathf.FloorToInt (battleCards.Count / 2);

        for (int i = 0; i < battleCards.Count; i++) {
            BattleCard battleCard = battleCards[i];
            float endX = cardEndPosList[i];

            Vector3 endPos = new Vector3 (endX, 0, 0);
            Vector3 endAngle = new Vector3 (0, 0, startIndex * this.angleValue);

            battleCard.rectTransform.DOLocalMove (endPos, this.cardAnimationTime);

            battleCard.rectTransform.localEulerAngles = endAngle;

            battleCard.setCardInfo (endPos, endAngle, battleCard.rectTransform.localScale);
            startIndex++;
        }
    }

    private List<float> calculateCardPos (List<BattleCard> battleCards) {
        int battleCardCount = battleCards.Count;
        float interval = this.fixedWidth / (battleCardCount + 1);

        bool isDouble = battleCardCount % 2 == 0;
        int startIndex = Mathf.FloorToInt (battleCardCount / 2);

        List<float> posList = new List<float> ();

        float realInterval = interval > this.cardInterval?this.cardInterval : interval;

        if (!isDouble) {
            for (int i = -startIndex; i <= startIndex; i++) {
                float endPos = i * realInterval;
                posList.Add (endPos);
            }
        } else {
            for (int j = -startIndex; j <= startIndex; j++) {
                if (j == 0) {
                    continue;
                }
                int sign = 1;
                int index = 0;
                if (j > 0) {
                    sign = 1;
                    index = j - 1;
                } else {
                    sign = -1;
                    index = j + 1;
                }
                float endPos = sign * (realInterval / 2) + index * realInterval;
                posList.Add (endPos);
            }
        }

        return posList;
    }

    public void removeBatteleCard (BattleCard battleCard) {
        this.battleCardList.Remove (battleCard);
        // TODO: 转移到弃牌堆

        this.sectorArrayCard (this.battleCardList);
    }
}