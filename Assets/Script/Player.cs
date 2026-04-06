using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, InputSystem_Actions.IPlayerActions
{
    [SerializeField] private TextMeshProUGUI hpUI;
    private InputSystem_Actions inputActions;
    private MoveBehaviour _mB;
    private Vector2 direction;
    private int HP = 10;
    void Awake()
    {
        _mB = GetComponent<MoveBehaviour>();
        inputActions = new InputSystem_Actions();
        inputActions.Player.SetCallbacks(this);
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void OnEnable()
    {
        inputActions.Enable();
    }
    private void OnDisable()
    {
        inputActions.Disable();
    }
    void Update()
    {
        _mB.MoveFirstPerson(new Vector3(direction.x, 0, direction.y));
    }
    public void OnAttack(InputAction.CallbackContext context)
    {
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>();
    }
    public void TakeDMG()
    {
        --HP;
        hpUI.text = HP.ToString();
        if (HP >= 0)
        {
            Debug.Log("Dead");
            Application.Quit();
        }
    }
}
