using UnityEngine;

namespace MyFramework{
    [CreateAssetMenu(fileName = "ExampleData", menuName = "MyFramework/ExampleData")]
    public class ExampleData : ScriptableObject
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("MyFramework/Example/13.Save manager example", false, 13)]
        private static void MenuClicked(){
            ExampleData exampleData = Resources.Load<ExampleData>("ExampleData");
            exampleData.randomInt = 0;
            SaveManager.LoadObject("ExampleData", exampleData);
            exampleData.Log();
            exampleData.randomInt = Random.Range(1, 51);
            exampleData.Log();
            SaveManager.SaveObject(exampleData, "ExampleData");
        }
#endif
        public string description;
        public int randomInt;
        public void Log(){
            Debug.LogFormat("description: {0}, random int: {1}", description, randomInt);
        }
    }
}