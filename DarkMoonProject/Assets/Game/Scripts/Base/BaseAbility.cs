/*
 * @Author: l hy 
 * @Date: 2020-11-19 09:33:00 
 * @Description: 能力基类
 */

public abstract class BaseAbility {

    protected CardData cardData = null;

    public int id = 0;

    public abstract void refreshCardData (CardData cardData);

    /// <summary>
    /// 回合开始时激发
    /// </summary>
    public abstract void turnBeginEffect ();

    /// <summary>
    /// 主动触发
    /// </summary>
    public abstract void effect ();

    /// <summary>
    /// 回合结束时激发
    /// </summary>
    public abstract void turnEndEffect ();

    /// <summary>
    /// 能力名称
    /// </summary>
    /// <returns></returns>
    public string abilityName () {
        string abilityName = this.cardData.abilityDataDic[id].abilityName;
        return abilityName;
    }

    /// <summary>
    /// 能力描述
    /// </summary>
    /// <returns></returns>
    public string abilityDescribe () {
        AbilityData abilityData = this.cardData.abilityDataDic[id];
        double baseValue = abilityData.baseValue;
        string describeStr = abilityData.abilityDescribe.Replace ("X", baseValue.ToString ());
        return describeStr;
    }
}