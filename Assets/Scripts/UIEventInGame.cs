using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SFramework;

public class UIEventInGame : MonoBehaviour
{
    public void ResetScene(){
        LevelMgr.instance.ResetScene();
    }
    public void OnMenuBtn(){
        GameManager.instance.cameFromGame = true;
        LevelMgr.instance.LoadScene("MainMenu");
    }
}
