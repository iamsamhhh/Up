using UnityEngine;

public class BGMManager : MonoBehaviour
{
    [SerializeField]
    AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){
        DontDestroyOnLoad(this);
    }
    public void Play()
    {
        Debug.Log("On bgm start");
        
        audioSource.Play();
    }
}
