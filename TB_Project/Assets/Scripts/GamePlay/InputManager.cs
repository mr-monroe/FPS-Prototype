using UnityEngine;

public class InputManager: MonoBehaviour
{
    [SerializeField] private PauseMenu pauseMenu;

    private InputMaster controls;
    private MouseLook mouseLook;
    private Movement movement;
    private FiringSystem firingSystem;
    
    void Awake()
    {
        controls = new InputMaster();
        controls.Player.MouseLook.performed     += ctx => OnMouseLookPerformed(ctx.ReadValue<Vector2>());
        controls.Player.Movement.performed      += ctx => OnMovePerformed(ctx.ReadValue<Vector2>());
        controls.Player.Jump.performed          += ctx => OnJumpPerformed();
        controls.Player.Crouch.performed        += ctx => OnCrouchPerformed();
        controls.Player.Shoot.performed         += ctx => OnShootPerformed();
        controls.Player.SwapWeapon.performed    += ctx => OnMouseScroll(ctx.ReadValue<float>());

        controls.UI.PauseMenu.performed += ctx => OnPausePerformed();

        mouseLook = GetComponent<MouseLook>();
        movement = GetComponent<Movement>();
        firingSystem = GetComponent<FiringSystem>();

        Cursor.lockState = CursorLockMode.Locked;
    }

    #region Enable / Disable

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }
    #endregion

    private void OnMovePerformed(Vector2 direction) 
    {
        movement.Move(direction);
    }

    private void OnPausePerformed() 
    {
        pauseMenu.OnPausePressed();
    }

    private void OnMouseLookPerformed(Vector2 direction) 
    {
        mouseLook.UpdateMouseLook(direction);
    }

    private void OnJumpPerformed() 
    {
        movement.OnJumpPressed();
    }

    private void OnCrouchPerformed() 
    {
        movement.OnCrouchPressed();
        firingSystem.CrouchEffect();
    }

    private void OnShootPerformed() 
    {
        firingSystem.OnShootPressed();
    }

    private void OnMouseScroll(float scrollDirection) 
    {
        if (scrollDirection > 0) 
        {
            firingSystem.EquipPreviousWeapon();
        }
        if (scrollDirection < 0) 
        {
            firingSystem.EquipNextWeapon();
        }
    }
}
