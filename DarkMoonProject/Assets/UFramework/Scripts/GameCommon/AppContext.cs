/*
 * @Author: l hy 
 * @Date: 2020-12-03 15:25:34 
 * @Description: AppContext
 */
using LitJson;
using UFramework.Const;
using UFramework.GameCommon;
using UnityEngine;

public class AppContext : MonoBehaviour {

    private static AppContext _instance = null;

    public static AppContext instance {
        get {
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

    private AbilityManager _abilityManager = null;
    public AbilityManager abilityManager {
        get {
            if (_abilityManager == null) {
                _abilityManager = new AbilityManager ();
            }
            return _abilityManager;
        }
    }

    private void Awake () {
        _instance = this;

        this.loadCardPooJson ();
        this.loadAbilityPoolJson ();
        this.abilityManager.init ();
    }

    private void loadCardPooJson () {
        TextAsset cardPoolJson = assetsManager.getAssetByUrl<TextAsset> (CustomUrlString.cardJsonUrl);
        string context = cardPoolJson.text;
        CardPoolData cardPoolData = JsonMapper.ToObject<CardPoolData> (context);
        CustomDataManager.cardPoolData = cardPoolData;
    }

    private void loadAbilityPoolJson () {
        TextAsset abilityJson = assetsManager.getAssetByUrl<TextAsset> (CustomUrlString.abilityJsonUrl);
        string context = abilityJson.text;
        AbilityPoolData abilityPoolData = JsonMapper.ToObject<AbilityPoolData> (context);
        foreach (var abilityData in abilityPoolData.abilities) {
            CustomDataManager.abilityPoolDataDic.Add (abilityData.id, abilityData);
        }
    }
}