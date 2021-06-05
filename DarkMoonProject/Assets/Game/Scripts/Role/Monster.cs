/*
 * @Author: l hy 
 * @Date: 2021-05-27 10:10:11 
 * @Description:  
 */

using UnityEngine;
public class Monster {

    private MonsterData monsterData;

    private Animator animator;

    private int executeAction;

    private int executeMagic;

    public void init (MonsterData monsterData, Animator animator) {
        this.monsterData = monsterData.clone<MonsterData> ();
        this.animator = animator;
    }

    public void playAnimation () {
        this.animator.SetBool ("isStart", true);
    }

    public void localUpdate (float dt) {

    }

    public void recoveryState () {
        this.executeAction = this.monsterData.actionValue;
        this.executeMagic = this.monsterData.magicValue;
    }
}