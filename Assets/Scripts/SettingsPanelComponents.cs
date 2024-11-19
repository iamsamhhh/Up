using MyFramework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPanelComponents : MonoBehaviourSimplify
{
    [SerializeField]
    Button exitGameBtn, xBtn, resetDataBtn, saveGameBtn, addCoinBtn, setLevelBtn;
    [SerializeField]
    Slider volumeSlider;
    [SerializeField]
    TMP_InputField addCoinIF, setLevelIF;

    public Button GetExitGameBtn(){return exitGameBtn;}
    public Button GetXBtn(){return xBtn;}
    public Button OnResetDataBtn(UnityEngine.Events.UnityAction action){
        guiManager.OnClick(resetDataBtn, action);
        return resetDataBtn;
    }
    public Button OnSaveGameBtn(UnityEngine.Events.UnityAction action){
        guiManager.OnClick(saveGameBtn, action);
        return saveGameBtn;
    }
    public Button OnAddCoinBtn(UnityEngine.Events.UnityAction action){
        guiManager.OnClick(addCoinBtn, action);
        return addCoinBtn;
    }
    public Button OnSetLevelBtn(UnityEngine.Events.UnityAction action){
        guiManager.OnClick(setLevelBtn, action);
        return setLevelBtn;
    }

    public Slider OnVolumeSlider(UnityEngine.Events.UnityAction<float> action){
        volumeSlider.onValueChanged.AddListener(action);
        return volumeSlider;
    }

    public string GetAddCoinIFValue(){
        return addCoinIF.text;
    }

    public string GetSetLevelIFValue(){
        return setLevelIF.text;
    }
}
