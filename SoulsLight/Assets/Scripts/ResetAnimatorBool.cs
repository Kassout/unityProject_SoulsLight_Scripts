using UnityEngine;

/// <summary>
/// Class <c>ResetAnimatorBool</c> is a Unity component script used to manage the different player character animation status.
/// </summary>
public class ResetAnimatorBool : StateMachineBehaviour
{
    #region Fields / Properties

    /// <summary>
    /// Instance variable <c>targetBool</c> represents the boolean target to setup the status of.
    /// </summary>
    [SerializeField] private string targetBool;

    /// <summary>
    /// Instance variable <c>status</c> represents the boolean value to setup the target to.
    /// </summary>
    [SerializeField] private bool status;

    #endregion

    #region StateMachineBehaviour

    /// <summary>
    /// This function is called on the first Update frame when a state machine evaluate this state.
    /// </summary>
    /// <param name="animator">A Unity <c>Animator</c> component representing the game object animator.</param>
    /// <param name="stateInfo">A Unity <c>AnimatorStateInfo</c> structure representing game object status information about the animator</param>
    /// <param name="layerIndex">An integer value representing the animation layer index.</param>
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(targetBool, status);
    }

    #endregion
}
