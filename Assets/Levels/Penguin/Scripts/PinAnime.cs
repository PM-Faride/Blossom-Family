using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Spine;
using Spine.Unity;

public class PinAnime : MonoBehaviour
{
    [SpineAnimation] public string idleAnime;
    [SpineAnimation] public string walkAnime;

    private SkeletonAnimation pin;
    // Start is called before the first frame update
    void Start()
    {
        pin = GetComponent<SkeletonAnimation>();
        pin.AnimationName= idleAnime;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //public void IdleAnime()
    //{
    //    pin.AnimationName= idleAnime;
    //}

    public void WalkAnime()
    {
        pin.AnimationName = walkAnime;
    }
}
