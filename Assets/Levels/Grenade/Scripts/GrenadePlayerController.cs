using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GrenadePlayerController : MonoBehaviour
{
    [SerializeField] private GameObject thisLevel;

    private InputActionAsset inputActionAsset;
    private InputActionMap inputActionMap;
    private InputAction select;
    private PlayerInput player;

    private void Awake()
    {
        player = GetComponent<PlayerInput>();
        inputActionAsset = player.actions;
        inputActionMap = inputActionAsset.FindActionMap("MultiPlayer");
        select = inputActionMap.FindAction("Bottom");
    }

    private void OnEnable()
    {
        select.performed += Select_performed;
        select.canceled += Select_canceled;
        select.Enable();
    }
    private void OnDisable()
    {
        select.performed -= Select_performed;
        select.canceled -= Select_canceled;
        select.Disable();
    }

    private void Select_canceled(InputAction.CallbackContext obj)
    {
        thisLevel.GetComponent<Grenade>().BtnReleased();
    }

    private void Select_performed(InputAction.CallbackContext obj)
    {
        thisLevel.GetComponent<Grenade>().BtnPressed(player.playerIndex);
    }


    // Start is called before the first frame update
    void Start()
    {
        string name;
        name = player.devices[0].displayName;
        if (player.devices[0].displayName == "Wireless Controller")
        {
            name = player.devices[0].name;
        }
        thisLevel.GetComponent<Grenade>().DeviceName(player.playerIndex, name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
