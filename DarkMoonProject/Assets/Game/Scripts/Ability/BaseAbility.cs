﻿/*
 * @Author: l hy 
 * @Date: 2020-11-19 09:33:00 
 * @Description: 能力基类
 */

public abstract class BaseAbility {

    protected int id = 0;

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

    public BaseAbility (int id) {
        this.id = id;
    }
}