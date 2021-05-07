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
        this.loadCardPooJson ();
        this.loadAbilityPoolJson ();
        this.abilityManager.init ();
        this.uIManager.init (this.uiRoot);

        this.uIManager.showBoard (UIPath.HallBoard);
    }

    private void loadCardPooJson () {
        TextAsset cardPoolJson = assetsManager.getAssetByUrlSync<TextAsset> (CustomUrlString.cardJsonUrl);
        string context = cardPoolJson.text;
        CardPoolData cardPoolData = JsonMapper.ToObject<CardPoolData> (context);
        CustomDataManager.cardPoolData = cardPoolData;
    }

    private void loadAbilityPoolJson () {
        TextAsset abilityJson = assetsManager.getAssetByUrlSync<TextAsset> (CustomUrlString.abilityJsonUrl);
        string context = abilityJson.text;
        AbilityPoolData abilityPoolData = JsonMapper.ToObject<AbilityPoolData> (context);
        foreach (var abilityData in abilityPoolData.abilities) {
            CustomDataManager.abilityPoolDataDic.Add (abilityData.id, abilityData);
        }
    }
}