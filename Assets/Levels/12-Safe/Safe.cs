using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class Safe : MonoBehaviour
{
    [SerializeField] private GameObject thisLevel;
    [SerializeField] private string whichSafe;

    [SpineAnimation] public string idleAnime;
    [SpineAnimation] public string loseAnime;
    [SpineAnimation] public string winAnime;

    private SkeletonAnimation safe;
    // Start is called before the first frame update
    void Start()
    {
        safe = GetComponent<SkeletonAnimation>();
        safe.AnimationName = idleAnime;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PublicLoseAnime()
    {
        StartCoroutine(LoseAnime());
    }

    IEnumerator LoseAnime()
    {
        var anime = safe.state.SetAnimation(0, loseAnime, false);
        yield return new WaitForSpineAnimationComplete(anime);
        safe.AnimationName = idleAnime;
        thisLevel.GetComponent<SafeLevel>().TheLevel(whichSafe);
    }

    public void PublicWinAnime()
    {
        StartCoroutine(WinAnime());
    }

    IEnumerator WinAnime()
    {
        var anime = safe.state.SetAnimation(0, winAnime, false);
        yield return new WaitForSpineAnimationComplete(anime);
        safe.AnimationName = idleAnime;
        thisLevel.GetComponent<SafeLevel>().TheLevel(whichSafe);
    }
}
