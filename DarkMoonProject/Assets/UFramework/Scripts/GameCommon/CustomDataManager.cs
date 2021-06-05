using System.Collections.Generic;
/*
 * @Author: l hy 
 * @Date: 2020-12-07 13:42:52 
 * @Description: 用于读取自定义的配置数据 
 */

public class CustomDataManager {

    #region 游戏中使用数据
    public Dictionary<int, CustomCardData> cardDataDic;

    public Dictionary<int, AbilityData> abilityPoolDataDic;

    // level 
    //      page
    //          list<int>
    public Dictionary<int, Dictionary<int, List<int>>> battleLevelDic;

    public Dictionary<int, BattleItemData> battleItemDataDic;

    public Dictionary<int, MonsterData> monsterDataDic;

    #endregion

    #region  用于序列化数据

    public CardPoolData cardPoolData;
    #endregion

    public void init () {
        this.cardDataDic = new Dictionary<int, CustomCardData> ();
        this.abilityPoolDataDic = new Dictionary<int, AbilityData> ();
        this.battleLevelDic = new Dictionary<int, Dictionary<int, List<int>>> ();
        this.battleItemDataDic = new Dictionary<int, BattleItemData> ();
        this.monsterDataDic = new Dictionary<int, MonsterData> ();

        this.cardPoolData = new CardPoolData ();
    }
}