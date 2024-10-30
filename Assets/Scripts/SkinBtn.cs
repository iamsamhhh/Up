using UnityEngine;

public class SkinBtn : MonoBehaviour
{
    public Skin skin;
    public void OnClick(){
        GameManager.instance.currentSkin = skin;
    }
}
