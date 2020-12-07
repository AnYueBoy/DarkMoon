/*
 * @Author: l hy 
 * @Date: 2020-11-19 09:33:00 
 * @Description: 能力基类
 */

public abstract class BaseAbility {

    public abstract void refreshCardData (CardData cardData);

    public abstract void turnBeginEffect ();

    public abstract void effect ();

    public abstract void turnEndEffect ();

    public abstract string title ();

    public abstract string describe ();
}