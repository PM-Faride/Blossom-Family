using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class PlayAnime : MonoBehaviour
{
    [SpineAnimation] public string normalAnime;
    [SpineAnimation] public string angryAnime;

    private SkeletonAnimation face;
    // Start is called before the first frame update
    void Start()
    {
        face = GetComponent<SkeletonAnimation>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NormalAnime()
    {
        face.AnimationName = normalAnime;
    }
    public void AngryAnime()
    {
        face.AnimationName = angryAnime;
    }
}
