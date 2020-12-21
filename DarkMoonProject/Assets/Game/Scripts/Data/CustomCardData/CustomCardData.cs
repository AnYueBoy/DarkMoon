using System.Collections.Generic;
/*
 * @Author: l hy 
 * @Date: 2020-12-04 07:45:21 
 * @Description: 自定义卡牌数据(用来读取卡池数据)
 * @Last Modified by: l hy
 * @Last Modified time: 2020-12-07 15:30:00
 */

public class CustomCardData {
    public int id = 0;

    public int consumeEnergy = 0;

    public string cardName = "";

    public string textureUrl = "";

    public List<AbilityData> abilities = new List<AbilityData> ();
}