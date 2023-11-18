using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class BatLevel : MonoBehaviour
{
    //public string whichPlayer;
    [SerializeField] private bool minusScore;
    [SerializeField] private Transform hiddenHeadY;
    [SerializeField] private float headSpeed;
    [SerializeField] private float minReactionTime;
    [SerializeField] private int minFactor;
    [SerializeField] private int maxFactor;
    [SerializeField] private int[] counters;
    [SerializeField] private float timeToStayIn;
    //[SerializeField] private float totalOutTime; //in dg lazem nis dg age zaman o .. bedim
    //[SerializeField] private GameObject[] holes; //mishe biyam joda ham bedam left right o... k mafhum bashe
    [SerializeField] private Transform[] headPositions;
    //[SerializeField] private GameObject[] buttons;
    //[SerializeField] private GameObject[] pressedButtons;
    [SerializeField] private int[] changeFaceAfterScore;
    //[SerializeField] private GameObject ready;
    //[SerializeField] private GameObject go;
    [SerializeField] private GameObject bat;
    [SerializeField] private Transform[] batPositions;
    //[SerializeField] private GameObject[] heads;
    [SerializeField] private GameObject head;
    [SerializeField] private GameObject hurtHead;
    //[SerializeField] private GameObject[] bats;
    //[SerializeField] private Transform parent;
    [SerializeField] private float readyTime;
    [SerializeField] private float goTime;
    [SerializeField] private float showHurtTime;
    [SerializeField] private UnityEvent startTimerEvent;
    [SerializeField] private TextMeshProUGUI scoreTxt;

    private int playerAns;
    //private string leftPlayerAnswer;
    //private int rnd;
    private int activeHole;
    private int score;
    private float timer;
    private float[] timers;
    private float[] standardTimes;
    private int tmp = 0;
    private int timerIndex = 0;
    private GameObject[] heads;
    private GameObject activeHead;
    private Vector3 headFinalPos;
    private bool moveHeadUp = false;
    private bool moveHeadDown = false;
    private GameObject tmpHurtHead;
    //private GameObject hurtHead2;
    //private GameObject activeBat;
    private Transform parent;
    private bool started = false;
    private GameObject[] bats;
    // Start is called before the first frame update

    //IEnumerator Ready()
    //{
    //    yield return new WaitForSeconds(readyTime);
    //    ready.SetActive(false);
    //    go.SetActive(true);
    //    yield return new WaitForSeconds(goTime);
    //    go.SetActive(false);
    //    startTimerEvent.Invoke();
    //    StartTheGame();
    //}

    void Start()
    {
        tmpHurtHead = Instantiate(hurtHead);
        parent = gameObject.transform;
        for (int i = 0; i < counters.Length; i++)
        {
            tmp += counters[i];
        }

        timers = new float[tmp];

        standardTimes = new float[maxFactor - minFactor + 1];

        //ready.SetActive(true);

        int index = 0;
        tmp = minFactor;
        while(tmp <= maxFactor)
        {
            standardTimes[index] = tmp * minReactionTime;
            tmp += 1;
            index += 1;
        }
        index = 0;
        for (int i = 0; i < counters.Length; i++)
        {
            for (int j = 0; j < counters[i]; j++)
            {
                timers[index] = standardTimes[i];
                index += 1;
            }
        }

        bats = new GameObject[batPositions.Length];
        heads = new GameObject[headPositions.Length];
        Vector3 headPos;
        for (int i = 0; i < batPositions.Length; i++)
        {
            bats[i] = Instantiate(bat, batPositions[i].position, bat.transform.rotation, parent);
            headPos = new Vector3(headPositions[i].position.x, hiddenHeadY.position.y, 0);
            heads[i] = Instantiate(head, headPos, head.transform.rotation, parent);
            heads[i].SetActive(true);
        }

        RandomNumber.MakhlootArray(timers);
        startTimerEvent.Invoke();
        StartTheGame();
        //StartCoroutine(Ready());
    }

    private void StartTheGame()
    {
        started = true;
        //tmp = 0;
        timer = timers[timerIndex];
        timerIndex++;
        activeHole = RandomNumber.IntRandomNumber(1, 0, 2, false)[0];
        //head = Instantiate(heads[tmp], headPositions[activeHole].position, heads[tmp].transform.rotation, parent);
        //head
        headFinalPos = headPositions[activeHole].position;
        activeHead = heads[activeHole];
        //activeHead.SetActive(true);
        moveHeadUp = true;
        StartCoroutine(HeadOutTimer());
    }

    IEnumerator HeadOutTimer()
    {
        yield return new WaitForSeconds(timer);
        activeHole = -1000;
        moveHeadDown = true;
        //activeHead.SetActive(false);
        //Destroy(head);
        yield return new WaitForSeconds(timeToStayIn);
        CreateHead();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (moveHeadUp)
        {
            if(activeHead.transform.position.y < headFinalPos.y)
            {
                activeHead.transform.position += new Vector3(0, headSpeed * Time.fixedDeltaTime, 0);
            }
            else
            {
                moveHeadUp = false;
            }
        }
        if (moveHeadDown)
        {
            if (activeHead.transform.position.y > hiddenHeadY.position.y)
            {
                activeHead.transform.position -= new Vector3(0, headSpeed * Time.fixedDeltaTime, 0);
            }
            else
            {
                moveHeadDown = false;
            }
        }
    }

    public void LeftBtnPressed()
    {
        if (started)
        {
            //buttons[0].SetActive(false);
            //pressedButtons[0].SetActive(true);

            playerAns = 0;
            //activeBat = bats[playerAns];
            //activeBat = bats[0];
            bats[0].SetActive(true);
            //activeBat.GetComponent<Music>().PlaySound();
            bats[0].GetComponent<Music>().PlaySound(0);
            Score();
        }
    }

    public void BottomBtnPressed()
    {
        if (started)
        { 
            //buttons[1].SetActive(false);
            //pressedButtons[1].SetActive(true);
            playerAns = 1;

            //activeBat = bats[playerAns];
            //activeBat = bats[1];
            //activeBat.SetActive(true);
            bats[1].SetActive(true);
            //activeBat.GetComponent<Music>().PlaySound();
            bats[1].GetComponent<Music>().PlaySound(0);
            Score();
        }
    }

    public void RightBtnPressed()
    {
        if (started)
        {
            //buttons[2].SetActive(false);
            //pressedButtons[2].SetActive(true);
            playerAns = 2;

            //activeBat = bats[playerAns];
            //activeBat.SetActive(true);
            bats[2].SetActive(true);
            //activeBat.GetComponent<Music>().PlaySound();
            bats[2].GetComponent<Music>().PlaySound(0);
            Score();
        }
    }

    public void LeftBtnReleased()
    {
        if (started)
        {
            //activeBat.SetActive(false);
            bats[0].SetActive(false);
            //pressedButtons[0].SetActive(false);
            //buttons[0].SetActive(true);
        }
    }

    public void BottomBtnReleased()
    {
        if (started)
        { 
            //activeBat.SetActive(false);
            bats[1].SetActive(false);
            //pressedButtons[1].SetActive(false);
            //buttons[1].SetActive(true);
        }
    }

    public void RightBtnReleased()
    {
        if (started)
        {
            //activeBat.SetActive(false);
            bats[2].SetActive(false);
            //pressedButtons[2].SetActive(false);
            //buttons[2].SetActive(true);
        }
    }

    //private void Score
    private void CreateHead()
    {
        //if(score > changeFaceAfterScore[tmp] && tmp < changeFaceAfterScore.Length - 1)
        //{
        //    tmp += 1;
        //}
        timer = timers[timerIndex];
        if (timerIndex < timers.Length - 1)
        {
            timerIndex++;
        }

        activeHole = RandomNumber.IntRandomNumber(1, 0, 2, false)[0];
        headFinalPos = headPositions[activeHole].position;
        activeHead = heads[activeHole];
        //activeHead.SetActive(true);
        moveHeadUp = true;
        //head = Instantiate(heads[tmp], headPositions[activeHole].position, heads[tmp].transform.rotation, parent);
        //head = Instantiate(heads[tmp], headPositions[activeHole].position, heads[tmp].transform.rotation, parent);

        //head.SetActive(true);
        StartCoroutine(HeadOutTimer());
    }

    private void Score()
    {
        if (!moveHeadDown)
        {
            if(playerAns == activeHole)
            {
                StartCoroutine(HurtHeadDisplay());
                score += 1;
                scoreTxt.text = score.ToString();
            }
            else if(minusScore)
            {
                score -= 1;
                scoreTxt.text = score.ToString();
            }
        }
    }

    public void StopTheGame()
    {
        activeHole = -1000;
        //Time.timeScale = 0;
        started = false;
        StopAllCoroutines();
    }

    IEnumerator HurtHeadDisplay()
    {
        activeHead.SetActive(false);
        tmpHurtHead.transform.position = activeHead.transform.position;
        tmpHurtHead.SetActive(true);
        yield return new WaitForSeconds(showHurtTime);
        if (activeHead)
        {
            activeHead.SetActive(true);
        }
        tmpHurtHead.SetActive(false);
    }
}
