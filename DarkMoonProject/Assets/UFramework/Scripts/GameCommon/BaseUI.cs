/*
 * @Author: l hy 
 * @Date: 2020-03-07 16:37:25 
 * @Description: 界面基类 
 * @Last Modified by: l hy
 * @Last Modified time: 2021-05-17 22:20:45
 */
namespace UFramework.GameCommon {
    using UnityEngine;

    public class BaseUI : MonoBehaviour {

        public virtual void onShow (params object[] args) {
        }
    }
}