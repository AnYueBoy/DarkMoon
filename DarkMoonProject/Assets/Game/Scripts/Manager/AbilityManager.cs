/*
 * @Author: l hy 
 * @Date: 2020-11-28 16:58:34 
 * @Description: 能力插槽管理
 */

using System.Collections.Generic;
public class AbilityManager {
    public Dictionary<int, BaseAbility> abilityDic = new Dictionary<int, BaseAbility> ();

    public void init () {
        // FIXME: 初始化方式待修改
        AttackAbility attackAbility = new AttackAbility (1);
        this.abilityDic.Add (1, attackAbility);

        DefenceAbility defenceAbility = new DefenceAbility (2);
        this.abilityDic.Add (2, defenceAbility);

        NotingAbility notingAbility = new NotingAbility (3);
        this.abilityDic.Add (3, notingAbility);
    }
}