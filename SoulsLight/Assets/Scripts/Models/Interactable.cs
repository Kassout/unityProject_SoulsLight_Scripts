using UnityEngine;

/// <summary>
/// Class <c>Interactable</c> is a Unity component script used to manage interactable game objects behaviour.
/// </summary>
public class Interactable : MonoBehaviour
{
    #region Fields / Properties

    /// <summary>
    /// Instance variable <c>radius</c> represents the radius value of the gizmos sphere of interaction to draw.
    /// </summary>
    public float radius = 0.6f;
    
    /// <summary>
    /// Instance variable <c>interactableText</c> represents the pop-up text to display on interaction.
    /// </summary>
    public string interactableText;

    #endregion

    #region MonoBehaviour

    /// <summary>
    /// This function is responsible for drawing the gizmos sphere of influence on Unity editor selection.
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position, radius);
    }

    #endregion

    #region Public

    /// <summary>
    /// This function is responsible for managing the interaction behaviour of the associated game object.
    /// </summary>
    /// <param name="playerManager">A Unity <c>PlayerManagement</c> component representing the general player game behaviour manager.</param>
    public virtual void Interact(PlayerManager playerManager)
    {
        // Called when player interacts
        Debug.Log("You interacted with an object.");
    }

    #endregion
}
