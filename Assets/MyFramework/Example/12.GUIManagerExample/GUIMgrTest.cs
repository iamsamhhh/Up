using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MyFramework{
    public class GUIMgrTest : MonoBehaviourSimplify
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("MyFramework/Example/12.GUI manager example", false, 12)]
        private static void MenuClicked(){
            UnityEditor.EditorApplication.isPlaying = true;
            new GameObject("Gui test").AddComponent<GUIMgrTest>();
        }
#endif
        [SerializeField]
        GameObject bullet;
        GUIManager guiMgr = GUIManager.instance;
        const string FIRE_PANEL_NAME = "fire panel";
        const string NEXTLEVEL_BTN_NAME = "next level";
        const string FIRE_BTN_NAME = "Fire btn";
        const string OPEN_POOL_BTN_NAME = "Openpool";
        const string OPEN_SAVE_BTN_NAME = "OpenMenu";
        const string MENU_NAME = "Menu";
        const string PANEL_NAME = "Panel";
        const string SAVE_BTN_NAME = "save btn";
        const string QUIT_BTN_NAME = "quit btn";
        const string SAVE_FILE_NAME = "Text";
        InputField inptField;
        // SimpleObjectPool<GameObject> pool;
        bool menuIsActive = true;
        GameObject menuGo;
        private void Awake() {
            // pool = new SimpleObjectPool<GameObject>(BulletFactoryMethod, BulletResetMethod, 5);

            OnStart(()=>{
                mOnStart();
            });

            OnUpdate(()=>{
                Debug.Log("test");
            });
        }

        void mOnStart(){
            guiMgr.Set(new Vector2(3840, 2160), 1);
                
            menuGo = guiMgr.AddPanel(MENU_NAME, ELayer.Bottom);

            if (!guiMgr.OnClick(MENU_NAME, OPEN_SAVE_BTN_NAME, OnClickOpenSave)){
                // fail to add onClick
                Debug.LogErrorFormat("Fail to add OnClick for btn {0}", OPEN_SAVE_BTN_NAME);
            }

            if(!guiMgr.OnClick(MENU_NAME, OPEN_POOL_BTN_NAME, OnClickOpenPool)){

                Debug.LogErrorFormat("Fail to add OnClick for btn {0}", OPEN_POOL_BTN_NAME);
            }

            if(!guiMgr.OnClick(MENU_NAME, NEXTLEVEL_BTN_NAME, ()=>{
                RemoveAllLoacalUpdates();
            })){

                Debug.LogErrorFormat("Fail to add OnClick for btn {0}", NEXTLEVEL_BTN_NAME);
            }

        }

        private void OnDestroy() {
            guiMgr.RemovePanel(MENU_NAME);
            guiMgr.RemovePanel(PANEL_NAME);
            guiMgr.RemovePanel(FIRE_PANEL_NAME);
        }

        private void OnClickOpenSave(){
            menuIsActive = false;
            menuGo.SetActive(menuIsActive);
            inptField = guiMgr.AddPanel(PANEL_NAME, ELayer.Top).transform.GetComponentInChildren<InputField>();



            // save action
            guiMgr.OnClick(PANEL_NAME, SAVE_BTN_NAME, ()=>{

                Debug.Log("into save");

                

            });

            // quit/remove panel
            guiMgr.OnClick(PANEL_NAME, QUIT_BTN_NAME, ()=>{

                Debug.Log("into remove");
                menuIsActive = true;
                menuGo.SetActive(menuIsActive);
                guiMgr.RemovePanel(PANEL_NAME);


            });
                    
        }

        private void OnClickOpenPool(){
            SetMenuGo(false);
            var go = guiMgr.AddPanel(FIRE_PANEL_NAME, ELayer.Top);

            guiMgr.OnClick(FIRE_PANEL_NAME, FIRE_BTN_NAME, ()=>{
                
            });

            guiMgr.OnClick(FIRE_PANEL_NAME, QUIT_BTN_NAME, ()=>{
                SetMenuGo(true);
                Debug.Log("into remove");
                guiMgr.RemovePanel(FIRE_PANEL_NAME);
            });

        }

        void SetMenuGo(bool active){
            menuIsActive = active;
            menuGo.SetActive(menuIsActive);
        }
    }
}