using System.Collections;
using UnityEngine;

namespace MyFramework
{
    public class MessageExample : MonoBehaviourSimplify
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("MyFramework/Example/11.Message example", false, 11)]
        private static void MenuClicked()
        {
            MsgCenter.RemoveAllEvent("Do");
            MsgCenter.RemoveAllEvent("Do2");
            UnityEditor.EditorApplication.isPlaying = true;

            new GameObject("MsgReceiverObj")
                .AddComponent<MessageExample>();
        }
#endif
        void DoSomething(object data)
        {
            
            Debug.LogFormat("Received Do msg:{0}", data);
        }

        void DoSomething2(object data)
        {
            Debug.LogFormat("Received Do2 msg: {0}", (bool)data);
        }

        private class Example{
            public Example(string message, int number){
                this.message = message;
                this.number = number;
            }
            string message;
            int number;
            public void Log(){
                Debug.LogFormat("message is : {0}, with number: {1}", message, number);
            }
        }

        void EventWithCustomObject(object data){
            Example test = data as Example;
            test.Log();
        }

        private void Awake() {

            AddEvent("Do", DoSomething);
            AddEvent("Do2", DoSomething2);
            AddEvent("CustomExample", EventWithCustomObject);
        }

        private IEnumerator Start()
        {
            BroadcastEvent("Do", "hello");
            BroadcastEvent("Do2", false);
            BroadcastEvent("CustomExample", new Example("custom class", 1));

            yield return new WaitForSeconds(1.0f);

            BroadcastEvent("Do", "hello1");
            BroadcastEvent("Do2", true);
            BroadcastEvent("CustomExample", new Example("custom class", 2));
            Destroy(this);
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }

        void OnDestroy()
        {
            BroadcastEvent("Do", "event removing...");
            RemoveAllLocalEvents();
            Debug.Log("events removed");
            BroadcastEvent("Do", "haha");
            BroadcastEvent("CustomExample", new Example("custom class", 2));
        }
    }
}