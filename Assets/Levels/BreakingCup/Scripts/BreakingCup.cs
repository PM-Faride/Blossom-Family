using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
//using TMPro;

public class BreakingCup : MonoBehaviour
{
    [SerializeField] private int howManyEqualAttackHappens;
    [SerializeField] private int[] howManyCups;
    [SerializeField] private GameObject redCup;
    [SerializeField] private GameObject blueCup;
    [SerializeField] private float cupHeight;
    [SerializeField] private float xDifference;
    [SerializeField] private float downSpeed;
    [SerializeField] private Transform firstCupPos;
    [SerializeField] private Transform parent;
    [SerializeField] private GameObject ready;
    [SerializeField] private GameObject go;
    [SerializeField] private float readyTime;
    [SerializeField] private float goTime;
    [SerializeField] private UnityEvent startTheTimerEvent;
    [SerializeField] private UnityEvent stopTheTimerEvent;
    //[SerializeField] private GameObject[] buttons;
    //[SerializeField] private GameObject[] pressedButtons;
    [SerializeField] private GameObject musicHanler;

    private GameObject[] cups;
    private bool started = false;
    private bool move = false;
    //private int playerAns = -10;
    private int whichCup = 10;
    private int counter = 0;
    private int layerNo = 0;
    //private SpriteRenderer renderer;
    //left blue
    //right red

    //IEnumerator Ready()
    //{
    //    ready.SetActive(true);
    //    yield return new WaitForSeconds(readyTime);
    //    ready.SetActive(false);
    //    go.SetActive(true);
    //    yield return new WaitForSeconds(goTime);
    //    go.SetActive(false);
    //    cups = new GameObject[howManyCups.Length];
    //    if (howManyCups[0] == 0)
    //    {
    //        cups[0] = Instantiate(blueCup, firstCupPos.position, blueCup.transform.rotation, parent);
    //        whichCup = 0;
    //    }
    //    else
    //    {
    //        whichCup = 1;
    //        cups[0] = Instantiate(redCup, firstCupPos.position, redCup.transform.rotation, parent);
    //    }
    //    cups[0].SetActive(true);
    //    for (int i = 1; i < howManyCups.Length; i++)
    //    {
    //        if (howManyCups[i] == 0)
    //        {
    //            if (howManyCups[i - 1] == 0)
    //            {
    //                //create blue cup on top of a blue cup
    //                cups[i] = Instantiate(blueCup, cups[i - 1].transform.position + new Vector3(0, cupHeight, 0), blueCup.transform.rotation, parent);
    //            }
    //            else
    //            {
    //                //create blue cup on top of a red cup
    //                cups[i] = Instantiate(blueCup, cups[i - 1].transform.position + new Vector3(-xDifference, cupHeight, 0), blueCup.transform.rotation, parent);
    //            }
    //        }
    //        else
    //        {
    //            if (howManyCups[i - 1] == 1)
    //            {
    //                //create red cup on top of a red cup
    //                cups[i] = Instantiate(redCup, cups[i - 1].transform.position + new Vector3(0, cupHeight, 0), redCup.transform.rotation, parent);
    //            }
    //            else
    //            {
    //                //create red cup on top of a blue cup
    //                cups[i] = Instantiate(redCup, cups[i - 1].transform.position + new Vector3(xDifference, cupHeight, 0), redCup.transform.rotation, parent);
    //            }
    //        }
    //        layerNo += 1;
    //        cups[i].GetComponent<SpriteRenderer>().sortingOrder = layerNo;
    //        cups[i].SetActive(true);
    //    }
    //    startTheTimerEvent.Invoke();
    //    started = true;
    //    //StartTheGame();
    //}

