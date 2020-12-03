/*
 * @Author: l hy 
 * @Date: 2020-10-10 06:54:10 
 * @Description: 资源加载管理 
 * @Last Modified by: l hy
 * @Last Modified time: 2020-12-03 15:57:37
 */

using UnityEngine;

public class AssetsLoadManager {

    public T loadAssets<T> (string assetsUrl) where T : Object {
        //  load json prefab or audioclip 
        return Resources.Load<T> (assetsUrl);
    }
}