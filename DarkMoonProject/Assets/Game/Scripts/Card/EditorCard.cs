/*
 * @Author: l hy 
 * @Date: 2021-06-05 17:13:10 
 * @Description: 编辑器卡牌
 */
using System;

public class EditorCard : BaseCard {

    private void OnEnable () {
        this.addChangeListener ();
    }

    private void addChangeListener () {
        this.cardName.onValueChanged.AddListener ((string cardNameValue) => {
            this.cardData.cardName = cardNameValue;
            this.showCardInfo ();
        });

        this.cardConsume.onValueChanged.AddListener ((string consumeValue) => {
            if (String.IsNullOrEmpty (consumeValue)) {
                this.cardData.consumeEnergy = 0;
            } else {
                this.cardData.consumeEnergy = int.Parse (consumeValue);
            }
            this.showCardInfo ();
        });
    }

    private void OnDisable () {
        this.cardName.onValueChanged.RemoveAllListeners ();
        this.cardConsume.onValueChanged.RemoveAllListeners ();
    }
}