/*
 * @Author: l hy 
 * @Date: 2021-05-11 15:04:20 
 * @Description: 卡牌icon
 */
using System;
using UnityEngine;
using UnityEngine.UI;

public class CardIcon : MonoBehaviour {

    public Image cardIconImage = null;

    public GameObject selectedNode;

    private string cardIconUrl;

    private Action<string> selectedCall;

    public void init (Sprite icon, string iconUrl, Action<string> selected) {
        this.cardIconImage.sprite = icon;
        this.cardIconUrl = iconUrl;
        this.selectedCall = selected;
        this.selectedNode.SetActive (false);
    }

    public void selectedClick () {
        this.selectedCall?.Invoke (this.cardIconUrl);
        this.selectedNode.SetActive (true);
    }

    public void unSelected () {
        this.selectedNode.SetActive (false);
    }
}