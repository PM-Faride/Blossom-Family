using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SumPlayerC : MonoBehaviour
{
    [SerializeField] private GameObject thisLevel;
    private InputActionAsset inputActionAsset;
    private InputActionMap inputActionMap;
    private InputAction left;
    private InputAction bottom;
    private InputAction right;
    private PlayerInput player;

    private void Awake()
    {
        player = GetComponent<PlayerInput>();
        inputActionAsset = player.actions;
        inputActionMap = inputActionAsset.FindActionMap("MultiPlayer");
        left = inputActionMap.FindAction("Left");
        bottom = inputActionMap.FindAction("Bottom");
        right = inputActionMap.FindAction("Right");
    }

    private void OnEnable()
    {
        left.performed += Left_performed;
        right.performed += Right_performed;
        bottom.performed += Bottom_performed;
        right.Enable();
        bottom.Enable();
        left.Enable();
    }

    private void Bottom_performed(InputAction.CallbackContext obj)
    {
        //throw new System.NotImplementedException();
        thisLevel.GetComponent<SumMath>().MiddleBtnPressed(player.playerIndex);
    }

    private void OnDisable()
    {
        left.performed -= Left_performed;
        right.performed -= Right_performed;
        bottom.performed -= Bottom_performed;
        right.Disable();
        bottom.Disable();
        left.Disable();
    }
    private void Right_performed(InputAction.CallbackContext obj)
    {
        thisLevel.GetComponent<SumMath>().RightBtnPressed(player.playerIndex);
    }

    private void Left_performed(InputAction.CallbackContext obj)
    {
        thisLevel.GetComponent<SumMath>().LeftBtnPressed(player.playerIndex);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
