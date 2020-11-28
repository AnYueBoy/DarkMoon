/*
 * @Author: l hy 
 * @Date: 2020-11-19 09:33:00 
 * @Description: 能力基类
 */

public abstract class BaseAbility {

    protected abstract void turnBeginEffect (CardData cardData);

    protected abstract void effect (CardData cardData);

    protected abstract void turnEndEffect (CardData cardData);
}