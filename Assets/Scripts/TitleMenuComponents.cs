using UnityEngine;
using UnityEngine.UI;

public class TitleMenuComponents : MonoBehaviour
{
    [SerializeField]
    Button startBtn, exitBtn;

    public Button GetStartBtn(){return startBtn;}
    public Button GetExitBtn(){return exitBtn;}
}
