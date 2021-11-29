using System;
using UnityEngine;

/// <summary>
/// Class <c>PlayerLocomotion</c> is a Unity component script used to manage the player movement behaviour.
/// </summary>
public class PlayerLocomotion : MonoBehaviour
{
    #region Fields / Properties

    /// <summary>
    /// TODO: comments
    /// </summary>
    private CameraHandler _cameraHandler;
    
    /// <summary>
    /// Instance variable <c>animatorHandler</c> is a Unity <c>AnimatorHandler</c> component object representing the player animator handler.
    /// </summary>
    private AnimatorHandler _animatorHandler;
    
    /// <summary>
    /// Instance variable <c>playerManager</c> is a Unity <c>PlayerManager</c> script component representing the player behaviour manager.
    /// </summary>
    private PlayerManager _playerManager;

    /// <summary>
    /// Instance variable <c>inputHandler</c> is a Unity <c>InputHandler</c> script component object representing the player inputs manager.
    /// </summary>
    private InputHandler _inputHandler;
    
    /// <summary>
    /// Instance variable <c>myTransform</c> is a Unity <c>Transform</c> component object representing the position, rotation and scale values of the player.
    /// </summary>
    private Transform _myTransform;
    
    /// <summary>
    /// Instance variable <c>cameraObject</c> is a Unity <c>Transform</c> component object representing player camera position, rotation and scale values.
    /// </summary>
    private Transform _cameraObject;
    
    /// <summary>
    /// Instance variable <c>ignoreForGroundCheck</c> is a Unity <c>LayerMask</c> struct representing the layer mask of the player game object.
    /// </summary>
    private LayerMask _ignoreForGroundCheck;
    
    /// <summary>
    /// Instance variable <c>normalVector</c> is a Unity <c>Vector3</c> component object representing the player movement direction vector.
    /// </summary>
    private Vector3 _normalVector;
    
    /// <summary>
    /// Instance variable <c>targetPosition</c> is a Unity <c>Vector3</c> component object representing the player target position.
    /// </summary>
    private Vector3 _targetPosition;

    /*
    /// <summary>
    /// Instance variable <c>normalCamera</c> represents the scene main camera game object.
    /// </summary>
    public GameObject normalCamera;
    TODO: remove if not used
    */

    /// <summary>
    /// Instance variable <c>groundDetectionRayStartPoint</c> represents the y position altitude value of the character to start drawing the ground detection raycast from.
    /// </summary>
    [Header("Ground & Air Detection Stats")] 
    [SerializeField] private float groundDetectionRayStartPoint = 0.5f;

    /// <summary>
    /// Instance variable <c>minimumDistanceNeededToBeginFall</c> represents the minimum altitude value difference between ground and player to consider the character is falling.
    /// </summary>
    [SerializeField] private float minimumDistanceNeededToBeginFall = 1f;

    /// <summary>
    /// Instance variable <c>groundDirectionRayDistance</c> represents the ground detection raycast length value to draw.
    /// </summary>
    [SerializeField] private float groundDirectionRayDistance = 0.2f;
    
    /// <summary>
    /// Instance variable <c>fallingSpeed</c> represents the player falling speed value.
    /// </summary>
    [SerializeField] private float fallingSpeed = 45;

    /// <summary>
    /// Instance variable <c>movementSpeed</c> represents the player movement speed value.
    /// </summary>
    [Header("Movement Stats")]
    [SerializeField] private float movementSpeed = 5;

    /// <summary>
    /// Instance variable <c>walkingSpeed</c> represents the player walking speed value.
    /// </summary>
    [SerializeField] private float walkingSpeed = 1;

    /// <summary>
    /// Instance variable <c>sprintSpeed</c> represents the player movement speed value on sprint.
    /// </summary>
    [SerializeField] private float sprintSpeed = 7;
    
    /// <summary>
    /// Instance variable <c>rotationSpeed</c> represents the player rotation speed value.
    /// </summary>
    [SerializeField] private float rotationSpeed = 10;
    
    /// <summary>
    /// Instance variable <c>rigidbody</c> is a Unity <c>RigidBody</c> component object representing the player rigidbody.
    /// </summary>
    [HideInInspector] public new Rigidbody rigidbody;
    
    /// <summary>
    /// Instance variable <c>movementDirection</c> is a Unity <c>Vector3</c> component object representing the player movement direction vector.
    /// </summary>
    [HideInInspector] public Vector3 movementDirection;
    
    /// <summary>
    /// Instance variable <c>inAirTimer</c> represents the duration value the character spend in the air while falling.
    /// </summary>
    [HideInInspector] public float inAirTimer;
    
    #endregion

    #region MonoBehaviour

    /// <summary>
    /// TODO: comments
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
        _playerManager = GetComponent<PlayerManager>();
        rigidbody = GetComponent<Rigidbody>();
        _inputHandler = GetComponent<InputHandler>();
        _animatorHandler = GetComponentInChildren<AnimatorHandler>();
            
        if (Camera.main)
        {
            _cameraObject = Camera.main.transform;
        }
        
        _myTransform = transform;
        _animatorHandler.Initialize();

