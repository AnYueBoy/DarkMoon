/*
 * @Author: l hy 
 * @Date: 2020-11-19 09:33:00 
 * @Description: 能力基类
 */

public abstract class BaseAbility {

    public abstract void turnBeginEffect (CardData cardData);

    public abstract void effect (CardData cardData);

    public abstract void turnEndEffect (CardData cardData);
}