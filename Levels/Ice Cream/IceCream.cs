using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class IceCream : MonoBehaviour
{
    [SerializeField] private float readyTime;
    [SerializeField] private float goTime;
    [SerializeField] private float betweenRoundsTime;
    [SerializeField] private int minNoOfArrows;
    [SerializeField] private int maxNoOfArrows;
    [SerializeField] private int noOfRounds;
    [SerializeField] private Transform[] leftArrowsPositions;
    [SerializeField] private Transform[] rightArrowsPositions;
    [SerializeField] private Vector2[] whereToStartFrom; //makane flash ha vase har round az koja bashe
    [SerializeField] private GameObject ready;
    [SerializeField] private GameObject go;
    [SerializeField] private GameObject up;
    [SerializeField] private GameObject down;
    [SerializeField] private GameObject left;
    [SerializeField] private GameObject right;
    [SerializeField] private GameObject button;
    [SerializeField] private Transform arrowsParent;
    [SerializeField] private UnityEvent P1IdleAnime;
    [SerializeField] private UnityEvent P1LoseAnime;
    [SerializeField] private UnityEvent P1WinAnime;
    [SerializeField] private UnityEvent P2IdleAnime;
    [SerializeField] private UnityEvent P2LoseAnime;
    [SerializeField] private UnityEvent P2WinAnime;
    [SerializeField] private TextMeshProUGUI playerOneScoreTxt;
    [SerializeField] private TextMeshProUGUI playerTwoScoreTxt;
    [SerializeField] private TextMeshProUGUI winner;

    //[SerializeField] private GameObject ;
    private string[] directions;
    private string direction = "";
    private int thisRoundArrows;
    private int rnd;
    private int counter = 0;
    private int p1KeyCounter = 0;
    private int p2KeyCounter = 0;
    private int playerTwoScore = 0;
    private int playerOneScore = 0;
    private string answer;
    private string playerOneAnswer;
    private string playerTwoAnswer;
    private bool playerOneDone = false;
    private bool playerTwoDone = false;
    private bool canPress = false;
    private GameObject tmp;
    private GameObject btn1;
    private GameObject btn2;
    private List<GameObject> leftArrows = new List<GameObject>();
    private List<GameObject> rightArrows = new List<GameObject>();
    //private int
    IEnumerator Ready()
    {
        ready.SetActive(true);
        yield return new WaitForSeconds(readyTime);
        ready.SetActive(false);
        go.SetActive(true);
        yield return new WaitForSeconds(goTime);
        go.SetActive(false);
        IceCreamLevel();
        //StartCoroutine(Clock());
    }
    // Start is called before the first frame update
    void Start()
    {
        directions = new string[noOfRounds];
        thisRoundArrows = minNoOfArrows;
        DirectionCreator();
        //directions = new string[noOfRounds];
        StartCoroutine(Ready());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DirectionCreator()
    {
        for (int i = 0; i < noOfRounds; i++)
        {
            for (int j = 0; j < thisRoundArrows; j++)
            {
                //Debug.Log(22);
                rnd = RandomNumber.IntRandomNumber(1, 1, 4, false)[0];
                if(rnd == 1)
                {
                    direction += "U";
                    //Debug.Log(1);
                }
                else if(rnd == 2)
                {
                    direction += "R";
                    //Debug.Log(2);
                }
                else if(rnd == 3)
                {
                    direction += "D";
                    //Debug.Log(3);
                }
                else
                {
                    direction += "L";
                    //Debug.Log(4);
                }
            }
            directions[i] = direction;
            direction = "";
            if(thisRoundArrows != maxNoOfArrows)
            {
                thisRoundArrows += 1;
            }
        }
    }

    void IceCreamLevel()
    {
        answer = directions[counter];
        leftArrows = new List<GameObject>();
        rightArrows = new List<GameObject>();
        for (int i = 0; i < directions[counter].Length; i++)
        {
            if(directions[counter].Substring(i, 1) == "U")
            {
                tmp = Instantiate(up, leftArrowsPositions[i + WhereToStartFrom(directions[counter].Length)].position, up.transform.rotation, arrowsParent);
                tmp.SetActive(true);
                leftArrows.Add(tmp);
                tmp = Instantiate(up, rightArrowsPositions[i + WhereToStartFrom(directions[counter].Length)].position, up.transform.rotation, arrowsParent);
                tmp.SetActive(true);
                rightArrows.Add(tmp);
            }
            else if (directions[counter].Substring(i, 1) == "D")
            {
                tmp = Instantiate(down, leftArrowsPositions[i + WhereToStartFrom(directions[counter].Length)].position, down.transform.rotation, arrowsParent);
                tmp.SetActive(true);
                leftArrows.Add(tmp);
                tmp = Instantiate(down, rightArrowsPositions[i + WhereToStartFrom(directions[counter].Length)].position, down.transform.rotation, arrowsParent);
                tmp.SetActive(true);
                rightArrows.Add(tmp);
            }
            else if (directions[counter].Substring(i, 1) == "L")
            {
                tmp = Instantiate(left, leftArrowsPositions[i + WhereToStartFrom(directions[counter].Length)].position, left.transform.rotation, arrowsParent);
                tmp.SetActive(true);
                leftArrows.Add(tmp);
                tmp = Instantiate(left, rightArrowsPositions[i + WhereToStartFrom(directions[counter].Length)].position, left.transform.rotation, arrowsParent);
                tmp.SetActive(true);
                rightArrows.Add(tmp);
            }
            else
            {
                tmp = Instantiate(right, leftArrowsPositions[i + WhereToStartFrom(directions[counter].Length)].position, right.transform.rotation, arrowsParent);
                tmp.SetActive(true);
                leftArrows.Add(tmp);
                tmp = Instantiate(right, rightArrowsPositions[i + WhereToStartFrom(directions[counter].Length)].position, right.transform.rotation, arrowsParent);
                tmp.SetActive(true);
                rightArrows.Add(tmp);
            }
        }
        btn1 = Instantiate(button, leftArrowsPositions[directions[counter].Length + WhereToStartFrom(directions[counter].Length)].position, button.transform.rotation);
        btn1.SetActive(true);
        btn2 = Instantiate(button, rightArrowsPositions[directions[counter].Length + WhereToStartFrom(directions[counter].Length)].position, button.transform.rotation);
        btn2.SetActive(true);
        canPress = true;
        //counter += 1;
    }

    int WhereToStartFrom(int arrowNo) 
    {
        int index = -1;
        for (int i = 0; i < whereToStartFrom.Length; i++)
        {
            if(i == whereToStartFrom.Length - 1)
            {
                index = (int)whereToStartFrom[whereToStartFrom.Length - 1].y;
                break;
            }
            if (whereToStartFrom[i].x <= arrowNo && whereToStartFrom[i + 1].x >= arrowNo)
            {
                index = (int)whereToStartFrom[i].y;
                break;
            }
        }
        return index;
    }
    public void PlayerOneUp()
    {

        if (!playerOneDone && canPress)
        {
            if(answer[p1KeyCounter].ToString() != "U")
            {
                leftArrows[p1KeyCounter].GetComponent<Renderer>().material.color = Color.red;
            }
            else
            {
                leftArrows[p1KeyCounter].GetComponent<Renderer>().material.color = Color.green;
            }
            p1KeyCounter += 1;
            playerOneAnswer += "U";
        }
    }

    public void PlayerTwoUp()
    {
        if (!playerTwoDone && canPress)
        {
            if (answer[p2KeyCounter].ToString() != "U")
            {
                rightArrows[p2KeyCounter].GetComponent<Renderer>().material.color = Color.red;
            }
            else
            {
                rightArrows[p2KeyCounter].GetComponent<Renderer>().material.color = Color.green;
            }
            //rightArrows[p2KeyCounter].GetComponent<Renderer>().material.color = Color.green;
            p2KeyCounter += 1;
            playerTwoAnswer += "U";
        }
    }

    public void PlayerOneDown()
    {
        if (!playerOneDone && canPress)
        {
            if (answer[p1KeyCounter].ToString() != "D")
            {
                leftArrows[p1KeyCounter].GetComponent<Renderer>().material.color = Color.red;
            }
            else
            {
                leftArrows[p1KeyCounter].GetComponent<Renderer>().material.color = Color.green;
            }
            //leftArrows[p1KeyCounter].GetComponent<Renderer>().material.color = Color.green;
            p1KeyCounter += 1;
            playerOneAnswer += "D";
        }
    }

    public void PlayerTwoDown()
    {
        if (!playerTwoDone && canPress)
        {
            if (answer[p2KeyCounter].ToString() != "D")
            {
                rightArrows[p2KeyCounter].GetComponent<Renderer>().material.color = Color.red;
            }
            else
            {
                rightArrows[p2KeyCounter].GetComponent<Renderer>().material.color = Color.green;
            }
            //rightArrows[p2KeyCounter].GetComponent<Renderer>().material.color = Color.green;
            p2KeyCounter += 1;
            playerTwoAnswer += "D";
        }
    }

    public void PlayerOneLeft()
    {
        if (!playerOneDone && canPress)
        {
            if (answer[p1KeyCounter].ToString() != "L")
            {
                leftArrows[p1KeyCounter].GetComponent<Renderer>().material.color = Color.red;
            }
            else
            {
                leftArrows[p1KeyCounter].GetComponent<Renderer>().material.color = Color.green;
            }
            //leftArrows[p1KeyCounter].GetComponent<Renderer>().material.color = Color.green;
            p1KeyCounter += 1;
            playerOneAnswer += "L";
        }
    }

    public void PlayerTwoLeft()
    {
        if (!playerTwoDone && canPress)
        {
            if (answer[p2KeyCounter].ToString() != "L")
            {
                rightArrows[p2KeyCounter].GetComponent<Renderer>().material.color = Color.red;
            }
            else
            {
                rightArrows[p2KeyCounter].GetComponent<Renderer>().material.color = Color.green;
            }
            //rightArrows[p2KeyCounter].GetComponent<Renderer>().material.color = Color.green;
            p2KeyCounter += 1;
            playerTwoAnswer += "L";
        }
    }

    public void PlayerOneRight()
    {
        if (!playerOneDone && canPress)
        {
            if (answer[p1KeyCounter].ToString() != "R")
            {
                leftArrows[p1KeyCounter].GetComponent<Renderer>().material.color = Color.red;
            }
            else
            {
                leftArrows[p1KeyCounter].GetComponent<Renderer>().material.color = Color.green;
            }
            //leftArrows[p1KeyCounter].GetComponent<Renderer>().material.color = Color.green;
            p1KeyCounter += 1;
            playerOneAnswer += "R";
        }
    }

    public void PlayerTwoRight()
    {
        if (!playerTwoDone && canPress)
        {
            if (answer[p2KeyCounter].ToString() != "R")
            {
                rightArrows[p2KeyCounter].GetComponent<Renderer>().material.color = Color.red;
            }
            else
            {
                rightArrows[p2KeyCounter].GetComponent<Renderer>().material.color = Color.green;
            }
            //rightArrows[p2KeyCounter].GetComponent<Renderer>().material.color = Color.green;
            p2KeyCounter += 1;
            playerTwoAnswer += "R";
        }
    }

    public void PlayerOneCheck()
    {
        playerOneDone = true;
        p1KeyCounter = 0;
        if (!playerTwoDone)
        {
            if(playerOneAnswer == answer)
            {
                playerOneAnswer = "";
                playerOneScore += 1;
                canPress = false;
                playerOneScoreTxt.text = playerOneScore.ToString();
                //P1WinAnime.Invoke();
                //P2LoseAnime.Invoke();
                for (int i = 0; i < leftArrows.Count; i++)
                {
                    Destroy(leftArrows[i]);
                    Destroy(rightArrows[i]);
                }
                leftArrows = null;
                rightArrows = null;
                if((counter + 1) == noOfRounds)
                {
                    playerTwoDone = true;
                    if(playerOneScore > playerTwoScore)
                    {
                        P1WinAnime.Invoke();
                        P2LoseAnime.Invoke();
                        winner.text = "Left Player Won";
                    }
                    else if(playerOneScore < playerTwoScore)
                    {
                        P2WinAnime.Invoke();
                        P1LoseAnime.Invoke();
                        winner.text = "Right Player Won";
                    }
                    else
                    {
                        P1WinAnime.Invoke();
                        P2WinAnime.Invoke();
                        winner.text = "Draw";
                    }
                }
                else
                {
                    P1WinAnime.Invoke();
                    P2LoseAnime.Invoke();
                    StartCoroutine(BetweenRounds());
                }
            }
            else
            {
                for (int i = 0; i < leftArrows.Count; i++)
                {
                    leftArrows[i].GetComponent<Renderer>().material.color = Color.white;
                    //p2KeyCounter += 1;
                }
                playerOneAnswer = "";
                playerOneDone = false;
            }
        }
    }

    public void PlayerTwoCheck()
    {
        playerTwoDone = true;
        p2KeyCounter = 0;
        if (!playerOneDone)
        {
            if (playerTwoAnswer == answer)
            {
                playerTwoAnswer = "";
                playerTwoScore += 1;
                canPress = false;
                playerTwoScoreTxt.text = playerTwoScore.ToString();
                //P2WinAnime.Invoke();
                //P1LoseAnime.Invoke();
                for (int i = 0; i < leftArrows.Count; i++)
                {
                    Destroy(leftArrows[i]);
                    Destroy(rightArrows[i]);
                }
                leftArrows = null;
                rightArrows = null;
                if ((counter + 1) == noOfRounds)
                {
                    playerOneDone = true;

                    if (playerOneScore > playerTwoScore)
                    {
                        P1WinAnime.Invoke();
                        P2LoseAnime.Invoke();
                        winner.text = "Left Player Won";
                    }
                    else if(playerOneScore < playerTwoScore)
                    {
                        P2WinAnime.Invoke();
                        P1LoseAnime.Invoke();
                        winner.text = "Right Player Won";
                    }
                    else
                    {
                        P2WinAnime.Invoke();
                        P1WinAnime.Invoke();
                        winner.text = "Draw";
                    }
                }
                else
                {
                    P2WinAnime.Invoke();
                    P1LoseAnime.Invoke();
                    StartCoroutine(BetweenRounds());
                }
            }
            else
            {
                for (int i = 0; i < rightArrows.Count; i++)
                {
                    rightArrows[i].GetComponent<Renderer>().material.color = Color.white;
                    //p2KeyCounter += 1;
                }
                playerTwoDone = false;
                playerTwoAnswer = "";
            }
        }
    }

    IEnumerator BetweenRounds()
    {
        Destroy(btn1);
        Destroy(btn2);
        yield return new WaitForSeconds(betweenRoundsTime);
        p1KeyCounter = 0;
        p2KeyCounter = 0;
        P1IdleAnime.Invoke();
        P2IdleAnime.Invoke();
        playerOneAnswer = "";
        playerTwoAnswer = "";
        answer = "";
        playerOneDone = false;
        //canPress = true; vaqti flesh umad 
        playerTwoDone = false;
        counter += 1;
        IceCreamLevel();
    }
}
