using UnityEngine;

/// <summary>
/// Class <c>AnimatorHandler</c> is a Unity component script used to manage the player character animations behaviour.
/// </summary>
public class AnimatorHandler : MonoBehaviour
{
    #region Fields / Properties
    
    /// <summary>
    /// Instance variable <c>inputHandler</c> is a Unity <c>InputHandler</c> script component representing the player input handler.
    /// </summary>
    private InputHandler _inputHandler;
    
    /// <summary>
    /// Instance variable <c>playerManager</c> is a Unity <c>PlayerManager</c> script component representing the player behaviour manager.
    /// </summary>
    private PlayerManager _playerManager;

    /// <summary>
    /// Instance variable <c>playerLocomotion</c> is a Unity <c>PlayerLocomotion</c> script component representing the player locomotion manager.
    /// </summary>
    private PlayerLocomotion _playerLocomotion;

    /// <summary>
    /// Instance variable <c>vertical</c> represents the parameter id of the <c>Vertical</c> animator parameter.
    /// </summary>
    private int _vertical;
    
    /// <summary>
    /// Instance variable <c>horizontal</c> represents the parameter id of the <c>Horizontal</c> animator parameter.
    /// </summary>
    private int _horizontal;
    
    /// <summary>
    /// Instance variable <c>canRotate</c> represents the rotation able status of the player character.
    /// </summary>
    public bool canRotate;
    
    /// <summary>
    /// Instance variable <c>animator</c> is a Unity <c>Animator</c> component representing the player character animator.
    /// </summary>
    [HideInInspector] public Animator animator;

    #endregion

    #region MonoBehaviour

    /// <summary>
    /// This function is called at each frame after the state machines and the animations have been evaluated, but before OnAnimatorIK.
    /// </summary>
    private void OnAnimatorMove()
    {
        // If player is not interacting, don't execute the function
        if (_playerManager.isInteracting == false)
        {
            return;
        }

        // On animation moved, readjust the character model position to the center of the game object
        float delta = Time.deltaTime;
        _playerLocomotion.rigidbody.drag = 0;
        Vector3 deltaPosition = animator.deltaPosition;
        deltaPosition.y = 0;
        Vector3 velocity = deltaPosition / delta;
        _playerLocomotion.rigidbody.velocity = velocity;
    }

    #endregion

    #region Public

    /// <summary>
    /// This function is called to initialize the attributes of the <c>AnimatorHandler</c> component object. 
    /// </summary>
    public void Initialize()
    {
        _playerManager = GetComponentInParent<PlayerManager>();
        animator = GetComponent<Animator>();
        _inputHandler = GetComponentInParent<InputHandler>();
        _playerLocomotion = GetComponentInParent<PlayerLocomotion>();
        _vertical = Animator.StringToHash("Vertical");
        _horizontal = Animator.StringToHash("Horizontal");
    }

    /// <summary>
    /// This function is responsible for updating the player character animator parameters.
    /// </summary>
    /// <param name="verticalMovement">A float value representing the vertical parameter value to setup.</param>
    /// <param name="horizontalMovement">A float value representing the horizontal parameter value to setup.</param>
    public void UpdateAnimatorValues(float verticalMovement, float horizontalMovement, bool isSprinting)
    {
        #region Vertical

        float v = 0;

        if (verticalMovement > 0 && verticalMovement < 0.55f)
        {
            v = 0.5f;
        }
        else if (verticalMovement > 0.55f)
        {
            v = 1f;
        }
        else if (verticalMovement < 0 && verticalMovement > -0.55f)
        {
            v = -0.5f;
        }
        else if (verticalMovement < -0.55f)
        {
            v = -1f;
        }
        else
        {
            v = 0f;
        }

        #endregion

        #region Horizontal


        float h = 0;

        if (horizontalMovement > 0 && horizontalMovement < 0.55f)
        {
            h = 0.5f;
        }
        else if (horizontalMovement > 0.55f)
        {
            h = 1f;
        }
        else if (horizontalMovement < 0 && horizontalMovement > -0.55f)
        {
            h = -0.5f;
        }
        else if (horizontalMovement < -0.55f)
        {
            h = -1f;
        }
        else
        {
            h = 0f;
        }

        #endregion

        if (isSprinting)
        {
            v = 2;
            h = horizontalMovement;
        }

        animator.SetFloat(_vertical, v, 0.1f, Time.deltaTime);
        animator.SetFloat(_horizontal, h, 0.1f, Time.deltaTime);
    }

    /// <summary>
    /// This function is responsible for playing a given animation by name when called.
    /// </summary>
    /// <param name="targetAnim">A string value representing the animation name to play.</param>
    /// <param name="isInteracting">A boolean value representing the interaction status of the game object to animate.</param>
    public void PlayTargetAnimation(string targetAnim, bool isInteracting)
    {
        animator.applyRootMotion = isInteracting;
        animator.SetBool("isInteracting", isInteracting);
        animator.CrossFade(targetAnim, 0.2f);
    }

    /// <summary>
    /// This function is responsible for checking the rotation able status of the player character.
    /// </summary>
    public void CanRotate()
    {
        canRotate = true;
    }

    /// <summary>
    /// This function is responsible for stopping the player character rotation.
    /// </summary>
    public void StopRotation()
    {
        canRotate = false;
    }

    /// <summary>
    /// This function is responsible for enabling the player combo attack.
    /// </summary>
    public void EnableCombo()
    {
        animator.SetBool("canDoCombo", true);
    }

    /// <summary>
    /// This function is responsible for disabling the player combo attack.
    /// </summary>
    public void DisableCombo()
    {
        animator.SetBool("canDoCombo", false);
    }

    #endregion
}