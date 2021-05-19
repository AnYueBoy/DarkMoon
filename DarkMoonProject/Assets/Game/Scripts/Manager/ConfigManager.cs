using System.Collections.Generic;
/*
 * @Author: l hy 
 * @Date: 2021-05-13 15:50:18 
 * @Description: 配置文件管理
 */
using System;
using LitJson;
using UFramework.Promise;
using UnityEngine;

public class ConfigManager {

    public Promise init () {
        Promise[] promises = new Promise[] {
            this.loadCardPooConfig (),
            this.loadAbilityPoolConfig (),
            this.loadBattleLevelConfig (),
            this.loadItemsConfig ()
        };
        return Promise.all (promises);
    }

    private Promise loadCardPooConfig () {
        return new Promise ((Action resolve, Action<Exception> reject) => {
            TextAsset cardPoolConfig = AppContext.instance.assetsManager.getAssetByUrlSync<TextAsset> (ConfigPath.cardPoolConfig);
            string context = cardPoolConfig.text;
            CardPoolData cardPoolData = JsonMapper.ToObject<CardPoolData> (context);
            AppContext.instance.customDataManager.cardPoolData = cardPoolData;
            resolve?.Invoke ();
        });
    }

    private Promise loadAbilityPoolConfig () {
        return new Promise ((Action resolve, Action<Exception> reject) => {
            TextAsset abilityPoolConfig = AppContext.instance.assetsManager.getAssetByUrlSync<TextAsset> (ConfigPath.abilityPoolConfig);
            string context = abilityPoolConfig.text;
            AbilityPoolData abilityPoolData = JsonMapper.ToObject<AbilityPoolData> (context);
            foreach (AbilityData abilityData in abilityPoolData.abilities) {
                AppContext.instance.customDataManager.abilityPoolDataDic.Add (abilityData.id, abilityData);
            }
            resolve?.Invoke ();
        });
    }

    private Promise loadBattleLevelConfig () {
        return new Promise ((Action resolve, Action<Exception> reject) => {
            TextAsset battleLevelConfig = AppContext.instance.assetsManager.getAssetByUrlSync<TextAsset> (ConfigPath.battleLevelConfig);
            string context = battleLevelConfig.text;
            LevelBattleConfigData levelBattleConfigData = JsonMapper.ToObject<LevelBattleConfigData> (context);

            Dictionary<int, Dictionary<int, List<int>>> battleLevelDic = AppContext.instance.customDataManager.battleLevelDic;

            foreach (LevelConfigData levelConfigData in levelBattleConfigData.levels) {
                int level = levelConfigData.level;

                Dictionary<int, List<int>> pageDic = null;
                if (!battleLevelDic.ContainsKey (level)) {
                    pageDic = new Dictionary<int, List<int>> ();
                    battleLevelDic.Add (level, pageDic);
                }

                pageDic = battleLevelDic[level];

                foreach (PageConfigData pageConfigData in levelConfigData.pages) {
                    int pageIndex = pageConfigData.page;
                    List<int> itemIdList = null;
                    if (!pageDic.ContainsKey (pageIndex)) {
                        itemIdList = new List<int> ();
                        pageDic.Add (pageIndex, itemIdList);
                    }

                    foreach (ItemConfigData itemConfig in pageConfigData.itemIds) {
                        itemIdList.Add (itemConfig.itemId);
                    }
                }
            }

            resolve?.Invoke ();
        });
    }

    private Promise loadItemsConfig () {
        return new Promise ((Action resolve, Action<Exception> reject) => {
            TextAsset itemsConfig = AppContext.instance.assetsManager.getAssetByUrlSync<TextAsset> (ConfigPath.itemsConfig);
            string context = itemsConfig.text;

            BattleItemList battleItemList = JsonMapper.ToObject<BattleItemList> (context);
            foreach (BattleItemData battleItemData in battleItemList.items) {
                AppContext.instance.customDataManager.battleItemDataDic.Add (battleItemData.id, battleItemData);
            }
        });
    }

}