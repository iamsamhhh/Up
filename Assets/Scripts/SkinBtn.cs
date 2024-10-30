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
        GameManager.instance.currentSkin = skin;
    }
}
