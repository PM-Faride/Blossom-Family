using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class LevelSelectionController : MonoBehaviour
{
    [SerializeField] private UnityEvent Up;
    [SerializeField] private UnityEvent Down;
    [SerializeField] private UnityEvent Left;
    [SerializeField] private UnityEvent Right;
    [SerializeField] private UnityEvent Select;
    //[SerializeField] private UnityEvent ClosePopUp;

    //[SerializeField] private bool upEnable;
    //[SerializeField] private bool downEnable;
    //[SerializeField] private bool leftEnable;
    //[SerializeField] private bool rightEnable;
    //[SerializeField] private bool selectEnable;
    //[SerializeField] private bool escapeEnable;

    private InputActionAsset inputActionAsset;
    private InputActionMap inputActionMap;
    private InputAction up;
    private InputAction down;
    private InputAction left;
    private InputAction right;
    //private InputAction x;
    private InputAction a;
    //private InputAction b;
    private InputAction y;
    //private int playerIndex;
    private void Awake()
    {
        inputActionAsset = GetComponent<PlayerInput>().actions;
        inputActionMap = inputActionAsset.FindActionMap("MultiPlayer");
        y = inputActionMap.FindAction("Up");
        up = inputActionMap.FindAction("UpArrow");
        down = inputActionMap.FindAction("DownArrow");
        a = inputActionMap.FindAction("Buttom");
        left = inputActionMap.FindAction("LeftArrow");
        right = inputActionMap.FindAction("RightArrow");
    }

    private void OnEnable()
    {
        up.performed += Up_performed;
        up.Enable();
        down.performed += Down_performed;
        down.Enable();
        a.performed += A_performed;
        a.Enable();
        y.performed += Y_performed;
        y.Enable();
        left.performed += Left_performed;
        left.Enable();
        right.performed += Right_performed;
        right.Enable();
    }

    private void Right_performed(InputAction.CallbackContext obj)
    {
        //throw new System.NotImplementedException();
        YesNoRight.Invoke();
    }

    private void Left_performed(InputAction.CallbackContext obj)
    {
        //throw new System.NotImplementedException();
        YesNoLeft.Invoke();
    }

    private void OnDisable()
    {
        up.performed -= Up_performed;
        up.Disable();
        down.performed -= Down_performed;
        down.Disable();
        a.performed -= A_performed;
        a.Disable();
        y.performed -= Y_performed;
        y.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        //playerIndex = GetComponent<PlayerInput>().playerIndex;
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void A_performed(InputAction.CallbackContext obj)
    {
        //select
        Select.Invoke();
    }

    private void Y_performed(InputAction.CallbackContext obj)
    {
        //escape
        ClosePopUp.Invoke();
    }

    private void Down_performed(InputAction.CallbackContext obj)
    {
        DownInMainMenu.Invoke();
    }

    private void Up_performed(InputAction.CallbackContext obj)
    {
        UpInMainMenu.Invoke();
    }
}
