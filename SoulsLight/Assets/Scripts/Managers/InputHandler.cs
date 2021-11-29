using UnityEngine;

/// <summary>
/// Class <c>InputHandler</c> is a Unity component script used to manage the general game inputs behaviour.
/// </summary>
public class InputHandler : MonoBehaviour
{
    #region Fields / Properties

    /// <summary>
    /// Instance variable <c>inputActions</c> is a Unity <c>InputSystem</c> component object representing the general input bindings of the game.
    /// </summary>
    private PlayerControls _inputActions;

    /// <summary>
    /// Instance variable <c>playerAttacker</c> is a Unity <c>PlayerAttacker</c> component object representing the attack behaviour controller of the player character.
    /// </summary>
    private PlayerAttacker _playerAttacker;

    /// <summary>
    /// Instance variable <c>playerInventory</c> is a Unity <c>PlayerInventory</c> component object representing the inventory controller of the player character.
    /// </summary>
    private PlayerInventory _playerInventory;

    /// <summary>
    /// Instance variable <c>playerManager</c> is a Unity <c>PlayerManager</c> script component representing the player behaviour manager.
    /// </summary>
    private PlayerManager _playerManager;

    /// <summary>
    /// Instance variable <c>uiManager</c> is a Unity <c>UIManager</c> script component representing the game UI behaviour manager.
    /// </summary>
    private UIManager _uiManager;

    /// <summary>
    /// Instance variable <c>cameraHandler</c> is a Unity <c>CameraHandler</c> script component representing the camera behaviour manager.
    /// </summary>
    private CameraHandler _cameraHandler;

    /// <summary>
    /// Instance variable <c>movementInput</c> is a Unity <c>Vector2</c> component object representing the movement input vector of the player.
    /// </summary>
    private Vector2 _movementInput;
    
    /// <summary>
    /// Instance variable <c>cameraInput</c> is a Unity <c>Vector2</c> component object representing the camera position input vector of the player.
    /// </summary>
    private Vector2 _cameraInput;
    
    /// <summary>
    /// Instance variable <c>horizontal</c> represents the movement input value along the x axis.
    /// </summary>
    [HideInInspector] public float horizontal;
    
    /// <summary>
    /// Instance variable <c>vertical</c> represents the movement input value along the y axis.
    /// </summary>
    [HideInInspector] public float vertical;
    
    /// <summary>
    /// Instance variable <c>movementMagnitude</c> represents the global movement input magnitude.
    /// </summary>
    [HideInInspector] public float movementMagnitude;
    
    /// <summary>
    /// Instance variable <c>mouseX</c> represents the mouse movement value along the x axis.
    /// </summary>
    [HideInInspector] public float mouseX;
    
    /// <summary>
    /// Instance variable <c>mouseY</c> represents the mouse movement value along the y axis.
    /// </summary>
    [HideInInspector] public float mouseY;

    /// <summary>
    /// Instance variable <c>rollInput</c> represents the roll input status of the game.
    /// </summary>
    [HideInInspector] public bool rollInput;

    /// <summary>
    /// Instance variable <c>interactionInput</c> represents the interaction input status of the game.
    /// </summary>
    [HideInInspector] public bool interactionInput;
    
    /// <summary>
    /// Instance variable <c>lightAttackInput</c> represents the light attack input status of the game.
    /// </summary>
    [HideInInspector] public bool lightAttackInput;

    /// <summary>
    /// Instance variable <c>heavyAttackInput</c> represents the heavy attack input status of the game.
    /// </summary>
    [HideInInspector] public bool heavyAttackInput;

    /// <summary>
    /// Instance variable <c>jumpInput</c> represents the jump input status of the game.
    /// </summary>
    [HideInInspector] public bool jumpInput;

    /// <summary>
    /// Instance variable <c>inventoryInput</c> represents the inventory input status of the game.
    /// </summary>
    [HideInInspector] public bool inventoryInput;

    /// <summary>
    /// Instance variable <c>upInput</c> represents the up input status of the game.
    /// </summary>
    [HideInInspector] public bool upInput;
    
    /// <summary>
    /// Instance variable <c>downInput</c> represents the down input status of the game.
    /// </summary>
    [HideInInspector] public bool downInput;
    
    /// <summary>
    /// Instance variable <c>rightInput</c> represents the right input status of the game.
    /// </summary>
    [HideInInspector] public bool rightInput;
    
