/*
 * @Author: l hy 
 * @Date: 2020-11-19 09:33:00 
 * @Description: 能力基类
 */

public abstract class BaseAbility {

    private int _id = 0;

    public virtual int id {
        get {
            return this._id;
        }

        set {
            this._id = value;
        }
    }

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
}