        _playerManager.isGrounded = true;
        _ignoreForGroundCheck = ~(1 << 8 | 1 << 11);
    }

    #endregion

    #region Private
    
    /// <summary>
    /// This function is responsible for rotating the player character.
    /// </summary>
    /// <param name="delta">A float value representing the elapsed time since the last game time tick.</param>
    private void HandleRotation(float delta)
    {
        if (_inputHandler.lockOnFlag)
        {
            if (_inputHandler.sprintFlag || _inputHandler.rollFlag)
            {
                Vector3 targetDirection = Vector3.zero;
                targetDirection = _cameraHandler.cameraTransform.forward * _inputHandler.vertical;
                targetDirection += _cameraHandler.cameraTransform.right * _inputHandler.horizontal;
                targetDirection.Normalize();
                targetDirection.y = 0;

                if (targetDirection == Vector3.zero)
                {
                    targetDirection = transform.forward;
                }
            
                Quaternion tr = Quaternion.LookRotation(targetDirection);
                Quaternion targetRotation = Quaternion.Slerp(transform.rotation, tr, rotationSpeed * Time.deltaTime);

                transform.rotation = targetRotation;
            }
            else
            {
                Vector3 rotationDirection = movementDirection;
                rotationDirection = _cameraHandler.currentLockOnTarget.position - transform.position;
                rotationDirection.y = 0;
                rotationDirection.Normalize();
                
                Quaternion tr = Quaternion.LookRotation(rotationDirection);
                Quaternion targetRotation = Quaternion.Slerp(transform.rotation, tr, rotationSpeed * Time.deltaTime);

                transform.rotation = targetRotation;
            }
        }
        else
        {
            float moveOverride = _inputHandler.movementMagnitude;

            // Setup target direction from input values and normalize
            Vector3 targetDirection = _cameraObject.forward * _inputHandler.vertical;
            targetDirection += _cameraObject.right * _inputHandler.horizontal;
            targetDirection.Normalize();
        
            // Prevent the character from rotation around the x or z axis
            targetDirection.y = 0;

            // If the player don't move, then is character his facing forward
            if (targetDirection == Vector3.zero)
            {
                targetDirection = _myTransform.forward;
            }

            // Apply movement stats of the script to the player target rotation
            float rs = rotationSpeed;
        
            // Compute the target rotation from the direction the player is targeting and slerp it to make it smooth
            Quaternion tr = Quaternion.LookRotation(targetDirection);
            Quaternion targetRotation = Quaternion.Slerp(_myTransform.rotation, tr, rs * delta);

            _myTransform.rotation = targetRotation;   
        }
    }
     
    #endregion

    #region Public

    /// <summary>
    /// This function is responsible for handling the movement behaviour of the player character.
    /// </summary>
    /// <param name="delta">A float value representing the elapsed time since the last game time tick.</param>
    public void HandleMovement(float delta)
    {
        // If the player is rolling, don't move
        if (_inputHandler.rollFlag)
            return;
        
        if (_playerManager.isInteracting)
            return;
        
        // Setup movement direction from input values
        movementDirection = _cameraObject.forward * _inputHandler.vertical;
        movementDirection += _cameraObject.right * _inputHandler.horizontal;
        movementDirection.Normalize();
        movementDirection.y = 0;

        // Apply movement stats of the script to the movement direction
        float speed = movementSpeed;

        // If the player is sprinting, setup a higher speed and raise the sprint status
        if (_inputHandler.sprintFlag && _inputHandler.movementMagnitude > 0.5)
        {
            speed = sprintSpeed;
            _playerManager.isSprinting = true;
            movementDirection *= speed;
        }
        else
        {
            if(_inputHandler.movementMagnitude < 0.5)
            {
                movementDirection *= walkingSpeed;
                _playerManager.isSprinting = false;
            }
            else
            {
                movementDirection *= speed;
                _playerManager.isSprinting = false;
            }
        }
        
        // Compute projected velocity from movement direction and normal vector and set it up on player rigidbody
        Vector3 projectedVelocity = Vector3.ProjectOnPlane(movementDirection, _normalVector);
        rigidbody.velocity = projectedVelocity;

        if (_inputHandler.lockOnFlag && !_inputHandler.sprintFlag)
        {
            // Update animator parameters value
            _animatorHandler.UpdateAnimatorValues(_inputHandler.vertical, _inputHandler.horizontal, _playerManager.isSprinting);
        }
        else
        {
            // Update animator parameters value
            _animatorHandler.UpdateAnimatorValues(_inputHandler.movementMagnitude, 0, _playerManager.isSprinting);
        }

        // Apply character rotation if concerned
        if (_animatorHandler.canRotate)
        {
            HandleRotation(delta);
        }
    }

    /// <summary>
    /// This function is responsible for handling the rolling and sprinting behaviours of the player character.
    /// </summary>
    /// <param name="delta">A float value representing the elapsed time since the last game time tick.</param>
    public void HandleRollingAndSprinting(float delta)
    {
        // If the player is already interacting, don't roll or sprint
        if (_animatorHandler.animator.GetBool("isInteracting"))
        {
            return;
        }

        // If the player called a roll interaction
        if (_inputHandler.rollFlag)
        {
            // Setup movement direction from input values
            movementDirection = _cameraObject.forward * _inputHandler.vertical;
            movementDirection += _cameraObject.right * _inputHandler.horizontal;
            
            if (_inputHandler.movementMagnitude > 0)
            {
                _animatorHandler.PlayTargetAnimation("Rolling", true);
                // Avoid rolling movement to move or character up
                movementDirection.y = 0;
                Quaternion rollRotation = Quaternion.LookRotation(movementDirection);
                _myTransform.rotation = rollRotation;
            }
            // If movement magnitude is zero or less, then call a backstep interaction
            else
            {
                _animatorHandler.PlayTargetAnimation("Backstep", true);
            }
        }
    }

    /// <summary>
    /// This function is responsible for handling the falling behaviour of the player character.
    /// </summary>
    /// <param name="delta">A float value representing the elapsed time since the last game time tick.</param>
    /// <param name="movementDirection">A <c>Vector3</c> Unity component representing the character movement direction.</param>
    public void HandleFalling(float delta, Vector3 movementDirection)
    {
        // Reset grounded status
        _playerManager.isGrounded = false;
        
        RaycastHit hit;
        
        // Setup the raycast origin position
        Vector3 origin = _myTransform.position;
        origin.y += groundDetectionRayStartPoint;

        // If raycast hit (if player is on ground), reset movement direction
        if (Physics.Raycast(origin, _myTransform.forward, out hit, 0.4f))
        {
            movementDirection = Vector3.zero;
        }

        // If player character is in air, add force to make it fall (the character don't fall by itself)
        if (_playerManager.isInAir)
        {
            rigidbody.AddForce(-Vector3.up * fallingSpeed);
            // To give the character a falling trajectory movement direction-related
            rigidbody.AddForce(movementDirection * fallingSpeed / 10f);
        }

        // Project the raycast a little further of the player character to test for a pit in front of the character
        Vector3 direction = movementDirection;
        direction.Normalize();
        origin += direction * groundDirectionRayDistance;

        _targetPosition = _myTransform.position;
        
        Debug.DrawRay(origin, -Vector3.up * minimumDistanceNeededToBeginFall, Color.red, 0.1f, false);
        if (Physics.Raycast(origin, -Vector3.up, out hit, minimumDistanceNeededToBeginFall, _ignoreForGroundCheck))
        {
            _playerManager.isGrounded = true;
            
            _normalVector = hit.normal;
            // Setup the character target y position to the raycast hit point position
            Vector3 targetPosition = hit.point;
            _targetPosition.y = targetPosition.y;

            if (_playerManager.isInAir)
            {
                // If player is in air for more than 0,5 seconds, then on raycast hit ground, play land animation
                if (inAirTimer > 0.5f)
                {
                    Debug.Log("You were in the air for " + inAirTimer);
                    _animatorHandler.PlayTargetAnimation("Land", true);
                    inAirTimer = 0;
                }
                // Else, the pit isn't high enough to make the land animation worth playing
                else
                {
                    _animatorHandler.PlayTargetAnimation("Empty", false);
                    inAirTimer = 0;
                }

                // The player isn't in air anymore
                _playerManager.isInAir = false;
            }
        }
        else
        {
            // If the raycast don't hit, it means the character will no longer be grounded
            if (_playerManager.isGrounded)
            {
                _playerManager.isGrounded = false;
            }

            // If the player is not already in the air
            if (!_playerManager.isInAir)
            {
                // If the player isn't interacting, play the fall animation
                if (!_playerManager.isInteracting)
                {
                    _animatorHandler.PlayTargetAnimation("Fall", true);
                }

                // Normalize player character velocity  
                Vector3 velocity = rigidbody.velocity;
                velocity.Normalize();
                rigidbody.velocity = velocity * (movementSpeed / 2);
                _playerManager.isInAir = true;
            }
        }

        // If player is grounded, adjust position to fit the player foot on the ground
        if (_playerManager.isGrounded)
        {
            if (_playerManager.isInteracting || _inputHandler.movementMagnitude > 0)
            {
                _myTransform.position =
                    Vector3.Lerp(_myTransform.position, _targetPosition, Time.deltaTime / 0.1f);
            }
            else
            {
                _myTransform.position = _targetPosition;
            }
        }
    }

    /// <summary>
    /// This function is responsible for handling the jumping behaviour of the player character.
    /// </summary>
    public void HandleJumping()
    {
        if (_playerManager.isInteracting)
        {
            return;
        }

        if (_inputHandler.jumpInput)
        {
            if (_inputHandler.movementMagnitude > 0)
            {
                movementDirection = _cameraObject.forward * _inputHandler.vertical;
                movementDirection += _cameraObject.right * _inputHandler.horizontal;
                
                _animatorHandler.PlayTargetAnimation("Jump", false);

                movementDirection.y = 0;
                Quaternion jumpRotation = Quaternion.LookRotation(movementDirection);
                _myTransform.rotation = jumpRotation;
            }
        }
    }

    #endregion
}