    /// <summary>
    /// Instance variable <c>leftInput</c> represents the left input status of the game.
    /// </summary>
    [HideInInspector] public bool leftInput;

    /// <summary>
    /// Instance variable <c>lockOnInput</c> represents the lock on input status of the game.
    /// </summary>
    [HideInInspector] public bool lockOnInput;

    /// <summary>
    /// Instance variable <c>lockOnRightTargetInput</c> represents the lock on right target input status of the game.
    /// </summary>
    [HideInInspector] public bool lockOnRightTargetInput;

    /// <summary>
    /// Instance variable <c>lockOnLeftTargetInput</c> represents the lock on left target input status of the game.
    /// </summary>
    [HideInInspector] public bool lockOnLeftTargetInput;

    /// <summary>
    /// Instance variable <c>rollFlag</c> represents the rolling status of the player character.
    /// </summary>
    [HideInInspector] public bool rollFlag;

    /// <summary>
    /// Instance variable <c>sprintFlag</c> represents the sprinting status of the player character.
    /// </summary>
    [HideInInspector] public bool sprintFlag;

    /// <summary>
    /// Instance variable <c>comboFlag</c> represents the attacking combo status of the player character.
    /// </summary>
    [HideInInspector] public bool comboFlag;

    /// <summary>
    /// Instance variable <c>inventoryFlag</c> represents the inventory status of the player character.
    /// </summary>
    [HideInInspector] public bool inventoryFlag;

    /// <summary>
    /// Instance variable <c>lockOnFlag</c> represents the lock on status of the player character.
    /// </summary>
    [HideInInspector] public bool lockOnFlag;

    /// <summary>
    /// Instance variable <c>rollInputTimer</c> represents the key pressing time needed to perform a character sprint.
    /// </summary>
    [HideInInspector] public float rollInputTimer;

    #endregion

    #region MonoBehaviour

