using UnityEngine;
using MyFramework;

public class UIEventInGame : MonoBehaviour
{
    [SerializeField]
    GameObject pausePanel;
    [SerializeField]
    PlayerController player;
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
        player.SaveGame();
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
