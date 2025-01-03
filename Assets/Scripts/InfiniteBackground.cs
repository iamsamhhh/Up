using UnityEngine;
using MyFramework;
using System.Collections.Generic;

public class InfiniteBackground : MonoBehaviourSimplify
{
    public List<Transform> backgrounds;
    private int[] backgroundIndex = { 0, 1, 2,};
    public float backgroundWidth;

    [SerializeField]
    Transform playerTrans;

    private void Update() {
        if (playerTrans.position.x > backgrounds[backgroundIndex[1]].position.x + backgroundWidth/2){
            TransformSimplify.SetPosX(backgrounds[backgroundIndex[0]], backgrounds[backgroundIndex[2]].position.x + backgroundWidth);
            var temp = backgroundIndex[0];
            backgroundIndex[0] = backgroundIndex[1];
            backgroundIndex[1] = backgroundIndex[2];
            backgroundIndex[2] = temp;
        }
        else if (playerTrans.position.x < backgrounds[backgroundIndex[1]].position.x - backgroundWidth/2){
            TransformSimplify.SetPosX(backgrounds[backgroundIndex[2]], backgrounds[backgroundIndex[0]].position.x - backgroundWidth);
            var temp = backgroundIndex[2];
            backgroundIndex[2] = backgroundIndex[1];
            backgroundIndex[1] = backgroundIndex[0];
            backgroundIndex[0] = temp;
        }
    }
}
