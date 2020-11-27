/*
 * @Author: l hy 
 * @Date: 2020-11-26 16:24:14 
 * @Description: 数据管理
 */
public class DataManager {

    private static DataManager _instance = null;

    public static DataManager instance () {
        if (_instance == null) {
            _instance = new DataManager ();
        }

        return _instance;
    }

    private BaseRoleData _battleRoleData = null;
    public BaseRoleData battleRoleData () {
        return _battleRoleData;
    }

    private BaseRoleData _battleEnemyData = null;
    public BaseRoleData battleEnemyData () {
        return _battleEnemyData;
    }
}