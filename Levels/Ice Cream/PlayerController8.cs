using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerController8 : MonoBehaviour
{
    [SerializeField] private UnityEvent PlayerOneLeft;
    [SerializeField] private UnityEvent PlayerTwoLeft;
    [SerializeField] private UnityEvent PlayerOneRight;
    [SerializeField] private UnityEvent PlayerTwoRight;
    [SerializeField] private UnityEvent PlayerOneDown;
    [SerializeField] private UnityEvent PlayerTwoDown;
    [SerializeField] private UnityEvent PlayerOneUp;
    [SerializeField] private UnityEvent PlayerTwoUp;
    [SerializeField] private UnityEvent PlayerOneCheckAnswer;
    [SerializeField] private UnityEvent PlayerTwoCheckAnswer;

    private InputActionAsset inputActionAsset;
    private InputActionMap inputActionMap;
    private InputAction up;
    private InputAction down;
    private InputAction left;
    private InputAction right;
    private InputAction submitAns;
    private int playerIndex;
    //private

    private void Awake()
    {
        inputActionAsset = GetComponent<PlayerInput>().actions;
        inputActionMap = inputActionAsset.FindActionMap("MultiPlayer");
        up = inputActionMap.FindAction("UpArrow");
        down = inputActionMap.FindAction("DownArrow");
        left = inputActionMap.FindAction("LeftArrow");
        right = inputActionMap.FindAction("RightArrow");
        submitAns = inputActionMap.FindAction("Left");
    }

    private void OnEnable()
    {
        up.performed += Up_performed;
        down.performed += Down_performed;
        left.performed += Left_performed;
        right.performed += Right_performed;
        submitAns.performed += SubmitAns_performed;
        //up.canceled += Up_canceled;
        //down.canceled += Down_canceled;
        //left.canceled += Left_canceled;
        //right.canceled += Right_canceled;
        right.Enable();
        left.Enable();
        down.Enable();
        up.Enable();
        submitAns.Enable();
    }

    private void OnDisable()
    {
        up.performed -= Up_performed;
        down.performed -= Down_performed;
        left.performed -= Left_performed;
        right.performed -= Right_performed;
        submitAns.performed -= SubmitAns_performed;
        //up.canceled += Up_canceled;
        //down.canceled += Down_canceled;
        //left.canceled += Left_canceled;
        //right.canceled += Right_canceled;
        right.Disable();
        left.Disable();
        down.Disable();
        up.Disable();
        submitAns.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        playerIndex = GetComponent<PlayerInput>().playerIndex;
    }

    // Update is called once per frame
    void Update()
    {

    }

    //private void Right_canceled(InputAction.CallbackContext obj)
    //{
    //}

    //private void Left_canceled(InputAction.CallbackContext obj)
    //{
    //}

    //private void Down_canceled(InputAction.CallbackContext obj)
    //{
    //}

    //private void Up_canceled(InputAction.CallbackContext obj)
    //{
    //}


    private void SubmitAns_performed(InputAction.CallbackContext obj)
    {
        //throw new System.NotImplementedException();
        if (playerIndex == 0)
        {
            PlayerOneCheckAnswer.Invoke();
        }
        else
        {
            PlayerTwoCheckAnswer.Invoke();
        }
        //CheckAnswer.Invoke();
    }

    private void Right_performed(InputAction.CallbackContext obj)
    {
        if (playerIndex == 0)
        {
            PlayerOneRight.Invoke();
        }
        else
        {
            PlayerTwoRight.Invoke();
        }
    }

    private void Left_performed(InputAction.CallbackContext obj)
    {
        if (playerIndex == 0)
        {
            PlayerOneLeft.Invoke();
        }
        else
        {
            PlayerTwoLeft.Invoke();
        }
    }

    private void Down_performed(InputAction.CallbackContext obj)
    {
        if (playerIndex == 0)
        {
            PlayerOneDown.Invoke();
        }
        else
        {
            PlayerTwoDown.Invoke();
        }
    }

    private void Up_performed(InputAction.CallbackContext obj)
    {
        if (playerIndex == 0)
        {
            PlayerOneUp.Invoke();
        }
        else
        {
            PlayerTwoUp.Invoke();
        }
    }
}
