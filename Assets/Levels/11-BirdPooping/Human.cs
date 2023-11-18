using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using TMPro;
//using UnityEngine.Events;
public class Human : MonoBehaviour
{
    //[SerializeField] private TextMeshProUGUI winner;
    [SerializeField] private TextMeshProUGUI scoreTxt;
    [SerializeField] private float movementSpeed;
    //[SerializeField] private UnityEvent HoneyHit;
    [SpineAnimation] public string movingAnime;

    private int score = 0;
    private SkeletonAnimation human;
    private bool moveRight = false;
    private bool moveLeft = false;
    // Start is called before the first frame update
    void Start()
    {
        human = GetComponent<SkeletonAnimation>();
        human.AnimationName = movingAnime;
        //if (transform.position.x < 0)
        //{
        //    transform.localScale = new Vector3(-1, 1, 1);
        //}
        //else
        //{
        //    transform.localScale = new Vector3(1, 1, 1);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //transform.position += 
        if (moveLeft)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            //transform.position -= new Vector3(movementSpeed, 0, 0);
            //transform.position -= new Vector3(movementSpeed * Time.fixedDeltaTime, 0, 0);
            transform.Translate(new Vector3(-movementSpeed * Time.fixedDeltaTime, 0, 0));
        }
        if (moveRight)
        {
            transform.localScale = new Vector3(1, 1, 1);
            //transform.position += new Vector3(movementSpeed, 0, 0);
            //transform.position += new Vector3(movementSpeed * Time.fixedDeltaTime, 0, 0);
            transform.Translate(new Vector3(movementSpeed * Time.fixedDeltaTime, 0, 0));
        }
    }

    public void MoveAnime()
    {

    }

    public void MoveLeft()
    {
        moveLeft = true;

        //transform.localScale = new Vector3(-1, 1, 1);
        ////transform.position -= new Vector3(movementSpeed, 0, 0);
        ////transform.position -= new Vector3(movementSpeed * Time.fixedDeltaTime, 0, 0);
        //transform.Translate(new Vector3(-movementSpeed * Time.fixedDeltaTime, 0, 0));
    }

    public void MoveRight()
    {
        moveRight = true;

        //transform.localScale = new Vector3(1, 1, 1);
        ////transform.position += new Vector3(movementSpeed, 0, 0);
        ////transform.position += new Vector3(movementSpeed * Time.fixedDeltaTime, 0, 0);
        //transform.Translate(new Vector3(movementSpeed * Time.fixedDeltaTime, 0, 0));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Poop")
        {

            //Destroy(collision.gameObject);
            collision.gameObject.GetComponent<PoopCode>().HitHuman();
            //HoneyHit.Invoke();
            score -= 1;
            scoreTxt.text = score.ToString();
        }
    }

    public void StopMoveLeft()
    {
        moveLeft = false;
    }

    public void StopMoveRight()
    {
        moveRight = false;
    }
}
