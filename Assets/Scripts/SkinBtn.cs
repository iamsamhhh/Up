using TMPro;
using UnityEngine;
using UnityEngine.UI;
using MyFramework;

public class SkinBtn : MonoBehaviourSimplify
{
    public Skin skin;
    public Button btn;
    public TMP_Text costText;
    public RawImage rawImage;
    
    UserData userData {
        get {return UserData.userData;}
    }

    private void Awake() {
        AddEvent("OnResetData", OnResetData);
    }

    private void OnDestroy() {
        RemoveAllLocalEvents();
    }

    public void OnClick(){
        if (userData.purchasedSkins.Contains(skin)){
            userData.currentSkin = skin;
            BroadcastEvent("OnChangeSkin", skin);
        }
        else{
            if (userData.coinCount >= skin.cost){
                GUIManager.instance.AddPopUp(
                    "Are you sure you want to buy this skin that cost " + skin.cost + " Coin?",
                    OnConfirmBtn,
                    ()=>{}
                    );
            }
        }
    }

    private void OnConfirmBtn(){
        userData.coinCount -= skin.cost;
        userData.currentSkin = skin;
        userData.purchasedSkins.Add(skin);
        rawImage.color = Color.white;
        costText.text = "";
        GUIManager.instance.GetPanel("MainMenu")
        .GetComponent<MainMenuComponents>()
        .coinCntTxt.text = userData.coinCount.ToString();
        BroadcastEvent("OnChangeSkin", skin);
    }

    private void OnResetData(object sender){
        Debug.LogFormat("{0} recieved event sent by {1}", this, sender);
        if (userData.purchasedSkins.Contains(skin)){
            rawImage.color = Color.white;
            costText.text = "";
        }
        else{
            rawImage.color = new Color(.5f, .5f, .5f);
            costText.text = skin.cost.ToString();
        }
    }

}
