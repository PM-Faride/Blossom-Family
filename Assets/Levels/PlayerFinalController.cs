using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
public class PlayerFinalController : MonoBehaviour
{
    //player controller
    [SerializeField] private UnityEvent PlayerOneLeft;
    [SerializeField] private UnityEvent PlayerTwoLeft;
    [SerializeField] private UnityEvent PlayerOneRight;
    [SerializeField] private UnityEvent PlayerTwoRight;
    [SerializeField] private UnityEvent PlayerOneLeftCanlced;
    [SerializeField] private UnityEvent PlayerTwoLeftCanlced;
    [SerializeField] private UnityEvent PlayerOneRightCancled;
    [SerializeField] private UnityEvent PlayerTwoRightCancled;
    [SerializeField] private UnityEvent PlayerOneDown;
    [SerializeField] private UnityEvent PlayerTwoDown;
    [SerializeField] private UnityEvent PlayerOneUp;
    [SerializeField] private UnityEvent PlayerTwoUp;
    [SerializeField] private UnityEvent PlayerOneX;
    [SerializeField] private UnityEvent PlayerOneA;
    [SerializeField] private UnityEvent PlayerOneY;
    [SerializeField] private UnityEvent PlayerOneB;
    [SerializeField] private UnityEvent PlayerTwoX;
    [SerializeField] private UnityEvent PlayerTwoA;
    [SerializeField] private UnityEvent PlayerTwoY;
    [SerializeField] private UnityEvent PlayerTwoB;

    [SerializeField] private bool upEnable;
    [SerializeField] private bool downEnable;
    [SerializeField] private bool leftEnable;
    [SerializeField] private bool rightEnable;
    [SerializeField] private bool xEnable;
    [SerializeField] private bool yEnable;
    [SerializeField] private bool aEnable;
    [SerializeField] private bool bEnable;


    private InputActionAsset inputActionAsset;
    private InputActionMap inputActionMap;
    private InputAction up;
    private InputAction down;
    private InputAction left;
    private InputAction right;
    private InputAction x;
    private InputAction a;
    private InputAction b;
    private InputAction y;
    private int playerIndex;
    //private

    private void Awake()
    {
        inputActionAsset = GetComponent<PlayerInput>().actions;
        inputActionMap = inputActionAsset.FindActionMap("MultiPlayer");
        //Arrows
        if (upEnable)
        {
            up = inputActionMap.FindAction("UpArrow");
        }
        if (downEnable)
        {
            down = inputActionMap.FindAction("DownArrow");
        }
        if (leftEnable)
        {
            left = inputActionMap.FindAction("LeftArrow");
        }
        if (rightEnable)
        {
            right = inputActionMap.FindAction("RightArrow");
        }
        if (aEnable)
        {
            a = inputActionMap.FindAction("Buttom");
        }
        if (bEnable)
        {
            b = inputActionMap.FindAction("Right");
        }
        if (xEnable)
        {
            x = inputActionMap.FindAction("Left");
        }
        if (yEnable)
        {
            y = inputActionMap.FindAction("Up");
        }
    }

    private void OnEnable()
    {
        if (upEnable)
        {
            up.performed += Up_performed;
            up.Enable();
        }
        if (downEnable)
        {
            down.performed += Down_performed;
            down.Enable();
        }
        if (leftEnable)
        {
            left.performed += Left_performed;
            left.canceled += Left_canceled;
            left.Enable();
        }
        if (rightEnable)
        {
            right.performed += Right_performed;
            right.canceled += Right_canceled;
            right.Enable();
        }
        if (aEnable)
        {
            a.performed += A_performed;
            a.Enable();
        }
        if (bEnable)
        {
            b.performed += B_performed;
            b.Enable();
        }
        if (xEnable)
        {
            x.performed += X_performed;
            x.Enable();
        }
        if (yEnable)
        {
            y.performed += Y_performed;
            y.Enable();
        }
    }

    private void Right_canceled(InputAction.CallbackContext obj)
    {
        if (playerIndex == 0)
        {
            PlayerOneRightCancled.Invoke();
        }
        else
        {
            PlayerTwoRightCancled.Invoke();
        }
    }

    private void Left_canceled(InputAction.CallbackContext obj)
    {
        if (playerIndex == 0)
        {
            PlayerOneLeftCanlced.Invoke();
        }
        else
        {
            PlayerTwoLeftCanlced.Invoke();
        }
        //throw new System.NotImplementedException();
    }

    private void OnDisable()
    {
        if (upEnable)
        {
            up.performed -= Up_performed;
            up.Disable();
        }
        if (downEnable)
        {
            down.performed -= Down_performed;
            down.Disable();
        }
        if (leftEnable)
        {
            left.performed -= Left_performed;
            left.Disable();
        }
        if (rightEnable)
        {
            right.performed -= Right_performed;
            right.Disable();
        }
        if (aEnable)
        {
            a.performed -= A_performed;
            a.Disable();
        }
        if (bEnable)
        {
            b.performed -= B_performed;
            b.Disable();
        }
        if (xEnable)
        {
            x.performed -= X_performed;
            x.Disable();
        }
        if (yEnable)
        {
            y.performed -= Y_performed;
            y.Disable();
        }
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

    private void B_performed(InputAction.CallbackContext obj)
    {
        if (playerIndex == 0)
        {
            PlayerOneB.Invoke();
        }
        else
        {
            PlayerTwoB.Invoke();
        }
    }

    private void A_performed(InputAction.CallbackContext obj)
    {
        if (playerIndex == 0)
        {
            PlayerOneA.Invoke();
        }
        else
        {
            PlayerTwoA.Invoke();
        }
    }

    private void Y_performed(InputAction.CallbackContext obj)
    {
        if (playerIndex == 0)
        {
            PlayerOneY.Invoke();
        }
        else
        {
            PlayerTwoY.Invoke();
        }
    }

    private void X_performed(InputAction.CallbackContext obj)
    {
        if (playerIndex == 0)
        {
            PlayerOneX.Invoke();
        }
        else
        {
            PlayerTwoX.Invoke();
        }
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
    //private void Left
}
