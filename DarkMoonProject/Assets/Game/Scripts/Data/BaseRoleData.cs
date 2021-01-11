/*
 * @Author: l hy 
 * @Date: 2020-11-25 16:17:16 
 * @Description: 角色数据基类
 */

using System.Collections.Generic;

public abstract class BaseRoleData {

    public double roleHp = 0;

    public double armor = 0;

    public double energy = 0;

    private Dictionary<int, double> abilityLayerDic = new Dictionary<int, double> ();

    public void addAbilityLayer (int abilityId, double abilityLayer = 1) {
        if (abilityLayerDic.ContainsKey (abilityId)) {
            abilityLayerDic[abilityId] += abilityLayer;
            return;
        }

        abilityLayerDic.Add (abilityId, abilityLayer);
    }
}