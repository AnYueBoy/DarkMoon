/*
 * @Author: l hy 
 * @Date: 2021-05-27 10:10:11 
 * @Description:  
 */

using UnityEngine;
public class Monster {

    private MonsterData _monsterData;

    private Animator animator;

    private int executeAction;

    private int executeMagic;

    public void init (MonsterData monsterData, Animator animator) {
        this._monsterData = monsterData.clone<MonsterData> ();
        this.animator = animator;
    }

    public void playAnimation () {
        this.animator.SetBool ("isStart", true);
    }

    public void localUpdate (float dt) {

    }

    public void recoveryState () {
        this.executeAction = this._monsterData.actionValue;
        this.executeMagic = this._monsterData.magicValue;
    }

    public void damage (int damage) {
        int armor = this._monsterData.armor;
        if (armor >= damage) {
            this._monsterData.armor -= damage;
            return;
        }

        this._monsterData.monsterHp -= (damage - armor);
        this._monsterData.armor = 0;
    }

    public MonsterData monsterData {
        get {
            return this._monsterData;
        }
    }
}