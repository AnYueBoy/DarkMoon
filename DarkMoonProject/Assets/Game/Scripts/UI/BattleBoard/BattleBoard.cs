/*
 * @Author: l hy 
 * @Date: 2021-05-21 13:10:25 
 * @Description: {} 
 */

using DG.Tweening;
using UFramework.GameCommon;
using UnityEngine;
public class BattleBoard : BaseUI {

    private MonsterData monsterData;

    private Animator animator;

    public override void onShow (params object[] args) {
        int monsterId = (int) args[0];
        this.monsterData = AppContext.instance.customDataManager.monsterDataDic[monsterId];

        this.createMonster ();

        // 临时启动动画
        AppContext.instance.promiseTimer.waitFor (3).then (
            () => {
                this.animator.SetBool ("isStart", true);
            }
        );
    }

    private void createMonster () {
        string monsterUrl = this.monsterData.url;
        GameObject monsterPrefab = AppContext.instance.assetsManager.getAssetByUrlSync<GameObject> (monsterUrl);
        GameObject monsterNode = ObjectPool.instance.requestInstance (monsterPrefab);
        monsterNode.transform.SetParent (AppContext.instance.monsterParent);
        this.animator = monsterNode.GetComponent<Animator> ();
    }
}