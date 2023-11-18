using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FaceOff : MonoBehaviour
{
    //music
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
    //IEnumerator Ready()
    //{
    //    ready.SetActive(true);
    //    yield return new WaitForSeconds(readyTimer);
    //    ready.SetActive(false);
    //    go.SetActive(true);
    //    yield return new WaitForSeconds(goTimer);
    //    go.SetActive(false);
    //    answer = heads[turns[0]].name;
    //    MixAndShowPhotos();
    //    //StartCoroutine(Clock());
    //}

    void RightPartMove(int middleImgIndex, string whichLine, int whichDirection)
    {
        int j = 1;
        if (whichLine == "Head")
        {
            for (int i = 0; i < middleImgIndex; i++)
            {
                rightHeads[i].transform.position = rightHeadPoses[middleImgIndex + i];
            }
            for (int i = middleImgIndex + 1; i < rightHeads.Length; i++)
            {
                if(middleImgIndex + i >= rightHeads.Length)
                {
                    rightHeads[i].transform.position = rightHeadPoses[rightHeadPoses.Length - 1] +
                        new Vector3(whichDirection * j * photoLength, 0, 0);
                    j += 1;
                }
                else
                {
                    rightHeads[i].transform.position = rightHeadPoses[middleImgIndex + i];
                }
            }
        }

        j = 1;
        if(whichLine == "Middle")
        {
            for (int i = 0; i < middleImgIndex; i++)
            {
                rightMiddles[i].transform.position = rightMiddlePoses[middleImgIndex + i];
            }
            for (int i = middleImgIndex + 1; i < rightMiddles.Length; i++)
            {
                if (middleImgIndex + i >= rightMiddles.Length)
                {
                    rightMiddles[i].transform.position = rightMiddlePoses[rightMiddlePoses.Length - 1] +
                        new Vector3(whichDirection * j * photoLength, 0, 0);
                    j += 1;
                }
                else
                {
                    rightMiddles[i].transform.position = rightMiddlePoses[middleImgIndex + i];
                }
            }
        }

        j = 1;

        if (whichLine == "Tail")
        {
            for (int i = 0; i < middleImgIndex; i++)
            {
                rightTails[i].transform.position = rightTailPoses[middleImgIndex + i];
            }
            for (int i = middleImgIndex + 1; i < rightTails.Length; i++)
            {
                if (middleImgIndex + i >= rightTails.Length)
                {
                    rightTails[i].transform.position = rightTailPoses[rightTailPoses.Length - 1] +
                        new Vector3(whichDirection * j * photoLength, 0, 0);
                    j += 1;
                }
                else
                {
                    rightTails[i].transform.position = rightTailPoses[middleImgIndex + i];
                }
            }
        }
    }

    void LeftPartMove(int middleImgIndex, string whichLine, int whichDirection)
    {
        int j = 1;
        if (whichLine == "Head")
        {
            for (int i = 0; i < middleImgIndex; i++)
            {
                leftHeads[i].transform.position = leftHeadPoses[middleImgIndex + i];
            }
            for (int i = middleImgIndex + 1; i < leftHeads.Length; i++)
            {
                if (middleImgIndex + i >= leftHeads.Length)
                {
                    leftHeads[i].transform.position = leftHeadPoses[leftHeadPoses.Length - 1] +
                        new Vector3(whichDirection * j * photoLength, 0, 0);
                    j += 1;
                }
                else
                {
                    leftHeads[i].transform.position = leftHeadPoses[middleImgIndex + i];
                }
            }
        }

        if (whichLine == "Middle")
        {
            for (int i = 0; i < middleImgIndex; i++)
            {
                leftMiddles[i].transform.position = leftMiddlePoses[middleImgIndex + i];
            }
            for (int i = middleImgIndex + 1; i < leftMiddles.Length; i++)
            {
                if (middleImgIndex + i >= leftMiddles.Length)
                {
                    leftMiddles[i].transform.position = leftMiddlePoses[leftMiddlePoses.Length - 1] +
                        new Vector3(whichDirection * j * photoLength, 0, 0);
                    j += 1;
                }
                else
                {
                    leftMiddles[i].transform.position = leftMiddlePoses[middleImgIndex + i];
                }
            }
        }

        if (whichLine == "Tail")
        {
            for (int i = 0; i < middleImgIndex; i++)
            {
                leftTails[i].transform.position = leftTailPoses[middleImgIndex + i];
            }
            for (int i = middleImgIndex + 1; i < leftTails.Length; i++)
            {
                if (middleImgIndex + i >= leftTails.Length)
                {
                    leftTails[i].transform.position = leftTailPoses[leftTailPoses.Length - 1] +
                        new Vector3(whichDirection * j * photoLength, 0, 0);
                    j += 1;
                }
                else
                {
                    leftTails[i].transform.position = leftTailPoses[middleImgIndex + i];
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSrc = GetComponent<AudioSource>();

        photoHeight = leftStaticPhoto[0].transform.position.y - leftStaticPhoto[1].transform.position.y;

        // show current place of the single frame
        leftWhichFace1 = leftWhichFace2 = leftWhichFace3 = (heads.Length + 1) / 2;
        rightWhichFace1 = rightWhichFace2 = rightWhichFace3 = (heads.Length + 1) / 2;

        howManyFace = heads.Length + 1;

        //the creation of picsHolder arrays
        rightHeads = new GameObject[heads.Length + 1];
        leftHeads = new GameObject[heads.Length + 1];
        rightMiddles = new GameObject[heads.Length + 1];
        leftMiddles= new GameObject[heads.Length + 1];
        rightTails= new GameObject[heads.Length + 1];
        leftTails = new GameObject[heads.Length + 1];

        //the creation of pics places arrays
        leftHeadPoses = new Vector3[heads.Length + 1];
        leftMiddlePoses = new Vector3[heads.Length + 1];
        leftTailPoses = new Vector3[heads.Length + 1];
        rightHeadPoses = new Vector3[heads.Length + 1];
        rightMiddlePoses = new Vector3[heads.Length + 1];
        rightTailPoses = new Vector3[heads.Length + 1];

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

        //pictures poses for let and right creation
        for (int i = -(heads.Length / 2); i <= heads.Length / 2; i++)
        {
            //if(i == 0)
            //{
            //    leftHeadPoses[heads.Length / 2] = leftPhotoPlaces[0].position;
            //    rightHeadPoses[heads.Length / 2] = rightPhotoPlaces[0].position;
            //    leftMiddlePoses[heads.Length / 2] = leftPhotoPlaces[1].position;
            //    rightMiddlePoses[heads.Length / 2] = rightPhotoPlaces[1].position;
            //    leftTailPoses[heads.Length / 2] = leftPhotoPlaces[2].position;
            //    rightTailPoses[heads.Length / 2] = rightPhotoPlaces[2].position;
            //    continue;
            //}
            leftHeadPoses[i + heads.Length / 2] = new Vector3(leftPhotoPlaces[0].position.x + i * photoLength, leftPhotoPlaces[0].position.y, 0);
            rightHeadPoses[i + heads.Length / 2] = new Vector3(rightPhotoPlaces[0].position.x + i * photoLength, rightPhotoPlaces[0].position.y, 0);
            leftMiddlePoses[i + heads.Length / 2] = new Vector3(leftPhotoPlaces[1].position.x + i * photoLength, leftPhotoPlaces[1].position.y, 0);
            rightMiddlePoses[i + heads.Length / 2] = new Vector3(rightPhotoPlaces[1].position.x + i * photoLength, rightPhotoPlaces[1].position.y, 0);
            leftTailPoses[i + heads.Length / 2] = new Vector3(leftPhotoPlaces[2].position.x + i * photoLength, leftPhotoPlaces[2].position.y, 0);
            rightTailPoses[i + heads.Length / 2] = new Vector3(rightPhotoPlaces[2].position.x + i * photoLength, rightPhotoPlaces[2].position.y, 0);
        }

        answer = heads[turns[0]].name;
        MixAndShowPhotos();

        //StartCoroutine(Ready());
    }

    // Update is called once per frame
    void FixedUpdate()
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
            if (leftHeads[leftWhichFace1 + 1].transform.position.x > leftPhotoPlaces[0].transform.position.x)
            {
                //Debug.Log(1);
                for (int i = 0; i < leftHeads.Length; i++)
                {
                    leftHeads[i].transform.position -= new Vector3(faceMoveSpeed * Time.fixedDeltaTime, 0, 0);
                }
            }
            else
            {
                leftHeads[leftWhichFace1 + 1].transform.position = leftPhotoPlaces[0].transform.position;
                leftWhichFace1 += 1;
                LeftPartMove(leftWhichFace1, "Head", 1);
                moveLeftHeadsLeft = false;
                canLeftPress = true;
            }
        }
        if (moveLeftHeadsRight)
        {
            if (leftHeads[leftWhichFace1 - 1].transform.position.x < leftPhotoPlaces[0].transform.position.x)
            {
                for (int i = 0; i < leftHeads.Length; i++)
                {
                    leftHeads[i].transform.position += new Vector3(faceMoveSpeed * Time.fixedDeltaTime, 0, 0);
                }
            }
            else
            {
                leftHeads[leftWhichFace1 - 1].transform.position = leftPhotoPlaces[0].transform.position;
                leftWhichFace1 -= 1;
                LeftPartMove(leftWhichFace1, "Head", -1);
                moveLeftHeadsRight = false;
                canLeftPress = true;
            }
        }
        if (moveRightHeadsLeft)
        {
            if (rightHeads[rightWhichFace1 + 1].transform.position.x > rightPhotoPlaces[0].transform.position.x)
            {
                for (int i = 0; i < rightHeads.Length; i++)
                {
                    rightHeads[i].transform.position -= new Vector3(faceMoveSpeed * Time.fixedDeltaTime, 0, 0);
                }
            }
            else
            {
                rightHeads[rightWhichFace1 + 1].transform.position = rightPhotoPlaces[0].transform.position;
                rightWhichFace1 += 1;
                RightPartMove(rightWhichFace1, "Head", 1);

                moveRightHeadsLeft = false;
                canRightPress = true;
            }
        }
        if (moveRightHeadsRight)
        {
            if (rightHeads[rightWhichFace1 - 1].transform.position.x < rightPhotoPlaces[0].transform.position.x)
            {
                for (int i = 0; i < rightHeads.Length; i++)
                {
                    rightHeads[i].transform.position += new Vector3(faceMoveSpeed * Time.fixedDeltaTime, 0, 0);
                }
            }
            else
            {
                rightHeads[rightWhichFace1 - 1].transform.position = rightPhotoPlaces[0].transform.position;
                rightWhichFace1 -= 1;
                RightPartMove(rightWhichFace1, "Head", -1);

                moveRightHeadsRight = false;
                canRightPress = true;
            }
        }
        if (moveLeftMiddlesLeft)
        {
            if (leftMiddles[leftWhichFace2 + 1].transform.position.x > leftPhotoPlaces[1].transform.position.x)
            {
                for (int i = 0; i < leftMiddles.Length; i++)
                {
                    leftMiddles[i].transform.position -= new Vector3(faceMoveSpeed * Time.fixedDeltaTime, 0, 0);
                }
            }
            else
            {
                leftMiddles[leftWhichFace2 + 1].transform.position = leftPhotoPlaces[1].transform.position;
                leftWhichFace2 += 1;
                LeftPartMove(leftWhichFace2, "Middle", 1);

                moveLeftMiddlesLeft = false;   
                canLeftPress= true;
            }

        }
        if (moveLeftMiddlesRight)
        {
            if(leftMiddles[leftWhichFace2 - 1].transform.position.x < leftPhotoPlaces[1].transform.position.x)
            {
                for (int i = 0; i < leftMiddles.Length; i++)
                {
                    leftMiddles[i].transform.position += new Vector3(faceMoveSpeed * Time.fixedDeltaTime, 0, 0);
                }
            }
            else
            {
                leftMiddles[leftWhichFace2 - 1].transform.position = leftPhotoPlaces[1].transform.position;
                leftWhichFace2 -= 1;
                LeftPartMove(leftWhichFace2, "Middle", -1);

                moveLeftMiddlesRight = false;
                canLeftPress= true;
            }
        }
        if (moveRightMiddlesLeft)
        {
            if (rightMiddles[rightWhichFace2 + 1].transform.position.x > rightPhotoPlaces[1].transform.position.x)
            {
                for (int i = 0; i < rightMiddles.Length; i++)
                {
                    rightMiddles[i].transform.position -= new Vector3(faceMoveSpeed * Time.fixedDeltaTime, 0, 0);
                }
            }
            else
            {
                rightMiddles[rightWhichFace2 + 1].transform.position = rightPhotoPlaces[1].transform.position;
                rightWhichFace2 += 1;
                RightPartMove(rightWhichFace2, "Middle", 1);

                moveRightMiddlesLeft = false;
                canRightPress= true;
            }
        }
        if(moveRightMiddlesRight)
        {
            if (rightMiddles[rightWhichFace2 - 1].transform.position.x < rightPhotoPlaces[1].transform.position.x)
            {
                for (int i = 0; i < rightMiddles.Length; i++)
                {
                    rightMiddles[i].transform.position += new Vector3(faceMoveSpeed * Time.fixedDeltaTime, 0, 0);
                }
            }
            else
            {
                rightMiddles[rightWhichFace2 - 1].transform.position = rightPhotoPlaces[1].transform.position;
                rightWhichFace2 -= 1;
                RightPartMove(rightWhichFace2, "Middle", -1);

                moveRightMiddlesRight = false;
                canRightPress = true;
            }
        }
        if(moveLeftTailsLeft)
        {
            if (leftTails[leftWhichFace3 + 1].transform.position.x > leftPhotoPlaces[2].transform.position.x)
            {
                for (int i = 0; i < leftTails.Length; i++)
                {
                    leftTails[i].transform.position -= new Vector3(faceMoveSpeed * Time.fixedDeltaTime, 0, 0);
                }
            }
            else
            {
                leftTails[leftWhichFace3 + 1].transform.position = leftPhotoPlaces[2].transform.position;
                leftWhichFace3 += 1;
                LeftPartMove(leftWhichFace3, "Tail", 1);

                moveLeftTailsLeft = false;
                canLeftPress= true;
            }

        }
        if (moveLeftTailsRight)
        {
            if (leftTails[leftWhichFace3 - 1].transform.position.x < leftPhotoPlaces[2].transform.position.x)
            {
                for (int i = 0; i < leftTails.Length; i++)
                {
                    leftTails[i].transform.position += new Vector3(faceMoveSpeed * Time.fixedDeltaTime, 0, 0);
                }
            }
            else
            {
                leftTails[leftWhichFace3 - 1].transform.position = leftPhotoPlaces[2].transform.position;
                leftWhichFace3 -= 1;
                LeftPartMove(leftWhichFace3, "Tail", -1);

                moveLeftTailsRight = false;
                canLeftPress = true;
            }
        }
        if(moveRightTailsLeft)
        {
            if (rightTails[rightWhichFace3 + 1].transform.position.x > rightPhotoPlaces[2].transform.position.x)
            {
                for (int i = 0; i < rightTails.Length; i++)
                {
                    rightTails[i].transform.position -= new Vector3(faceMoveSpeed * Time.fixedDeltaTime, 0, 0);
                }
            }
            else
            {
                rightTails[rightWhichFace3 + 1].transform.position = rightPhotoPlaces[2].transform.position;
                rightWhichFace3 += 1;
                RightPartMove(rightWhichFace3, "Tail", 1);

                moveRightTailsLeft = false;
                canRightPress= true;
            }
        }
        if (moveRightTailsRight)
        {
            if (rightTails[rightWhichFace3 - 1].transform.position.x < rightPhotoPlaces[2].transform.position.x)
            {
                for (int i = 0; i < rightTails.Length; i++)
                {
                    rightTails[i].transform.position += new Vector3(faceMoveSpeed * Time.fixedDeltaTime, 0, 0);
                }
            }
            else
            {
                rightTails[rightWhichFace3 - 1].transform.position = rightPhotoPlaces[2].transform.position;
                rightWhichFace3 -= 1;
                RightPartMove(rightWhichFace3, "Tail", -1);

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
        for (int i = 0; i < heads.Length + 1; i++)
        {
            if(i == (heads.Length + 1) / 2)
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
                rightHeads[i] = Instantiate(heads[counter], rightHeadPoses[i], heads[counter].transform.rotation, rightParent);
                //rightHeads[i].transform.localScale = new Vector3(0.9f, 0.9f, 1);
                rightHeads[i].name = heads[counter].name;
                rightHeads[i].GetComponent<SpriteRenderer>().sortingOrder = 65;
                rightHeads[i].SetActive(true);

                leftHeads[i] = Instantiate(heads[counter], leftHeadPoses[i], heads[counter].transform.rotation, leftParent);
                //leftHeads[i].transform.localScale = new Vector3(0.9f, 0.9f, 1);
                leftHeads[i].name = heads[counter].name;
                leftHeads[i].GetComponent<SpriteRenderer>().sortingOrder = 25;
                leftHeads[i].SetActive(true);

                rightMiddles[i] = Instantiate(middles[counter], rightMiddlePoses[i], middles[counter].transform.rotation, rightParent);
                //rightMiddles[i].transform.localScale = new Vector3(0.9f, 0.9f, 1);
                rightMiddles[i].name = middles[counter].name;
                rightMiddles[i].GetComponent<SpriteRenderer>().sortingOrder = 65;
                rightMiddles[i].SetActive(true);

                leftMiddles[i] = Instantiate(middles[counter], leftMiddlePoses[i], middles[counter].transform.rotation, leftParent);
                //leftMiddles[i].transform.localScale = new Vector3(0.9f, 0.9f, 1);
                leftMiddles[i].name = middles[counter].name;
                leftMiddles[i].GetComponent<SpriteRenderer>().sortingOrder = 25;
                leftMiddles[i].SetActive(true);

                rightTails[i] = Instantiate(tails[counter], rightTailPoses[i], tails[counter].transform.rotation, rightParent);
                //rightTails[i].transform.localScale = new Vector3(0.9f, 0.9f, 1);
                rightTails[i].name = tails[counter].name;
                rightTails[i].GetComponent<SpriteRenderer>().sortingOrder = 65;
                rightTails[i].SetActive(true);

                leftTails[i] = Instantiate(tails[counter], leftTailPoses[i], tails[counter].transform.rotation, leftParent);
                //leftTails[i].transform.localScale = new Vector3(0.9f, 0.9f, 1);
                leftTails[i].name = tails[counter].name;
                leftTails[i].GetComponent<SpriteRenderer>().sortingOrder = 25;
                leftTails[i].SetActive(true);
                counter += 1;
            }
        }

        moveMainHeads = true;
        moveMainMiddles = true;
        moveMainTails = true;
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitUntil(() => headPlaced && tailPlaced && middlePlaced);
        canLeftPress= true;
        canRightPress= true;
    }

    #region commented
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
    #endregion
    public void PlayerOneUp()
    {
        if(whereIsLeftFrame != 0 && canLeftPress)
        {
            audioSrc.clip = verticalMove;
            audioSrc.Play();
            //canLeftPress = false;
            greenSingleFrame.transform.position += new Vector3(0, photoHeight, 0);
            whereIsLeftFrame -= 1;
            //canLeftPress = true;
        }
    }

    public void PlayerTwoUp()
    {
        if(whereIsRightFrame != 0 && canRightPress)
        {
            audioSrc.clip = verticalMove;
            audioSrc.Play();
            //canRightPress = false;
            redSingleFrame.transform.position += new Vector3(0, photoHeight, 0);
            whereIsRightFrame -= 1;
            //canRightPress = true;
        }
    }

    public void PlayerOneDown()
    {
        if (whereIsLeftFrame != 2 && canLeftPress)
        {
            audioSrc.clip = verticalMove;
            audioSrc.Play();
            //canLeftPress = false;
            greenSingleFrame.transform.position -= new Vector3(0, photoHeight, 0);
            whereIsLeftFrame += 1;
            //canLeftPress = true;
        }
    }

    public void PlayerTwoDown()
    {
        if (whereIsRightFrame != 2 && canRightPress)
        {
            audioSrc.clip = verticalMove;
            audioSrc.Play();
            //canRightPress = false;
            redSingleFrame.transform.position -= new Vector3(0, photoHeight, 0);
            whereIsRightFrame += 1;
            //canRightPress = true;
        }
    }

    public void PlayerOneLeft()
    {
        if(canLeftPress)
        {
            audioSrc.clip = horizontalMove;
            audioSrc.Play();
            //canLeftPress = false;
            if (whereIsLeftFrame == 0)
            {
                if (leftWhichFace1 < howManyFace - 1)
                {
                    canLeftPress = false;
                    playerOneAnswer[0] = leftHeads[leftWhichFace1 + 1].name;
                    moveLeftHeadsLeft = true;
                }
            }
            else if (whereIsLeftFrame == 1)
            {
                if (leftWhichFace2 < howManyFace - 1)
                {
                    canLeftPress = false;
                    playerOneAnswer[1] = leftMiddles[leftWhichFace2 + 1].name;
                    moveLeftMiddlesLeft = true;
                }
            }
            else 
            {
                if (leftWhichFace3 < howManyFace - 1)
                {
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
                    canRightPress = false;
                    playerTwoAnswer[0] = rightHeads[rightWhichFace1 + 1].name;
                    moveRightHeadsLeft = true;
                }
            }
            else if (whereIsRightFrame == 1)
            {
                if (rightWhichFace2 < howManyFace - 1)
                {
                    canRightPress = false;
                    playerTwoAnswer[1] = rightMiddles[rightWhichFace2 + 1].name;
                    moveRightMiddlesLeft = true;
                }
            }
            else
            {
                if (rightWhichFace3 < howManyFace - 1)
                {
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
                    canLeftPress = false;
                    playerOneAnswer[1] = leftMiddles[leftWhichFace2 - 1].name;
                    moveLeftMiddlesRight = true;
                }

            }
            else
            {
                if (leftWhichFace3 > 0)
                {
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
                    canRightPress = false;
                    playerTwoAnswer[0] = rightHeads[rightWhichFace1 - 1].name;
                    moveRightHeadsRight = true;
                }
            }
            else if (whereIsRightFrame == 1)
            {
                if (rightWhichFace2 > 0)
                {
                    canRightPress = false;
                    playerTwoAnswer[1] = rightMiddles[rightWhichFace2 - 1].name;
                    moveRightMiddlesRight = true;
                }
            }
            else
            {
                if (rightWhichFace3 > 0)
                {
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
        else if(whichSide == 1 && canLeftPress)
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
