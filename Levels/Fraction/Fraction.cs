using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class Fraction : MonoBehaviour
{
    [SerializeField] private float[] speeds;
    [SerializeField] private UnityEvent TimerResetEvent;
    [SerializeField] private UnityEvent TimerStopEvent;
    [SerializeField] private Vector2[] timeScoreTable;
    [SerializeField] private int howManyFractions;
    [SerializeField] private GameObject leftWrong;
    [SerializeField] private GameObject rightWrong;
    [SerializeField] private GameObject go;
    [SerializeField] private GameObject ready;
    [SerializeField] private GameObject rightFraction;
    [SerializeField] private GameObject leftFraction;
    //[SerializeField] private GameObject fraction;
    [SerializeField] private float readyTime;
    [SerializeField] private float xTime;
    [SerializeField] private float goTime;
    [SerializeField] private TextMeshProUGUI winner;
    [SerializeField] private TextMeshProUGUI leftPlayerScore;
    [SerializeField] private TextMeshProUGUI rightPlayerScore;
    [SerializeField] private Transform leftUpSpawnPoint;
    [SerializeField] private Transform rightUpSpawnPoint;
    [SerializeField] private Transform rightDownSpawnPoint;
    [SerializeField] private Transform leftDownSpawnPoint;
    //[SerializeField] private RectTransform middlePos;
    //[SerializeField] private RectTransform leftParent;
    //[SerializeField] private RectTransform rightParent;
    [SerializeField] private Transform middlePos;
    [SerializeField] private Transform leftParent;
    [SerializeField] private Transform rightParent;
    //[SerializeField] private GameObject[] numbers = new GameObject[9];

    private int[] leftNumenatores;
    private int[] leftDenominatores;
    private int[] rightNumenatores;
    private int[] rightDenominatores;
    private int counter = 0;
    private int ans; //0 left 1 right
    private GameObject leftTmpFraction1;
    private GameObject rightTmpFraction1;
    private GameObject leftTmpFraction2;
    private GameObject rightTmpFraction2;
    private GameObject[] leftFractions;
    private GameObject[] rightFractions;
    //private TextMeshProUGUI tmpLNumenator;
    //private TextMeshProUGUI tmpLDenominator;
    //private TextMeshProUGUI tmpRNumenator;
    //private TextMeshProUGUI tmpRDenominator;
    private Transform leftSpawnPoint;
    private Transform rightSpawnPoint;
    private bool move = false;
    //private bool startTimer = false;
    private float timer = 0;
    private int leftSpeedFactor;
    private int rightSpeedFactor;
    //private Vector3 rightDestructionPoint;
    //private Vector3 leftDestructionPoint;
    private float leftScore;
    private float rightScore;
    private Vector3 leftMiddle;
    private Vector3 rightMiddle;
    private bool btnPressed = true;
    private float speed;
    private float yDifferenceToTheMiddle;
    private Sprite numberImg;
    IEnumerator Ready()
    {
        ready.SetActive(true);
        yield return new WaitForSeconds(readyTime);
        ready.SetActive(false);
        go.SetActive(true);
        yield return new WaitForSeconds(goTime);
        go.SetActive(false);
        FractionCreater();
    }

    // Start is called before the first frame update
    void Start()
    {
        yDifferenceToTheMiddle = middlePos.position.y - leftDownSpawnPoint.position.y;
        rightFractions = new GameObject[howManyFractions];
        leftFractions = new GameObject[howManyFractions];
        Initialization();
    }

    void Initialization()
    {
        leftMiddle = new Vector3(leftUpSpawnPoint.position.x, middlePos.position.y, 0);
        rightMiddle = new Vector3(rightUpSpawnPoint.position.x, middlePos.position.y, 0);

        leftDenominatores = new int[howManyFractions];
        rightDenominatores = new int[howManyFractions];
        rightNumenatores = new int[howManyFractions];
        leftNumenatores = new int[howManyFractions];

        leftDenominatores = RandomNumber.IntRandomNumber(howManyFractions, 2, 9, true);
        rightDenominatores = RandomNumber.IntRandomNumber(howManyFractions, 2, 9, true);

        for (int i = 0; i < howManyFractions; i++)
        {
            leftNumenatores[i] = RandomNumber.IntRandomNumber(1, 1, leftDenominatores[i] - 1, false)[0];
            rightNumenatores[i] = RandomNumber.IntRandomNumber(1, 1, rightDenominatores[i] - 1, false)[0];
            if ((rightNumenatores[i] == leftNumenatores[i] && leftDenominatores[i] == rightDenominatores[i])
                ||
                leftDenominatores[i] / leftNumenatores[i] == rightDenominatores[i] / rightNumenatores[i])
            {
                Debug.Log(leftDenominatores[i]);
                if (leftNumenatores[i] != 1)
                {
                    leftNumenatores[i] -= 1;
                }
                else if (rightNumenatores[i] != 1)
                {
                    leftNumenatores[i] -= 1;
                }
                else if (leftDenominatores[i] != 2)
                {
                    leftDenominatores[i] -= 1;
                }
                else
                {
                    leftDenominatores[i] += 1;
                }
                Debug.Log("faride");
                Debug.Log(leftDenominatores[i]);
            }
        }

        int rnd;
        rnd = RandomNumber.IntRandomNumber(1, 0, 1, false)[0];
        if (rnd == 0)
        {
            //left move down right move up
            rightSpawnPoint = rightDownSpawnPoint;
            leftSpawnPoint = leftUpSpawnPoint;
            rightSpeedFactor = 1;
            leftSpeedFactor = -1;
        }
        else
        {
            //right move down left move up
            rightSpawnPoint = rightUpSpawnPoint;
            leftSpawnPoint = leftDownSpawnPoint;
            rightSpeedFactor = -1;
            leftSpeedFactor = 1;
        }

        // create fractions and their position
        for (int i = 0; i < howManyFractions; i++)
        {
            leftTmpFraction1 = Instantiate(leftFraction, leftSpawnPoint.position, leftFraction.transform.rotation, leftParent);
            leftFractions[i] = leftTmpFraction1;
            //leftTmpFraction.
            for (int j = 0; j < leftTmpFraction1.transform.childCount; j++)
            {
                if (leftTmpFraction1.transform.GetChild(j).gameObject.name == "Numerator")
                {
                    leftTmpFraction1.transform.GetChild(j).gameObject.GetComponent<TextMeshProUGUI>().text = leftNumenatores[i].ToString();
                }
                if (leftTmpFraction1.transform.GetChild(j).gameObject.name == "Denominator")
                {
                    leftTmpFraction1.transform.GetChild(j).gameObject.GetComponent<TextMeshProUGUI>().text = leftDenominatores[i].ToString();
                }
            }

            // right creation
            rightTmpFraction1 = Instantiate(rightFraction, rightSpawnPoint.position, rightFraction.transform.rotation, rightParent);
            rightFractions[i] = rightTmpFraction1;
            for (int j = 0; j < rightTmpFraction1.transform.childCount; j++)
            {
                if (rightTmpFraction1.transform.GetChild(j).gameObject.name == "Numerator")
                {
                    rightTmpFraction1.transform.GetChild(j).gameObject.GetComponent<TextMeshProUGUI>().text = rightNumenatores[i].ToString();
                }
                if (rightTmpFraction1.transform.GetChild(j).gameObject.name == "Denominator")
                {
                    rightTmpFraction1.transform.GetChild(j).gameObject.GetComponent<TextMeshProUGUI>().text = rightDenominatores[i].ToString();
                }
            }
        }
        StartCoroutine(Ready());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (move)
        {
            for (int i = 0; i < counter - 1; i++)
            {
                leftFractions[i].transform.position += new Vector3(0, speed * Time.fixedDeltaTime * leftSpeedFactor, 0);
                rightFractions[i].transform.position += new Vector3(0, speed * Time.fixedDeltaTime * rightSpeedFactor, 0);
            }
            leftTmpFraction1.transform.position = Vector3.MoveTowards(leftTmpFraction1.transform.position, leftMiddle, speed * Time.fixedDeltaTime);
            rightTmpFraction1.transform.position = Vector3.MoveTowards(rightTmpFraction1.transform.position, rightMiddle, speed * Time.fixedDeltaTime);
            if (Vector3.Distance(rightTmpFraction1.transform.position, rightMiddle) < 0.001 && Vector3.Distance(leftTmpFraction1.transform.position, leftMiddle) < 0.001)
            {
                //Debug.Log(1);
                move = false;
                TimerResetEvent.Invoke();
                btnPressed = false;
            }
        }
    }

    void FractionCreater()
    {
        if (counter < howManyFractions)
        {
            if (counter < speeds.Length)
            {
                speed = speeds[counter];
            }
            else
            {
                speed = speeds[speeds.Length - 1];
            }

            float rightEq = (float)rightNumenatores[counter] / rightDenominatores[counter];
            float leftEq = (float)leftNumenatores[counter] / leftDenominatores[counter];
            rightTmpFraction1 = rightFractions[counter];
            leftTmpFraction1 = leftFractions[counter];
            //if(counter >= 1)
            //{
            //    rightTmpFraction2 = rightFractions[counter - 1];
            //    leftTmpFraction2 = leftFractions[counter - 1];
            //}
            //leftTmpFraction.transform.position = leftSpawnPoint.position;
            //rightTmpFraction.transform.position = rightSpawnPoint.position;
            leftTmpFraction1.SetActive(true);
            rightTmpFraction1.SetActive(true);
            if (rightEq > leftEq)
            {
                ans = 1;
            }
            else
            {
                ans = 0;
            }
            move = true;
            counter += 1;
        }
        else
        {
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
    }

    public void LeftBtnPressed(int playerIndex)
    {
        //counter += 1;

        if (!btnPressed)
        {
            btnPressed = true;
            TimerStopEvent.Invoke();
            if (ans == 0)
            {
                if (playerIndex == 0)
                {
                    //left player with correct ans
                    for (int i = 0; i < timeScoreTable.Length; i++)
                    {
                        if (timer < timeScoreTable[i].x)
                        {
                            leftScore += timeScoreTable[i].y;
                            break;
                        }
                    }
                    leftPlayerScore.text = leftScore.ToString();
                    //FractionCreater();
                }
                else
                {
                    for (int i = 0; i < timeScoreTable.Length; i++)
                    {
                        if (timer < timeScoreTable[i].x)
                        {
                            rightScore += timeScoreTable[i].y;
                            break;
                        }
                    }
                    rightPlayerScore.text = rightScore.ToString();
                }
                FractionCreater();
            }
            else
            {
                if (playerIndex == 0)
                {
                    leftScore -= 1;
                    leftPlayerScore.text = leftScore.ToString();
                    StartCoroutine(ShowX(leftWrong));
                }
                else
                {
                    rightScore -= 1;
                    rightPlayerScore.text = rightScore.ToString();
                    StartCoroutine(ShowX(rightWrong));
                }
            }
        }
    }

    public void RightBtnPressed(int playerIndex)
    {
        //counter += 1;

        if (!btnPressed)
        {
            btnPressed = true;
            TimerStopEvent.Invoke();
            if (ans == 1)
            {
                if (playerIndex == 0)
                {
                    //left player with correct ans
                    for (int i = 0; i < timeScoreTable.Length; i++)
                    {
                        if (timer < timeScoreTable[i].x)
                        {
                            leftScore += timeScoreTable[i].y;
                            break;
                        }
                    }
                    leftPlayerScore.text = leftScore.ToString();
                }
                else
                {
                    for (int i = 0; i < timeScoreTable.Length; i++)
                    {
                        if (timer < timeScoreTable[i].x)
                        {
                            rightScore += timeScoreTable[i].y;
                            break;
                        }
                    }
                    rightPlayerScore.text = rightScore.ToString();
                }
                FractionCreater();
            }
            else
            {
                if (playerIndex == 0)
                {
                    leftScore -= 1;
                    leftPlayerScore.text = leftScore.ToString();
                    StartCoroutine(ShowX(leftWrong));
                }
                else
                {
                    rightScore -= 1;
                    rightPlayerScore.text = rightScore.ToString();
                    StartCoroutine(ShowX(rightWrong));
                }
            }
        }
    }
    IEnumerator ShowX(GameObject x)
    {
        x.SetActive(true);
        yield return new WaitForSeconds(xTime);
        x.SetActive(false);
        FractionCreater();
    }
}
