using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class SkyColorChange : MonoBehaviour
{
    [SerializeField]
    Gradient color;
    Camera cam;

    [SerializeField]
    Vector2 colorChangeHeight;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        var height = Mathf.Clamp(transform.position.y, colorChangeHeight.x, colorChangeHeight.y);
        
        cam.backgroundColor = color.Evaluate((height-colorChangeHeight.x)/(colorChangeHeight.y-colorChangeHeight.x));
    }
}
