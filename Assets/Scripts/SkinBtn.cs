using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
                GameManager.instance.coinCount -= skin.cost;
                GameManager.instance.currentSkin = skin;
                skin.bought = true;
            }
        }
    }
}
