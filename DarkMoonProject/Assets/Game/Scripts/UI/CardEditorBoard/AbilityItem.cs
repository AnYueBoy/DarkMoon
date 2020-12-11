/*
 * @Author: l hy 
 * @Date: 2020-12-07 14:32:05 
 * @Description: 能力列表item
 */
using System.Collections;
using System.Collections.Generic;
using UFramework;
using UnityEngine;
using UnityEngine.UI;

public class AbilityItem : MonoBehaviour {

    [Header ("能力插槽背景")]
    public Image bgImage = null;

    [Header ("插槽名称")]
    public Text title = null;

    public void init (AbilityData abilityData = null) {
        Color randomColor = Util.getRandomColor ();
        bgImage.color = randomColor;

        // FIXME:等待修改
        // this.title.text = abilityData.title;
    }

    public void editorItem_Click () {

    }

    public void joinItem_Click () {

    }
}