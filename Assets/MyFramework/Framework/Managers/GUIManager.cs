using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MyFramework{
    public enum ELayer{
        Top = 1,
        Middle = 2,
        Bottom = 3
    }

    public class GUIManager : SingletonBase<GUIManager>{
        
        private GameObject _canvas;
        public GameObject canvas{
            get{
                if (_canvas == null){
                    var h = GameObject.Find("UIRoot");
                    if (h != null){
                        _canvas = h.transform.Find("Canvas").gameObject;
                    }
                    else{
                        _canvas = GameObject.Instantiate(Resources.Load<GameObject>("UIRoot")).transform.Find("Canvas").gameObject;
                    }
                    Init();
                }
                return _canvas;
            }
            private set{}
        }

        private GameObject popUpPrefab;
        Transform top, middle, bottom;
        private Dictionary<string, GameObject> panelDict;
        private GameObject popUpGO;

        private void Init(){
            top = canvas.transform.Find("Top");
            middle = canvas.transform.Find("Middle");
            bottom = canvas.transform.Find("Bottom");
            panelDict  = new Dictionary<string, GameObject>();
            popUpPrefab = Resources.Load<GameObject>("PopUp");
        }


        public void Set(Vector2 resolution , float match_w_or_h){
            var canvas_scaler = canvas.GetComponent<CanvasScaler>();
            canvas_scaler.matchWidthOrHeight = match_w_or_h;
            canvas_scaler.referenceResolution = resolution;
        }

        public GameObject AddPanel(string name, ELayer layer){
            
            if (_canvas == null) canvas.SetActive(true);
            var go = GameObject.Instantiate(Resources.Load<GameObject>(name));
            if (go == null) return null;
            go.name = name;
            panelDict.Add(name, go);

            switch(layer){
                case ELayer.Top:
                    go.transform.SetParent(top);
                    break;
                case ELayer.Middle:
                    go.transform.SetParent(middle);
                    break;
                case ELayer.Bottom:
                    go.transform.SetParent(bottom);
                    break;
            }
            
            var rect = go.transform as RectTransform;

            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;
            rect.anchoredPosition3D = Vector3.zero;
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.localScale = Vector3.one;

            return go;
        }

        public PopUpBox AddPopUp(string message, UnityEngine.Events.UnityAction onConfirmBtn, UnityEngine.Events.UnityAction onCancelBtn){
            if (_canvas == null) canvas.SetActive(true);
            if (popUpGO != null){
                Debug.LogWarning("There are already on pop up box in scene!");
                return popUpGO.GetComponent<PopUpBox>();
            }
            var go = GameObject.Instantiate(popUpPrefab);

            if (go == null) return null;

            go.transform.SetParent(top);

            var popUpbox = go.GetComponent<PopUpBox>();

            OnClick(popUpbox.confirmBtn, onConfirmBtn);
            OnClick(popUpbox.cancelBtn, onCancelBtn);
            OnClick(popUpbox.XBtn, RemovePopUp);
            OnClick(popUpbox.cancelBtn, RemovePopUp);
            OnClick(popUpbox.confirmBtn, RemovePopUp);

            popUpbox.text.text = message;

            var rect = go.transform as RectTransform;

            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;
            rect.anchoredPosition3D = Vector3.zero;
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.localScale = Vector3.one;
            popUpGO = go;
            return popUpbox;
        }

        private void RemovePopUp(){
            GameObject.Destroy(popUpGO);
            popUpGO = null;
        }

        public GameObject RemovePanel(string name, Callback afterRemove){
            if (!panelDict.ContainsKey(name)) return null;
            var temp = panelDict[name];
            panelDict.Remove(name);
            GameObject.Destroy(temp);
            afterRemove();
            return temp;
        }

        public GameObject RemovePanel(string name){
            if (!panelDict.ContainsKey(name)) return null;
            var temp = panelDict[name];
            panelDict.Remove(name);
            GameObject.Destroy(temp);
            return temp;
        }

        public Dictionary<string, GameObject> RemoveAllPanel(){
            var temp = panelDict;
            panelDict = new Dictionary<string, GameObject>();
            return temp;
        }

        public GameObject GetPanel(string name){
            if (!panelDict.ContainsKey(name)) return null;
            return panelDict[name];
        }

        public void OnClick(Button btn, UnityEngine.Events.UnityAction action){
            btn.onClick.AddListener(action);
        }

        public bool OnClick(string panelName, string btnName, UnityEngine.Events.UnityAction action){
            if (!panelDict.ContainsKey(panelName)) return false;
            if (panelName == btnName) return false;
            Button btn = panelDict[panelName].transform.Find(btnName).GetComponent<Button>();

            if (btn == null){
                btn = panelDict[panelName].GetComponent<Button>();
                if (btn == null) return false;
            }

            btn.onClick.AddListener(action);
            return true;
        }

    }

}