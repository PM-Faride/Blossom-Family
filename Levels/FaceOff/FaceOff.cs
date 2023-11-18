using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FaceOff : MonoBehaviour
{
    [SerializeField] private GameObject[] heads;
    [SerializeField] private GameObject[] middles;
    [SerializeField] private GameObject[] tails;
    [SerializeField] private GameObject ready;
    [SerializeField] private GameObject go;
    [SerializeField] private GameObject greenSingleFrame;
    [SerializeField] private GameObject redSingleFrame;
    [SerializeField] private GameObject leftCross;
    [SerializeField] private GameObject rightCross;
    [SerializeField] private Transform leftParent;
    [SerializeField] private Transform rightParent;
    [SerializeField] private Transform mainFacesParent;
    [SerializeField] private float readyTimer;
    [SerializeField] private float goTimer;
    [SerializeField] private float stayOnCrossTimer;
    [SerializeField] private float betweenRoundTimer;
    [SerializeField] private float firstFrameSpeed;
    [SerializeField] private float secondFrameSpeed;
    [SerializeField] private float thirdFrameSpeed;
    [SerializeField] private float faceMoveSpeed;
    [SerializeField] private int howManyRounds;
    [SerializeField] private GameObject[] rightStaticPhoto;
    [SerializeField] private GameObject[] leftStaticPhoto;
    [SerializeField] private Transform[] mainFramePositions;
    [SerializeField] private float photoLength; // tule tasvir baraye kenar ham gozashtane tasavir va sakht makaneshun
    [SerializeField] private float photoLength08; // tule tasvir ba size .8 baraye tasvoire asli
    [SerializeField] private float photoHeight; // ertefa tasvir baray harekate kadrha\
    [SerializeField] private TextMeshProUGUI leftScore;
    [SerializeField] private TextMeshProUGUI rightScore;
    [SerializeField] private TextMeshProUGUI winner;
    //[SerializeField] private 

    private Vector3[] leftHeadPoses;
    private Vector3[] leftMiddlePoses;
    private Vector3[] leftTailPoses;
    private Vector3[] rightHeadPoses;
    private Vector3[] rightMiddlePoses;
    private Vector3[] rightTailPoses;
    private GameObject[] rightHeads;
    private GameObject[] leftHeads;
    private GameObject[] rightMiddles;
    private GameObject[] leftMiddles;
    private GameObject[] rightTails;
    private GameObject[] leftTails;
    private GameObject[] mainHeads;
    private GameObject[] mainMiddles;
    private GameObject[] mainTails;
    private GameObject tmpFace;
    private string answer;
    private int[] turns;
    private int roundCounter = 0;
    private int leftWhichFace;
    private int rightWhichFace;
    private GameObject[] answerGO = new GameObject[3];
    private bool canRightPress = false;
    private bool canLeftPress = false;
    private bool headPlaced = false;
    private bool tailPlaced = false;
    private bool middlePlaced = false;

    private bool moveMainHeads = false;
    private bool moveMainMiddles = false;
    private bool moveMainTails = false;

    private bool moveLeftHeadsLeft = false;
    private bool moveLeftHeadsRight = false;
    private bool moveLeftMiddlesLeft = false;
    private bool moveLeftMiddlesRight = false;
    private bool moveLeftTailsLeft = false;
    private bool moveLeftTailsRight = false;

    private bool moveRightHeadsLeft = false;
    private bool moveRightHeadsRight = false;
    private bool moveRightMiddlesLeft = false;
    private bool moveRightMiddlesRight = false;
    private bool moveRightTailsLeft = false;
    private bool moveRightTailsRight = false;

    private int whereIsLeftFrame = 0;
    private int whereIsRightFrame = 0;
    private int howManyFace;
    private int playerOneScore = 0;
    private int playerTwoScore = 0;
    private string[] playerOneAnswer = new string[3];
    private string[] playerTwoAnswer = new string[3];
    int counter = 0;

    IEnumerator Ready()
    {
        ready.SetActive(true);
        yield return new WaitForSeconds(readyTimer);
        ready.SetActive(false);
        go.SetActive(true);
        yield return new WaitForSeconds(goTimer);
        go.SetActive(false);
        answer = heads[turns[0]].name;
        MixAndShowPhotos();
        //StartCoroutine(Clock());
    }

    // Start is called before the first frame update
    void Start()
    {
        leftWhichFace = (heads.Length + 1) / 2;
        rightWhichFace = (heads.Length + 1) / 2;

        howManyFace = heads.Length + 1;

        rightHeads = new GameObject[heads.Length + 1];
        leftHeads = new GameObject[heads.Length + 1];
        rightMiddles = new GameObject[heads.Length + 1];
        leftMiddles= new GameObject[heads.Length + 1];
        rightTails= new GameObject[heads.Length + 1];
        leftTails = new GameObject[heads.Length + 1];

        leftHeadPoses = new Vector3[heads.Length + 1];
        leftMiddlePoses = new Vector3[heads.Length + 1];
        leftTailPoses = new Vector3[heads.Length + 1];
        rightHeadPoses = new Vector3[heads.Length + 1];
        rightMiddlePoses = new Vector3[heads.Length + 1];
        rightTailPoses = new Vector3[heads.Length + 1];

        mainHeads = new GameObject[heads.Length];
        mainMiddles = new GameObject[heads.Length];
        mainTails = new GameObject[heads.Length];

        int[] tmpRndNumbers = new int[heads.Length];
        turns = new int[heads.Length * 2];
        tmpRndNumbers = RandomNumber.IntRandomNumber(heads.Length, 0, heads.Length - 1, false);
        for (int i = 0; i < tmpRndNumbers.Length; i++)
        {
            turns[i] = tmpRndNumbers[i];
        }
        tmpRndNumbers = RandomNumber.IntRandomNumber(heads.Length, 0, heads.Length - 1, false);
        for (int i = 0; i < tmpRndNumbers.Length; i++)
        {
            turns[tmpRndNumbers.Length + i] = tmpRndNumbers[i];
        }
        tmpRndNumbers = null;
        //poses
        for (int i = 0; i < heads.Length / 2; i++)
        {
            leftHeadPoses[i] = new Vector3(leftStaticPhoto[0].transform.position.x - (i + 1) * photoLength, leftStaticPhoto[0].transform.position.y, 0);
            rightHeadPoses[i] = new Vector3(rightStaticPhoto[0].transform.position.x - (i + 1) * photoLength, rightStaticPhoto[0].transform.position.y, 0);
            leftMiddlePoses[i] = new Vector3(leftStaticPhoto[1].transform.position.x - (i + 1) * photoLength, leftStaticPhoto[1].transform.position.y, 0);
            rightMiddlePoses[i] = new Vector3(rightStaticPhoto[1].transform.position.x - (i + 1) * photoLength, rightStaticPhoto[1].transform.position.y, 0);
            leftTailPoses[i] = new Vector3(leftStaticPhoto[2].transform.position.x - (i + 1) * photoLength, leftStaticPhoto[2].transform.position.y, 0);
            rightTailPoses[i] = new Vector3(rightStaticPhoto[2].transform.position.x - (i + 1) * photoLength, rightStaticPhoto[2].transform.position.y, 0);
        }

        leftHeadPoses[heads.Length / 2] = leftStaticPhoto[0].transform.position;
        rightHeadPoses[heads.Length / 2] = rightStaticPhoto[0].transform.position;
        leftMiddlePoses[heads.Length / 2] = leftStaticPhoto[1].transform.position;
        rightMiddlePoses[heads.Length / 2] = rightStaticPhoto[1].transform.position;
        leftTailPoses[heads.Length / 2] = leftStaticPhoto[2].transform.position;
        rightTailPoses[heads.Length / 2] = rightStaticPhoto[2].transform.position;

        for (int i = heads.Length / 2 + 1; i < heads.Length + 1; i++)
        {
            leftHeadPoses[i] = new Vector3(leftStaticPhoto[0].transform.position.x + (i + 1) * photoLength, leftStaticPhoto[0].transform.position.y, 0);
            rightHeadPoses[i] = new Vector3(rightStaticPhoto[0].transform.position.x + (i + 1) * photoLength, rightStaticPhoto[0].transform.position.y, 0);
            leftMiddlePoses[i] = new Vector3(leftStaticPhoto[1].transform.position.x + (i + 1) * photoLength, leftStaticPhoto[1].transform.position.y, 0);
            rightMiddlePoses[i] = new Vector3(rightStaticPhoto[1].transform.position.x + (i + 1) * photoLength, rightStaticPhoto[1].transform.position.y, 0);
            leftTailPoses[i] = new Vector3(leftStaticPhoto[2].transform.position.x + (i + 1) * photoLength, leftStaticPhoto[2].transform.position.y, 0);
            rightTailPoses[i] = new Vector3(rightStaticPhoto[2].transform.position.x + (i + 1) * photoLength, rightStaticPhoto[2].transform.position.y, 0);
        }

        for (int i = 0; i < heads.Length; i++)
        {
            mainHeads[i] = Instantiate(heads[i], new Vector3(mainFramePositions[0].position.x
                - photoLength08 * (i + 1), mainFramePositions[0].position.y, 0), heads[i].transform.rotation, mainFacesParent);
            mainHeads[i].name = heads[i].name;
            mainHeads[i].transform.localScale = new Vector3(0.8f, 0.8f, 1);
            mainHeads[i].SetActive(true);

            mainMiddles[i] = Instantiate(middles[i], new Vector3(mainFramePositions[1].position.x
                - photoLength08 * (i + 1), mainFramePositions[1].position.y, 0), middles[i].transform.rotation, mainFacesParent);
            mainMiddles[i].name = middles[i].name;
            mainMiddles[i].transform.localScale = new Vector3(0.8f, 0.8f, 1);
            //mainHeads[i].transform.localScale = 0.8f;
            mainMiddles[i].SetActive(true);

            mainTails[i] = Instantiate(tails[i], new Vector3(mainFramePositions[2].position.x
                 - photoLength08 * (i + 1), mainFramePositions[2].position.y, 0), tails[i].transform.rotation, mainFacesParent);
            mainTails[i].name = tails[i].name;
            mainTails[i].transform.localScale = new Vector3(0.8f, 0.8f, 1);
            mainTails[i].SetActive(true);
        }
        StartCoroutine(Ready());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (moveMainHeads)
        {
            if (answerGO[0].transform.position.x < mainFramePositions[0].position.x)
            {
                //Debug.Log(2);
                for (int i = 0; i < mainHeads.Length; i++)
                {
                    mainHeads[i].transform.position += new Vector3(firstFrameSpeed * Time.fixedDeltaTime, 0, 0);
                }
            }
            else
            {
                answerGO[0].transform.position = mainFramePositions[0].position;
                headPlaced = true;
                moveMainHeads = false;
            }
        }
        if (moveMainTails)
        {
            if (answerGO[2].transform.position.x < mainFramePositions[2].position.x)
            {
                for (int i = 0; i < mainTails.Length; i++)
                {
                    mainTails[i].transform.position += new Vector3(secondFrameSpeed * Time.fixedDeltaTime, 0, 0);
                }
            }
            else
            {
                moveMainTails= false;
                answerGO[2].transform.position = mainFramePositions[2].position;
                tailPlaced = true;
            }
        }
        if (moveMainMiddles)
        {
            if (answerGO[1].transform.position.x < mainFramePositions[1].position.x)
            {
                for (int i = 0; i < mainHeads.Length; i++)
                {
                    mainMiddles[i].transform.position += new Vector3(thirdFrameSpeed * Time.fixedDeltaTime, 0, 0);
                }
            }
            else
            {
                moveMainMiddles= false;
                answerGO[1].transform.position = mainFramePositions[1].position;
                middlePlaced = true;
            }
        }
        if (moveLeftHeadsLeft)
        {
            // left has va left zade shode
            if (leftHeads[leftWhichFace + 1].transform.position.x > leftStaticPhoto[0].transform.position.x)
            {
                for (int i = 0; i < leftHeads.Length; i++)
                {
                    leftHeads[i].transform.position += new Vector3(faceMoveSpeed * Time.fixedDeltaTime, 0, 0);
                }
            }
            else
            {
                moveLeftHeadsLeft = false;
                leftHeads[leftWhichFace + 1].transform.position = leftStaticPhoto[0].transform.position;
                leftWhichFace += 1;
                canLeftPress = true;
            }
        }
        if (moveLeftHeadsRight)
        {
            if (leftHeads[leftWhichFace - 1].transform.position.x > leftStaticPhoto[0].transform.position.x)
            {
                for (int i = 0; i < leftHeads.Length; i++)
                {
                    leftHeads[i].transform.position -= new Vector3(faceMoveSpeed * Time.fixedDeltaTime, 0, 0);
                }
            }
            else
            {
                moveLeftHeadsRight = false;
                leftHeads[leftWhichFace - 1].transform.position = leftStaticPhoto[0].transform.position;
                leftWhichFace -= 1;
                canLeftPress = true;
            }
        }
        if (moveRightHeadsLeft)
        {
            if (rightHeads[rightWhichFace + 1].transform.position.x > rightStaticPhoto[0].transform.position.x)
            {
                for (int i = 0; i < rightHeads.Length; i++)
                {
                    rightHeads[i].transform.position += new Vector3(faceMoveSpeed * Time.fixedDeltaTime, 0, 0);
                }
            }
            else
            {
                moveRightHeadsLeft= false;
                canRightPress = true;
                rightHeads[rightWhichFace + 1].transform.position = rightStaticPhoto[0].transform.position;
                rightWhichFace += 1;
            }
        }
        if (moveRightHeadsRight)
        {
            if (rightHeads[rightWhichFace - 1].transform.position.x < rightStaticPhoto[0].transform.position.x)
            {
                for (int i = 0; i < rightHeads.Length; i++)
                {
                    rightHeads[i].transform.position -= new Vector3(faceMoveSpeed * Time.fixedDeltaTime, 0, 0);
                }
            }
            else
            {
                moveRightHeadsRight = true;
                canRightPress = true;
                rightHeads[rightWhichFace - 1].transform.position = rightStaticPhoto[0].transform.position;
                rightWhichFace -= 1;
            }
        }
        if (moveLeftMiddlesLeft)
        {
            if (leftMiddles[leftWhichFace + 1].transform.position.x > leftStaticPhoto[1].transform.position.x)
            {
                for (int i = 0; i < leftMiddles.Length; i++)
                {
                    leftMiddles[i].transform.position += new Vector3(faceMoveSpeed * Time.fixedDeltaTime, 0, 0);
                }
            }
            else
            {
                canLeftPress= true;
                moveLeftMiddlesLeft = false;   
                leftMiddles[leftWhichFace + 1].transform.position = leftStaticPhoto[1].transform.position;
                leftWhichFace += 1;
            }

        }
        if (moveLeftMiddlesRight)
        {
            if(leftMiddles[leftWhichFace - 1].transform.position.x < leftStaticPhoto[1].transform.position.x)
            {
                for (int i = 0; i < leftMiddles.Length; i++)
                {
                    leftMiddles[i].transform.position -= new Vector3(faceMoveSpeed * Time.fixedDeltaTime, 0, 0);
                }
            }
            else
            {
                canLeftPress= true;
                moveLeftMiddlesRight= false;
                leftMiddles[leftWhichFace - 1].transform.position = leftStaticPhoto[1].transform.position;
                leftWhichFace -= 1;
            }
        }
        if (moveRightMiddlesLeft)
        {
            if (rightMiddles[rightWhichFace + 1].transform.position.x > rightStaticPhoto[1].transform.position.x)
            {
                for (int i = 0; i < rightMiddles.Length; i++)
                {
                    rightMiddles[i].transform.position += new Vector3(faceMoveSpeed * Time.fixedDeltaTime, 0, 0);
                }
            }
            else
            {
                canRightPress= true;
                moveRightMiddlesLeft = false;
                rightMiddles[rightWhichFace + 1].transform.position = rightStaticPhoto[1].transform.position;
                rightWhichFace += 1;
            }
        }
        if(moveRightMiddlesRight)
        {
            if (rightMiddles[rightWhichFace - 1].transform.position.x < rightStaticPhoto[1].transform.position.x)
            {
                for (int i = 0; i < rightMiddles.Length; i++)
                {
                    rightMiddles[i].transform.position -= new Vector3(faceMoveSpeed * Time.fixedDeltaTime, 0, 0);
                }
            }
            else
            {
                canRightPress = true;
                moveRightMiddlesRight = false;
                rightMiddles[rightWhichFace - 1].transform.position = rightStaticPhoto[1].transform.position;
                rightWhichFace -= 1;
            }
        }
        if(moveLeftTailsLeft)
        {
            if (leftTails[leftWhichFace + 1].transform.position.x > leftStaticPhoto[2].transform.position.x)
            {
                for (int i = 0; i < leftTails.Length; i++)
                {
                    leftTails[i].transform.position += new Vector3(faceMoveSpeed * Time.fixedDeltaTime, 0, 0);
                }
            }
            else
            {
                moveLeftTailsLeft = false;
                canLeftPress= true;
                leftTails[leftWhichFace + 1].transform.position = leftStaticPhoto[2].transform.position;
                leftWhichFace += 1;
            }

        }
        if (moveLeftTailsRight)
        {
            if (leftTails[leftWhichFace - 1].transform.position.x < leftStaticPhoto[2].transform.position.x)
            {
                for (int i = 0; i < leftTails.Length; i++)
                {
                    leftTails[i].transform.position -= new Vector3(faceMoveSpeed * Time.fixedDeltaTime, 0, 0);
                }
            }
            else
            {
                moveLeftTailsRight = false;
                canLeftPress = true;
                leftTails[leftWhichFace - 1].transform.position = leftStaticPhoto[2].transform.position;
                leftWhichFace -= 1;
            }
        }
        if(moveRightTailsLeft)
        {
            if (rightTails[rightWhichFace + 1].transform.position.x > rightStaticPhoto[2].transform.position.x)
            {
                for (int i = 0; i < rightTails.Length; i++)
                {
                    rightTails[i].transform.position += new Vector3(faceMoveSpeed * Time.fixedDeltaTime, 0, 0);
                }
            }
            else
            {
                moveRightTailsLeft = false;
                canRightPress= true;
                rightTails[rightWhichFace + 1].transform.position = rightStaticPhoto[2].transform.position;
                rightWhichFace += 1;
            }
        }
        if (moveRightTailsRight)
        {
            if (rightTails[rightWhichFace - 1].transform.position.x < rightStaticPhoto[2].transform.position.x)
            {
                for (int i = 0; i < rightTails.Length; i++)
                {
                    rightTails[i].transform.position -= new Vector3(faceMoveSpeed * Time.fixedDeltaTime, 0, 0);
                }
            }
            else
            {
                canRightPress = true;
                moveRightTailsRight = false;
                rightTails[rightWhichFace - 1].transform.position = rightStaticPhoto[2].transform.position;
                rightWhichFace -= 1;
            }
        }
    }

    void MixAndShowPhotos()
    {
        for (int i = 0; i < mainHeads.Length; i++)
        {
            if (heads[i].name == answer)
            {
                answerGO[0] = mainHeads[i];
                answerGO[1] = mainMiddles[i];
                answerGO[2] = mainTails[i];
                break;
            }
        }

        counter = 0;
        RandomNumber.MakhlootArray(heads);
        RandomNumber.MakhlootArray(middles);
        RandomNumber.MakhlootArray(tails);
        //int counter = 0;
        for (int i = 0; i < heads.Length + 1; i++)
        {
            if(i == (heads.Length + 1) / 2)
            {
                rightHeads[i] = rightStaticPhoto[0];
                leftHeads[i] = leftStaticPhoto[0];
                rightMiddles[i] = rightStaticPhoto[1];
                leftMiddles[i] = leftStaticPhoto[1];
                rightTails[i] = rightStaticPhoto[2];
                leftTails[i] = leftStaticPhoto[2];
                //continue;
            }
            else
            {
                rightHeads[i] = Instantiate(heads[counter], rightHeadPoses[i], heads[counter].transform.rotation, rightParent);
                rightHeads[i].name = heads[counter].name;
                rightHeads[i].SetActive(true);

                leftHeads[i] = Instantiate(heads[counter], leftHeadPoses[i], heads[counter].transform.rotation, leftParent);
                leftHeads[i].name = heads[counter].name;
                leftHeads[i].SetActive(true);

                rightMiddles[i] = Instantiate(middles[counter], rightMiddlePoses[i], middles[counter].transform.rotation, rightParent);
                rightMiddles[i].name = middles[counter].name;
                rightMiddles[i].SetActive(true);

                leftMiddles[i] = Instantiate(middles[counter], leftMiddlePoses[i], middles[counter].transform.rotation, leftParent);
                leftMiddles[i].name = middles[counter].name;
                leftMiddles[i].SetActive(true);

                rightTails[i] = Instantiate(tails[counter], rightTailPoses[i], tails[counter].transform.rotation, rightParent);
                rightTails[i].name = tails[counter].name;
                rightTails[i].SetActive(true);

                leftTails[i] = Instantiate(tails[counter], leftTailPoses[i], tails[counter].transform.rotation, leftParent);
                leftTails[i].name = tails[counter].name;
                leftTails[i].SetActive(true);
                counter += 1;
            }
        }

        //MoveMainHead();
        moveMainHeads = true;
        moveMainMiddles = true;
        moveMainTails = true;
        //MoveMainMiddle();
        //MoveMainTail();
        StartCoroutine(Wait());
        //MainPhotoAppears();
    }

    IEnumerator Wait()
    {
        yield return new WaitUntil(() => headPlaced && tailPlaced && middlePlaced);
        canLeftPress= true;
        canRightPress= true;
    }

    //void MoveMainHead()
    //{
    //    //Debug.Log(1);
    //    while (answerGO[0].transform.position.x < mainFramePositions[0].position.x)
    //    {
    //        //Debug.Log(2);
    //        for (int i = 0; i < mainHeads.Length; i++)
    //        {
    //            mainHeads[i].transform.position += new Vector3(firstFrameSpeed * Time.fixedDeltaTime, 0, 0);
    //        }
    //    }
    //    answerGO[0].transform.position = mainFramePositions[0].position;
    //    headPlaced = true;
    //}

    //void MoveMainTail()
    //{
    //    while (answerGO[2].transform.position.x < mainFramePositions[2].position.x)
    //    {
    //        for (int i = 0; i < mainTails.Length; i++)
    //        {
    //            mainTails[i].transform.position += new Vector3(secondFrameSpeed * Time.fixedDeltaTime, 0, 0);
    //        }
    //    }
    //    answerGO[2].transform.position = mainFramePositions[2].position;
    //    tailPlaced = true;
    //}

    //void MoveMainMiddle()
    //{
    //    while (answerGO[1].transform.position.x < mainFramePositions[1].position.x)
    //    {
    //        for (int i = 0; i < mainHeads.Length; i++)
    //        {
    //            mainMiddles[i].transform.position += new Vector3(thirdFrameSpeed * Time.fixedDeltaTime, 0, 0);
    //        }
    //    }
    //    answerGO[1].transform.position = mainFramePositions[1].position;
    //    middlePlaced= true;
    //}

    //void LeftHeadMove(int whichDirection)
    //{
    //    if(whichDirection == -1)
    //    {
    //        while (leftHeads[leftWhichFace + 1].transform.position.x > leftStaticPhoto[0].transform.position.x)
    //        {
    //            for (int i = 0; i < leftHeads.Length; i++)
    //            {
    //                leftHeads[i].transform.position += new Vector3(faceMoveSpeed * Time.fixedDeltaTime, 0, 0);
    //            }
    //        }
    //        leftHeads[leftWhichFace + 1].transform.position = leftStaticPhoto[0].transform.position;
    //        leftWhichFace += 1;
    //    }
    //    else
    //    {
    //        while (leftHeads[leftWhichFace - 1].transform.position.x > leftStaticPhoto[0].transform.position.x)
    //        {
    //            for (int i = 0; i < leftHeads.Length; i++)
    //            {
    //                leftHeads[i].transform.position -= new Vector3(faceMoveSpeed * Time.fixedDeltaTime, 0, 0);
    //            }
    //        }
    //        leftHeads[leftWhichFace - 1].transform.position = leftStaticPhoto[0].transform.position;
    //        leftWhichFace -= 1;
    //    }
    //    //leftHeads[leftWhichFace - 1].transform.position = leftStaticPhoto[0].transform.position;
    //    //leftWhichFace -= 1;
    //    canLeftPress = true;
    //    //answerGO[1].transform.position = mainFramePositions[1].position;
    //    //middlePlaced = true;
    //}

    //void RightHeadMove(int whichDirection)
    //{
    //    if (whichDirection == -1)
    //    {
    //        while (rightHeads[rightWhichFace + 1].transform.position.x > rightStaticPhoto[0].transform.position.x)
    //        {
    //            for (int i = 0; i < rightHeads.Length; i++)
    //            {
    //                rightHeads[i].transform.position += new Vector3(faceMoveSpeed * Time.fixedDeltaTime, 0, 0);
    //            }
    //        }
    //        rightHeads[rightWhichFace + 1].transform.position = rightStaticPhoto[0].transform.position;
    //        rightWhichFace += 1;
    //    }
    //    else
    //    {
    //        while (rightHeads[rightWhichFace - 1].transform.position.x < rightStaticPhoto[0].transform.position.x)
    //        {
    //            for (int i = 0; i < rightHeads.Length; i++)
    //            {
    //                rightHeads[i].transform.position -= new Vector3(faceMoveSpeed * Time.fixedDeltaTime, 0, 0);
    //            }
    //        }
    //        rightHeads[rightWhichFace - 1].transform.position = rightStaticPhoto[0].transform.position;
    //        rightWhichFace -= 1;
    //    }

    //    canRightPress = true;
    //}

    //void LeftMiddleMove(int whichDirection)
    //{
    //    if (whichDirection == -1)
    //    {
    //        while (leftMiddles[leftWhichFace + 1].transform.position.x > leftStaticPhoto[1].transform.position.x)
    //        {
    //            for (int i = 0; i < leftMiddles.Length; i++)
    //            {
    //                leftMiddles[i].transform.position += new Vector3(faceMoveSpeed * Time.fixedDeltaTime, 0, 0);
    //            }
    //        }
    //        leftMiddles[leftWhichFace + 1].transform.position = leftStaticPhoto[1].transform.position;
    //        leftWhichFace += 1;
    //    }
    //    else
    //    {
    //        while (leftMiddles[leftWhichFace - 1].transform.position.x < leftStaticPhoto[1].transform.position.x)
    //        {
    //            for (int i = 0; i < leftMiddles.Length; i++)
    //            {
    //                leftMiddles[i].transform.position -= new Vector3(faceMoveSpeed * Time.fixedDeltaTime, 0, 0);
    //            }
    //        }
    //        leftMiddles[leftWhichFace - 1].transform.position = leftStaticPhoto[1].transform.position;
    //        leftWhichFace -= 1;
    //    }
    //    //leftMiddles[currentFace + 1].transform.position = leftStaticPhoto[1].transform.position;
    //    //currentFace += 1;
    //    canLeftPress = true;
    //}

    //void RightMiddleMove(int whichDirection)
    //{
    //    if (whichDirection == -1)
    //    {
    //        while (rightMiddles[rightWhichFace + 1].transform.position.x > rightStaticPhoto[1].transform.position.x)
    //        {
    //            for (int i = 0; i < rightMiddles.Length; i++)
    //            {
    //                rightMiddles[i].transform.position += new Vector3(faceMoveSpeed * Time.fixedDeltaTime, 0, 0);
    //            }
    //        }
    //        rightMiddles[rightWhichFace + 1].transform.position = rightStaticPhoto[1].transform.position;
    //        rightWhichFace += 1;
    //    }
    //    else
    //    {
    //        while (rightMiddles[rightWhichFace - 1].transform.position.x < rightStaticPhoto[1].transform.position.x)
    //        {
    //            for (int i = 0; i < rightMiddles.Length; i++)
    //            {
    //                rightMiddles[i].transform.position -= new Vector3(faceMoveSpeed * Time.fixedDeltaTime, 0, 0);
    //            }
    //        }
    //        rightMiddles[rightWhichFace - 1].transform.position = rightStaticPhoto[1].transform.position;
    //        rightWhichFace -= 1;
    //    }

    //    canRightPress = true;
    //}

    //void LeftTailMove(int whichDirection)
    //{
    //    if (whichDirection == -1)
    //    {
    //        while (leftTails[leftWhichFace + 1].transform.position.x > leftStaticPhoto[2].transform.position.x)
    //        {
    //            for (int i = 0; i < leftTails.Length; i++)
    //            {
    //                leftTails[i].transform.position += new Vector3(faceMoveSpeed * Time.fixedDeltaTime, 0, 0);
    //            }
    //        }
    //        leftTails[leftWhichFace + 1].transform.position = leftStaticPhoto[2].transform.position;
    //        leftWhichFace += 1;
    //    }
    //    else
    //    {
    //        while (leftTails[leftWhichFace - 1].transform.position.x < leftStaticPhoto[2].transform.position.x)
    //        {
    //            for (int i = 0; i < leftTails.Length; i++)
    //            {
    //                leftTails[i].transform.position -= new Vector3(faceMoveSpeed * Time.fixedDeltaTime, 0, 0);
    //            }
    //        }
    //        leftTails[leftWhichFace - 1].transform.position = leftStaticPhoto[2].transform.position;
    //        leftWhichFace -= 1;
    //    }

    //    canLeftPress = true;
    //}

    //void RightTailMove(int whichDirection)
    //{
    //    if (whichDirection == -1)
    //    {
    //        while (rightTails[rightWhichFace + 1].transform.position.x > rightStaticPhoto[2].transform.position.x)
    //        {
    //            for (int i = 0; i < rightTails.Length; i++)
    //            {
    //                rightTails[i].transform.position += new Vector3(faceMoveSpeed * Time.fixedDeltaTime, 0, 0);
    //            }
    //        }
    //        rightTails[rightWhichFace + 1].transform.position = rightStaticPhoto[2].transform.position;
    //        rightWhichFace += 1;
    //    }
    //    else
    //    {
    //        while (rightTails[rightWhichFace - 1].transform.position.x < rightStaticPhoto[2].transform.position.x)
    //        {
    //            for (int i = 0; i < rightTails.Length; i++)
    //            {
    //                rightTails[i].transform.position -= new Vector3(faceMoveSpeed * Time.fixedDeltaTime, 0, 0);
    //            }
    //        }
    //        rightTails[rightWhichFace - 1].transform.position = rightStaticPhoto[2].transform.position;
    //        rightWhichFace -= 1;
    //    }

    //    canRightPress = true;
    //}

    public void PlayerOneUp()
    {
        if(whereIsLeftFrame != 0 && canLeftPress)
        {
            //move up
            Debug.Log(1);
            greenSingleFrame.transform.position += new Vector3(0, photoHeight, 0);
            whereIsLeftFrame += 1;
        }
    }

    public void PlayerTwoUp()
    {
        if(whereIsRightFrame != 0 && canRightPress)
        {
            redSingleFrame.transform.position += new Vector3(0, photoHeight, 0);
            whereIsRightFrame += 1;
        }
    }

    public void PlayerOneDown()
    {
        Debug.Log(3);

        if (whereIsLeftFrame != 2 && canLeftPress)
        {
            Debug.Log(2);

            greenSingleFrame.transform.position -= new Vector3(0, photoHeight, 0);
            whereIsLeftFrame -= 1;
        }
    }

    public void PlayerTwoDown()
    {
        if (whereIsRightFrame != 2 && canRightPress)
        {
            redSingleFrame.transform.position -= new Vector3(0, photoHeight, 0);
            whereIsRightFrame -= 1;
        }
    }

    public void PlayerOneLeft()
    {
            Debug.Log(5);

        if (leftWhichFace < howManyFace - 1 && canLeftPress)
        {
            Debug.Log(4);

            canLeftPress = false;
            leftWhichFace += 1;
            //playerOneAnswer[0] = 
            if(whereIsLeftFrame == 0)
            {
                playerOneAnswer[0] = leftHeads[leftWhichFace].name;
                //LeftHeadMove(-1);
                moveLeftHeadsLeft = true;
            }
            else if(whereIsLeftFrame == 1)
            {
                playerOneAnswer[1] = leftMiddles[leftWhichFace].name;
                //LeftMiddleMove(-1);
                moveLeftMiddlesLeft = true;
            }
            else
            {
                playerOneAnswer[2] = leftTails[leftWhichFace].name;
                //LeftTailMove(-1);
                moveLeftTailsLeft= true;
            }
            //move to next photo
            //while()
        }
    }

    public void PlayerTwoLeft()
    {
        if (rightWhichFace < howManyFace - 1 && canRightPress)
        {
            rightWhichFace += 1;
            if(whereIsRightFrame == 0)
            {
                playerTwoAnswer[0] = rightHeads[rightWhichFace].name;
                moveRightHeadsLeft= true;
                //RightHeadMove(-1);
            }
            else if(whereIsRightFrame == 1)
            {
                playerTwoAnswer[1] = rightMiddles[rightWhichFace].name;
                moveRightMiddlesLeft= true;
                //RightMiddleMove(-1);
            }
            else
            {
                playerTwoAnswer[2] = rightTails[rightWhichFace].name;
                moveRightTailsLeft = true;
                //RightTailMove(-1);
            }
            //move to next photo
        }
    }

    public void PlayerOneRight()
    {
        if (leftWhichFace > 0 && canLeftPress)
        {
            leftWhichFace += 1;
            if (whereIsLeftFrame == 0)
            {
                playerOneAnswer[0] = leftHeads[leftWhichFace].name;
                moveLeftHeadsRight = true;
                //LeftHeadMove(1);
            }
            else if (whereIsLeftFrame == 1)
            {
                playerOneAnswer[1] = leftMiddles[leftWhichFace].name;
                moveLeftMiddlesRight= true;
                //LeftMiddleMove(1);
            }
            else
            {
                playerOneAnswer[2] = leftTails[leftWhichFace].name;
                moveLeftTailsRight = true;
                //LeftTailMove(1);
            }
            //move to next photo
        }
    }

    public void PlayerTwoRight()
    {
        if (rightWhichFace > 0 && canRightPress)
        {
            rightWhichFace -= 1;
            if (whereIsRightFrame == 0)
            {
                playerTwoAnswer[0] = rightHeads[rightWhichFace].name;
                moveRightHeadsRight = true;
                //RightHeadMove(1);
            }
            else if (whereIsRightFrame == 1)
            {
                playerTwoAnswer[1] = rightMiddles[rightWhichFace].name;
                moveRightHeadsRight= true;
                //RightMiddleMove(1);
            }
            else
            {
                playerTwoAnswer[2] = rightTails[rightWhichFace].name;
                moveRightTailsRight= true;
                //RightTailMove(1);
            }
            //move to next photo
        }
    }

    public void PlayerOneSubmit()
    {
        if (canLeftPress)
        {
            canLeftPress = false;
            if(playerOneAnswer[0] == playerOneAnswer[1] && playerOneAnswer[1] == playerOneAnswer[2])
            {
                //javab doroste
                canRightPress = false;
                playerOneScore += 1;
                leftScore.text = playerOneScore.ToString();
                roundCounter += 1;
                if(roundCounter == howManyRounds)
                {
                    if(playerOneScore > playerTwoScore)
                    {
                        winner.text = "Left Player Won";
                    }
                    else if(playerOneScore < playerTwoScore)
                    {
                        winner.text = "Right Player Won";
                    }
                    else
                    {
                        winner.text = "Draw";
                    }
                }
                else
                {
                    StartCoroutine(FaceOffLevel());
                }
            }
            else
            {
                //qalat zade chi beshe?
                StartCoroutine(ShowCross(leftCross, -1));
            }
        }
    }

    public void PlayerTwoSubmit()
    {
        if (canRightPress)
        {
            canRightPress = false;
            if(playerTwoAnswer[0] == playerTwoAnswer[1] && playerTwoAnswer[1] == playerTwoAnswer[2])
            {
                //javab doros 
                //playerTwoScore += 1;
                //rightScore.text = playerTwoScore.ToString();
                canLeftPress = false;
                playerOneScore += 1;
                leftScore.text = playerOneScore.ToString();
                roundCounter += 1;
                if (roundCounter == howManyRounds)
                {
                    if (playerOneScore > playerTwoScore)
                    {
                        winner.text = "Left Player Won";
                    }
                    else if (playerOneScore < playerTwoScore)
                    {
                        winner.text = "Right Player Won";
                    }
                    else
                    {
                        winner.text = "Draw";
                    }
                }
                else
                {
                    StartCoroutine(FaceOffLevel());
                }
            }
            else
            {
                //qalat
                StartCoroutine(ShowCross(rightCross, 1));
            }
        }
    }

    IEnumerator ShowCross(GameObject whichCross, int whichSide)
    {
        whichCross.SetActive(true);
        yield return new WaitForSeconds(stayOnCrossTimer);
        if (whichSide == -1 && canRightPress)
        {
            canLeftPress = true;
            //canRightPress = true;
        }
        else if(whichSide == 1 && canLeftPress)
        {
            canRightPress = true;
        }
    }
    
    IEnumerator FaceOffLevel()
    {
        yield return new WaitForSeconds(betweenRoundTimer);
        answer = heads[turns[roundCounter]].name;
        //answer =
        MixAndShowPhotos();
    }
}
