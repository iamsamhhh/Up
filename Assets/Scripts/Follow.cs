using UnityEngine;

public class Follow : MonoBehaviour
{
    [SerializeField]
    Transform follow;
    [SerializeField]
    float slow;
    [SerializeField]
    Vector3 offset;
    private void FixedUpdate() {
        transform.position = (follow.position / slow) + offset;
    }
}
