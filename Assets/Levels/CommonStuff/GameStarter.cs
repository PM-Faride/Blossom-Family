using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    //menii 3 ya 4 part dare
    [SerializeField] private GameObject[] resumeMenu;
    [SerializeField] private GameObject[] newGameMenu;
    [SerializeField] private GameObject popUp;
    [SerializeField] private GameObject arrow;
    [SerializeField] private GameObject yesNoSelectItem;
    [SerializeField] private GameObject noItem;
    [SerializeField] private GameObject yesItem;
    [SerializeField] private float spaceBetweenOptionsResume;
    [SerializeField] private float spaceBetweenOptionsNewGame;

    //1 main meu without resume
    //2 main meu with resume
    private int whichMenu;
    private int arrowSituation = 0;
    private bool yes = true;
    private void Awake()
    {
        if (!SaveFirstTime.LoadFirstTimeInfo().firstTime)
        {
            //arr
            for (int i = 0; i < newGameMenu.Length; i++)
            {
                newGameMenu[i].SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < resumeMenu.Length; i++)
            {
                resumeMenu[i].SetActive(true);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
        // load firsttime
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpInMenu()
    {
        if(arrowSituation != 0)
        {
            if (whichMenu == 1)
            {
                arrow.transform.position += new Vector3(0, spaceBetweenOptionsNewGame, 0);
                arrowSituation -= 1;
            }
            else
            {
                arrow.transform.position += new Vector3(0, spaceBetweenOptionsResume, 0);
                arrowSituation -= 1;
            }
        }
    }
    public void DownInMenu()
    {
        if (whichMenu == 1)
        {
            if (arrowSituation != newGameMenu.Length - 1)
            {
                arrowSituation += 1;
                arrow.transform.position -= new Vector3(0, spaceBetweenOptionsNewGame, 0);
            }
        }
        else
        {
            if (arrowSituation != resumeMenu.Length - 1)
            {
                arrowSituation += 1;
                arrow.transform.position -= new Vector3(0, spaceBetweenOptionsResume, 0);
            }
        }
        
    }
    public void LeftInYesNo()
    {
        if (popUp.activeSelf && !yes)
        {
            yes = true;
            yesNoSelectItem.transform.position = yesItem.transform.position;
        }
    }
    public void RightInYesNo()
    {
        if (popUp.activeSelf && yes)
        {
            yes = false;
            yesNoSelectItem.transform.position = noItem.transform.position;
        }
    }
    public void Escape()
    {
        if (popUp.activeSelf)
        {
            popUp.SetActive(false);
        }
    }
    public void Select()
    {
        //bar asase inke chi entekhab shode bas begim
        if (popUp.activeSelf && yes)
        {
            //yes entekhab shode yani bayad save pak beshe yechi khali biiyazd rush
        }
        else if(popUp.activeSelf && !yes)
        {
            popUp.SetActive(false);
        }
        else if(whichMenu == 1)
        {
            if(arrowSituation == 0)
            {
                //scene levvels for new game
            }
            else if(arrowSituation == 1)
            {
                //options
            }
            else
            {
                //quit the game
            }
        }
        else
        {
            if (arrowSituation == 0)
            {
                //show levels with info load save
            }
            else if (arrowSituation == 1)
            {
                popUp.SetActive(true);
            }
            else if(arrowSituation == 2)
            {
                //options
            }
            else
            {
                //exit
            }
        }
    }
}
