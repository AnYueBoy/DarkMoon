/*
 * @Author: l hy 
 * @Date: 2020-11-26 16:24:14 
 * @Description: 数据管理
 */

using UnityEngine;

public class DataManager {

    private BaseRoleData _battlePlayerData = null;
    // public BaseRoleData battleRoleData () {
    //     return _battleRoleData;
    // }

    private BaseRoleData _battleEnemyData = null;
    // public BaseRoleData battleEnemyData () {
    //     return _battleEnemyData;
    // }

    /// <summary>
    /// 获取对应阵营的角色数据
    /// </summary>
    /// <param name="camp"></param>
    /// <returns></returns>
    public BaseRoleData getCampRoleData (CampEnum camp) {
        switch (camp) {
            case CampEnum.ENEMY:
                return this._battleEnemyData;

            case CampEnum.PLAYER:
                return this._battlePlayerData;

            default:
                Debug.LogError ("can not get campData: " + camp);
                return null;
        }
    }

    public BaseRoleData getOtherCampRoleData (CampEnum camp) {
        switch (camp) {
            case CampEnum.ENEMY:
                return this._battlePlayerData;

            case CampEnum.PLAYER:
                return this._battleEnemyData;

            default:
                Debug.LogError ("can not get otherCampData: " + camp);
                return null;
        }
    }
}