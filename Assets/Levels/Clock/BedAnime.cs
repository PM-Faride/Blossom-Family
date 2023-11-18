using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;
public class BedAnime : MonoBehaviour
{
    [SpineAnimation] public string asleepAnime;
    [SpineAnimation] public string awakeAnime;

    private SkeletonAnimation bed;

    private void Awake()
    {
        bed = GetComponent<SkeletonAnimation>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //bed = GetComponent<SkeletonAnimation>();
        bed.AnimationName = asleepAnime;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetAsleepAnime()
    {
        bed.AnimationName = asleepAnime;
    }

    public void SetAwakeAnime()
    {
        bed.AnimationName = awakeAnime;
    }
}
