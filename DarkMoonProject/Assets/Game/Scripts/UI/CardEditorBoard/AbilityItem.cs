/*
 * @Author: l hy 
 * @Date: 2020-12-07 14:32:05 
 * @Description: 能力列表item
 */

using UFramework;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AbilityItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    [Header ("能力插槽背景")]
    public Image bgImage = null;

    [Header ("插槽名称")]
    public Text abilityName = null;

    [Header ("插槽描述")]
    public Text abilityDescribe = null;

    private string describeStr = null;

    private bool isJoined = false;

    private AbilityData abilityData = null;

    public AbilityData AbilityData {
        get {
            return this.abilityData;
        }
    }

    public void init (AbilityData abilityData = null) {
        this.abilityData = abilityData;

        this.abilityDescribe.transform.parent.gameObject.SetActive (false);

        Color randomColor = Util.getRandomColor ();
        bgImage.color = randomColor;

        this.abilityName.text = abilityData.abilityName;

        this.describeStr = abilityData.abilityDescribe;

        this.isJoined = false;
    }

    public void editorItem_Click () {
        AppContext.instance.uIManager.showDialog (UIPath.AbilityItemEditorDialog, this.abilityData.id);
    }

    public void joinItem_Click () {
        this.isJoined = true;
        this.hideExplain ();
    }

    public void OnPointerEnter (PointerEventData eventData) {
        if (this.isJoined) {
            return;
        }

        this.showExplain ();
    }

    private void showExplain () {
        if (!this.abilityDescribe.transform.parent.gameObject.activeSelf) {
            this.abilityDescribe.transform.parent.gameObject.SetActive (true);
        }

        string currentText = this.abilityDescribe.text;

        if (currentText != this.describeStr) {
            currentText = this.describeStr;
            this.abilityDescribe.text = currentText;
        }
    }

    private void hideExplain () {
        if (this.abilityDescribe.transform.parent.gameObject.activeSelf) {
            this.abilityDescribe.transform.parent.gameObject.SetActive (false);
        }
    }

    public void OnPointerExit (PointerEventData eventData) {
        if (this.isJoined) {
            return;
        }

        this.hideExplain ();
    }

    public bool getJoinedState () {
        return this.isJoined;
    }

    private void OnDisable () {
        this.isJoined = false;
    }
}