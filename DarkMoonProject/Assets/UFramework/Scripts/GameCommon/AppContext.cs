/*
 * @Author: l hy 
 * @Date: 2020-12-03 15:25:34 
 * @Description: AppContext
 */
using LitJson;
using UFramework.GameCommon;
using UnityEngine;

public class AppContext : MonoBehaviour {

    private static AppContext _instance = null;

    #region 管理类
    public AssetsManager assetsManager = new AssetsManager ();

    public AudioManager audioManager = null;

    public ListenerManager listenerManager = new ListenerManager ();

    public AbilityManager abilityManager = new AbilityManager ();

    public DataManager dataManager = new DataManager ();

    public UIManager uIManager = new UIManager ();

    public CustomDataManager customDataManager = new CustomDataManager ();

    public ConfigManager configManager = new ConfigManager ();

    #endregion

    public GameObject uiRoot = null;

    public static AppContext instance {
        get {
            return _instance;
        }
    }

    private void Awake () {
        _instance = this;

        this.init ();
    }

    private void init () {
        this.customDataManager.init ();
        // FIXME: 可能要等待
        this.configManager.init ();

        this.abilityManager.init ();
        this.uIManager.init (this.uiRoot);
        this.uIManager.showBoard (UIPath.HallBoard);
    }

}