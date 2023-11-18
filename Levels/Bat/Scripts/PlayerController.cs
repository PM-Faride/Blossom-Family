using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //[SerializeField] private  levelCode;
    [SerializeField] private GameObject leftGame;
    [SerializeField] private GameObject rightGame;
    //[SerializeField] private UnityEvent LeftBtnPressedEvent;
    //[SerializeField] private UnityEvent RightBtnPressedEvent;
    //[SerializeField] private UnityEvent BottomBtnPressedEvent;
    //[SerializeField] private UnityEvent LeftBtnReleasedEvent;
    //[SerializeField] private UnityEvent RightBtnReleasedEvent;
    //[SerializeField] private UnityEvent BottomBtnReleasedEvent;
    private InputActionAsset inputActionAsset;
    private InputActionMap inputActionMap;
    private InputAction left;
    private InputAction right;
    private InputAction bottom;
    private int playerIndex;
    private GameObject playerGame;
    private void Awake()
    {
        inputActionAsset = GetComponent<PlayerInput>().actions;
        inputActionMap = inputActionAsset.FindActionMap("MultiPlayer");
        bottom = inputActionMap.FindAction("Bottom");
        left = inputActionMap.FindAction("Left");
        right = inputActionMap.FindAction("Right");
    }

    private void OnEnable()
    {
        left.performed += Left_performed;
        right.performed += Right_performed;
        bottom.performed += Bottom_performed;
        left.canceled += Left_canceled;
        right.canceled += Right_canceled;
        bottom.canceled += Bottom_canceled; 
        left.Enable();
        right.Enable();
        bottom.Enable();
    }

    private void Bottom_canceled(InputAction.CallbackContext obj)
    {
        playerGame.GetComponent<BatLevel>().BottomBtnReleased();
        //BottomBtnReleasedEvent.Invoke();
        //throw new System.NotImplementedException();
    }

    private void Right_canceled(InputAction.CallbackContext obj)
    {
        //RightBtnReleasedEvent.Invoke();
        playerGame.GetComponent<BatLevel>().RightBtnReleased();
        //throw new System.NotImplementedException();
    }

    private void Left_canceled(InputAction.CallbackContext obj)
    {
        //LeftBtnReleasedEvent.Invoke();
        playerGame.GetComponent<BatLevel>().LeftBtnReleased();
        //throw new System.NotImplementedException();
    }

    private void Bottom_performed(InputAction.CallbackContext obj)
    {
        //throw new System.NotImplementedException();
        //level.GetComponent<>
        //BottomBtnPressedEvent.Invoke();\
        playerGame.GetComponent<BatLevel>().BottomBtnPressed();
    }

    private void Right_performed(InputAction.CallbackContext obj)
    {
        playerGame.GetComponent<BatLevel>().RightBtnPressed();
        //throw new System.NotImplementedException();
        //RightBtnPressedEvent.Invoke();
    }

    private void Left_performed(InputAction.CallbackContext obj)
    {
        playerGame.GetComponent<BatLevel>().LeftBtnPressed();
        //throw new System.NotImplementedException();
        //LeftBtnPressedEvent.Invoke();
    }

    private void OnDisable()
    {
        left.performed -= Left_performed;
        right.performed -= Right_performed;
        bottom.performed -= Bottom_performed;
        left.canceled -= Left_canceled;
        right.canceled -= Right_canceled;
        bottom.canceled -= Bottom_canceled;
        left.Disable();
        right.Disable();
        bottom.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        playerIndex = GetComponent<PlayerInput>().playerIndex;
        if(playerIndex == 0)
        {
            playerGame = leftGame;
        }
        if(playerIndex == 1)
        {
            playerGame = rightGame;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
