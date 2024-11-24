using UnityEngine;

public class Follow : MonoBehaviour
{
    [SerializeField]
    float followSlow;
    [SerializeField]
    Transform followTrans;
    [SerializeField]
    Vector3 offset;
    private void LateUpdate() {
        transform.position = followTrans.position/followSlow + offset;
    }
}
