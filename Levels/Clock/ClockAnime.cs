using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;
public class ClockAnime : MonoBehaviour
{
    [SpineAnimation] public string idleAnime;
    [SpineAnimation] public string rangAnime;

    private SkeletonAnimation bed;

    private void Awake()
    {
        bed = GetComponent<SkeletonAnimation>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //bed = GetComponent<SkeletonAnimation>();
        bed.AnimationName = idleAnime;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetIdleAnime()
    {
        bed.AnimationName = idleAnime;
    }

    public void SetRangAnime()
    {
        bed.AnimationName = rangAnime;
    }
}
