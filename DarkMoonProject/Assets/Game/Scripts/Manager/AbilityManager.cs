/*
 * @Author: l hy 
 * @Date: 2020-11-28 16:58:34 
 * @Description: 能力插槽管理
 */

using System.Collections.Generic;
public class AbilityManager {
    private static AbilityManager _instance = null;

    public static AbilityManager getInstance () {
        if (_instance == null) {
            _instance = new AbilityManager ();
        }

        return _instance;
    }

    public Dictionary<int, BaseAbility> abilityDic = new Dictionary<int, BaseAbility> ();

    public void init () {
        // FIXME: 初始化方式待修改
        AttackAbility attackAbility = new AttackAbility ();
        this.abilityDic.Add (attackAbility.id, attackAbility);

        DefenceAbility defenceAbility = new DefenceAbility ();
        this.abilityDic.Add (defenceAbility.id, defenceAbility);

        NotingAbility notingAbility = new NotingAbility ();
        this.abilityDic.Add (notingAbility.id, notingAbility);
    }
}