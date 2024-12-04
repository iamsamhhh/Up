using UnityEngine;
using MyFramework;
using Unity.Mathematics;

public class SkinMove : MonoBehaviourSimplify
{
    // UserData userData {
    //     get {return UserData.userData;}
    // }
    float time;
    [SerializeField]
    float speed, range;

    void OnChangeSkin(object _skin){
        // Debug.Log("On change skin");
        Skin skin = (Skin)_skin;
        if (skin.haveTrail){
            var trailRenderer = GetComponent<TrailRenderer>();
            trailRenderer.enabled = true;
            trailRenderer.startColor = skin.trailColor;
            trailRenderer.endColor = new Color(skin.trailColor.r, skin.trailColor.g, skin.trailColor.b, 0f);
        }
        else{
            GetComponent<TrailRenderer>().enabled = false;
        }
        
        var renderer = GetComponent<SpriteRenderer>();
        renderer.sprite = skin.sprite;
        renderer.color = skin.spriteColor;
    }

    private void Awake() {
        AddEvent("OnChangeSkin", OnChangeSkin);
        OnChangeSkin(UserData.userData.currentSkin);
    }

    private void OnDestroy() {
        RemoveAllLocalEvents();
    }

    void Update(){
        time += Time.deltaTime;
        TransformSimplify.SetPosX(transform, math.sin(time*speed)*range);
    }
}
