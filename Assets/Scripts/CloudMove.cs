using UnityEngine;
using MyFramework;

public class CloudMove : MonoBehaviourSimplify
{
    private float speed;
    void OnStart(){
        speed = Random.Range(-5f, 5f);
    }

    void OnUpdate(){
        TransformSimplify.SetPosX(transform, transform.position.x + speed*Time.deltaTime);
    }

    private void Awake() {
        OnStart(OnStart);
        OnUpdate(OnUpdate);
    }

    private void OnDestroy() {
        RemoveAllLoacalUpdates();
    }
}
