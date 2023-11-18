using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DressUp : MonoBehaviour
{
    public static string lCurrentPhoto;
    public static string rCurrentPhoto;

    //[SerializeField] private float photoLength;
    [SerializeField] private float aimBoxSpeed;
    [SerializeField] private float aimBoxTime;
    [SerializeField] private float onXTime;
    [SerializeField] private float[] speeds;

    [SerializeField] private Transform secondLeftHatsPos;
    [SerializeField] private Transform secondLeftClothesPos;
    [SerializeField] private Transform secondLeftShoesPos;
    [SerializeField] private Transform firstRightHatsPos;
    [SerializeField] private Transform firstRightClothesPos;
    [SerializeField] private Transform firstRightShoesPos;
    [SerializeField] private Transform secondRightHatsPos;
    [SerializeField] private Transform secondRightClothesPos;
    [SerializeField] private Transform secondRightShoesPos;
    [SerializeField] private Transform finalImgPos;

    [SerializeField] private GameObject leftHatsGO;
    [SerializeField] private GameObject leftClothesGO;
    [SerializeField] private GameObject leftShoesGO;
    [SerializeField] private GameObject aimHat;
    [SerializeField] private GameObject aimCloth;
    [SerializeField] private GameObject aimShoes;
    [SerializeField] private GameObject aimBox;
    [SerializeField] private GameObject leftHatX;
    [SerializeField] private GameObject leftClothX;
    [SerializeField] private GameObject leftShoeX;
    [SerializeField] private GameObject rightHatX;
    [SerializeField] private GameObject rightClothX;
    [SerializeField] private GameObject rightShoeX;
    [SerializeField] private GameObject[] leftHats;
    [SerializeField] private GameObject[] leftClothes;
    [SerializeField] private GameObject[] leftShoes;

    [SerializeField] private BoxCollider2D leftLineOneCollider;
    [SerializeField] private BoxCollider2D leftLineTwoCollider;
    [SerializeField] private BoxCollider2D leftLineThreeCollider;
    [SerializeField] private BoxCollider2D rightLineOneCollider;
    [SerializeField] private BoxCollider2D rightLineTwoCollider;
    [SerializeField] private BoxCollider2D rightLineThreeCollider;

    //4 events for sad normal faces of two people
    [SerializeField] private UnityEvent rightNormalFace;
    [SerializeField] private UnityEvent rightAngryFace;
    [SerializeField] private UnityEvent leftNormalFace;
    [SerializeField] private UnityEvent leftAngryFace;

    [SerializeField] private UnityEvent rightStartTimerEvent;
    [SerializeField] private UnityEvent leftStartTimerEvent;
    [SerializeField] private UnityEvent rightStopTimerEvent;
    [SerializeField] private UnityEvent leftStopTimerEvent;

    [SerializeField] private AudioClip readySound;
    [SerializeField] private AudioClip wrongSound;
    [SerializeField] private AudioClip goSound;
    [SerializeField] private AudioClip bgSound;

    [SerializeField] private GameObject[] ready;
    [SerializeField] private GameObject[] go;

    private AudioSource audioSrc;
    private GameObject leftHatsSecondGO;
    private GameObject leftClothesSecondGO;
    private GameObject leftShoesSecondGO;
    private GameObject rightHatsFirstGO;
    private GameObject rightClothesFirstGO;
    private GameObject rightShoesFirstGO;
    private GameObject rightHatsSecondGO;
    private GameObject rightClothesSecondGO;
    private GameObject rightShoesSecondGO;

    private bool leftHatsMove = false;
    private bool leftClothesMove = false;
    private bool leftShoesMove = false;
    private bool rightHatsMove = false;
    private bool rightClothesMove = false;
    private bool rightShoesMove = false;

    private Vector3 aimBoxFirstPos;
    private Vector3[] leftHatsPos;
    private Vector3[] leftClothesPos;
    private Vector3[] leftShoesPos;
    private int leftWhichPart = 1;
    private int rightWhichPart = 1;
    private int rnd;
    private string ans;
    private bool aimBoxMoveToMiddle = false;
    private bool aimBoxMoveBack = false;
    private bool wait = false;
    //private bool canRightPress = false;
    //private bool canLeftPress = false;

    //private string lCurrentPhoto;
    //private string rCurrentPhoto;
    IEnumerator Init()
    {
        leftHatsPos = new Vector3[leftHats.Length];
        leftClothesPos = new Vector3[leftHats.Length];
        leftShoesPos = new Vector3[leftHats.Length];
        leftLineOneCollider.enabled = true;
        rightLineOneCollider.enabled = true;
        //vaqti raft line bad in do khamush do ta badi roshan
        aimBoxFirstPos = aimBox.transform.position;
        for (int i = 0; i < leftHats.Length; i++)
        {
            leftHatsPos[i] = leftHats[i].transform.position;
            leftClothesPos[i] = leftClothes[i].transform.position;
            leftShoesPos[i] = leftShoes[i].transform.position;
        }

        //mixing the images
        RandomNumber.MakhlootArray(leftHats);
        RandomNumber.MakhlootArray(leftShoes);
        RandomNumber.MakhlootArray(leftClothes);

        for (int i = 0; i < leftHats.Length; i++)
        {
            leftHats[i].transform.position = leftHatsPos[i];
            leftClothes[i].transform.position = leftClothesPos[i];
            leftShoes[i].transform.position = leftShoesPos[i];
        }

        //create second part and the right part
        leftHatsSecondGO = Instantiate(leftHatsGO, secondLeftHatsPos.position, leftHatsGO.transform.rotation);
        leftHatsSecondGO.name = "LeftHats2";
        leftClothesSecondGO = Instantiate(leftClothesGO, secondLeftClothesPos.position, leftClothesGO.transform.rotation);
        leftClothesSecondGO.name = "LeftClothes2";
        leftShoesSecondGO = Instantiate(leftShoesGO, secondLeftShoesPos.position, leftShoesGO.transform.rotation);
        leftShoesSecondGO.name = "LeftShoes2";

        rightHatsFirstGO = Instantiate(leftHatsGO, firstRightHatsPos.position, leftHatsGO.transform.rotation);
        rightHatsFirstGO.name = "RightHats1";
        GameObject tmp;
        for (int i = 0; i < rightHatsFirstGO.transform.childCount; i++)
        {
            tmp = rightHatsFirstGO.transform.GetChild(i).gameObject;
            tmp.tag = "Right";
            tmp.GetComponent<SpriteRenderer>().sortingOrder = 15;
        }

        rightClothesFirstGO = Instantiate(leftClothesGO, firstRightClothesPos.position, leftClothesGO.transform.rotation);
        rightClothesFirstGO.name = "RightClothes1";
        for (int i = 0; i < rightClothesFirstGO.transform.childCount; i++)
        {
            tmp = rightClothesFirstGO.transform.GetChild(i).gameObject;
            tmp.tag = "Right";
            tmp.GetComponent<SpriteRenderer>().sortingOrder = 15;
        }

        rightShoesFirstGO = Instantiate(leftShoesGO, firstRightShoesPos.position, leftShoesGO.transform.rotation);
        rightShoesFirstGO.name = "RightShoes1";
        for (int i = 0; i < rightShoesFirstGO.transform.childCount; i++)
        {
            tmp = rightShoesFirstGO.transform.GetChild(i).gameObject;
            tmp.tag = "Right";
            tmp.GetComponent<SpriteRenderer>().sortingOrder = 15;
        }

        rightHatsSecondGO = Instantiate(leftHatsGO, secondRightHatsPos.position, leftHatsGO.transform.rotation);
        rightHatsSecondGO.name = "RightHats2";
        for (int i = 0; i < rightHatsSecondGO.transform.childCount; i++)
        {
            tmp = rightHatsSecondGO.transform.GetChild(i).gameObject;
            tmp.tag = "Right";
            tmp.GetComponent<SpriteRenderer>().sortingOrder = 15;
        }

        rightClothesSecondGO = Instantiate(leftClothesGO, secondRightClothesPos.position, leftClothesGO.transform.rotation);
        rightClothesSecondGO.name = "RightClothes2";
        for (int i = 0; i < rightClothesSecondGO.transform.childCount; i++)
        {
            tmp = rightClothesSecondGO.transform.GetChild(i).gameObject;
            tmp.tag = "Right";
            tmp.GetComponent<SpriteRenderer>().sortingOrder = 15;
        }

        rightShoesSecondGO = Instantiate(leftShoesGO, secondRightShoesPos.position, leftShoesGO.transform.rotation);
        rightShoesSecondGO.name = "RightShoes2";
        for (int i = 0; i < rightShoesSecondGO.transform.childCount; i++)
        {
            tmp = rightShoesSecondGO.transform.GetChild(i).gameObject;
            tmp.tag = "Right";
            tmp.GetComponent<SpriteRenderer>().sortingOrder = 15;
        }

        for (int i = 0; i < ready.Length; i++)
        {
            ready[i].SetActive(true);
        }
        audioSrc.clip = readySound;
        audioSrc.Play();
        yield return new WaitForSeconds(audioSrc.clip.length);
        for (int i = 0; i < ready.Length; i++)
        {
            ready[i].SetActive(false);
        }
        //ready.SetActive(false);
        for (int i = 0; i < go.Length; i++)
        {
            go[i].SetActive(true);
        }
        //go.SetActive(true);
        audioSrc.clip = goSound;
        audioSrc.Play();
        yield return new WaitForSeconds(audioSrc.clip.length);
        //go.SetActive(false);
        for (int i = 0; i < go.Length; i++)
        {
            go[i].SetActive(false);
        }
        audioSrc.loop = true;
        audioSrc.clip = bgSound;
        audioSrc.Play();

        rnd = RandomNumber.IntRandomNumber(1, 1, leftHats.Length - 1, false)[0];
        ans = leftHats[rnd].name;

        //nemayesh tasvir
        for (int i = 0; i < leftHats.Length; i++)
        {
            if(leftHats[i].name == ans)
            {
                aimHat.GetComponent<SpriteRenderer>().sprite = leftHats[i].GetComponent<SpriteRenderer>().sprite;
            }
            if(leftClothes[i].name == ans)
            {
                aimCloth.GetComponent<SpriteRenderer>().sprite = leftClothes[i].GetComponent<SpriteRenderer>().sprite;
            }
            if(leftShoes[i].name == ans)
            {
                aimShoes.GetComponent<SpriteRenderer>().sprite = leftShoes[i].GetComponent<SpriteRenderer>().sprite;
            }
        }


        lCurrentPhoto = leftHats[0].name;
        rCurrentPhoto = leftHats [0].name;
        aimBoxMoveToMiddle = true;
        yield return new WaitUntil(() => wait);
        wait = false;
        yield return new WaitForSeconds(aimBoxTime);
        aimBoxMoveBack = true;
        yield return new WaitUntil(() => wait);
        // harekat tasavir + shoru harekate tasavir
        leftHatsMove = true;
        leftClothesMove = true;
        leftShoesMove = true;
        rightHatsMove = true;
        rightClothesMove = true;
        rightShoesMove = true;
        //canLeftPress = true;
        //canRightPress = true;
        leftStartTimerEvent.Invoke();
        rightStartTimerEvent.Invoke();
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        StartCoroutine(Init());
        //audioSrc.clip = readySound;
        //StartCoroutine(PlayMusic());

    }

    private void FixedUpdate()
    {
        //Debug.Log(lCurrentPhoto);
        //Debug.Log(rCurrentPhoto);
        if (aimBoxMoveToMiddle)
        {
            aimBox.transform.position = Vector3.MoveTowards(aimBox.transform.position, finalImgPos.position, aimBoxSpeed * Time.fixedDeltaTime);
            if(Mathf.Abs(Vector3.Distance(finalImgPos.position, aimBox.transform.position)) <= Time.fixedDeltaTime)
            {
                aimBoxMoveToMiddle = false;
                wait = true;
            }
        }
        else if (aimBoxMoveBack)
        {
            aimBox.transform.position = Vector3.MoveTowards(aimBox.transform.position, aimBoxFirstPos, aimBoxSpeed * Time.fixedDeltaTime);
            if (Mathf.Abs(Vector3.Distance(aimBoxFirstPos, aimBox.transform.position)) <= Time.fixedDeltaTime)
            {
                aimBoxMoveBack = false;
                wait = true;
            }
        }
        if (leftHatsMove)
        {
            leftHatsGO.transform.position -= new Vector3(speeds[0] * Time.fixedDeltaTime, 0, 0);
            leftHatsSecondGO.transform.position -= new Vector3(speeds[0] * Time.fixedDeltaTime, 0, 0);
            //se
        }
        if (leftClothesMove)
        {
            leftClothesGO.transform.position -= new Vector3(speeds[1] * Time.fixedDeltaTime, 0, 0);
            leftClothesSecondGO.transform.position -= new Vector3(speeds[1] * Time.fixedDeltaTime, 0, 0);
        }
        if (leftShoesMove)
        {
            leftShoesGO.transform.position -= new Vector3(speeds[2] * Time.fixedDeltaTime, 0, 0);
            leftShoesSecondGO.transform.position -= new Vector3(speeds[2] * Time.fixedDeltaTime, 0, 0);
        }
        if (rightHatsMove)
        {
            rightHatsFirstGO.transform.position -= new Vector3(speeds[0] * Time.fixedDeltaTime, 0, 0);
            rightHatsSecondGO.transform.position -= new Vector3(speeds[0] * Time.fixedDeltaTime, 0, 0);
        }
        if (rightClothesMove)
        {
            rightClothesFirstGO.transform.position -= new Vector3(speeds[1] * Time.fixedDeltaTime, 0, 0);
            rightClothesSecondGO.transform.position -= new Vector3(speeds[1] * Time.fixedDeltaTime, 0, 0);
        }
        if (rightShoesMove)
        {
            rightShoesFirstGO.transform.position -= new Vector3(speeds[2] * Time.fixedDeltaTime, 0, 0);
            rightShoesSecondGO.transform.position -= new Vector3(speeds[2] * Time.fixedDeltaTime, 0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LeftSubmit()
    {
        //canLeftPress = false;
        if (leftWhichPart == 1)
        {
            leftHatsMove = false;
            if(lCurrentPhoto == ans)
            {
                //right ans
                //collider avval khamush do roshan
                leftLineOneCollider.enabled = false;
                leftLineTwoCollider.enabled = true;
                //canLeftPress = true;
                leftWhichPart = 2;
            }
            else
            {
                leftAngryFace.Invoke();

                leftHatX.SetActive(true);
                StartCoroutine(XWait(leftHatX));
                //yield return new WaitForSeconds(onXTime);
                //leftHatX.SetActive(false);
                //leftHatsMove = true;
                //canLeftPress = true;
            }
        }
        else if(leftWhichPart == 2)
        {
            leftClothesMove = false;
            if (lCurrentPhoto == ans)
            {
                //Debug.Log(lCurrentPhoto);
                //right ans
                leftLineTwoCollider.enabled = false;
                leftLineThreeCollider.enabled = true;
                //canLeftPress = true;
                leftWhichPart = 3;
            }
            else
            {
                leftAngryFace.Invoke();

                leftClothX.SetActive(true);
                StartCoroutine(XWait(leftClothX));
                //yield return new WaitForSeconds(onXTime);
                //leftClothX.SetActive(false);
                //leftClothesMove = true;
                //canLeftPress = true;
            }
        }
        else
        {
            leftShoesMove = false;
            if (lCurrentPhoto == ans)
            {
                leftLineThreeCollider.enabled = false;
                //leftLineTwoCollider.enabled = true;
                //show the full photo
                //canRightPress = false;
                //canLeftPress = false;
                leftHatsMove = false;
                leftClothesMove = false;
                leftShoesMove = false;
                rightHatsMove = false;
                rightClothesMove = false;
                rightShoesMove = false;
                leftStopTimerEvent.Invoke();
                rightStopTimerEvent.Invoke();
                Time.timeScale = 0;
            }
            else
            {
                leftAngryFace.Invoke();
                leftShoeX.SetActive(true);
                StartCoroutine(XWait(leftShoeX));
                //yield return new WaitForSeconds(onXTime);
                //leftShoeX.SetActive(false);
                //leftShoesMove = true;
                //canLeftPress = true;
            }
        }
    }

    public void RightSubmit()
    {
        //canRightPress = false;
        //canLeftPress = false;
        if (rightWhichPart == 1)
        {
            rightHatsMove = false;
            if (rCurrentPhoto == ans)
            {
                //right ans
                rightLineOneCollider.enabled = false;
                rightLineTwoCollider.enabled = true;
                //canRightPress = true;
                rightWhichPart = 2;
            }
            else
            {
                rightAngryFace.Invoke();

                rightHatX.SetActive(true);
                StartCoroutine(XWait(rightHatX));
                //yield return new WaitForSeconds(onXTime);
                //rightHatX.SetActive(false);
                //rightHatsMove = true;
                //canRightPress = true;
            }
        }
        else if (rightWhichPart == 2)
        {
            rightClothesMove = false;
            if (rCurrentPhoto == ans)
            {
                //right ans
                rightLineTwoCollider.enabled = false;
                rightLineThreeCollider.enabled = true;
                //canRightPress = true;
                rightWhichPart = 3;
            }
            else
            {
                rightAngryFace.Invoke();
                rightClothX.SetActive(true);
                StartCoroutine(XWait(rightClothX));

                //yield return new WaitForSeconds(onXTime);
                //rightClothX.SetActive(false);
                //rightClothesMove = true;
                //canRightPress = true;
            }
        }
        else
        {
            rightShoesMove = false;
            if (lCurrentPhoto == ans)
            {
                //show the full photo
                rightLineThreeCollider.enabled = false;
                //leftLineTwoCollider.enabled = true;
                //canRightPress = false;
                //canLeftPress = false;
                rightHatsMove = false;
                rightClothesMove = false;
                rightShoesMove = false;
                rightHatsMove = false;
                rightClothesMove = false;
                rightShoesMove = false;
                leftStopTimerEvent.Invoke();
                rightStopTimerEvent.Invoke();
                Time.timeScale = 0;
            }
            else
            {
                rightAngryFace.Invoke();
                rightShoeX.SetActive(true);
                StartCoroutine(XWait(rightShoeX));
                //yield return new WaitForSeconds(onXTime);
                //rightShoeX.SetActive(false);
                //rightShoesMove = true;
                //canRightPress = true;
            }
        }
    }

    IEnumerator XWait(GameObject whichX)
    {
        audioSrc.clip = wrongSound;
        audioSrc.loop = false;
        audioSrc.Play();
        yield return new WaitForSeconds(onXTime);
        whichX.SetActive(false);
        if (whichX == leftHatX)
        {
            leftNormalFace.Invoke();
            //canLeftPress = true;
            leftHatsMove = true;
        }
        else if (whichX == leftClothX)
        {
            leftNormalFace.Invoke();

            //canLeftPress = true;
            leftClothesMove = true;
        }
        else if(whichX == leftShoeX)
        {
            leftNormalFace.Invoke();

            //canLeftPress = true;
            leftShoesMove = true;
        }
        if (whichX == rightHatX)
        {
            rightNormalFace.Invoke();
            //canRightPress = true;
            rightHatsMove = true;
        }
        else if (whichX == rightClothX)
        {
            rightNormalFace.Invoke();
            //canRightPress = true;
            rightClothesMove = true;
        }
        else if (whichX == rightShoeX)
        {
            rightNormalFace.Invoke();
            rightShoesMove = true;
            //canRightPress = true;
        }
    }

    //public void SetLeftCurrent(string current)
    //{
    //    lCurrentPhoto = current;
    //}

    //public void SetRightCurrent(string current)
    //{
    //    rCurrentPhoto = current;
    //}
}
