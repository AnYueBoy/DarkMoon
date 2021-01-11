/*
 * @Author: l hy 
 * @Date: 2020-11-19 09:33:00 
 * @Description: 能力基类
 */

public abstract class BaseAbility {

    protected CardData cardData = null;

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
    /// 能力标题
    /// </summary>
    /// <returns></returns>
    public abstract string title ();

    /// <summary>
    /// 能力描述
    /// </summary>
    /// <returns></returns>
    public abstract string describe ();
}