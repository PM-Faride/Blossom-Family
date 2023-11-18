using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
//using TMPro;
public class Initialization : MonoBehaviour
{
    [SerializeField] private Transform[] leftBeeSpawnPoints;
    [SerializeField] private Transform[] rightBeeSpawnPoints;
    [SerializeField] private int firstTimeHowManyBees;
    [SerializeField] private GameObject simpleBee;
    [SerializeField] private GameObject fastBee;
    [SerializeField] private int howManyFastBeess;
    [SerializeField] private float[] whenToAddFastBee;
    [SerializeField] private float[] simpleBeeCreationTime;
    [SerializeField] private UnityEvent Timer;
    //[SerializeField] private TextMeshProUGUI winner;

    private int rnd;
    private int fastBeeRnd;
    private int beeWhichSide = -100;
    private int fastBeeSide = -100; //chap 0 rast 1
    private GameObject beeTmp;
    private GameObject fastBeeTmp;
    private int fastBeeCounter = 0;
    // Start is called before the first frame update
    void Start()
    {
        fastBeeRnd = RandomNumber.IntRandomNumber(1, 0, whenToAddFastBee.Length - 1, false)[0];

        beeWhichSide = RandomNumber.IntRandomNumber(1, 0, 1, false)[0];
        for (int i = 0; i < firstTimeHowManyBees; i++)
        {
            if(beeWhichSide == 0)
            {
                rnd = RandomNumber.IntRandomNumber(1, 0, leftBeeSpawnPoints.Length - 1, false)[0];
                beeTmp = Instantiate(simpleBee, leftBeeSpawnPoints[rnd].position, simpleBee.transform.rotation);
                //beeTmp.name = "bee";
                beeTmp.SetActive(true);
                beeWhichSide = 1;
            }
            else
            {
                rnd = RandomNumber.IntRandomNumber(1, 0, rightBeeSpawnPoints.Length - 1, false)[0];
                beeTmp = Instantiate(simpleBee, rightBeeSpawnPoints[rnd].position, simpleBee.transform.rotation);
                beeTmp.transform.localScale = new Vector3(-1, 1, 1);

                //beeTmp.name = "bee";
                beeTmp.SetActive(true);
                beeWhichSide = 0;
            }
        }
        Timer.Invoke();
        StartCoroutine(SimpleBeeCreation());
        StartCoroutine(FastBeeCreation());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StopGame()
    {
        //if()
        StopAllCoroutines();
        Time.timeScale = 0;
    }

    IEnumerator FastBeeCreation()
    {
        fastBeeCounter += 1;
        yield return new WaitForSeconds(whenToAddFastBee[fastBeeRnd]);
        if(fastBeeSide == -100)
        {
            fastBeeSide = RandomNumber.IntRandomNumber(1, 0, 1, false)[0];
        }
        else
        {
            if(fastBeeSide == 0)
            {
                fastBeeSide = 1;
                //create on right
                rnd = RandomNumber.IntRandomNumber(1, 0, rightBeeSpawnPoints.Length - 1, false)[0];
                fastBeeTmp = Instantiate(fastBee, rightBeeSpawnPoints[rnd].position, fastBee.transform.rotation);
                //fastBeeTmp.name = "fastBee";
                fastBeeTmp.transform.localScale = new Vector3(-1, 1, 1);
                fastBeeTmp.SetActive(true);
            }
            else
            {
                fastBeeSide = 0;
                //create on left
                rnd = RandomNumber.IntRandomNumber(1, 0, leftBeeSpawnPoints.Length - 1, false)[0];
                fastBeeTmp = Instantiate(fastBee, leftBeeSpawnPoints[rnd].position, fastBee.transform.rotation);
                //fastBeeTmp.name = "fastBee";
                //fastBeeTmp.transform.localScale = new Vector3(1, 1, 1);
                fastBeeTmp.SetActive(true);
            }
        }
        if (fastBeeCounter < howManyFastBeess)
        {
            StartCoroutine(FastBeeCreation());
        }
    }

    IEnumerator SimpleBeeCreation()
    {
        rnd = RandomNumber.IntRandomNumber(1, 0, simpleBeeCreationTime.Length - 1, false)[0];
        yield return new WaitForSeconds(simpleBeeCreationTime[rnd]);
        if (beeWhichSide == -100)
        {
            beeWhichSide = RandomNumber.IntRandomNumber(1, 0, 1, false)[0];
        }
        else if(beeWhichSide == 0)
            {
            beeWhichSide = 1;
            //create on right
            rnd = RandomNumber.IntRandomNumber(1, 0, rightBeeSpawnPoints.Length - 1, false)[0];
            beeTmp = Instantiate(simpleBee, rightBeeSpawnPoints[rnd].position, simpleBee.transform.rotation);
            //beeTmp.name = "fastBee";
            beeTmp.transform.localScale = new Vector3(-1, 1, 1);
            beeTmp.SetActive(true);
        }
        else
        {
            beeWhichSide = 0;
            //create on left
            rnd = RandomNumber.IntRandomNumber(1, 0, leftBeeSpawnPoints.Length - 1, false)[0];
            beeTmp = Instantiate(simpleBee, leftBeeSpawnPoints[rnd].position, simpleBee.transform.rotation);
            //beeTmp.name = "fastBee";
            //fastBeeTmp.transform.localScale = new Vector3(1, 1, 1);
            beeTmp.SetActive(true);
        }
        //{
        //    if (beeWhichSide == 0)
        //    {
        //        beeWhichSide = 1;
        //        //create on right
        //        rnd = RandomNumber.IntRandomNumber(1, 0, rightBeeSpawnPoints.Length - 1, false)[0];
        //        beeTmp = Instantiate(simpleBee, rightBeeSpawnPoints[rnd].position, simpleBee.transform.rotation);
        //        //beeTmp.name = "fastBee";
        //        beeTmp.transform.localScale = new Vector3(-1, 1, 1);
        //        beeTmp.SetActive(true);
        //    }
        //    else
        //    {
        //        beeWhichSide = 0;
        //        //create on left
        //        rnd = RandomNumber.IntRandomNumber(1, 0, leftBeeSpawnPoints.Length - 1, false)[0];
        //        beeTmp = Instantiate(simpleBee, leftBeeSpawnPoints[rnd].position, simpleBee.transform.rotation);
        //        //beeTmp.name = "fastBee";
        //        //fastBeeTmp.transform.localScale = new Vector3(1, 1, 1);
        //        beeTmp.SetActive(true);
        //    }
        //}
        StartCoroutine(SimpleBeeCreation());
    }
}
