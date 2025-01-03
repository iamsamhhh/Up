using UnityEngine;
using MyFramework;

public class CloudMove : MonoBehaviourSimplify
{
    private float speed;
    void Start(){
        speed = Random.Range(-5f, 5f);
    }

    void Update(){
        TransformSimplify.SetPosX(transform, transform.position.x + speed*Time.deltaTime);
    }
}
