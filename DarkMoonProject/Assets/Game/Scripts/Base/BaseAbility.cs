/*
 * @Author: l hy 
 * @Date: 2020-11-19 09:33:00 
 * @Description: 能力基类
 */

public abstract class BaseAbility {

    protected int id = 1;

    protected abstract void effect (BaseRoleData targetRole);
}