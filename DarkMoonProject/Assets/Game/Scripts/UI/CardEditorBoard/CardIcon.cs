/*
 * @Author: l hy 
 * @Date: 2021-05-11 15:04:20 
 * @Description: 卡牌icon
 */
using UnityEngine;
using UnityEngine.UI;

public class CardIcon : MonoBehaviour {

    private Image _cardIconImage = null;

    private string _cardIconUrl;

    public void init (Sprite icon, string iconUrl) {
        this._cardIconImage.sprite = icon;
        this._cardIconUrl = iconUrl;
    }

    public string cardIconUrl {
        get {
            return this._cardIconUrl;
        }
    }
}