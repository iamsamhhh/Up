using TMPro;
using UnityEngine;
using UnityEngine.UI;
using MyFramework;

public class SkinBtn : MonoBehaviour
{
    public Skin skin;
    public Button btn;
    public TMP_Text costText;
    public RawImage rawImage;
    
    UserData userData {
        get {return UserData.defaultUserData;}
    }
    public void OnClick(){
        if (userData.purchasedSkins.Contains(skin)){
            userData.currentSkin = skin;
        }
        else{
            if (userData.coinCount >= skin.cost){
                GUIManager.instance.AddPopUp(
                    "Are you sure you want to buy this skin that cost " + skin.cost + " Coin?",
                    OnConfirmBtn,
                    onCancelBtn
                    );
            }
        }
    }

    private void OnConfirmBtn(){
        userData.coinCount -= skin.cost;
        userData.currentSkin = skin;
        userData.purchasedSkins.Add(skin);
        rawImage.color = Color.white;
        GUIManager.instance.GetPanel("MainMenu")
        .GetComponent<MainMenuComponents>()
        .GetCoinCntTxt().text = userData.coinCount.ToString();
    }

    private void onCancelBtn(){}
}
