using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class PoopCode : MonoBehaviour
{
    [SpineAnimation] public string poopDestructionAnime;
    [SpineAnimation] public string poopIdleAnime;

    [SerializeField] private float speed;
    [SerializeField] private Transform floorY;

    private SkeletonAnimation poop;
    private bool destroyed = false;
    // Start is called before the first frame update
    void Start()
    {
        poop = GetComponent<SkeletonAnimation>();
        poop.AnimationName = poopIdleAnime;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        transform.position -= new Vector3(0, speed * Time.fixedDeltaTime, 0);
        if(transform.position.y < floorY.position.y && !destroyed)
        {
            destroyed = true;
            StartCoroutine(PoopDestruction());
            //Destroy(gameObject);
        }
    }

    IEnumerator PoopDestruction()
    {
        speed = 0;
        var anime = poop.state.SetAnimation(0, poopDestructionAnime, false);
        yield return new WaitForSpineAnimationComplete(anime);
        Destroy(gameObject);
    }

    public void HitHuman()
    {
        StartCoroutine(PoopDestruction());
    }
}
