/*
 * @Author: l hy 
 * @Date: 2020-12-07 14:32:05 
 * @Description: 能力列表item
 */
using System.Collections;
using System.Collections.Generic;
using UFramework;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AbilityItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    [Header ("能力插槽背景")]
    public Image bgImage = null;

    [Header ("插槽能力")]
    public Text describe = null;

    [Header ("能力解释")]
    public Text explain = null;

    private string explainStr = null;

    private bool isJoined = false;

    public void init (AbilityData abilityData = null) {
        this.explain.transform.parent.gameObject.SetActive (false);

        Color randomColor = Util.getRandomColor ();
        bgImage.color = randomColor;

        this.explainStr = abilityData.explain;
        string patternStr = abilityData.describe;
        int targetIndex = patternStr.IndexOf ('X');
        if (targetIndex != -1) {
            patternStr = patternStr.Replace ("X", abilityData.baseValue.ToString ());
        }

        this.describe.text = patternStr;
    }

    public void editorItem_Click () {

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
        if (!this.explain.transform.parent.gameObject.activeSelf) {
            this.explain.transform.parent.gameObject.SetActive (true);
        }

        string currentText = this.explain.text;

        if (currentText != this.explainStr) {
            currentText = this.explainStr;
            this.explain.text = currentText;
        }
    }

    private void hideExplain () {
        if (this.explain.transform.parent.gameObject.activeSelf) {
            this.explain.transform.parent.gameObject.SetActive (false);
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
}