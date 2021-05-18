/*
 * @Author: l hy 
 * @Date: 2021-05-18 16:05:44 
 * @Description: GameCommon
 */

using UnityEngine;
public class GameCommon : MonoBehaviour {

    private float saveTimer = 0;

    public void localUpdate (float dt) {
        this.saveDataByFixedTime (dt);
    }

    private void saveDataByFixedTime (float dt) {
        this.saveTimer += dt;
        if (this.saveTimer < GameCommonConfig.saveInterval) {
            return;
        }

        this.saveTimer = 0;
        AppContext.instance.playerDataManager.saveData ();
    }

    private void OnApplicationPause (bool pauseStatus) {
        if (pauseStatus) {
            this.onHideCall ();
        } else {
            this.onShowCall ();
        }
    }

    private void onHideCall () {
        AppContext.instance.playerDataManager.saveData ();
    }

    private void onShowCall () {

    }
}