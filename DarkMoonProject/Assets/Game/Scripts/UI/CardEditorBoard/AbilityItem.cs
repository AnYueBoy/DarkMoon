using System.Net.Mime;
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

    [Header ("插槽名称")]
    public Text title = null;

    [Header ("描述")]
    public Text describe = null;

    private string targetDescribe = null;
    public void init (AbilityData abilityData = null) {
        this.describe.gameObject.SetActive (false);

        Color randomColor = Util.getRandomColor ();
        bgImage.color = randomColor;
        this.title.text = abilityData.title;

        string patternStr = abilityData.describe;
        int targetIndex = patternStr.IndexOf ('X');
        if (targetIndex != -1) {
            patternStr = patternStr.Replace ("X", abilityData.baseValue.ToString ());
        }

        targetDescribe = patternStr;
    }

    public void editorItem_Click () {

    }

    public void joinItem_Click () {

    }

    public void OnPointerEnter (PointerEventData eventData) {
        if (!this.describe.gameObject.activeSelf) {
            this.describe.gameObject.SetActive (true);
        }

        string currentText = this.describe.text;

        if (currentText != this.targetDescribe) {
            currentText = this.targetDescribe;
            this.describe.text = currentText;
        }
    }

    public void OnPointerExit (PointerEventData eventData) {
        if (this.describe.gameObject.activeSelf) {
            this.describe.gameObject.SetActive (false);
        }
    }
}