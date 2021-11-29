using UnityEngine;

/// <summary>
/// Class <c>PlayerManager</c> is a Unity component script used to manage the general player behaviour.
/// </summary>
public class PlayerManager : CharacterManager
{
    #region Fields / Properties

    /// <summary>
    /// Instance variable <c>inputHandler</c> is a Unity <c>InputHandler</c> script component representing the player input handler.
    /// </summary>
    private InputHandler _inputHandler;
    
    /// <summary>
    /// Instance variable <c>animator</c> is a Unity <c>Animator</c> component representing the player character animator.
    /// </summary>
    private Animator _animator;
    
    /// <summary>
    /// Instance variable <c>cameraHandler</c> is a Unity <c>CameraHandler</c> script component object representing the camera player manager.
    /// </summary>
    private CameraHandler _cameraHandler;

    /// <summary>
    /// Instance variable <c>playerLocomotion</c> is a Unity <c>PlayerLocomotion</c> script component representing the player locomotion manager.
    /// </summary>
    private PlayerLocomotion _playerLocomotion;

    /// <summary>
    /// Instance variable <c>interactableUI</c> is a Unity <c>InteractableUI</c> script component representing the interactable UI manager.
    /// </summary>
    private InteractableUI _interactableUI;

    /// <summary>
    /// Instance variable <c>interactableUIGameObject</c> is a Unity <c>GameObject</c> component representing the interactable UI game object;
    /// </summary>
    public GameObject interactableUIGameObject;

    /// <summary>
    /// Instance variable <c>itemInteractableGameObject</c> is a Unity <c>GameObject</c> component representing the item interactable UI game object;
    /// </summary>
    public GameObject itemInteractableGameObject;
    
    /// <summary>
    /// Instance variable <c>isSprinting</c> represents the sprinting status of the player character.
    /// </summary>
    [Header("Player flags")] 
    public bool isSprinting;
    
    /// <summary>
    /// Instance variable <c>isInteracting</c> represents the interacting status of the player character.
    /// </summary>
    public bool isInteracting;

    /// <summary>
    /// Instance variable <c>isInAir</c> represents the in air status of the player character.
    /// </summary>
    public bool isInAir;

    /// <summary>
    /// Instance variable <c>isGrounded</c> represents the grounded status of the player character.
    /// </summary>
    public bool isGrounded;

    /// <summary>
    /// Instance variable <c>canDoCombo</c> represents the attack combo status of the player character.
    /// </summary>
    public bool canDoCombo;

    #endregion
    
    #region MonoBehaviour

    /// <summary>
    /// This function is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        _cameraHandler = FindObjectOfType<CameraHandler>();
    }

    /// <summary>
    /// This function is called on the frame when a script is enabled just before any of the Update methods are called the first time.
    /// </summary>
    private void Start()
    {
        _inputHandler = GetComponent<InputHandler>();
        _animator = GetComponentInChildren<Animator>();
        _playerLocomotion = GetComponent<PlayerLocomotion>();
        _interactableUI = FindObjectOfType<InteractableUI>();
    }

    /// <summary>
    /// This function is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {
        float delta = Time.deltaTime;
        isInteracting = _animator.GetBool("isInteracting");
        canDoCombo = _animator.GetBool("canDoCombo");
        _animator.SetBool("isInAir", isInAir);
        
        // Update player inputs value
        _inputHandler.TickInput(delta);
        
        _playerLocomotion.HandleRollingAndSprinting(delta);
        _playerLocomotion.HandleJumping();

        CheckForInteractableObject();
    }
    
    /// <summary>
    /// This function is called every fixed frame-rate frame.
    /// </summary>
    private void FixedUpdate()
    {
        float delta = Time.fixedDeltaTime;
        
        _playerLocomotion.HandleMovement(delta);
        _playerLocomotion.HandleFalling(delta, _playerLocomotion.movementDirection);
    }

    /// <summary>
    /// This function is called after all Update functions have been called.
    /// </summary>
    private void LateUpdate()
    {
        // To avoid multiple calls on 1 frame
        _inputHandler.rollFlag = false;
        _inputHandler.lightAttackInput = false;
        _inputHandler.heavyAttackInput = false;
        _inputHandler.upInput = false;
        _inputHandler.downInput = false;
        _inputHandler.leftInput = false;
        _inputHandler.rightInput = false;
        _inputHandler.interactionInput = false;
        _inputHandler.jumpInput = false;
        _inputHandler.inventoryInput = false;

        float delta = Time.deltaTime;
        
        if (_cameraHandler != null)
        {
            _cameraHandler.FollowTarget(delta);
            _cameraHandler.HandleCameraRotation(delta, _inputHandler.mouseX, _inputHandler.mouseY);
        }
        
        // if player is in air, increase in air timer
        if (isInAir)
        {
            _playerLocomotion.inAirTimer += Time.deltaTime;
        }
    }

    #endregion

    #region Public

    /// <summary>
    /// This function is responsible for checking potential interactable objects in the player character defined range.
    /// </summary>
    public void CheckForInteractableObject()
    {
        RaycastHit hit;
        
        if (Physics.SphereCast(transform.position, 0.3f, transform.forward, out hit, 1f, _cameraHandler.ignoreLayers))
        {
            if (hit.collider.CompareTag("Interactable"))
            {
                Interactable interactableObject = hit.collider.GetComponent<Interactable>();

                if (interactableObject)
                {
                    string interactableText = interactableObject.interactableText;
                    _interactableUI.interactableText.text = interactableText;
                    interactableUIGameObject.SetActive(true);

                    if (_inputHandler.interactionInput)
                    {
                        hit.collider.GetComponent<Interactable>().Interact(this);
                    }
                }
            }
        }
        else
        {
            if (interactableUIGameObject)
            {
                interactableUIGameObject.SetActive(false);
            }

            if (itemInteractableGameObject && _inputHandler.interactionInput)
            {
                itemInteractableGameObject.SetActive(false); 
            }
        }
    }

    #endregion
}
