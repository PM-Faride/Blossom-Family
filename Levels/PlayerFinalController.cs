using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
public class PlayerFinalController : MonoBehaviour
{
    [SerializeField] private UnityEvent PlayerOneLeft;
    [SerializeField] private UnityEvent PlayerTwoLeft;
    [SerializeField] private UnityEvent PlayerOneRight;
    [SerializeField] private UnityEvent PlayerTwoRight;
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
    [SerializeField] private bool arrows = false;
    [SerializeField] private bool aby = false;
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
        if (arrows)
        {
            up = inputActionMap.FindAction("UpArrow");
            down = inputActionMap.FindAction("DownArrow");
            left = inputActionMap.FindAction("LeftArrow");
            right = inputActionMap.FindAction("RightArrow");
        }

        x = inputActionMap.FindAction("Left");
        //4 Buttons
        if (aby)
        {
            y = inputActionMap.FindAction("Up");
            a = inputActionMap.FindAction("Buttom");
            b = inputActionMap.FindAction("Right");
        }
    }

    private void OnEnable()
    {
        if (arrows)
        {
            up.performed += Up_performed;
            down.performed += Down_performed;
            left.performed += Left_performed;
            right.performed += Right_performed;
            right.Enable();
            left.Enable();
            down.Enable();
            up.Enable();
        }

        x.performed += X_performed;
        x.Enable();
        if (aby)
        {
            y.performed += Y_performed;
            a.performed += A_performed;
            b.performed += B_performed;
            y.Enable();
            a.Enable();
            b.Enable();
        }
        
    }

    private void OnDisable()
    {
        if (arrows)
        {
            up.performed -= Up_performed;
            down.performed -= Down_performed;
            left.performed -= Left_performed;
            right.performed -= Right_performed;
            right.Disable();
            left.Disable();
            down.Disable();
            up.Disable();
        }

        x.performed -= X_performed;
        x.Disable();
        if (aby)
        {
            y.performed -= Y_performed;
            a.performed -= A_performed;
            b.performed -= B_performed;
            y.Disable();
            a.Disable();
            b.Disable();
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
}
