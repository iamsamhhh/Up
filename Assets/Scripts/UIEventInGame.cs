using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SFramework;

public class UIEventInGame : MonoBehaviour
{
    [SerializeField]
    GameObject pausePanel;
    GameManager gameManager {
        get{
            return GameManager.instance;
        }
    }
    
    public void ResetScene(){
        LevelMgr.instance.ResetScene();
    }

    public void OnMenuBtn(){
        gameManager.cameFromGame = true;
        LevelMgr.instance.LoadScene("MainMenu");
    }

    public void OnPauseMenuBtn(){
        gameManager.coinCount += gameManager.inGameCoinCnt*gameManager.gameLevel;
        gameManager.SaveGame();
        gameManager.cameFromGame = true;
        gameManager.gamePaused = false;
        LevelMgr.instance.LoadScene("MainMenu");
    }

    public void OnResumeBtn(){
        gameManager.gamePaused = false;
        pausePanel.SetActive(false);
    }

    public void OnPause(){
        gameManager.gamePaused = true;
        pausePanel.SetActive(true);
    }
}
