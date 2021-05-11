/*
 * @Author: l hy 
 * @Date: 2021-01-20 10:07:15 
 * @Description: 自定义资源路径
 */

using UFramework.Const;

public class CustomUrlString : UrlString {

    #region 配置文件
    public const string cardJson = "Json/cardPool";

    public const string abilityJson = "Json/abilityPool";
    #endregion

    #region 预制资源
    public const string abilityPrefab = "Prefabs/abilityItem";

    #endregion

    #region 图片资源
    public const string consumePreTexture = "Textures/Consume/";

    public const string cardIconTexture = "Textures/CardIcon/";

    #endregion
}