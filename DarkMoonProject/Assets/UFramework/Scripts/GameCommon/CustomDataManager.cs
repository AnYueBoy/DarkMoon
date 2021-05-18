using System.Collections.Generic;
/*
 * @Author: l hy 
 * @Date: 2020-12-07 13:42:52 
 * @Description: 自定义数据管理
 */

public class CustomDataManager {
    public CardPoolData cardPoolData;

    public Dictionary<int, AbilityData> abilityPoolDataDic;

    // level 
    //      page
    //          list<int>
    public Dictionary<int, Dictionary<int, List<int>>> battleLevelDic;

    public void init () {
        this.cardPoolData = new CardPoolData ();
        abilityPoolDataDic = new Dictionary<int, AbilityData> ();
        this.battleLevelDic = new Dictionary<int, Dictionary<int, List<int>>> ();
    }
}