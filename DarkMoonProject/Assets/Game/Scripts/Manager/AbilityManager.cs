/*
 * @Author: l hy 
 * @Date: 2020-11-28 16:58:34 
 * @Description: 能力插槽管理
 */
public class AbilityManager {
    private static AbilityManager _instance = null;

    public static AbilityManager getInstance () {
        if (_instance == null) {
            _instance = new AbilityManager ();
        }

        return _instance;
    }

    
}