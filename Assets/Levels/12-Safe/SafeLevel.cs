using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class SafeLevel : MonoBehaviour
{
    [SerializeField] private UnityEvent rightSafeLoseAnime;
    [SerializeField] private UnityEvent rightSafeWinAnime;
    [SerializeField] private UnityEvent leftSafeLoseAnime;
    [SerializeField] private UnityEvent leftSafeWinAnime;
    [SerializeField] private UnityEvent resetTimer;
    [SerializeField] private int howManyDigits;
    [SerializeField] private GameObject leftNumbersCircle;
    [SerializeField] private GameObject rightNumbersCircle;
    //[SerializeField] private TextMeshProUGUI leftNo;
    //[SerializeField] private TextMeshProUGUI rightNo;
    [SerializeField] private TextMeshProUGUI[] leftNo;
    [SerializeField] private TextMeshProUGUI[] rightNo;
    [SerializeField] private TextMeshProUGUI leftScoreTxt;
    [SerializeField] private TextMeshProUGUI rightScoreTxt;

    private string targetNo;
    private List<string> leftNumbers = new List<string>();
    private List<string> rightNumbers = new List<string>();
    private int leftRoundCounter = 0;
    private int rightRoundCounter = 0;
    private int leftScore;
    private int rightScore;
    private bool rightCanClick = false;
    private bool leftCanClick = false;
    private int leftSafeNum = 0;
    private int rightSafeNum = 0;
    private int leftDigitCounter = 0;
    private int rightDigitCounter = 0;
    private bool leftAnsSituation = true;
    private bool rightAnsSituation = true;
    private int[] digits;
    private string leftTempTxt;
    private string rightTempTxt;

    // Start is called before the first frame update
    void Start()
    {
        leftCanClick = rightCanClick = true;
        digits = new int[howManyDigits];
        digits = RandomNumber.IntRandomNumber(howManyDigits, 0, 9, true);
        for (int i = 0; i < howManyDigits; i++)
        {
            targetNo += digits[i].ToString();
            leftNo[i].text = digits[i].ToString();
            rightNo[i].text = digits[i].ToString();
        }
        leftNumbers.Add(targetNo);
        rightNumbers.Add(targetNo);
        //fc
        //leftNo.text = targetNo;
        //rightNo.text = targetNo;
        resetTimer.Invoke();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TheLevel(string whichSide)
    {
        digits = new int[howManyDigits];
        targetNo = "";
        if (whichSide == "left")
        {
            //kollan sefid she
            for (int i = 0; i < leftNo.Length; i++)
            {
                leftNo[i].color = Color.black;
                //leftNo[i].gameObject.SetActive(true)
            }
            leftNumbersCircle.SetActive(true);
            leftDigitCounter = 0;
            leftRoundCounter += 1;
            if(leftNumbers.Count > leftRoundCounter)
            {
                targetNo = leftNumbers[leftRoundCounter];
                for (int i = 0; i < leftNo.Length; i++)
                {
                    leftNo[i].color = Color.black;
                    leftNo[i].text = targetNo[i].ToString();
                }
            }
            else
            {
                digits = RandomNumber.IntRandomNumber(howManyDigits, 0, 9, true);
                for (int i = 0; i < howManyDigits; i++)
                {
                    targetNo += digits[i].ToString();
                }
                leftNumbers.Add(targetNo);
                rightNumbers.Add(targetNo);
                //leftNo.text = targetNo;
            }
            for (int i = 0; i < leftNo.Length; i++)
            {
                //leftNo[i].color = Color.black;
                leftNo[i].text = targetNo[i].ToString();
            }
            leftCanClick = true;
        }
        else if (whichSide == "right")
        {
            for (int i = 0; i < leftNo.Length; i++)
            {
                rightNo[i].color = Color.black;
            }
            //rightNo.color = Color.black;
            rightNumbersCircle.SetActive(false);

            rightDigitCounter = 0;
            rightRoundCounter += 1;
            if (rightNumbers.Count > rightRoundCounter)
            {
                targetNo = rightNumbers[rightRoundCounter];
                //rightNo.text = targetNo;
            }
            else
            {
                digits = RandomNumber.IntRandomNumber(howManyDigits, 0, 9, true);
                for (int i = 0; i < howManyDigits; i++)
                {
                    targetNo += digits[i].ToString();
                }
                rightNumbers.Add(targetNo);
                leftNumbers.Add(targetNo);
                //rightNo.text = targetNo;
            }
            for (int i = 0; i < rightNo.Length; i++)
            {
                //rightNo[i].color = Color.black;
                rightNo[i].text = targetNo[i].ToString();
            }
            //...
            rightCanClick = true;
        }
    }

    public void StopGame()
    {
        StopAllCoroutines();
        leftCanClick = false;
        rightCanClick = false;
        Time.timeScale = 0;
    }

    public void LeftPressedLeft()
    {
        if (leftCanClick)
        {
            leftNumbersCircle.transform.Rotate(0, 0, 36f);
            if (leftSafeNum == 9)
            {
                leftSafeNum = 0;
            }
            else
            {
                leftSafeNum += 1;
            }
        }
    }

    public void RightPressedLeft()
    {
        if (rightCanClick)
        {
            rightNumbersCircle.transform.Rotate(0, 0, 36f);
            if (rightSafeNum == 9)
            {
                rightSafeNum = 0;
            }
            else
            {
                rightSafeNum += 1;
            }
        }
    }

    public void LeftPressedRight()
    {
        if (leftCanClick)
        {
            leftNumbersCircle.transform.Rotate(0, 0, -36f);
            if (leftSafeNum == 0)
            {
                leftSafeNum = 9;
            }
            else
            {
                leftSafeNum -= 1;
            }
        }
    }

    public void RightPressedRight()
    {
        if (rightCanClick)
        {
            rightNumbersCircle.transform.Rotate(0, 0, -36f);
            if (rightSafeNum == 0)
            {
                rightSafeNum = 9;
            }
            else
            {
                rightSafeNum -= 1;
            }
        }
    }

    public void LeftSubmit()
    {
        if (leftCanClick)
        {
            //leftTempTxt = leftNo.text;
            if (leftDigitCounter == howManyDigits - 1)
            {
                leftNumbersCircle.SetActive(false);
                if (leftSafeNum.ToString() == leftNumbers[leftRoundCounter][leftDigitCounter].ToString() && leftAnsSituation)
                {
                    leftNo[leftDigitCounter].color = Color.green;
                    //leftNo.text = leftTempTxt.Substring(0, leftDigitCounter) + "<color=green>" + leftTempTxt.Substring(leftDigitCounter, 1) + "</color>" + leftTempTxt.Substring(leftDigitCounter + 1);
                    leftCanClick = false;
                    leftScore += 1;
                    leftScoreTxt.text = leftScore.ToString();
                    for (int i = 0; i < leftNo.Length; i++)
                    {
                        //leftNo[i].gameObject.SetActive(false);
                        leftNo[i].text = "";
                    }
                    leftSafeWinAnime.Invoke();
                }
                else
                {
                    //leftNo.text = leftTempTxt.Substring(0, leftDigitCounter) + "<color=red>" + leftTempTxt.Substring(leftDigitCounter, 1) + "</color>" + leftTempTxt.Substring(leftDigitCounter + 1);
                    leftNo[leftDigitCounter].color = Color.red;
                    leftCanClick = false;
                    leftScore -= 1;
                    leftScoreTxt.text = leftScore.ToString();
                    for (int i = 0; i < leftNo.Length; i++)
                    {
                        //leftNo[i].gameObject.SetActive(false);
                        leftNo[i].text = "";
                    }
                    leftSafeLoseAnime.Invoke();
                }
            }
            else
            {
                if (leftSafeNum.ToString() != leftNumbers[leftRoundCounter][leftDigitCounter].ToString())
                {
                    leftNo[leftDigitCounter].color = Color.red;

                    //leftNo.text = leftTempTxt.Substring(0, leftDigitCounter) + "<color=red>" + leftTempTxt.Substring(leftDigitCounter, 1) + "</color>" + leftTempTxt.Substring(leftDigitCounter + 1);
                    //leftCanClick = false;
                }
                else
                {
                    //leftNo.text = leftTempTxt.Substring(0, leftDigitCounter) + "<color=green>" + leftTempTxt.Substring(leftDigitCounter, 1) + "</color>" + leftTempTxt.Substring(leftDigitCounter + 1);
                    leftNo[leftDigitCounter].color = Color.green;

                }
                leftDigitCounter += 1;
            }
        }
    }

    public void RightSubmit()
    {
        if (rightCanClick)
        {
            if (rightDigitCounter == howManyDigits - 1)
            {
                if (rightSafeNum.ToString() == rightNumbers[rightRoundCounter][rightDigitCounter].ToString() && rightAnsSituation)
                {
                    //rightNo.text = rightTempTxt.Substring(0, rightDigitCounter) + "<color=green>" + rightTempTxt.Substring(rightDigitCounter, 1) + "</color>" + rightTempTxt.Substring(rightDigitCounter + 1);
                    rightNo[rightDigitCounter].color = Color.green;
                    rightCanClick = false;
                    rightScore += 1;
                    rightScoreTxt.text = rightScore.ToString();
                    rightNumbersCircle.SetActive(false);
                    for (int i = 0; i < rightNo.Length; i++)
                    {
                        //rightNo[i].gameObject.SetActive(false);
                        rightNo[i].text = "";
                    }
                    rightSafeWinAnime.Invoke();
                }
                else
                {
                    //rightNo.text = rightTempTxt.Substring(0, rightDigitCounter) + "<color=red>" + rightTempTxt.Substring(rightDigitCounter, 1) + "</color>" + rightTempTxt.Substring(rightDigitCounter + 1);
                    rightNo[rightDigitCounter].color = Color.red;
                    rightCanClick = false;
                    rightScore -= 1;
                    rightScoreTxt.text = rightScore.ToString();
                    for (int i = 0; i < rightNo.Length; i++)
                    {
                        //rightNo[i].gameObject.SetActive(false);
                        rightNo[i].text = "";
                    }
                    rightSafeLoseAnime.Invoke();
                }
            }
            else
            {
                if (rightSafeNum.ToString() != rightNumbers[rightRoundCounter][rightDigitCounter].ToString())
                {
                    //rightNo.text = rightTempTxt.Substring(0, rightDigitCounter) + "<color=red>" + rightTempTxt.Substring(rightDigitCounter, 1) + "</color>" + rightTempTxt.Substring(rightDigitCounter + 1);
                    rightNo[rightDigitCounter].color = Color.red;

                    //rightCanClick = false;
                }
                else
                {
                    //rightNo.text = rightTempTxt.Substring(0, rightDigitCounter) + "<color=green>" + rightTempTxt.Substring(rightDigitCounter, 1) + "</color>" + rightTempTxt.Substring(rightDigitCounter + 1);
                    rightNo[rightDigitCounter].color = Color.green;

                }
                rightDigitCounter += 1;
            }
        }
    }
}
