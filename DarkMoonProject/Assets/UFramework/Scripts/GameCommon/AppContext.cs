/*
 * @Author: l hy 
 * @Date: 2020-12-03 15:25:34 
 * @Description: AppContext
 */
public class AppContext {

    private static AppContext _instance = null;
    public static AppContext instance {
        get {
            if (_instance == null) {
                _instance = new AppContext ();
            }

            return _instance;
        }
    }

    private AssetsManager _assetsManager = null;
    public AssetsManager assetsManager {
        get {
            if (_assetsManager == null) {
                _assetsManager = new AssetsManager ();
            }
            return this._assetsManager;
        }
    }
}