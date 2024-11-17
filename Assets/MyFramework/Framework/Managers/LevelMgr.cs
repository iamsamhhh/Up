using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace MyFramework{
public class LevelMgr : SingletonBase<LevelMgr>
{
    LevelMgrGo mgrGo = LevelMgrGo.instance;
    public void LoadNext(bool sync = false){
        if (sync){

        }
        else{
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void LoadPrevious(bool sync = false){
        if (sync){

        }
        else{
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }

    public void ResetScene(bool sync = false){
        if (sync){

        }
        else{
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void LoadScene(int index, bool sync = false){
        if (sync){

        }
        else{
            SceneManager.LoadScene(index);
        }
    }

    public void LoadScene(string sceneName, bool sync = false){
        if (sync){
            
        }
        else{
            SceneManager.LoadScene(sceneName);
        }
    }
}
}