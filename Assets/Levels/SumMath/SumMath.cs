using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class SumMath : MonoBehaviour
{
    [SerializeField] private GameObject musicHandler;

    [SerializeField] private UnityEvent TimerResetEvent;
    [SerializeField] private UnityEvent TimerStopEvent;

    //[SerializeField] private bool steByStepFading;
    [SerializeField] private float[] handSpeeds;
    [SerializeField] private float[] equationSpeeds;
    //age adade random ta khune avval araye bud mishe resulat, dovvom bud mishe adade chap va adade se mishe adade 2 moadele
    [SerializeField] private int[] whichOneToHidePossibilities = new int[3];
    [SerializeField] private Vector2[] timeScoreTable;
    //[SerializeField] private float readyTime;
    //[SerializeField] private float goTime;
    //[SerializeField] private GameObject go;
    //[SerializeField] private GameObject ready;
    //[SerializeField] private float xTime;
    [SerializeField] private float[] betweenRoundTime;
    //[SerializeField] private float handDownSpeed;
    [SerializeField] private float[] opacity;
    //[SerializeField] private int howManyFadedHandEquations;
    [SerializeField] private int howManySolidHandEquations;
    [SerializeField] private GameObject leftWrong;
    [SerializeField] private GameObject rightWrong;
    [SerializeField] private GameObject equation;
    [SerializeField] private GameObject hand;
    [SerializeField] private TextMeshProUGUI firstNo;
    [SerializeField] private TextMeshProUGUI secondNo;
    [SerializeField] private TextMeshProUGUI result;
    [SerializeField] private TextMeshProUGUI ans1;
    [SerializeField] private TextMeshProUGUI ans2;
    [SerializeField] private TextMeshProUGUI ans3;
    [SerializeField] private GameObject line;
    [SerializeField] private TextMeshProUGUI winner;
    [SerializeField] private TextMeshProUGUI leftPlayerScore;
    [SerializeField] private TextMeshProUGUI rightPlayerScore;
    [SerializeField] private Transform bottomPosition;
    [SerializeField] private Transform middlePosition;
    [SerializeField] private Transform underline;
    [SerializeField] private Transform downHandY;
    [SerializeField] private Transform equationHandY;
    //[SerializeField] private Transform underlineYPos;
    //[SerializeField] private Transform middlePos;
    [SerializeField] private Transform equationParent;

    private int counter = 0;
    private int no1;
    private int no2;
    private int resultNo;
    private int[] wrongAnswers;
    private bool eqMove = false;
    private bool btnPressed = true;
    private int trueAnswer;
    private Vector3 eqPosition;
    private float underlineYPos;
    private int playerAns;
    //private int[] answersPlacements = new int[3];
    private int rnd;
    private float timer = 0;
    private bool startTimer = false;
    private int playerID;
    private float rightScore;
    private float leftScore;
    private float tmpScore;
    private int totalNo;
    private GameObject answer;
    private GameObject spareNum;
    private GameObject staticNo;
    private Color handColor;
    private float handSpeed;
    private float numbersSpeed;
    private bool moveNum = false;
    private bool handDown = false;
    private int howManyFadedHandEquations;
    //private Transform firstNoPos;
    //private Transform secondNoPos;
    //private Transform resultNoPos;
    private Vector3 sparePos;
    private Vector3 linePos;
    private Vector3 handPos;
    //private float opacities;
    //IEnumerator Ready()
    //{
        //ready.SetActive(true);
        //yield return new WaitForSeconds(readyTime);
        //ready.SetActive(false);
        //go.SetActive(true);
        //yield return new WaitForSeconds(goTime);
        //go.SetActive(false);
        //Round1();
    //}

    void Round1()
    {
        //hhavaset bashe eq bayad az  payin meqdare makan begire
        answer = result.gameObject;
        answer.SetActive(false);
        no1 = RandomNumber.IntRandomNumber(1, 0, 9, false)[0];
        no2 = RandomNumber.IntRandomNumber(1, 0, (9 - no1), false)[0];
        resultNo = no1 + no2;
        firstNo.text = no1.ToString();
        secondNo.text = no2.ToString();
        //result.text = "";
        result.text = resultNo.ToString();
        line.transform.position = new Vector3(result.transform.position.x, underlineYPos, 0);
        equation.transform.position = bottomPosition.position;
        wrongAnswers = RandomNumber.IntRandomNumber(2, 0, 9, resultNo);
        PlaceAnswers(resultNo);
        equation.transform.position = bottomPosition.position;
        equation.SetActive(true);
        counter += 1;
        eqMove= true;
    }

    void PlaceAnswers(int resultNO)
    {
        //where to put the result
        rnd = RandomNumber.IntRandomNumber(1, 0, 2, false)[0];
        if (rnd == 0)
        {
            trueAnswer = 0;
            ans1.text = resultNO.ToString();
            ans2.text = wrongAnswers[0].ToString();
            ans3.text = wrongAnswers[1].ToString();
        }
        else if (rnd == 1)
        {
            trueAnswer = 1;
            ans1.text = wrongAnswers[0].ToString();
            ans2.text = resultNO.ToString();
            ans3.text = wrongAnswers[1].ToString();
        }
        else
        {
            trueAnswer = 2;
            ans1.text = wrongAnswers[0].ToString();
            ans2.text = wrongAnswers[1].ToString();
            ans3.text = resultNO.ToString();
        }
    }

    void EquationCreator()
    {
        handDown = false;
        timer = 0;
        if(counter < totalNo)
        {
            if (counter > howManyFadedHandEquations)
            {
                handColor.a = 1;
                //hand.GetComponent<SpriteRenderer>().color = handColor;
            }
            else
            {
                handColor.a = opacity[counter - 1];
            }
            hand.GetComponent<SpriteRenderer>().color = handColor;

            if (!hand.activeSelf)
            {
                hand.SetActive(true);
            }
            //sakht moadele va... bad residan be sakht o harekate dast
            if(counter < equationSpeeds.Length)
            {
                numbersSpeed = equationSpeeds[counter];
            }
            else
            {
                numbersSpeed = equationSpeeds[equationSpeeds.Length - 1];
            }
            
            if(counter < handSpeeds.Length)
            {
                handSpeed = handSpeeds[counter - 1];
            }
            else
            {
                handSpeed= handSpeeds[handSpeeds.Length - 1];
            }

            rnd = RandomNumber.IntRandomNumber(1, 0, whichOneToHidePossibilities[2], false)[0];
            if(rnd <= whichOneToHidePossibilities[0])
            {
                // hand on result
                //staticNo = result.gameObject;
                //so the numebrs move
                rnd = RandomNumber.IntRandomNumber(1, 0, 1, false)[0];
                if(rnd == 0)
                {
                    //left num is created and right is mystry
                    staticNo = result.gameObject;
                    answer = secondNo.gameObject;
                    no1 = RandomNumber.IntRandomNumber(1, 0, resultNo, false)[0];
                    no2 = resultNo - no1;
                    wrongAnswers = RandomNumber.IntRandomNumber(2, 0, 9, no2);
                    PlaceAnswers(no2);
                    secondNo.gameObject.SetActive(false);
                    secondNo.text = no2.ToString();
                    firstNo.gameObject.SetActive(false);
                    firstNo.text = no1.ToString();
                    line.SetActive(false);
                    linePos = new Vector3(secondNo.transform.position.x, line.transform.position.y, 0);
                    line.transform.position = new Vector3(linePos.x, bottomPosition.position.y, 0);
                    sparePos = firstNo.transform.position;
                    firstNo.transform.position = new Vector3(sparePos.x, bottomPosition.position.y + 0.25f, 0);
                    spareNum = firstNo.gameObject;
                    line.SetActive(true);
                    firstNo.gameObject.SetActive(true);
                    handPos = result.transform.position;
                    moveNum = true;

                }
                else
                {
                    staticNo = result.gameObject;
                    answer = firstNo.gameObject;
                    no2 = RandomNumber.IntRandomNumber(1, 0, resultNo, false)[0];
                    no1 = resultNo - no2;
                    wrongAnswers = RandomNumber.IntRandomNumber(2, 0, 9, no1);
                    PlaceAnswers(no1);
                    secondNo.gameObject.SetActive(false);
                    secondNo.text = no2.ToString();
                    firstNo.gameObject.SetActive(false);
                    firstNo.text = no1.ToString();
                    //trueAnswer = ;
                    line.SetActive(false);
                    linePos = new Vector3(firstNo.transform.position.x, line.transform.position.y, 0);
                    line.transform.position = new Vector3(linePos.x, bottomPosition.position.y, 0);
                    sparePos = secondNo.transform.position;
                    secondNo.transform.position = new Vector3(sparePos.x, bottomPosition.position.y + 0.25f, 0);
                    spareNum = secondNo.gameObject;
                    line.SetActive(true);
                    secondNo.gameObject.SetActive(true);
                    //hand.SetActive(true);
                    handPos = result.transform.position;
                    moveNum = true;
                }
            }
            else if (rnd <= whichOneToHidePossibilities[1])
            {
                //hand on first
                rnd = RandomNumber.IntRandomNumber(1, 0, 1, false)[0];
                if (rnd == 0)
                {
                    //the num is created and result is mystry
                    staticNo = firstNo.gameObject;
                    answer = result.gameObject;
                    no2 = RandomNumber.IntRandomNumber(1, 0, 9 - no1, false)[0];
                    resultNo = no1 + no2;
                    wrongAnswers = RandomNumber.IntRandomNumber(2, 0, 9, resultNo);
                    PlaceAnswers(resultNo);
                    secondNo.gameObject.SetActive(false);
                    result.gameObject.SetActive(false);
                    line.SetActive(false);
                    secondNo.text = no2.ToString();
                    result.text = resultNo.ToString();
                    linePos = new Vector3(result.transform.position.x, line.transform.position.y, 0);
                    line.transform.position = new Vector3(linePos.x, bottomPosition.position.y, 0);
                    sparePos = secondNo.transform.position;
                    secondNo.transform.position = new Vector3(sparePos.x, bottomPosition.position.y + 0.25f, 0);
                    spareNum = secondNo.gameObject;
                    line.SetActive(true);
                    secondNo.gameObject.SetActive(true);
                    handPos = firstNo.transform.position;
                    moveNum = true;
                }
                else
                {
                    // the result is created sec mystry
                    // hand + _ = res
                    staticNo = firstNo.gameObject;
                    answer = secondNo.gameObject;
                    resultNo = RandomNumber.IntRandomNumber(1, no1, 9, false)[0];
                    no2 = resultNo - no1;
                    wrongAnswers = RandomNumber.IntRandomNumber(2, 0, 9, no2);
                    PlaceAnswers(no2);
                    secondNo.gameObject.SetActive(false);
                    result.gameObject.SetActive(false);
                    line.SetActive(false);
                    secondNo.text = no2.ToString();
                    result.text = resultNo.ToString();
                    linePos = new Vector3(secondNo.transform.position.x, line.transform.position.y, 0);
                    line.transform.position = new Vector3(linePos.x, bottomPosition.position.y, 0);
                    sparePos = result.transform.position;
                    result.transform.position = new Vector3(sparePos.x, bottomPosition.position.y + 0.25f, 0);
                    spareNum = result.gameObject;
                    line.SetActive(true);
                    result.gameObject.SetActive(true);
                    handPos = firstNo.transform.position;
                    moveNum = true;
                }
            }
            else
            {
                //hand on second
                rnd = RandomNumber.IntRandomNumber(1, 0, 1, false)[0];
                if (rnd == 0)
                {
                    //left num is created and result is mystry
                    // x + hand = _
                    staticNo = secondNo.gameObject;
                    answer = result.gameObject;
                    no1 = RandomNumber.IntRandomNumber(1, 0, (9 - no2), false)[0];
                    resultNo = no1 + no2;
                    wrongAnswers = RandomNumber.IntRandomNumber(2, 0, 9, resultNo);
                    PlaceAnswers(resultNo);
                    firstNo.gameObject.SetActive(false);
                    result.gameObject.SetActive(false);
                    line.SetActive(false);
                    firstNo.text = no1.ToString();
                    result.text = resultNo.ToString();
                    linePos = new Vector3(result.transform.position.x, line.transform.position.y, 0);
                    line.transform.position = new Vector3(linePos.x, bottomPosition.position.y, 0);
                    sparePos = firstNo.transform.position;
                    firstNo.transform.position = new Vector3(sparePos.x, bottomPosition.position.y + 0.25f, 0);
                    spareNum = firstNo.gameObject;
                    line.SetActive(true);
                    firstNo.gameObject.SetActive(true);
                    handPos = secondNo.transform.position;
                    moveNum = true;

                }
                else
                {
                    // the result num is created
                    // _ + hand = x
                    staticNo = secondNo.gameObject;
                    answer = firstNo.gameObject;
                    resultNo = RandomNumber.IntRandomNumber(1, no2, 9, false)[0];
                    no1 = resultNo - no2;
                    wrongAnswers = RandomNumber.IntRandomNumber(2, 0, 9, no1);
                    PlaceAnswers(no1);
                    result.gameObject.SetActive(false);
                    firstNo.gameObject.SetActive(false);
                    line.SetActive(false);
                    firstNo.text = no1.ToString();
                    result.text = resultNo.ToString();
                    linePos = new Vector3(firstNo.transform.position.x, line.transform.position.y, 0);
                    line.transform.position = new Vector3(linePos.x, bottomPosition.position.y, 0);
                    sparePos = result.transform.position;
                    result.transform.position = new Vector3(sparePos.x, bottomPosition.position.y + 0.25f, 0);
                    spareNum = result.gameObject;
                    line.SetActive(true);
                    result.gameObject.SetActive(true);
                    handPos = secondNo.transform.position;
                    moveNum = true;

                }
            }
            counter += 1;
        }
        else
        {
            Time.timeScale = 0;
            equation.SetActive(false);
            hand.SetActive(false);
            if (leftScore > rightScore)
            {
                winner.text = "Left Won";
            }
            else
            {
                winner.text = "Right Won";
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {

        howManyFadedHandEquations = opacity.Length;
        eqPosition = equation.transform.position;
        underlineYPos = underline.position.y;
        totalNo = howManyFadedHandEquations + howManySolidHandEquations + 1;
        //opacities = new float[howManyFadedHandEquations];
        handColor = hand.GetComponent<SpriteRenderer>().color;
        Round1();

        //StartCoroutine(Ready());    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (moveNum)
        {
            //static x gerefte y vorudi
            //hand.transform.position = Vector3.MoveTowards(hand.transform.position, handPos, handSpeed * Time.fixedDeltaTime);
            hand.transform.position = Vector3.MoveTowards(hand.transform.position, handPos, handSpeed);
            //spareNum.transform.position = Vector3.MoveTowards(spareNum.transform.position, sparePos, numbersSpeed * Time.fixedDeltaTime);
            //spareNum.transform.position = Vector3.MoveTowards(spareNum.transform.position, sparePos, numbersSpeed);
            line.transform.position = Vector3.MoveTowards(line.transform.position, linePos, numbersSpeed * Time.fixedDeltaTime);
            //spareNum.transform.position = Vector3.MoveTowards(hand.transform.position, new Vector3(staticNo.transform.position.x, equationHandY.position.y, 0), handSpeed * Time.fixedDeltaTime);
            //line.transform.position += new Vector3(0, numbersSpeed * Time.fixedDeltaTime, 0);
            spareNum.transform.position += new Vector3(0, numbersSpeed * Time.fixedDeltaTime, 0);
            //if (Vector3.Distance(hand.transform.position, handPos) < 0.001 && Vector3.Distance(spareNum.transform.position, sparePos) < 0.001 && Vector3.Distance(line.transform.position, linePos) < 0.001) 
            if (Vector3.Distance(hand.transform.position, handPos) < 0.001 && (spareNum.transform.position.y > (sparePos.y - 0.001)) && Vector3.Distance(line.transform.position, linePos) < 0.001) 
            {
                spareNum.transform.position = sparePos;
                moveNum = false;
                startTimer = true;
                btnPressed = false;
                TimerResetEvent.Invoke();
            }
        }

        if (handDown)
        {
            //hand.transform.position = Vector3.MoveTowards(hand.transform.position, new Vector3(staticNo.transform.position.x, downHandY.position.y,0), handSpeed * Time.fixedDeltaTime);
            hand.transform.position = Vector3.MoveTowards(hand.transform.position, new Vector3(staticNo.transform.position.x, downHandY.position.y,0), handSpeed);
            if (Vector3.Distance(hand.transform.position, new Vector3(staticNo.transform.position.x, downHandY.position.y, 0)) < 0.001)
            {
                handDown = false;
                //EquationCreator();
                StartCoroutine(Wait());
                //startTimer = true;
                //TimerResetEvent.Invoke();
            }
        }

        if (eqMove)
        {
            equation.transform.position = Vector3.MoveTowards(equation.transform.position, middlePosition.position, equationSpeeds[0] * Time.fixedDeltaTime);
            if (Vector3.Distance(equation.transform.position, middlePosition.position) < 0.001)
            {
                eqMove = false;
                btnPressed = false;
                startTimer = true;
                TimerResetEvent.Invoke();
            }
        }
        if (startTimer)
        {
            timer += Time.fixedDeltaTime;
        }
    }

    public void LeftBtnPressed(int playerIndex)
    {
        startTimer = false;
        TimerStopEvent.Invoke();
        if (!btnPressed)
        {
            btnPressed = true;
            playerID = playerIndex;
            playerAns = 0;
            CheckTheAnswer();
        }
    }

    public void MiddleBtnPressed(int playerIndex)
    {
        startTimer = false;
        TimerStopEvent.Invoke();
        if (!btnPressed)
        {
            btnPressed = true;
            playerID = playerIndex;
            playerAns = 1;
            CheckTheAnswer();
        }
    }

    public void RightBtnPressed(int playerIndex)
    {
        startTimer = false;
        TimerStopEvent.Invoke();
        if (!btnPressed)
        {
            btnPressed = true;
            playerID = playerIndex;
            playerAns = 2;
            CheckTheAnswer();
        }
    }

    void CheckTheAnswer()
    {
        //hand move and show answer
        
        //hand
        if(playerAns == trueAnswer)
        {
            Debug.Log(1);
            musicHandler.GetComponent<Music>().PlaySound(0);
            for (int i = 0; i < timeScoreTable.Length; i++)
            {
                if (timer < timeScoreTable[i].x)
                {
                    if (playerID == 0)
                    {
                        //firstPlayerScore
                        leftScore += timeScoreTable[i].y;
                        leftPlayerScore.text = leftScore.ToString();
                    }
                    else
                    {
                        rightScore += timeScoreTable[i].y;
                        rightPlayerScore.text = rightScore.ToString();
                    }
                    //tmpScore += timeScoreTable[i].y;
                    break;
                }
            }
            if (playerID == 0)
            {
                //firstPlayerScore
                leftScore += tmpScore;
                leftPlayerScore.text = leftScore.ToString();
            }
            else
            {
                rightScore += tmpScore;
                rightPlayerScore.text = rightScore.ToString();
            }
            //answer.SetActive(true);
            //if (counter > 1)
            //{
            //    handDown = true;
            //}
            //handDown/
            //StartCoroutine(ShowResult());
            //EquationCreator();
        }
        else
        {
            musicHandler.GetComponent<Music>().PlaySound(1);
            //playerID lost score
            if (playerID == 0)
            {
            //Debug.Log(2);
                //firstPlayerScore
                leftScore -= 1;
                leftPlayerScore.text = leftScore.ToString();
                leftWrong.SetActive(true);
                //StartCoroutine(ShowX(leftWrong));
            }
            else
            {
                rightScore -= 1;
                rightPlayerScore.text = rightScore.ToString();
                rightWrong.SetActive(true);
                //StartCoroutine(ShowX(rightWrong));
            }
        }
        answer.SetActive(true);
        if (counter > 1)
        {
            handDown = true;
            //x.SetActive(true);
        }
        else
        {
            StartCoroutine(Wait());
        }
        //else
        //{        //EquationCreator();
        //   EquationCreator();
        //}
    }
    //IEnumerator ShowX(GameObject x)
    //{
    //    //x.SetActive(true);
    //    //yield return new WaitForSeconds(xTime);
    //    //x.SetActive(false);
    //    //EquationCreator();

    //    //level seda zade beshe
    //    //EquationCreator();
    //    //FractionCreater();
    //}

    IEnumerator Wait()
    {
        //Debug.Log(2);

        //if (leftWrong.activeSelf)
        //{
        //    leftWrong.SetActive(false);
        //}
        //else if(rightWrong.activeSelf)
        //{
        //    rightWrong.SetActive(false);
        //}
        if(counter <= betweenRoundTime.Length)
        {
            yield return new WaitForSeconds(betweenRoundTime[counter - 1]);
            leftWrong.SetActive(false);
            rightWrong.SetActive(false);
            EquationCreator();
        }
        else
        {
            yield return new WaitForSeconds(betweenRoundTime[betweenRoundTime.Length - 1]);
            leftWrong.SetActive(false);
            rightWrong.SetActive(false);
            EquationCreator();
        }
    }
}
