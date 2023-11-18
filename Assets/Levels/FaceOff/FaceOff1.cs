using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FaceOff1 : MonoBehaviour
{
    [SerializeField] private AudioClip verticalMove;
    [SerializeField] private AudioClip horizontalMove;

    [SerializeField] private GameObject[] heads;
    [SerializeField] private GameObject[] middles;
    [SerializeField] private GameObject[] tails;
    //[SerializeField] private GameObject ready;
    //[SerializeField] private GameObject go;
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
    [SerializeField] private Transform[] rightPhotoPlaces;
    [SerializeField] private Transform[] leftPhotoPlaces;
    [SerializeField] private Transform[] mainFramePositions;
    [SerializeField] private float photoLength; // tule tasvir baraye kenar ham gozashtane tasavir va sakht makaneshun
    [SerializeField] private float photoLength08; // tule tasvir ba size .8 baraye tasvoire asli
    [SerializeField] private TextMeshProUGUI leftScore;
    [SerializeField] private TextMeshProUGUI rightScore;
    [SerializeField] private TextMeshProUGUI winner;
    //[SerializeField] private 

    private AudioSource audioSrc;
    private Vector3[] leftHeadPoses =  new Vector3[2]; //2 ta chap rast
    private Vector3[] leftMiddlePoses = new Vector3[2];
    private Vector3[] leftTailPoses = new Vector3[2];
    private Vector3[] rightHeadPoses = new Vector3[2];
    private Vector3[] rightMiddlePoses = new Vector3[2];
    private Vector3[] rightTailPoses = new Vector3[2];

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
    private int leftWhichFace1;
    private int leftWhichFace2;
    private int leftWhichFace3;
    private int rightWhichFace1;
    private int rightWhichFace2;
    private int rightWhichFace3;
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
    private float photoHeight; // ertefa tasvir baray harekate kadrha\
    private GameObject[] tmpheads;
    private GameObject[] tmptails;
    private GameObject[] tmpmiddles;

    private Vector3 leftHeadLeftPos;
    private Vector3 leftMiddleLeftPos;
    private Vector3 leftTailLeftPos;
    private Vector3 rightHeadLeftPos;
    private Vector3 rightMiddleLeftPos;
    private Vector3 rightTailLeftPos;
    private Vector3 leftHeadRightPos;
    private Vector3 leftMiddleRightPos;
    private Vector3 leftTailRightPos;
    private Vector3 rightHeadRightPos;
    private Vector3 rightMiddleRightPos;
    private Vector3 rightTailRightPos;
    // Start is called before the first frame update
    void Start()
    {
        rightHeads = new GameObject[heads.Length + 1];
        leftHeads = new GameObject[heads.Length + 1];
        rightMiddles = new GameObject[heads.Length + 1];
        leftMiddles = new GameObject[heads.Length + 1];
        rightTails = new GameObject[heads.Length + 1];
        leftTails = new GameObject[heads.Length + 1];

        audioSrc = GetComponent<AudioSource>();

        photoHeight = leftStaticPhoto[0].transform.position.y - leftStaticPhoto[1].transform.position.y;

        // show current place of the single frame
        leftWhichFace1 = leftWhichFace2 = leftWhichFace3 = (heads.Length + 1) / 2;
        rightWhichFace1 = rightWhichFace2 = rightWhichFace3 = (heads.Length + 1) / 2;

        howManyFace = heads.Length + 1;

        //the creation of main arrays
        mainHeads = tmpheads = new GameObject[heads.Length];
        mainMiddles = tmpmiddles = new GameObject[heads.Length];
        mainTails = tmptails = new GameObject[heads.Length];

        for (int i = 0; i < heads.Length; i++)
        {
            tmpheads[i] = heads[i];
            tmpmiddles[i] = middles[i];
            tmptails[i] = tails[i];
        }

        //creating photo selection turns
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
        RandomNumber.MakhlootArray(turns);

        leftHeadLeftPos = new Vector3(leftPhotoPlaces[0].position.x - photoLength, leftPhotoPlaces[0].position.y, 0);
        leftHeadRightPos = new Vector3(leftPhotoPlaces[0].position.x + photoLength, leftPhotoPlaces[0].position.y, 0);
        leftMiddleLeftPos = new Vector3(leftPhotoPlaces[1].position.x - photoLength, leftPhotoPlaces[1].position.y, 0);
        leftMiddleRightPos = new Vector3(leftPhotoPlaces[1].position.x + photoLength, leftPhotoPlaces[1].position.y, 0);
        leftTailLeftPos = new Vector3(leftPhotoPlaces[2].position.x - photoLength, leftPhotoPlaces[2].position.y, 0);
        leftTailRightPos = new Vector3(leftPhotoPlaces[2].position.x + photoLength, leftPhotoPlaces[2].position.y, 0);

        rightHeadLeftPos = new Vector3(rightPhotoPlaces[0].position.x - photoLength, rightPhotoPlaces[0].position.y, 0);
        rightHeadRightPos = new Vector3(rightPhotoPlaces[0].position.x + photoLength, rightPhotoPlaces[0].position.y, 0);
        rightMiddleLeftPos = new Vector3(rightPhotoPlaces[1].position.x - photoLength, rightPhotoPlaces[1].position.y, 0);
        rightMiddleRightPos = new Vector3(rightPhotoPlaces[1].position.x + photoLength, rightPhotoPlaces[1].position.y, 0);
        rightTailLeftPos = new Vector3(rightPhotoPlaces[2].position.x - photoLength, rightPhotoPlaces[2].position.y, 0);
        rightTailRightPos = new Vector3(rightPhotoPlaces[2].position.x + photoLength, rightPhotoPlaces[2].position.y, 0);

        answer = heads[turns[0]].name;
        MixAndShowPhotos();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (moveMainHeads)
        {
            if (answerGO[0].transform.position.x < mainFramePositions[0].position.x)
            {
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
                moveMainTails = false;
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
                moveMainMiddles = false;
                answerGO[1].transform.position = mainFramePositions[1].position;
                middlePlaced = true;
            }
        }
        if (moveLeftHeadsLeft)
        {
            // left has va left zade shode
            if (leftHeads[leftWhichFace1 + 1].transform.position.x > leftPhotoPlaces[0].transform.position.x)
            {
                //vasat va rast harekat
                leftHeads[leftWhichFace1].transform.position -= new Vector3(faceMoveSpeed * Time.fixedDeltaTime, 0, 0);
                leftHeads[leftWhichFace1 + 1].transform.position -= new Vector3(faceMoveSpeed * Time.fixedDeltaTime, 0, 0);

            }
            else
            {
                leftHeads[leftWhichFace1 + 1].transform.position = leftPhotoPlaces[0].transform.position;
                leftWhichFace1 += 1;
                moveLeftHeadsLeft = false;
                canLeftPress = true;
            }
        }
        if (moveLeftHeadsRight)
        {
            if (leftHeads[leftWhichFace1 - 1].transform.position.x < leftPhotoPlaces[0].transform.position.x)
            {
               leftHeads[leftWhichFace1].transform.position += new Vector3(faceMoveSpeed * Time.fixedDeltaTime, 0, 0);
               leftHeads[leftWhichFace1 - 1].transform.position += new Vector3(faceMoveSpeed * Time.fixedDeltaTime, 0, 0);
            }
            else
            {
                leftHeads[leftWhichFace1 - 1].transform.position = leftPhotoPlaces[0].transform.position;
                leftWhichFace1 -= 1;
                moveLeftHeadsRight = false;
                canLeftPress = true;
            }
        }
        if (moveRightHeadsLeft)
        {
            if (rightHeads[rightWhichFace1 + 1].transform.position.x > rightPhotoPlaces[0].transform.position.x)
            {
               rightHeads[rightWhichFace1 + 1].transform.position -= new Vector3(faceMoveSpeed * Time.fixedDeltaTime, 0, 0);
               rightHeads[rightWhichFace1].transform.position -= new Vector3(faceMoveSpeed * Time.fixedDeltaTime, 0, 0);
            }
            else
            {
                rightHeads[rightWhichFace1 + 1].transform.position = rightPhotoPlaces[0].transform.position;
                rightWhichFace1 += 1;
                moveRightHeadsLeft = false;
                canRightPress = true;
            }
        }
        if (moveRightHeadsRight)
        {
            if (rightHeads[rightWhichFace1 - 1].transform.position.x < rightPhotoPlaces[0].transform.position.x)
            {
                rightHeads[rightWhichFace1 - 1].transform.position += new Vector3(faceMoveSpeed * Time.fixedDeltaTime, 0, 0);
                rightHeads[rightWhichFace1].transform.position += new Vector3(faceMoveSpeed * Time.fixedDeltaTime, 0, 0);
            }
            else
            {
                rightHeads[rightWhichFace1 - 1].transform.position = rightPhotoPlaces[0].transform.position;
                rightWhichFace1 -= 1;
                moveRightHeadsRight = false;
                canRightPress = true;
            }
        }
        if (moveLeftMiddlesLeft)
        {
            if (leftMiddles[leftWhichFace2 + 1].transform.position.x > leftPhotoPlaces[1].transform.position.x)
            {
                //vasat va rast harekat                
                leftMiddles[leftWhichFace2 + 1].transform.position -= new Vector3(faceMoveSpeed * Time.fixedDeltaTime, 0, 0);
                leftMiddles[leftWhichFace2].transform.position -= new Vector3(faceMoveSpeed * Time.fixedDeltaTime, 0, 0);
            }
            else
            {
                leftMiddles[leftWhichFace2 + 1].transform.position = leftPhotoPlaces[1].transform.position;
                leftWhichFace2 += 1;
                moveLeftMiddlesLeft = false;
                canLeftPress = true;
            }
        }
        if (moveLeftMiddlesRight)
        {
            if (leftMiddles[leftWhichFace2 - 1].transform.position.x < leftPhotoPlaces[1].transform.position.x)
            {                
                leftMiddles[leftWhichFace2 - 1].transform.position += new Vector3(faceMoveSpeed * Time.fixedDeltaTime, 0, 0);
                leftMiddles[leftWhichFace2].transform.position += new Vector3(faceMoveSpeed * Time.fixedDeltaTime, 0, 0);
            }
            else
            {
                leftMiddles[leftWhichFace2 - 1].transform.position = leftPhotoPlaces[1].transform.position;
                leftWhichFace2 -= 1;
                canLeftPress = true;
                moveLeftMiddlesRight = false;
            }
        }
        if (moveRightMiddlesLeft)
        {
            if (rightMiddles[rightWhichFace2 + 1].transform.position.x > rightPhotoPlaces[1].transform.position.x)
            {
                rightMiddles[rightWhichFace2 + 1].transform.position -= new Vector3(faceMoveSpeed * Time.fixedDeltaTime, 0, 0);
                rightMiddles[rightWhichFace2].transform.position -= new Vector3(faceMoveSpeed * Time.fixedDeltaTime, 0, 0);
            }
            else
            {
                rightMiddles[rightWhichFace2 + 1].transform.position = rightPhotoPlaces[1].transform.position;
                rightWhichFace2 += 1;
                moveRightMiddlesLeft = false;
                canRightPress = true;
            }
        }
        if (moveRightMiddlesRight)
        {
            if (rightMiddles[rightWhichFace2 - 1].transform.position.x < rightPhotoPlaces[1].transform.position.x)
            {
                rightMiddles[rightWhichFace2 - 1].transform.position += new Vector3(faceMoveSpeed * Time.fixedDeltaTime, 0, 0);
                rightMiddles[rightWhichFace2].transform.position += new Vector3(faceMoveSpeed * Time.fixedDeltaTime, 0, 0);

            }
            else
            {
                rightMiddles[rightWhichFace2 - 1].transform.position = rightPhotoPlaces[1].transform.position;
                rightWhichFace2 -= 1;
                moveRightMiddlesRight = false;
                canRightPress = true;
            }
        }
        if (moveLeftTailsLeft)
        {
            if (leftTails[leftWhichFace3 + 1].transform.position.x > leftPhotoPlaces[2].transform.position.x)
            {
                //vasat va rast harekat
                leftTails[leftWhichFace3 + 1].transform.position -= new Vector3(faceMoveSpeed * Time.fixedDeltaTime, 0, 0);
                leftTails[leftWhichFace3].transform.position -= new Vector3(faceMoveSpeed * Time.fixedDeltaTime, 0, 0);
            }
            else
            {
                leftTails[leftWhichFace3 + 1].transform.position = leftPhotoPlaces[2].transform.position;
                leftWhichFace3 += 1;
                moveLeftTailsLeft = false;
                canLeftPress = true;
            }
        }
        if (moveLeftTailsRight)
        {
            if (leftTails[leftWhichFace3 - 1].transform.position.x < leftPhotoPlaces[2].transform.position.x)
            {
                leftTails[leftWhichFace3 - 1].transform.position += new Vector3(faceMoveSpeed * Time.fixedDeltaTime, 0, 0);
                leftTails[leftWhichFace3].transform.position += new Vector3(faceMoveSpeed * Time.fixedDeltaTime, 0, 0);
            }
            else
            {
                leftTails[leftWhichFace3 - 1].transform.position = leftPhotoPlaces[2].transform.position;
                leftWhichFace3 -= 1;
                canLeftPress = true;
                moveLeftTailsRight = false;
            }
        }
        if (moveRightTailsLeft)
        {
            if (rightTails[rightWhichFace3 + 1].transform.position.x > rightPhotoPlaces[2].transform.position.x)
            {
               rightTails[rightWhichFace3 + 1].transform.position -= new Vector3(faceMoveSpeed * Time.fixedDeltaTime, 0, 0);
               rightTails[rightWhichFace3].transform.position -= new Vector3(faceMoveSpeed * Time.fixedDeltaTime, 0, 0);
            }
            else
            {
                rightTails[rightWhichFace3 + 1].transform.position = rightPhotoPlaces[2].transform.position;
                rightWhichFace3 += 1;
                moveRightTailsLeft = false;
                canRightPress = true;
            }
        }
        if (moveRightTailsRight)
        {
            if (rightTails[rightWhichFace3 - 1].transform.position.x < rightPhotoPlaces[1].transform.position.x)
            {
               rightTails[rightWhichFace3 - 1].transform.position += new Vector3(faceMoveSpeed * Time.fixedDeltaTime, 0, 0);
               rightTails[rightWhichFace3].transform.position += new Vector3(faceMoveSpeed * Time.fixedDeltaTime, 0, 0);
            }
            else
            {
                rightTails[rightWhichFace3 - 1].transform.position = rightPhotoPlaces[2].transform.position;
                rightWhichFace3 -= 1;
                moveRightTailsRight = false;
                canRightPress = true;
            }
        }
    }

    void MixAndShowPhotos()
    {
        for (int i = 0; i < tmpheads.Length; i++)
        {
            mainHeads[i] = Instantiate(tmpheads[i], new Vector3(mainFramePositions[0].position.x
                - photoLength08 * (i + 1), mainFramePositions[0].position.y, 0), tmpheads[i].transform.rotation, mainFacesParent);
            //mainHeads[i].transform.localScale = new Vector3(0.9f, 0.9f, 1);
            mainHeads[i].name = tmpheads[i].name;
            mainHeads[i].transform.localScale = new Vector3(0.9f, 0.9f, 1);
            mainHeads[i].SetActive(true);

            mainMiddles[i] = Instantiate(tmpmiddles[i], new Vector3(mainFramePositions[1].position.x
                - photoLength08 * (i + 1), mainFramePositions[1].position.y, 0), tmpmiddles[i].transform.rotation, mainFacesParent);
            //mainHeads[i].transform.localScale = new Vector3(0.9f, 0.9f, 1);
            mainMiddles[i].name = tmpmiddles[i].name;
            mainMiddles[i].transform.localScale = new Vector3(0.9f, 0.9f, 1);
            mainMiddles[i].SetActive(true);

            mainTails[i] = Instantiate(tmptails[i], new Vector3(mainFramePositions[2].position.x
                 - photoLength08 * (i + 1), mainFramePositions[2].position.y, 0), tmptails[i].transform.rotation, mainFacesParent);
            //mainHeads[i].transform.localScale = new Vector3(0.9f, 0.9f, 1);
            mainTails[i].name = tmptails[i].name;
            mainTails[i].transform.localScale = new Vector3(0.9f, 0.9f, 1);
            mainTails[i].SetActive(true);
        }

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

        // faqat axe qabl o bad + vasat bashe
        for (int i = 0; i < heads.Length + 1; i++)
        {
            if (i == (heads.Length + 1) / 2)
            {
                rightHeads[i] = rightStaticPhoto[0];
                rightHeads[i].transform.position = rightPhotoPlaces[0].position;
                rightHeads[i].GetComponent<SpriteRenderer>().sortingOrder = 65;
                rightHeads[i].SetActive(true);

                leftHeads[i] = leftStaticPhoto[0];
                leftHeads[i].transform.position = leftPhotoPlaces[0].position;
                leftHeads[i].GetComponent<SpriteRenderer>().sortingOrder = 25;
                leftHeads[i].SetActive(true);

                rightMiddles[i] = rightStaticPhoto[1];
                rightMiddles[i].transform.position = rightPhotoPlaces[1].position;
                rightMiddles[i].GetComponent<SpriteRenderer>().sortingOrder = 65;
                rightMiddles[i].SetActive(true);

                leftMiddles[i] = leftStaticPhoto[1];
                leftMiddles[i].transform.position = leftPhotoPlaces[1].position;
                leftMiddles[i].GetComponent<SpriteRenderer>().sortingOrder = 25;
                leftMiddles[i].SetActive(true);

                rightTails[i] = rightStaticPhoto[2];
                rightTails[i].transform.position = rightPhotoPlaces[2].position;
                rightTails[i].GetComponent<SpriteRenderer>().sortingOrder = 65;
                rightTails[i].SetActive(true);

                leftTails[i] = leftStaticPhoto[2];
                leftTails[i].transform.position = leftPhotoPlaces[2].position;
                leftTails[i].GetComponent<SpriteRenderer>().sortingOrder = 25;
                leftTails[i].SetActive(true);
                //continue;
            }
            else
            {
                rightHeads[i] = Instantiate(heads[counter], rightHeadLeftPos, heads[counter].transform.rotation, rightParent);
                rightHeads[i].name = heads[counter].name;
                rightHeads[i].GetComponent<SpriteRenderer>().sortingOrder = 65;
                rightHeads[i].SetActive(true);

                leftHeads[i] = Instantiate(heads[counter], leftHeadLeftPos, heads[counter].transform.rotation, leftParent);
                leftHeads[i].name = heads[counter].name;
                leftHeads[i].GetComponent<SpriteRenderer>().sortingOrder = 25;
                leftHeads[i].SetActive(true);

                rightMiddles[i] = Instantiate(middles[counter], rightMiddleLeftPos, middles[counter].transform.rotation, rightParent);
                rightMiddles[i].name = middles[counter].name;
                rightMiddles[i].GetComponent<SpriteRenderer>().sortingOrder = 65;
                rightMiddles[i].SetActive(true);

                leftMiddles[i] = Instantiate(middles[counter], leftMiddleLeftPos, middles[counter].transform.rotation, leftParent);
                leftMiddles[i].name = middles[counter].name;
                leftMiddles[i].GetComponent<SpriteRenderer>().sortingOrder = 25;
                leftMiddles[i].SetActive(true);

                rightTails[i] = Instantiate(tails[counter], rightTailLeftPos, tails[counter].transform.rotation, rightParent);
                rightTails[i].name = tails[counter].name;
                rightTails[i].GetComponent<SpriteRenderer>().sortingOrder = 65;
                rightTails[i].SetActive(true);

                leftTails[i] = Instantiate(tails[counter], leftTailLeftPos, tails[counter].transform.rotation, leftParent);
                leftTails[i].name = tails[counter].name;
                leftTails[i].GetComponent<SpriteRenderer>().sortingOrder = 25;
                leftTails[i].SetActive(true);
                counter += 1;
            }
        }


        //counter += 1;

        moveMainHeads = true;
        moveMainMiddles = true;
        moveMainTails = true;
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitUntil(() => headPlaced && tailPlaced && middlePlaced);
        canLeftPress = true;
        canRightPress = true;
    }

    public void PlayerOneUp()
    {
        if (whereIsLeftFrame != 0 && canLeftPress)
        {
            audioSrc.clip = verticalMove;
            audioSrc.Play();
            greenSingleFrame.transform.position += new Vector3(0, photoHeight, 0);
            whereIsLeftFrame -= 1;
        }
    }

    public void PlayerTwoUp()
    {
        if (whereIsRightFrame != 0 && canRightPress)
        {
            audioSrc.clip = verticalMove;
            audioSrc.Play();
            redSingleFrame.transform.position += new Vector3(0, photoHeight, 0);
            whereIsRightFrame -= 1;
        }
    }

    public void PlayerOneDown()
    {
        if (whereIsLeftFrame != 2 && canLeftPress)
        {
            audioSrc.clip = verticalMove;
            audioSrc.Play();
            greenSingleFrame.transform.position -= new Vector3(0, photoHeight, 0);
            whereIsLeftFrame += 1;
        }
    }

    public void PlayerTwoDown()
    {
        if (whereIsRightFrame != 2 && canRightPress)
        {
            audioSrc.clip = verticalMove;
            audioSrc.Play();
            redSingleFrame.transform.position -= new Vector3(0, photoHeight, 0);
            whereIsRightFrame += 1;
        }
    }

    public void PlayerOneLeft()
    {
        if (canLeftPress)
        {
            audioSrc.clip = horizontalMove;
            audioSrc.Play();
            if (whereIsLeftFrame == 0)
            {
                if (leftWhichFace1 < howManyFace - 1)
                {
                    //Debug.Log(1);
                    leftHeads[leftWhichFace1 + 1].transform.position = leftHeadRightPos;
                    canLeftPress = false;
                    playerOneAnswer[0] = leftHeads[leftWhichFace1 + 1].name;
                    moveLeftHeadsLeft = true;
                }
            }
            else if (whereIsLeftFrame == 1)
            {
                if (leftWhichFace2 < howManyFace - 1)
                {
                    leftMiddles[leftWhichFace2 + 1].transform.position = leftMiddleRightPos;
                    canLeftPress = false;
                    playerOneAnswer[1] = leftMiddles[leftWhichFace2 + 1].name;
                    moveLeftMiddlesLeft = true;
                }
            }
            else
            {
                if (leftWhichFace3 < howManyFace - 1)
                {
                    leftTails[leftWhichFace3 + 1].transform.position = leftTailRightPos;
                    canLeftPress = false;
                    playerOneAnswer[2] = leftTails[leftWhichFace3 + 1].name;
                    moveLeftTailsLeft = true;
                }
            }
        }
    }

    public void PlayerTwoLeft()
    {
        if (canRightPress)
        {
            audioSrc.clip = horizontalMove;
            audioSrc.Play();
            //canRightPress = false;
            if (whereIsRightFrame == 0)
            {
                if (rightWhichFace1 < howManyFace - 1)
                {
                    rightHeads[rightWhichFace1 + 1].transform.position = rightHeadRightPos;
                    canRightPress = false;
                    playerTwoAnswer[0] = rightHeads[rightWhichFace1 + 1].name;
                    moveRightHeadsLeft = true;
                }
            }
            else if (whereIsRightFrame == 1)
            {
                if (rightWhichFace2 < howManyFace - 1)
                {
                    rightMiddles[rightWhichFace2 + 1].transform.position = rightMiddleRightPos;
                    canRightPress = false;
                    playerTwoAnswer[1] = rightMiddles[rightWhichFace2 + 1].name;
                    moveRightMiddlesLeft = true;
                }
            }
            else
            {
                if (rightWhichFace3 < howManyFace - 1)
                {
                    rightTails[rightWhichFace3 + 1].transform.position = rightTailRightPos;
                    canRightPress = false;
                    playerTwoAnswer[2] = rightTails[rightWhichFace3 + 1].name;
                    moveRightTailsLeft = true;
                }

            }
        }
    }

    public void PlayerOneRight()
    {
        if (canLeftPress)
        {
            audioSrc.clip = horizontalMove;
            audioSrc.Play();
            if (whereIsLeftFrame == 0)
            {
                if (leftWhichFace1 > 0)
                {
                    leftHeads[leftWhichFace1 - 1].transform.position = leftHeadLeftPos;
                    canLeftPress = false;
                    playerOneAnswer[0] = leftHeads[leftWhichFace1 - 1].name;
                    moveLeftHeadsRight = true;
                }
                //else
                //{

                //}
            }
            else if (whereIsLeftFrame == 1)
            {
                if (leftWhichFace2 > 0)
                {
                    leftMiddles[leftWhichFace2 - 1].transform.position = leftMiddleLeftPos;
                    canLeftPress = false;
                    playerOneAnswer[1] = leftMiddles[leftWhichFace2 - 1].name;
                    moveLeftMiddlesRight = true;
                }

            }
            else
            {
                if (leftWhichFace3 > 0)
                {
                    leftTails[leftWhichFace3 - 1].transform.position = leftTailLeftPos;
                    canLeftPress = false;
                    playerOneAnswer[2] = leftTails[leftWhichFace3 - 1].name;
                    moveLeftTailsRight = true;
                }
            }
        }

    }

    public void PlayerTwoRight()
    {
        if (canRightPress)
        {
            audioSrc.clip = horizontalMove;
            audioSrc.Play();
            if (whereIsRightFrame == 0)
            {
                if (rightWhichFace1 > 0)
                {
                    rightHeads[rightWhichFace1 - 1].transform.position = rightHeadLeftPos;
                    canRightPress = false;
                    playerTwoAnswer[0] = rightHeads[rightWhichFace1 - 1].name;
                    moveRightHeadsRight = true;
                }
            }
            else if (whereIsRightFrame == 1)
            {
                if (rightWhichFace2 > 0)
                {
                    rightMiddles[rightWhichFace2 - 1].transform.position = rightMiddleLeftPos;
                    canRightPress = false;
                    playerTwoAnswer[1] = rightMiddles[rightWhichFace2 - 1].name;
                    moveRightMiddlesRight = true;
                }
            }
            else
            {
                if (rightWhichFace3 > 0)
                {
                    rightTails[rightWhichFace3 - 1].transform.position = rightTailLeftPos;
                    canRightPress = false;
                    playerTwoAnswer[2] = rightTails[rightWhichFace3 - 1].name;
                    moveRightTailsRight = true;
                }
            }
        }
    }

    public void PlayerOneSubmit()
    {
        //Debug.Log(1);
        if (canLeftPress)
        {
            canLeftPress = false;
            if (playerOneAnswer[0] == playerOneAnswer[1] && playerOneAnswer[1] == playerOneAnswer[2]
                && playerOneAnswer[0] != null && playerOneAnswer[1] != null && playerOneAnswer[2] != null)
            {
                //javab doroste
                canRightPress = false;
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
                StartCoroutine(ShowCross(leftCross, -1));
            }
        }
    }

    public void PlayerTwoSubmit()
    {
        if (canRightPress)
        {
            canRightPress = false;
            if (playerTwoAnswer[0] == playerTwoAnswer[1] && playerTwoAnswer[1] == playerTwoAnswer[2]
                &&
                playerTwoAnswer[0] != null && playerTwoAnswer[1] != null && playerTwoAnswer[2] != null)
            {
                canLeftPress = false;
                playerTwoScore += 1;
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
        whichCross.SetActive(false);
        if (whichSide == -1 && canRightPress)
        {
            canLeftPress = true;
        }
        else if (whichSide == 1 && canLeftPress)
        {
            canRightPress = true;
        }
    }

    IEnumerator FaceOffLevel()
    {
        yield return new WaitForSeconds(betweenRoundTimer);
        canRightPress = false;
        canLeftPress = false;
        moveRightHeadsLeft = false;
        moveRightHeadsRight = false;
        moveRightMiddlesLeft = false;
        moveRightMiddlesRight = false;
        moveRightTailsLeft = false;
        moveRightTailsRight = false;
        moveLeftHeadsLeft = false;
        moveLeftHeadsRight = false;
        moveLeftMiddlesLeft = false;
        moveLeftMiddlesRight = false;
        moveLeftTailsLeft = false;
        moveLeftTailsRight = false;
        leftWhichFace1 = leftWhichFace2 = leftWhichFace3 = (heads.Length + 1) / 2;
        rightWhichFace1 = rightWhichFace2 = rightWhichFace3 = (heads.Length + 1) / 2;
        for (int i = 0; i < leftHeads.Length; i++)
        {
            if (leftHeads[i].name != "StaticPic")
            {
                Destroy(leftHeads[i]);
                Destroy(rightHeads[i]);
                Destroy(leftMiddles[i]);
                Destroy(rightMiddles[i]);
                Destroy(leftTails[i]);
                Destroy(rightTails[i]);
            }
        }
        for (int i = 0; i < heads.Length; i++)
        {
            Destroy(mainHeads[i]);
            Destroy(mainMiddles[i]);
            Destroy(mainTails[i]);
        }
        answer = heads[turns[roundCounter]].name;
        MixAndShowPhotos();
    }
}
