/*
 * @Author: l hy 
 * @Date: 2021-05-21 13:10:25 
 * @Description:  战斗界面
 */

using TMPro;
using UFramework.GameCommon;
using UnityEngine;
using UnityEngine.UI;
public class BattleBoard : BaseUI {
    public RectTransform cardParent;

    public Image hpFillImage;

    public Image magicFillImage;

    public TMP_Text actionValue;

    private MonsterData battleMonsterData;

    public override void onShow (params object[] args) {
        int monsterId = (int) args[0];

        Monster monster = AppContext.instance.spawnManager.createMonster (monsterId);
        AppContext.instance.battleManager.battlePrepare (monster, this.cardParent);

        this.battleMonsterData = monster.monsterData;
    }

    private void Update () {
        this.refreshMonsterInfo ();
    }

    private int preMonsterHp;

    private int preMonsterMagic;

    private int preAction;

    private void refreshMonsterInfo () {
        int curMonsterHp = this.battleMonsterData.monsterHp;
        if (this.preMonsterHp != curMonsterHp) {
            this.preMonsterHp = curMonsterHp;
            this.hpFillImage.fillAmount = curMonsterHp / this.battleMonsterData.maxMonsterHp;
        }

        int curMonsterMagic = this.battleMonsterData.magicValue;
        if (this.preMonsterMagic != curMonsterMagic) {
            this.preMonsterMagic = curMonsterMagic;
            this.magicFillImage.fillAmount = curMonsterMagic / this.battleMonsterData.maxMagicValue;
        }

        int curActionValue = this.battleMonsterData.actionValue;
        if (this.preAction != curActionValue) {
            this.preAction = curActionValue;
            this.actionValue.text = curActionValue.ToString ();
        }
    }
}