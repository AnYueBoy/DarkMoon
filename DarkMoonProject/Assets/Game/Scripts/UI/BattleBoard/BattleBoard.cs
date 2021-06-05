/*
 * @Author: l hy 
 * @Date: 2021-05-21 13:10:25 
 * @Description: {} 
 */

using DG.Tweening;
using UFramework.GameCommon;
using UnityEngine;
public class BattleBoard : BaseUI {

    public override void onShow (params object[] args) {
        int monsterId = (int) args[0];

        Monster monster = AppContext.instance.spawnManager.createMonster (monsterId);
        AppContext.instance.battleManager.battlePrepare (monster);
    }
}