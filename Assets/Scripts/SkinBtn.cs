using TMPro;
using UnityEngine;
using UnityEngine.UI;
using MyFramework;
using System;
public class SkinBtn : MonoBehaviour
{
    public Skin skin;
    public Button btn;
    public TMP_Text costText;
    public RawImage rawImage;
    public void OnClick(){
        if (skin.bought){
            GameManager.instance.currentSkin = skin;
        }
        else{
            if (GameManager.instance.coinCount >= skin.cost){
                GUIManager.instance.AddPopUp(
                    "Are you sure you want to buy this skin that cost " + skin.cost + " Coin?",
                    OnConfirmBtn,
                    onCancelBtn
                    );
            }
        }
    }

    private void OnConfirmBtn(){
        GameManager.instance.coinCount -= skin.cost;
        GameManager.instance.currentSkin = skin;
        skin.bought = true;
        GUIManager.instance.GetPanel("MainMenu")
        .GetComponent<MainMenuComponents>()
        .GetCoinCntTxt().text = GameManager.instance.coinCount.ToString();
    }

    private void onCancelBtn(){}
}