    /// <summary>
    /// This function is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        _playerAttacker = GetComponent<PlayerAttacker>();
        _playerInventory = GetComponent<PlayerInventory>();
        _playerManager = GetComponent<PlayerManager>();
        _uiManager = FindObjectOfType<UIManager>();
        _cameraHandler = FindObjectOfType<CameraHandler>();
    }

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    public void OnEnable()
    {
        if (_inputActions == null)
        {
            _inputActions = new PlayerControls();
            _inputActions.PlayerMovement.Movement.performed += inputActions => _movementInput = inputActions.ReadValue<Vector2>();
            _inputActions.PlayerMovement.Camera.performed += inputActions => _cameraInput = inputActions.ReadValue<Vector2>();
            
            _inputActions.PlayerActions.RB.performed += i => lightAttackInput = true;
            _inputActions.PlayerActions.RT.performed += i => heavyAttackInput = true;
            _inputActions.PlayerQuickSlots.DPadRight.performed += i => rightInput = true;
            _inputActions.PlayerQuickSlots.DPadLeft.performed += i => leftInput = true;
            _inputActions.PlayerActions.Interaction.performed += i => interactionInput = true;
            _inputActions.PlayerActions.Jump.performed += i => jumpInput = true;
            _inputActions.PlayerActions.Inventory.performed += i => inventoryInput = true;
            _inputActions.PlayerActions.LockOn.performed += i => lockOnInput = true;
            _inputActions.PlayerMovement.LockOnTargetLeft.performed += i => lockOnLeftTargetInput = true;
            _inputActions.PlayerMovement.LockOnTargetRight.performed += i => lockOnRightTargetInput = true;
        }
            
        _inputActions.Enable();
    }

    /// <summary>
    /// This function is called when the behaviour becomes disabled.
    /// </summary>
    private void OnDisable()
    {
        _inputActions.Disable();
    }

    #endregion

    #region Private

    /// <summary>
    /// This function is responsible for managing movement input values when called.
    /// </summary>
    /// <param name="delta">A float value representing the elapsed time since the last game time tick.</param>
    private void HandleMoveInput(float delta)
    {
        horizontal = _movementInput.x;
        vertical = _movementInput.y;
        movementMagnitude = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
        
        mouseX = _cameraInput.x;
        mouseY = _cameraInput.y;
    }

    /// <summary>
    /// This function is responsible for managing the roll input behaviour of the player character.
    /// </summary>
    /// <param name="delta">A float value representing the elapsed time since the last game time tick.</param>
    private void HandleRollInput(float delta)
    {
        // Check for roll input triggered
        rollInput = _inputActions.PlayerActions.Roll.phase == UnityEngine.InputSystem.InputActionPhase.Started;
        sprintFlag = rollInput;
        
        // If roll input is true, then trigger roll animation
        if (rollInput)
        {
            rollInputTimer += delta;
        }
        else
        {
            if (rollInputTimer > 0 && rollInputTimer < 0.5f)
            {
                sprintFlag = false;
                rollFlag = true;
            }

            rollInputTimer = 0;
        }
    }

    /// <summary>
    /// This function is responsible for managing the attack input behaviour of the player character.
    /// </summary>
    /// <param name="delta">A float value representing the elapsed time since the last game time tick.</param>
    private void HandleAttackInput(float delta)
    {
        // RB input handle the RIGHT hand weapon's light attack
        if (lightAttackInput)
        {
            if (_playerManager.canDoCombo)
            {
                comboFlag = true;
                _playerAttacker.HandleWeaponCombo(_playerInventory.rightWeapon);
                comboFlag = false;
            }
            else
            {
                if (_playerManager.isInteracting)
                {
                    return;
                }
                if (_playerManager.canDoCombo)
                {
                    return;
                }
                _playerAttacker.HandleLightAttack(_playerInventory.rightWeapon);   
            }
        }

        if (heavyAttackInput)
        {
            _playerAttacker.HandleHeavyAttack(_playerInventory.rightWeapon);
        }
    }

    /// <summary>
    /// This function is responsible for managing the quick slots input behaviour of the player character.
    /// </summary>
    private void HandleQuickSlotsInput()
    {
        if (rightInput)
        {
            _playerInventory.ChangeRightWeapon();
        }
        else if (leftInput)
        {
            _playerInventory.ChangeLeftWeapon();
        }
    }

    /// <summary>
    /// This function is responsible for managing the inventory input behaviour of the player character.
    /// </summary>
    private void HandleInventoryInput()
    {
        if (inventoryInput)
        {
            inventoryFlag = !inventoryFlag;

            if (inventoryFlag)
            {
                _uiManager.OpenSelectWindow();
                _uiManager.UpdateUI();
                _uiManager.hudWindow.SetActive(false);
            }
            else
            {
                _uiManager.CloseSelectWindow();
                _uiManager.CloseAllInventoryWindows();
                _uiManager.hudWindow.SetActive(true);
            }
        }
    }

    /// <summary>
    /// This function is responsible for manager the lock on input behaviour of the player character.
    /// </summary>
    private void HandleLockOnInput()
    {
        if (lockOnInput && !lockOnFlag)
        {
            lockOnInput = false;
            _cameraHandler.HandleLockOn();

            if (_cameraHandler.nearestLockOnTarget)
            {
                _cameraHandler.currentLockOnTarget = _cameraHandler.nearestLockOnTarget;
                lockOnFlag = true;
            }
        } 
        else if (lockOnInput && lockOnFlag)
        {
            lockOnInput = false;
            lockOnFlag = false;
            _cameraHandler.ClearLockOnTargets();
        }

        if (lockOnFlag && lockOnLeftTargetInput)
        {
            lockOnLeftTargetInput = false;
            _cameraHandler.HandleLockOn();

            if (_cameraHandler.leftLockTarget)
            {
                _cameraHandler.currentLockOnTarget = _cameraHandler.leftLockTarget;
            }
        }

        if (lockOnFlag && lockOnRightTargetInput)
        {
            lockOnRightTargetInput = false;
            _cameraHandler.HandleLockOn();

            if (_cameraHandler.rightLockTarget)
            {
                _cameraHandler.currentLockOnTarget = _cameraHandler.rightLockTarget;
            }
        }
        
        _cameraHandler.SetCameraHeight();
    }

    #endregion

    #region Public
    
    /// <summary>
    /// This function is responsible for updating player input values when called.
    /// </summary>
    /// <param name="delta">A float value representing the elapsed time since the last game time tick.</param>
    public void TickInput(float delta)
    {
        HandleMoveInput(delta);
        HandleRollInput(delta);
        HandleAttackInput(delta);
        HandleQuickSlotsInput();
        HandleInventoryInput();
        HandleLockOnInput();
    }

    #endregion
}