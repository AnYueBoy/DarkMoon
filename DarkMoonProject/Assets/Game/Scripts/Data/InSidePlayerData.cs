using System.Collections.Generic;
/*
 * @Author: l hy 
 * @Date: 2020-11-21 10:52:35 
 * @Description: 局内玩家数据
 */
public class InSidePlayerData {

    public double currentHP = 0;

    public double armor = 0;

    /// <summary>
    /// 能力所对应的层数数据
    /// </summary>
    /// <typeparam name="int"></typeparam>
    /// <typeparam name="int"></typeparam>
    /// <returns></returns>
    public Dictionary<int, int> abilityLayerDic = new Dictionary<int, int> ();
}