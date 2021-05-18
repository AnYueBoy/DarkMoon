/*
 * @Author: l hy 
 * @Date: 2020-12-03 15:25:34 
 * @Description: AppContext
 */
using UFramework.GameCommon;
using UnityEngine;

public class AppContext : MonoBehaviour {

    private static AppContext _instance = null;

    public GameCommon gameCommon = null;

    #region 管理类
    public AssetsManager assetsManager = new AssetsManager ();

    public AudioManager audioManager = null;

    public ListenerManager listenerManager = new ListenerManager ();

    public AbilityManager abilityManager = new AbilityManager ();

    public DataManager dataManager = new DataManager ();

    public UIManager uIManager = new UIManager ();

    public CustomDataManager customDataManager = new CustomDataManager ();

    public ConfigManager configManager = new ConfigManager ();

    public PlayerrDataManager playerDataManager = new PlayerrDataManager ();

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

    private void Update () {
        float deltaTime = Time.deltaTime;
        this.gameCommon.localUpdate (deltaTime);
    }

    private void init () {
        this.playerDataManager.init ();
        this.customDataManager.init ();
        // FIXME: 可能要等待
        this.configManager.init ();

        this.abilityManager.init ();
        this.uIManager.init (this.uiRoot);
        this.uIManager.showBoard (UIPath.HallBoard);
    }

}