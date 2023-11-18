using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FPlayerController : MonoBehaviour
{
    [SerializeField] private GameObject thisLevel;
    private InputActionAsset inputActionAsset;
    private InputActionMap inputActionMap;
    private InputAction left;
    private InputAction right;
    private PlayerInput player;

    private void Awake()
    {
        player = GetComponent<PlayerInput>();
        inputActionAsset = player.actions;
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
        thisLevel.GetComponent<Fraction>().RightBtnPressed(player.playerIndex); 
    }

    private void Left_performed(InputAction.CallbackContext obj)
    {
        thisLevel.GetComponent<Fraction>().LeftBtnPressed(player.playerIndex);
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
