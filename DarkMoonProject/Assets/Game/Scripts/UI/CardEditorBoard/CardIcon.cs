/*
 * @Author: l hy 
 * @Date: 2021-05-11 15:04:20 
 * @Description: 卡牌icon
 */
using UnityEngine;
using UnityEngine.UI;

public class CardIcon : MonoBehaviour {

    public Image cardIconImage = null;

    public string cardIconUrl;

    public void init (Sprite icon, string iconUrl) {
        this.cardIconImage.sprite = icon;
        this.cardIconUrl = iconUrl;
    }

}