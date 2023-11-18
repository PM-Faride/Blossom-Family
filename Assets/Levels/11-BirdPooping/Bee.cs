using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class Bee : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float speedIncrease;
    [SerializeField] private float animeIncrease;
    [SerializeField] private float[] poopTimer;
    [SerializeField] private GameObject poop;
    [SerializeField] private Transform spawnPoint;
    [SpineAnimation] public string movementAnime;

    private float firstX1;
    private float firstX2;
    private SkeletonAnimation bee;
    private int rnd;
    private GameObject tmp;
    // Start is called before the first frame update
    void Start()
    {
        bee = GetComponent<SkeletonAnimation>();
        bee.AnimationName = movementAnime;
        bee.timeScale = 0.5f; 
        if (transform.position.x < 0)
        {
            firstX1 = transform.position.x;
            firstX2 = -1 * transform.position.x;
        }
        else
        {
            firstX1 = -1 * transform.position.x;
            firstX2 = transform.position.x;
        }

        if(transform.position.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        StartCoroutine(Poop());
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < firstX1)
        {
            //turn
            transform.localScale = new Vector3(-1 * transform.localScale.x, 1, 1);
            transform.position = new Vector3(firstX1, transform.position.y, 0);
            speed += speedIncrease;
            bee.timeScale += animeIncrease;
        }
        else if (transform.position.x > firstX2)
        {
            transform.localScale = new Vector3(-1 * transform.localScale.x, 1, 1);
            transform.position = new Vector3(firstX2, transform.position.y, 0);
            speed += speedIncrease;
            bee.timeScale += animeIncrease;
        }
        //if (transform.position.x < firstX1 || transform.position.x > firstX2)
        //{
        //    //turn
        //    transform.localScale = new Vector3(-1 * transform.localScale.x, 1, 1);
        //    //speed increasment
        //    speed += speedIncrease;
        //}
    }

    private void FixedUpdate()
    {
        if (transform.localScale.x < 0)
        {
            //going left
            transform.position -= new Vector3(speed * Time.fixedDeltaTime, 0, 0);
        }
        if (transform.localScale.x > 0)
        {
            //going right
            transform.position += new Vector3(speed * Time.fixedDeltaTime, 0, 0);
        }
    }

    IEnumerator Poop()
    {
        rnd = RandomNumber.IntRandomNumber(1, 0, poopTimer.Length - 1, false)[0];
        yield return new WaitForSeconds(poopTimer[rnd]);
        if(transform.localScale.x < 0)
        {
            tmp = Instantiate(poop, new Vector3(transform.position.x - 0.5f, transform.position.y - 1f, 0), transform.rotation);
        }
        else
        {
            tmp = Instantiate(poop, new Vector3(transform.position.x + 0.5f, transform.position.y - 1f, 0), transform.rotation);
        }
        //tmp.transform.localPosition = spawnPoint.position;
        tmp.name = "Poop";
        tmp.SetActive(true);
        StartCoroutine(Poop());
    }
}
