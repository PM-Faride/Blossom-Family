using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//using UnityEngine.InputSystem;
public class Grenade : MonoBehaviour
{
    [SerializeField] private AudioClip toggle;
    [SerializeField] private AudioClip select;
    [SerializeField] private AudioClip readySound;
    [SerializeField] private AudioClip goSound;
    [SerializeField] private GameObject musicHandler;
    [SerializeField] private GameObject angle;
    [SerializeField] private GameObject devil;
    [SerializeField] private Transform center;
    [SerializeField] private int howManyObjs;
    [SerializeField] private float radius;
    [SerializeField] private float yFactor;
    [SerializeField] private Transform parent;
    [SerializeField] private GameObject ready;
    [SerializeField] private GameObject go;
    [SerializeField] private float readyTime;
    [SerializeField] private float goTime;
    [SerializeField] private TextMeshProUGUI winner;
    [SerializeField] private TextMeshProUGUI leftPlayer;
    [SerializeField] private TextMeshProUGUI rightPlayer;
    [SerializeField] private Color firstPlayerChangedColor;
    [SerializeField] private Color secondPlayerChangedColor;
    [SerializeField] private Color deactivePlayerColor;
    [SerializeField] private GameObject leftBtn;
    [SerializeField] private GameObject rightBtn;
    [SerializeField] private GameObject leftPressedBtn;
    [SerializeField] private GameObject rightPressedBtn;
    //[SerializeField] private GameObject[] pressedButtons;
    [SerializeField] private int howManyTimeJoggle;
    [SerializeField] private float timeToStayColored;
    [SerializeField] private float timeToChange;
    [SerializeField] private float timeToStayOnSelected;
    [SerializeField] private float timeToPauseOnChange;

    private int devilCounter = 1;
    private GameObject[] angles;
    private float degree = 0;
    private int rnd;
    private Vector3 anglePos;
    private GameObject tmpDevil;
    private int selectedIndex = -1;
    //private int firstTurn;
    private int whoseTurnIs;
    //private Color previousColor;
    private Color tmpColor;
    private Color selectedColor;
    private SpriteRenderer currentRenderer;
    private bool stopTheMove = false;
    private AudioSource audioSrc;

    IEnumerator Ready()
    {
        //ready.SetActive(true);
        //yield return new WaitForSeconds(readyTime);
        //ready.SetActive(false);
        //go.SetActive(true);
        //yield return new WaitForSeconds(goTime);
        //go.SetActive(false);
        audioSrc = GetComponent<AudioSource>();
        audioSrc.clip = toggle;
        //audioSrc.Play();
        whoseTurnIs = RandomNumber.IntRandomNumber(1, 0, 1, false)[0];

        if (whoseTurnIs == 0)
        {
            if (howManyTimeJoggle % 2 == 1)
            {
                howManyTimeJoggle += 1;
            }
        }
        else
        {
            if (howManyTimeJoggle % 2 == 0)
            {
                howManyTimeJoggle += 1;
            }
        }
        
        //for choosing who to start (showing in btn)
        SpriteRenderer leftBtnRenderer = leftBtn.GetComponent<SpriteRenderer>();
        SpriteRenderer righttBtnRenderer = rightBtn.GetComponent<SpriteRenderer>();
        Color tmpColor;
        for (int i = 0; i < howManyTimeJoggle; i++)
        {
            //if(i == howManyTimeJoggle - 1) audioSrc.clip = select;
            if (i % 2 == 0)
            {
                //leftBtn.SetActive(false);
                //leftPressedBtn.SetActive(true);
                tmpColor = leftBtnRenderer.color;
                tmpColor.a = 1;
                //tmpColor.a = 0.65f;
                leftBtnRenderer.color = tmpColor;

                yield return new WaitForSeconds(timeToChange);
                audioSrc.Play();
                if(i != howManyTimeJoggle - 1)
                {
                    //leftBtn.SetActive(true);
                    //leftPressedBtn.SetActive(false);
                    //tmpColor.a = 1;
                    tmpColor.a = 0.65f;
                    leftBtnRenderer.color = tmpColor;
                }
            }
            else
            {
                tmpColor = righttBtnRenderer.color;
                //tmpColor.a = 0.65f;
                tmpColor.a = 1;
                righttBtnRenderer.color = tmpColor;
                //rightBtn.SetActive(false);
                //rightPressedBtn.SetActive(true);
                yield return new WaitForSeconds(timeToChange);
                audioSrc.Play();
                if (i != howManyTimeJoggle - 1)
                {
                    //tmpColor.a = 1;
                    tmpColor.a = 0.65f;
                    righttBtnRenderer.color = tmpColor;
                    //rightBtn.SetActive(true);
                    //rightPressedBtn.SetActive(false);
                }
            }
            //audioSrc.Play();
            //yield return new WaitForSeconds(audioSrc.clip.length);

        }
        //audioSrc.clip = select;
        //audioSrc.Play();
        //yield return new WaitForSeconds(audioSrc.clip.length);

        ready.SetActive(true);
        audioSrc.clip = readySound;
        audioSrc.Play();
        yield return new WaitForSeconds(audioSrc.clip.length);
        ready.SetActive(false);
        go.SetActive(true);
        audioSrc.clip = goSound;
        audioSrc.Play();
        yield return new WaitForSeconds(audioSrc.clip.length);
        go.SetActive(false);
        StartCoroutine(Game());
        //Game();
    }