    private void StartTheGame()
    {
        cups = new GameObject[howManyCups.Length];
        if (howManyCups[0] == 0)
        {
            cups[0] = Instantiate(blueCup, firstCupPos.position, blueCup.transform.rotation, parent);
            whichCup = 0;
        }
        else
        {
            whichCup = 1;
            cups[0] = Instantiate(redCup, firstCupPos.position, redCup.transform.rotation, parent);
        }
        cups[0].SetActive(true);
        for (int i = 1; i < howManyCups.Length; i++)
        {
            if (howManyCups[i] == 0)
            {
                if (howManyCups[i - 1] == 0)
                {
                    //create blue cup on top of a blue cup
                    cups[i] = Instantiate(blueCup, cups[i - 1].transform.position + new Vector3(0, cupHeight, 0), blueCup.transform.rotation, parent);
                }
                else
                {
                    //create blue cup on top of a red cup
                    cups[i] = Instantiate(blueCup, cups[i - 1].transform.position + new Vector3(-xDifference, cupHeight, 0), blueCup.transform.rotation, parent);
                }
            }
            else
            {
                if (howManyCups[i - 1] == 1)
                {
                    //create red cup on top of a red cup
                    cups[i] = Instantiate(redCup, cups[i - 1].transform.position + new Vector3(0, cupHeight, 0), redCup.transform.rotation, parent);
                }
                else
                {
                    //create red cup on top of a blue cup
                    cups[i] = Instantiate(redCup, cups[i - 1].transform.position + new Vector3(xDifference, cupHeight, 0), redCup.transform.rotation, parent);
                }
            }
            layerNo += 1;
            cups[i].GetComponent<SpriteRenderer>().sortingOrder = layerNo;
            cups[i].SetActive(true);
        }
        startTheTimerEvent.Invoke();
        started = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        //ready.GetComponent<render>
        CreateAttackArray();
        //cups = new GameObject[howManyCups.Length];
        //if(howManyCups[0] == 0)
        //{
        //    cups[0] = Instantiate(blueCup, firstCupPos.position, blueCup.transform.rotation, parent);
        //    whichCup = 0;
        //}
        //else
        //{
        //    whichCup = 1;
        //    cups[0] = Instantiate(redCup, firstCupPos.position, redCup.transform.rotation, parent);
        //}
        //cups[0].SetActive(true);
        //for (int i = 1; i < howManyCups.Length; i++)
        //{
        //    if(howManyCups[i] == 0)
        //    {
        //        if(howManyCups[i - 1] == 0)
        //        {
        //            //create blue cup on top of a blue cup
        //            cups[i] = Instantiate(blueCup, cups[i - 1].transform.position + new Vector3(0, cupHeight, 0), blueCup.transform.rotation, parent);
        //        }
        //        else
        //        {
        //            //create blue cup on top of a red cup
        //            cups[i] = Instantiate(blueCup, cups[i - 1].transform.position + new Vector3(-xDifference, cupHeight, 0), blueCup.transform.rotation, parent);
        //        }
        //    }
        //    else
        //    {
        //        if (howManyCups[i - 1] == 1)
        //        {
        //            //create red cup on top of a red cup
        //            cups[i] = Instantiate(redCup, cups[i - 1].transform.position + new Vector3(0, cupHeight, 0), redCup.transform.rotation, parent);
        //        }
        //        else
        //        {
        //            //create red cup on top of a blue cup
        //            cups[i] = Instantiate(redCup, cups[i - 1].transform.position + new Vector3(xDifference, cupHeight, 0), redCup.transform.rotation, parent);
        //        }
        //    }
        //    layerNo += 1;
        //    cups[i].GetComponent<SpriteRenderer>().sortingOrder = layerNo;
        //    cups[i].SetActive(true);
        //}
        StartTheGame();
        //StartCoroutine(Ready());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (move)
        {
            if(cups[counter].transform.position.y <= firstCupPos.position.y)
            {
                move = false;
            }
            else
            {
                parent.position -= new Vector3(0, downSpeed * Time.fixedDeltaTime, 0);
            }
        }
    }

    //private void Update()
    //{
    //    //if (counter == howManyCups.Length)
    //    //{
    //    //    started = false;
    //    //    stopTheTimerEvent.Invoke();
    //    //}
    //}
    void CreateAttackArray()
    {
        for (int i = 0; i < howManyEqualAttackHappens - 1; i++)
        {
            int tmpRndArrayNumber = RandomNumber.IntRandomNumber(1, 0, 1, false)[0];
            howManyCups[i] = tmpRndArrayNumber;
        }
        for (int i = howManyEqualAttackHappens - 1; i < howManyCups.Length - (howManyEqualAttackHappens - 1) + 1; i++)
        {
            for (int j = i - (howManyEqualAttackHappens - 1); j < i; j++)
            {
                if (howManyCups[j] == howManyCups[j + 1])
                {
                    if (howManyCups[j] == 0)
                    {
                        howManyCups[j + howManyEqualAttackHappens - 1] = 1;
                    }
                    else
                    {
                        howManyCups[j + howManyEqualAttackHappens - 1] = 0;
                    }
                }
                else
                {
                    howManyCups[j + howManyEqualAttackHappens - 1] = RandomNumber.IntRandomNumber(1, 0, 1, false)[0];
                }
            }
        }
    }
    public void LeftBtnPressed()
    {
        if (started)
        {
            //buttons[0].SetActive(false);
            //pressedButtons[0].SetActive(true);
            if(whichCup == 0)
            {
                //if(counter == howManyCups.Length)
                //{
                //    started = false;
                //    stopTheTimerEvent.Invoke();
                //}
                musicHanler.GetComponent<Music>().PlaySound(0);
                cups[counter].transform.parent = null;
                cups[counter].SetActive(false);
                move = true;
                if (counter == howManyCups.Length - 1)
                {
                    started = false;
                    stopTheTimerEvent.Invoke();
                }
                else
                {
                    counter += 1;
                    whichCup = howManyCups[counter];
                }
            }
            // nemidunam in bashe ya na
            else
            {
                musicHanler.GetComponent<Music>().PlaySound(1);
            }
        }
        //if()
    }

    public void RightBtnPressed()
    {
        if (started)
        {
            //buttons[1].SetActive(false);
            //pressedButtons[1].SetActive(true);
            if (whichCup == 1)
            {
                //if (counter == howManyCups.Length)
                //{
                //    started = false;
                //    stopTheTimerEvent.Invoke();
                //}
                musicHanler.GetComponent<Music>().PlaySound(0);
                cups[counter].transform.parent = null;
                cups[counter].SetActive(false);
                move = true;
                //if(counter < howManyCups.Length - 2)
                if (counter == howManyCups.Length - 1)
                {
                    started = false;
                    stopTheTimerEvent.Invoke();
                }
                else
                {
                    counter += 1;
                    whichCup = howManyCups[counter];
                }
            }
            else
            {
                musicHanler.GetComponent<Music>().PlaySound(1);

            }
        }
    }

    public void LeftBtnReleased()
    {
        //buttons[0].SetActive(true);
        //pressedButtons[0].SetActive(false);
    }
    public void RightBtnReleased()
    {
        //buttons[1].SetActive(true);
        //pressedButtons[1].SetActive(false);
    }

    //public void StopTheGame()
    //{
    //    //activeHole = -1000;
    //    //Time.timeScale = 0;
    //    started = false;
    //    //StopAllCoroutines();
    //}
}
