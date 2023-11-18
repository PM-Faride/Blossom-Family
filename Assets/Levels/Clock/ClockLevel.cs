using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;
using UnityEngine.Events;
using TMPro;
public class ClockLevel : MonoBehaviour
{
    //[SpineAnimation] public string asleepAnime;
    //[SpineAnimation] public string awakeAnime;
    //[SpineAnimation] public string clockIdleAnime;
    //[SpineAnimation] public string ringingClockAnime;
    [SerializeField] private AudioClip ring;

    [SerializeField] private Vector2[] timerNumber;
    [SerializeField] private Vector2[] timeScore;
    [SerializeField] private float timeToFreeze;
    //[SerializeField] private float readyTime;
    //[SerializeField] private float goTime;
    [SerializeField] private int minBothNo;
    [SerializeField] private int maxBothNo;
    [SerializeField] private GameObject leftBedGO;
    [SerializeField] private GameObject rightBedGO;
    [SerializeField] private GameObject leftClockGO;
    [SerializeField] private GameObject rightClockGO;
    [SerializeField] private GameObject ready;
    [SerializeField] private GameObject go;
    //[SerializeField] private GameObject leftBtn;
    //[SerializeField] private GameObject leftPressedBtn;
    //[SerializeField] private GameObject rightBtn;
    //[SerializeField] private GameObject rightPressedBtn;
    [SerializeField] private TextMeshProUGUI leftScoreTxt;
    [SerializeField] private TextMeshProUGUI rightScoreTxt;
    [SerializeField] private TextMeshProUGUI winner;
    [SerializeField] private UnityEvent LeftTimerReset;
    [SerializeField] private UnityEvent RightTimerReset;
    [SerializeField] private UnityEvent RightTimerStop;
    [SerializeField] private UnityEvent LeftTimerStop;
    [SerializeField] private UnityEvent LeftTimerZero;
    [SerializeField] private UnityEvent RightTimerZero;
    [SerializeField] private UnityEvent leftBedAsleepAnime;
    [SerializeField] private UnityEvent leftBedAwakeAnime;
    [SerializeField] private UnityEvent rightBedAsleepAnime;
    [SerializeField] private UnityEvent rightBedAwakeAnime;
    [SerializeField] private UnityEvent leftClockIdleAnime;
    [SerializeField] private UnityEvent leftClockRangAnime;
    [SerializeField] private UnityEvent rightClockIdleAnime;
    [SerializeField] private UnityEvent rightClockRangAnime;

    //private SkeletonAnimation rightClock;
    //private SkeletonAnimation leftClock;
    //private SkeletonAnimation leftBed;
    //private SkeletonAnimation rightBed;

    private int counter = 0;
    private int setCounter = 0;
    private float leftTimer = 0;
    private float rightTimer = 0;
    //private float timer = 0;
    private bool leftTimerStarted = false;
    private bool rightTimerStarted = false;
    private int rnd;
    private bool rightClockRang = false;
    private bool leftClockRang = false;
    private bool p1Pressed = false;
    private bool p2Pressed = false;
    private float leftScore = 0;
    private float rightScore = 0;
    private int howMany = 0;
    private int howManyCouple;
    //private int howMa
    private int[] whereToRing;
    private int totalCounter = 0;
    private AudioSource audioSrc;

    //IEnumerator Ready()
    //{
    //    ready.SetActive(true);
    //    yield return new WaitForSeconds(readyTime);
    //    ready.SetActive(false);
    //    go.SetActive(true);
    //    yield return new WaitForSeconds(goTime);
    //    go.SetActive(false);
    //    StartCoroutine(Clock());
    //}

    // Start is called before the first frame update
    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        audioSrc.clip = ring;
        for (int i = 0; i < timerNumber.Length; i++)
        {
            howMany += (int)timerNumber[i].y;
        }

        whereToRing = new int[howMany];

        howManyCouple = RandomNumber.IntRandomNumber(1, minBothNo, maxBothNo, false)[0];
        if(howManyCouple % 2 == 1)
        {
            howManyCouple += 1;
        }

        rnd = (howMany - howManyCouple) / 2;
        rnd += howManyCouple;

        for (int i = 0; i < howMany; i++)
        {
            if (i < howManyCouple) whereToRing[i] = 2;
            else if (i < rnd) whereToRing[i] = 1;
            else whereToRing[i] = 0;
        }

