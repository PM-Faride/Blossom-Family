using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PenguinLevel : MonoBehaviour
{
    [SerializeField] private int howManySprites;
    [SerializeField] private Transform firstPenPos;
    [SerializeField] private float penYDifference;
    [SerializeField] private float xOffset;
    [SerializeField] private float readyTime;
    [SerializeField] private float goTime;
    //???bashe
    //[SerializeField] private float betweenEachTime;
    //
    [SerializeField] private float[] movementSpeed;
    [SerializeField] private float[] whenToMove;
    [SerializeField] private GameObject parent;
    //[SerializeField] private GameObject leftBtn;
    //[SerializeField] private GameObject rightBtn;
    [SerializeField] private GameObject p1Img;
    [SerializeField] private GameObject p2Img;
    [SerializeField] private GameObject pinkSprite;
    [SerializeField] private GameObject blueSprite;
    [SerializeField] private GameObject ready;
    [SerializeField] private GameObject go;
    [SerializeField] private TextMeshProUGUI leftPlayerScore;
    [SerializeField] private TextMeshProUGUI rightPlayerScore;
    [SerializeField] private TextMeshProUGUI winner;
    [SerializeField] private Vector2[] timeScoreTable;
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    private int spriteCounter = 0;
    private bool move;
    private float speed;
    private GameObject[] sprites;
    private float timer = 0;
    //private float clickedTime = 0;
    private GameObject tmpSprite;
    private GameObject chosenSprite;
    private int howManyPink;
    private int[] chosenSpriteIndexes;
    private int leftOrRight;
    private GameObject tmpPlayerImg;
    //private int playerID = -1;
    private List<int> playedPlayers = new List<int>();
    private float leftScore = 0;
    private float rightScore = 0;
    private SpriteRenderer renderer;
    IEnumerator Ready()
    {
        ready.SetActive(true);
        yield return new WaitForSeconds(readyTime);
        ready.SetActive(false);
        go.SetActive(true);
        yield return new WaitForSeconds(goTime);
        go.SetActive(false);
        StartCoroutine(MoveHandler());
    }

    // Start is called before the first frame update
    void Start()
    {
        sprites = new GameObject[howManySprites];
        howManyPink = Mathf.FloorToInt(howManySprites / 2);
        //tmpSprite = Instantiate(pinkSprite, firstPenPos.position, pinkSprite.transform.rotation, parent.transform);
        //tmpSprite.SetActive(true);
        //sprites[0] = tmpSprite;
        Vector3 tmpSpritePos;
        float x;
        for (int i = 0; i < howManyPink; i++)
        {
            //x = RandomNumber.FloatRandomNumber(1, firstPenPos.position.x - xOffset, firstPenPos.position.x + xOffset, false)[0];
            //tmpSpritePos = new Vector3(x, firstPenPos.position.y + penYDifference * i, 0);
            tmpSprite = Instantiate(pinkSprite, parent.transform);
            //tmpSprite = Instantiate(pinkSprite, tmpSpritePos, pinkSprite.transform.rotation, parent.transform);
            sprites[i] = tmpSprite;
            tmpSprite.name = "p";
            //renderer = tmpSprite.GetComponent<SpriteRenderer>();
            //renderer.sortingOrder = i;
            tmpSprite.SetActive(true);
        }

        for (int i = howManyPink; i < howManySprites; i++)
        {
            //x = RandomNumber.FloatRandomNumber(1, firstPenPos.position.x - xOffset, firstPenPos.position.x + xOffset, false)[0];
            //tmpSpritePos = new Vector3(x, firstPenPos.position.y + penYDifference * i, 0);
            tmpSprite = Instantiate(blueSprite, parent.transform);
            //tmpSprite = Instantiate(blueSprite, tmpSpritePos, blueSprite.transform.rotation, parent.transform);
            sprites[i] = tmpSprite;
            tmpSprite.name = "b";
            //renderer = tmpSprite.GetComponent<SpriteRenderer>();
            //renderer.sortingOrder = i;
            tmpSprite.SetActive(true);
        }

        RandomNumber.MakhlootArray(sprites);
        for (int i = 0; i < sprites.Length; i++)
        {
            x = RandomNumber.FloatRandomNumber(1, firstPenPos.position.x - xOffset, firstPenPos.position.x + xOffset, false)[0];
            tmpSpritePos = new Vector3(x, firstPenPos.position.y + penYDifference * i, 0);
            sprites[i].transform.position = tmpSpritePos;
            renderer = sprites[i].GetComponent<SpriteRenderer>();
            renderer.sortingOrder -= i;
        }
        chosenSpriteIndexes = RandomNumber.IntRandomNumber(howManySprites, 0, howManySprites - 1, false);
        //leftOrRight = RandomNumber.IntRandomNumber(howManySprites, 0, 1, true);
        parent.SetActive(true);
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
            //SpriteRenderer[]
            chosenSprite.transform.position += new Vector3(speed * Time.fixedDeltaTime, 0, 0);
            timer += Time.fixedDeltaTime;
            if (chosenSprite.transform.position.x < leftEdge.position.x || chosenSprite.transform.position.x > rightEdge.position.x)
            {
                Destroy(chosenSprite);
                timer = 0;
                move = false;
                //playerID = -1;
                //if(
                StartCoroutine(MoveHandler());
            }
        }
    }

    IEnumerator MoveHandler()
    {
        if(spriteCounter < howManySprites)
        {
            playedPlayers = null;
            playedPlayers = new List<int>();
            if (spriteCounter < whenToMove.Length)
            {
                yield return new WaitForSeconds(whenToMove[spriteCounter]);
            }
            else
            {
                yield return new WaitForSeconds(whenToMove[whenToMove.Length - 1]);
            }
            if (spriteCounter < movementSpeed.Length)
            {
                speed = movementSpeed[spriteCounter];
                //if(leftOrRight[spriteCounter] == 0)
                //{
                //    speed 
                //}
            }
            else
            {
                speed = movementSpeed[movementSpeed.Length - 1];
            }
            chosenSprite = sprites[chosenSpriteIndexes[spriteCounter]];
            leftOrRight = RandomNumber.IntRandomNumber(1, 0, 1, false)[0];
            if (leftOrRight == 0)
            {
                speed = -speed;
            }
            move = true;
            spriteCounter += 1;
        }
        else
        {
            Time.timeScale = 0;
        }
    }

   public void LeftBtnPressed(int playerIndex)
    {
        if (!playedPlayers.Contains(playerIndex) && chosenSprite)
        {
            playedPlayers.Add(playerIndex);
            if(chosenSprite.name == "b")
            {
                if(playerIndex == 0)
                {
                    //ax emtiyaz
                    tmpPlayerImg = Instantiate(p1Img, chosenSprite.transform.position, p1Img.transform.rotation, parent.transform);
                    tmpPlayerImg.SetActive(true);
                    for (int i = 0; i < timeScoreTable.Length; i++)
                    {
                        if(timer < timeScoreTable[i].x)
                        {
                            leftScore += timeScoreTable[i].y;
                            break;
                        }
                    }
                    leftPlayerScore.text = leftScore.ToString();
                }
                else
                {
                    tmpPlayerImg = Instantiate(p2Img, chosenSprite.transform.position, p2Img.transform.rotation, parent.transform);
                    tmpPlayerImg.SetActive(true);
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
            }
        }
    }

    public void RightBtnPressed(int playerIndex)
    {
        //Debug.Log(!playedPlayers.Contains(playerIndex));
        if (!playedPlayers.Contains(playerIndex) && chosenSprite)
        {
            playedPlayers.Add(playerIndex);
            if (chosenSprite.name == "p")
            {
                if (playerIndex == 0)
                {
                    //ax emtiyaz
                    tmpPlayerImg = Instantiate(p1Img, chosenSprite.transform.position, p1Img.transform.rotation, parent.transform);
                    tmpPlayerImg.SetActive(true);

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
                    tmpPlayerImg = Instantiate(p2Img, chosenSprite.transform.position, p2Img.transform.rotation, parent.transform);
                    tmpPlayerImg.SetActive(true);
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
            }
        }
    }
}
