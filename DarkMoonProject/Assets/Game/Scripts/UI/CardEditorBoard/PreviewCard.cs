/*
 * @Author: l hy 
 * @Date: 2020-12-21 21:24:52 
 * @Description: 预览区卡牌
 * @Last Modified by: l hy
 * @Last Modified time: 2021-01-11 11:12:34
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreviewCard : MonoBehaviour {

    #region ui相关

    [Header ("立绘图片")]
    public Image drawImage = null;

    [Header ("背景图片")]
    public Image cardBgImage = null;

    [Header ("描述")]
    public Text describeText = null;

    #endregion

    public void refreshCard (PreviewData previewData) {
        // this.drawImage.sprite = new sprite();
        // this.cardBgImage.sprite = new Sprite();

        string describeStr = "";
        foreach (AbilityData abilityData in previewData.abilityDataList) {
            string replaceStr = abilityData.abilityEffect.Replace ("X", abilityData.baseValue.ToString ());
            describeStr += replaceStr + "\n";
        }
        this.describeText.text = describeStr;
    }
}