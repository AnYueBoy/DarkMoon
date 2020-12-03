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

    private AudioManager _audioManager = null;
    public AudioManager audioManager {
        get {
            if (_audioManager == null) {
                _audioManager = new AudioManager ();
            }

            return _audioManager;
        }
    }

    private ListenerManager _listenerManager = null;
    public ListenerManager listenerManager {
        get {
            if (_listenerManager == null) {
                _listenerManager = new ListenerManager ();
            }
            return _listenerManager;
        }
    }
}