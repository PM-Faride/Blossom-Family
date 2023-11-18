using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PenguinController : MonoBehaviour
{
    [SerializeField] private GameObject thisLevel;

    private InputActionAsset inputActionAsset;
    private InputActionMap inputActionMap;
    private InputAction left;
    private InputAction right;
    private PlayerInput playerInput;
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        inputActionAsset = playerInput.actions;
        inputActionMap = inputActionAsset.FindActionMap("MultiPlayer");
        left = inputActionMap.FindAction("Left");
        right = inputActionMap.FindAction("Right");
    }

    private void OnEnable()
    {
        left.performed += Left_performed;
        right.performed += Right_performed;
        right.Enable();
        left.Enable();
    }
    private void OnDisable()
    {
        left.performed -= Left_performed;
        right.performed -= Right_performed;
        right.Disable();
        left.Disable();
    }

    private void Right_performed(InputAction.CallbackContext obj)
    {
        thisLevel.GetComponent<PenguinLevel>().RightBtnPressed(playerInput.playerIndex);
    }

    private void Left_performed(InputAction.CallbackContext obj)
    {
        thisLevel.GetComponent<PenguinLevel>().LeftBtnPressed(playerInput.playerIndex);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //string name;
        //name = playerInput.devices[0].displayName;
        //if (playerInput.devices[0].displayName == "Wireless Controller")
        //{
        //    name = playerInput.devices[0].name;
        //}
        //thisLevel.GetComponent<Grenade>().DeviceName(playerInput.playerIndex, name);
    }
}
