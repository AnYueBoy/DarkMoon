/*
 * @Author: l hy 
 * @Date: 2021-05-18 16:13:48 
 * @Description: 玩家数据管理
 */

using System.IO;
using LitJson;
using UnityEngine;

public class PlayerDataManager {

    private PlayerData _playerData = new PlayerData ();

    public void init () {
        this.parseData ();
    }

    public void parseData () {
        TextAsset jsonAsset = AppContext.instance.assetsManager.getAssetByUrlSync<TextAsset> (ConfigPath.playerDataConfig);
        if (jsonAsset == null) {
            return;
        }
        string context = jsonAsset.text;
        if (string.IsNullOrEmpty (context)) {
            return;
        }

        this._playerData = JsonMapper.ToObject<PlayerData> (context);
        this._playerData.isNewPlayer = false;
    }

    public void saveData () {
        string playerDataStr = JsonMapper.ToJson (this._playerData);
        string filePath = Application.dataPath + "/Game/Resources/" + ConfigPath.playerDataConfig + ".json";

        StreamWriter sw = new StreamWriter (filePath);
        sw.Write (playerDataStr);
        sw.Close ();
    }

    public PlayerData playerData {
        get {
            return this._playerData;
        }
    }
}