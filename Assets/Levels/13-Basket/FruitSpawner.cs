using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    [SerializeField] private GameObject fruit;
    [SerializeField] private GameObject basket;
    //[SerializeField] private GameObject rightBasket;
    [SerializeField] private float[] eachDropStormLength;
    [SerializeField] private float[] dropStormInterval;
    [SerializeField] private float dropInterval;
    [SerializeField] private float dropStormPositionPeriod;
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;
    //miyam ye noqte az x start ta end-period migiram bad migam ta x + period random entekhab kone o besaze
    private int rnd;
    private int tmpX;
    private GameObject tmpFruit;
    private float timer = 0;
    private float stormLength;
    private Vector3 dropPosition;
    private bool runTimer = false;
    // Start is called before the first frame update
    void Start()
    {
        basket.GetComponent<Basket>().enabled = true;
        //leftBasket.GetComponent<Basket>().enabled = true;
        StartCoroutine(DropStormCreation());

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (runTimer)
        {
            timer += Time.fixedDeltaTime;
        }
        //if(timer > stormLength)
        //{
        //    StopCoroutine(DropCreation());
        //}
    }

    public void StopGame()
    {
        //StopAllCoroutines();
        Time.timeScale = 0;
    }

    IEnumerator DropStormCreation()
    {
        tmpX = RandomNumber.IntRandomNumber(1, (int)startPoint.position.x, (int)(endPoint.position.x - dropStormPositionPeriod), false)[0];
        dropPosition = new Vector3(tmpX, startPoint.position.y, 0);
        runTimer = true;
        StartCoroutine(DropCreation());
        rnd = RandomNumber.IntRandomNumber(1, 0, eachDropStormLength.Length - 1, false)[0];
        stormLength = eachDropStormLength[rnd];
        //bayad timer rah biyofte inja tamum k shod timer bad sabr beine ...
        yield return new WaitUntil(() => timer > stormLength);
        StopCoroutine(DropCreation());
        runTimer = false;
        timer = 0;
        rnd = RandomNumber.IntRandomNumber(1, 0, dropStormInterval.Length - 1, false)[0];
        yield return new WaitForSeconds(dropStormInterval[rnd]);
        StartCoroutine(DropStormCreation());
    }

    IEnumerator DropCreation()
    {
        //makane x ro random az tmpx ta tmpx + baz
        if (runTimer)
        {
            float x = Random.Range(tmpX, tmpX + dropStormPositionPeriod);
            tmpFruit = Instantiate(fruit, new Vector3(x, dropPosition.y, 0), fruit.transform.rotation);
            tmpFruit.SetActive(true);
            yield return new WaitForSeconds(dropInterval);
            StartCoroutine(DropCreation());
        }
    }
}