        RandomNumber.MakhlootArray(whereToRing);
        //leftBed = leftBedGO.GetComponent<SkeletonAnimation>();
        //rightBed = rightBedGO.GetComponent<SkeletonAnimation>();
        //leftClock = leftClockGO.GetComponent<SkeletonAnimation>();
        //rightClock = rightClockGO.GetComponent<SkeletonAnimation>();

        leftBedAsleepAnime.Invoke();
        rightBedAsleepAnime.Invoke();
        leftClockIdleAnime.Invoke();
        rightClockIdleAnime.Invoke();
        //leftBed.AnimationName = asleepAnime;
        //rightBed.AnimationName = asleepAnime;
        //leftClock.AnimationName = clockIdleAnime;
        //rightClock.AnimationName = clockIdleAnime;
        //StartCoroutine(Ready());
        StartCoroutine(Clock());

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (leftTimerStarted)
        {
            leftTimer += Time.fixedDeltaTime;
            //timer += Time.fixedDeltaTime;
        }
        if (rightTimerStarted)
        {

            rightTimer += Time.fixedDeltaTime;
        }
    }

    IEnumerator Clock()
    {
        audioSrc.Pause();

        if (totalCounter >= howMany)
        {
            //finish
            Time.timeScale = 0;
            if (leftScore > rightScore)
            {
                winner.text = "Left Won";
            }
            else
            {
                winner.text = "Right Won";
            }
        }
        if (counter < timerNumber[setCounter].y)
        {
            leftClockRang = false;
            rightClockRang = false;
            p1Pressed = false;
            p2Pressed = false;
            leftBedAsleepAnime.Invoke();
            rightBedAsleepAnime.Invoke();
            leftClockIdleAnime.Invoke();
            rightClockIdleAnime.Invoke();
            yield return new WaitForSeconds(timerNumber[setCounter].x);
            audioSrc.Play();
            //rnd = RandomNumber.IntRandomNumber(1, 0, 1, false)[0];
            if(whereToRing[totalCounter] == 2)
            {
                //2tayi
                //leftBedAsleepAnime.Invoke();
                //rightBedAsleepAnime.Invoke();
                leftTimerStarted = true;
                rightTimerStarted = true;
                leftClockRangAnime.Invoke();
                rightClockRangAnime.Invoke();
                //leftClock.AnimationName = ringingClockAnime;
                //rightClock.AnimationName = ringingClockAnime;
                leftClockRang = true;
                rightClockRang = true;
                //startTimer = true;
                LeftTimerReset.Invoke();
                RightTimerReset.Invoke();
            }
            else if(whereToRing[totalCounter] == 1)
            {
                //rast
                //rightClock.AnimationName = ringingClockAnime;
                rightClockRangAnime.Invoke();
                rightTimerStarted = true;
                //leftClockRang = true;
                rightClockRang = true;
                //startTimer = true;
                //LeftTimerReset.Invoke();
                RightTimerReset.Invoke();
            }
            else
            {
                //chap
                leftClockRangAnime.Invoke();
                leftTimerStarted = true;
                //leftClock.AnimationName = ringingClockAnime;
                //rightClock.AnimationName = ringingClockAnime;
                leftClockRang = true;
                //rightClockRang = true;
                //startTimer = true;
                LeftTimerReset.Invoke();
                //RightTimerReset.Invoke();
            }
            //startTimer = true;
            //leftTimerStarted = true;
            totalCounter += 1;
        }
        else if(setCounter < timerNumber.Length)
        {
            setCounter += 1;
            counter = 0;
            StartCoroutine(Clock());
        }
        //if(totalCounter >= howMany)
        //{
        //    //finish
        //    Time.timeScale = 0;
        //    if (leftScore > rightScore)
        //    {
        //        winner.text = "Left Won";
        //    }
        //    else
        //    {
        //        winner.text = "Right Won";
        //    }
        //}
    }

    //public void BtnWasPressed(int playerIndex)
    //{
    //    //float currentTime = timer;
    //    if(playerIndex == 0 && !p1Pressed)
    //    {
    //        leftBedAwakeAnime.Invoke();
    //        //leftBtn.SetActive(false);
    //        //leftPressedBtn.SetActive(true);
    //        p1Pressed = true;
    //        LeftTimerStop.Invoke();
    //        if (leftClockRang)
    //        {
    //            //LeftClockStop.Invoke();
    //            for (int i = 0; i < timeScore.Length; i++)
    //            {
    //                    //Debug.Log(1);
    //                if (leftTimer < timeScore[i].x)
    //                {
    //                    leftScore += timeScore[i].y;
    //                    break;
    //                }
    //            }
    //            leftScoreTxt.text = leftScore.ToString();
    //            if (!rightClockRang)
    //            {
    //                //startTimer = false;
    //                leftTimerStarted = false;
    //                StartCoroutine(Wait());
    //            }
    //            //startTimer = false;

    //            //RightClockStop.Invoke();
    //        }
    //        else
    //        {
    //            leftScore -= 1;
    //            leftScoreTxt.text = leftScore.ToString();
    //        }
            
    //    }
    //    else if(playerIndex == 1 && !p2Pressed)
    //    {
    //        rightBedAwakeAnime.Invoke();
    //        //Debug.Log(1);
    //        //rightBtn.SetActive(false);
    //        //rightPressedBtn.SetActive(true);
    //        p2Pressed = true;
    //        RightTimerStop.Invoke();
    //        if (rightClockRang)
    //        {
    //            //LeftClockStop.Invoke();
    //            for (int i = 0; i < timeScore.Length; i++)
    //            {
    //                if (rightTimer < timeScore[i].x)
    //                {
    //                    //Debug.Log(2);

    //                    rightScore += timeScore[i].y;
    //                    break;
    //                }
    //            }
    //            rightScoreTxt.text = rightScore.ToString();
    //            if (!leftClockRang)
    //            {
    //                //startTimer = false;
    //                rightTimerStarted = false;
    //                StartCoroutine(Wait());
    //            }
    //            //startTimer = false;

    //            //RightClockStop.Invoke();
    //        }
    //        else
    //        {
    //            rightScore -= 1;
    //            rightScoreTxt.text = rightScore.ToString();
    //        }
    //    }
    //    if(p1Pressed && p2Pressed && leftClockRang && rightClockRang)
    //    {
    //        StartCoroutine(Wait());
    //    }
    //}
    public void BtnWasPressed1()
    {
        //float currentTime = timer;
        leftTimerStarted = false;
        if (!p1Pressed)
        {
            //audioSrc.Pause();
            leftBedAwakeAnime.Invoke();
            //leftBtn.SetActive(false);
            //leftPressedBtn.SetActive(true);
            p1Pressed = true;
            LeftTimerStop.Invoke();
            if (leftClockRang)
            {
                //LeftClockStop.Invoke();
                for (int i = 0; i < timeScore.Length; i++)
                {
                    if (leftTimer < timeScore[i].x)
                    {
                        //Debug.Log(1);
                        leftScore += timeScore[i].y;
                        break;
                    }
                }
                leftScoreTxt.text = leftScore.ToString();
                if (!rightClockRang)
                {
                    //startTimer = false;
                    StartCoroutine(Wait());
                }
                //startTimer = false;

                //RightClockStop.Invoke();
            }
            else
            {
                leftScore -= 1;
                leftScoreTxt.text = leftScore.ToString();
            }

        }
        if (p1Pressed && p2Pressed)
        {
            StartCoroutine(Wait());
        }
    }
    public void BtnWasPressed2()
    {
        rightTimerStarted = false;

        if (!p2Pressed)
        {
            //audioSrc.Pause();
            rightBedAwakeAnime.Invoke();
            //Debug.Log(1);
            //rightBtn.SetActive(false);
            //rightPressedBtn.SetActive(true);
            p2Pressed = true;
            RightTimerStop.Invoke();
            if (rightClockRang)
            {
                //LeftClockStop.Invoke();
                for (int i = 0; i < timeScore.Length; i++)
                {
                    if (rightTimer < timeScore[i].x)
                    {
                        //Debug.Log(2);

                        rightScore += timeScore[i].y;
                        break;
                    }
                }
                rightScoreTxt.text = rightScore.ToString();
                if (!leftClockRang)
                {
                    //startTimer = false;
                    StartCoroutine(Wait());
                }
                //startTimer = false;

                //RightClockStop.Invoke();
            }
            else
            {
                rightScore -= 1;
                rightScoreTxt.text = rightScore.ToString();
            }
        }
        if (p1Pressed && p2Pressed)
        {
            StartCoroutine(Wait());
        }
    }
    IEnumerator Wait()
    {
        rightTimer = 0;
        leftTimer = 0;
        yield return new WaitForSeconds(timeToFreeze);
        //rightBtn.SetActive(true);
        //rightPressedBtn.SetActive(false);
        //leftBtn.SetActive(true);
        //leftPressedBtn.SetActive(false);
        LeftTimerZero.Invoke();
        RightTimerZero.Invoke();
        StartCoroutine(Clock());
        counter += 1;
        //totalCounter += 1;
    }
}
