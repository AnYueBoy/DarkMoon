/*
 * @Author: l hy 
 * @Date: 2021-05-27 10:16:38 
 * @Description: 生成器 
 */

using UFramework.GameCommon;
using UnityEngine;
public class SpawnManager {

    public Monster createMonster (int monsterId) {
        MonsterData monsterData = AppContext.instance.customDataManager.monsterDataDic[monsterId];
        string monsterUrl = monsterData.url;
        GameObject monsterPrefab = AppContext.instance.assetsManager.getAssetByUrlSync<GameObject> (monsterUrl);
        GameObject monsterNode = ObjectPool.instance.requestInstance (monsterPrefab);
        monsterNode.transform.SetParent (AppContext.instance.monsterParent);
        monsterNode.transform.localPosition = Vector3.zero;
        monsterNode.transform.localScale = Vector3.one;

        Animator animator = monsterNode.GetComponent<Animator> ();
        Monster monster = new Monster ();
        monster.init (monsterData, animator);
        return monster;
    }

    public BattleCard createBattleCard (Transform cardParent) {
        GameObject cardPrefab = AppContext.instance.assetsManager.getAssetByUrlSync<GameObject> (CustomUrlString.battleCardItemPrefab);
        GameObject cardNode = ObjectPool.instance.requestInstance (cardPrefab);
        cardNode.transform.SetParent (cardParent);
        cardNode.transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);
        cardNode.transform.localPosition = Vector3.zero;

        BattleCard battleCard = cardNode.GetComponent<BattleCard> ();
        return battleCard;
    }
}