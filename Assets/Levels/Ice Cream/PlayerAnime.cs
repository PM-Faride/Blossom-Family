using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class PlayerAnime : MonoBehaviour
{
    [SpineAnimation] public string idleAnime;
    [SpineAnimation] public string loseAnime;
    [SpineAnimation] public string winAnime;

    private SkeletonAnimation player;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<SkeletonAnimation>();
        player.AnimationName = idleAnime;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IdleAnime()
    {
        player.AnimationName = idleAnime;
    }

    public void LoseAnime()
    {
        player.AnimationName = loseAnime;
    }

    public void WinAnime()
    {
        player.AnimationName = winAnime;
    }
}
