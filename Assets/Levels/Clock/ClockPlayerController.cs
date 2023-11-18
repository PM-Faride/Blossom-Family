using UnityEngine;
using UnityEngine.InputSystem;

public class ClockPlayerController : MonoBehaviour
{
    [SerializeField] private GameObject thisLevel;
    private InputActionAsset inputActionAsset;
    private InputActionMap inputActionMap;
    private InputAction bottom;
    private int playerIndex;
    //private

    private void Awake()
    {
        inputActionAsset = GetComponent<PlayerInput>().actions;
        inputActionMap = inputActionAsset.FindActionMap("MultiPlayer");
        bottom = inputActionMap.FindAction("Bottom");
    }

    private void OnEnable()
    {
        bottom.performed += Bottom_performed;
        bottom.canceled += Bottom_canceled;
        bottom.Enable();
    }

    private void Bottom_canceled(InputAction.CallbackContext obj)
    {
        //throw new System.NotImplementedException();
    }

    private void Bottom_performed(InputAction.CallbackContext obj)
    {
        if(playerIndex == 0)
        {
            thisLevel.GetComponent<ClockLevel>().BtnWasPressed1();

        }
        else
        {
            thisLevel.GetComponent<ClockLevel>().BtnWasPressed2();

        }
        //throw new System.NotImplementedException();
    }

    private void OnDisable()
    {
        bottom.performed -= Bottom_performed;
        bottom.canceled -= Bottom_canceled;
        bottom.Disable();
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
}
