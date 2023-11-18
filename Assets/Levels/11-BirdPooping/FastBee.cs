using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class FastBee : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float betweenPoops;
    [SerializeField] private GameObject poop;
    [SerializeField] private int[] roundsBeforePoop;
    [SpineAnimation] public string movementAnime;
    [SerializeField] private Transform spawnPoint;

    private SkeletonAnimation bee;
    private GameObject tmp;
    private float firstX1;
    private float firstX2;
    private int counter = 0;
    private int rnd;
    // Start is called before the first frame update
    void Start()
    {
        rnd = RandomNumber.IntRandomNumber(1, 0, roundsBeforePoop.Length - 1, false)[0];
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

        bee = GetComponent<SkeletonAnimation>();
        bee.AnimationName = movementAnime;
        if (transform.position.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < firstX1)
        {
            //turn
            transform.localScale = new Vector3(-1 * transform.localScale.x, 1, 1);
            transform.position = new Vector3(firstX1, transform.position.y, 0);
            counter += 1;
            if(counter == roundsBeforePoop[rnd])
            {
                rnd = RandomNumber.IntRandomNumber(1, 0, roundsBeforePoop.Length - 1, false)[0];
                StartCoroutine(PoopCreator());
                counter = 0;
            }
        }
        else if(transform.position.x > firstX2)
        {
            transform.localScale = new Vector3(-1 * transform.localScale.x, 1, 1);
            transform.position = new Vector3(firstX2, transform.position.y, 0);
            counter += 1;
            if (counter == roundsBeforePoop[rnd])
            {
                rnd = RandomNumber.IntRandomNumber(1, 0, roundsBeforePoop.Length - 1, false)[0];
                StartCoroutine(PoopCreator());
                counter = 0;
            }
        }
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

    IEnumerator PoopCreator()
    {
        yield return new WaitForSeconds(betweenPoops);
        //tmp = Instantiate(poop, transform.position, poop.transform.rotation);
        if (transform.localScale.x < 0)
        {
            tmp = Instantiate(poop, new Vector3(transform.position.x - 0.5f, transform.position.y - 1f, 0), transform.rotation);
        }
        else
        {
            tmp = Instantiate(poop, new Vector3(transform.position.x + 0.5f, transform.position.y - 1f, 0), transform.rotation);
        }
        //tmp.transform.localPosition = spawnPoint.position;
        tmp.name = "poop";
        tmp.SetActive(true);
        StartCoroutine(PoopCreator());
    }
}