    IEnumerator Game()
    //void Game()
    {
        Color buttonColor;
        if (whoseTurnIs == 0)
        {
            buttonColor = rightBtn.GetComponent<SpriteRenderer>().color;
            //buttonColor.a = 1;
            buttonColor.a = 0.65f;
            rightBtn.GetComponent<SpriteRenderer>().color = buttonColor;
            selectedColor = firstPlayerChangedColor;
            buttonColor = leftBtn.GetComponent<SpriteRenderer>().color;
            //buttonColor.a = .7f;
            buttonColor.a = 1;
            leftBtn.GetComponent<SpriteRenderer>().color = buttonColor;
            //rightPressedBtn.SetActive(false);
            //rightBtn.SetActive(true);
            //leftBtn.SetActive(false);
            //leftPressedBtn.SetActive(true);

        }
        else
        {
            buttonColor = leftBtn.GetComponent<SpriteRenderer>().color;
            //buttonColor.a = 1;
            buttonColor.a = .65f;
            leftBtn.GetComponent<SpriteRenderer>().color = buttonColor;
            selectedColor = secondPlayerChangedColor;
            buttonColor = rightBtn.GetComponent<SpriteRenderer>().color;
            //buttonColor.a = .7f;
            buttonColor.a = 1;
            rightBtn.GetComponent<SpriteRenderer>().color = buttonColor;
            //leftPressedBtn.SetActive(false);
            //leftBtn.SetActive(true);
            //rightBtn.SetActive(false);
            //rightPressedBtn.SetActive(true);
        }

        for (int i = 0; i < howManyObjs; i++)
        {
            selectedIndex = i;
            currentRenderer = angles[i].GetComponent<SpriteRenderer>();
            tmpColor = currentRenderer.color;
            currentRenderer.color = selectedColor;
            musicHandler.GetComponent<Music>().PlaySound(0);
            yield return new WaitForSeconds(timeToStayColored);
            if (stopTheMove)
            {
                break;
            }
            currentRenderer.color = tmpColor;
            if (i == howManyObjs - 1)
            {
                i = 0;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rnd = RandomNumber.IntRandomNumber(1, 0, howManyObjs - 1, false)[0];
        angles = new GameObject[howManyObjs];

        for (int i = 0; i < howManyObjs; i++)
        {
            anglePos = new Vector3(center.position.x + radius * Mathf.Cos(degree * Mathf.PI / 180.0f), center.position.y + (radius * Mathf.Sin(degree * Mathf.PI / 180.0f)) / yFactor, 0);
            degree += (360 / howManyObjs);
            if(i == rnd)
            {
                //create devil
                angles[i] = Instantiate(devil, anglePos, devil.transform.rotation, parent);
                angles[i].name = "devil";
            }
            else
            {
                angles[i] = Instantiate(angle, anglePos, angle.transform.rotation, parent);
                angles[i].name = "angle";
            }
            angles[i].SetActive(true);
        }
        StartCoroutine(Ready());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BtnPressed(int playerIndex)
    {
        if(whoseTurnIs == playerIndex)
        {
            whoseTurnIs = (whoseTurnIs + 1) % 2;

            stopTheMove = true;
            if (angles[selectedIndex].name == "angle")
            {
                tmpDevil = Instantiate(devil, angles[selectedIndex].transform.position, devil.transform.rotation, parent);
                Destroy(angles[selectedIndex]);
                angles[selectedIndex] = tmpDevil;
                angles[selectedIndex].SetActive(true);
                angles[selectedIndex].name = "devil";

                devilCounter += 1;
                if(devilCounter == howManyObjs)
                {
                    //draw
                    winner.text = "Draw";
                    Time.timeScale = 0;
                }
                else
                {
                    StartCoroutine(WaitForTheNext());
                }
            }
            else
            {
                // show who won
                Time.timeScale = 0;
                if(whoseTurnIs == 0)
                {
                    winner.text = "Right Player Won";
                }
                else
                {
                    winner.text = "Left Player Won";
                }
            }
        }
    }

    public void BtnReleased()
    {
    }

    public void DeviceName(int playerIndex, string playerDevice)
    {
        if(playerIndex == 0)
        {
            leftPlayer.text = playerDevice;
        }
        else
        {
            rightPlayer.text = playerDevice;
        }
    }

    IEnumerator WaitForTheNext()
    {
        yield return new WaitForSeconds(timeToPauseOnChange);
        stopTheMove = false;
        //whoseTurnIs = (whoseTurnIs + 1) % 2;
        StartCoroutine(Game());
    }
}
