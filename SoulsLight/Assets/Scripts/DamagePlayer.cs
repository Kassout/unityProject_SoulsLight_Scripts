using UnityEngine;

/// <summary>
/// Class <c>DamagePlayer</c> is a Unity component script used to control damage dealing to player behaviour.
/// </summary>
public class DamagePlayer : MonoBehaviour
{
    #region Fields / Properties

    /// <summary>
    /// Instance variable <c>damage</c> represents the current damage value the player take when trigger get hit action.
    /// </summary>
    [SerializeField] private int damage = 25;

    #endregion

    #region MonoBehaviour

    /// <summary>
    /// This method is called when another object enters a trigger collider attached to this object.
    /// </summary>
    /// <param name="other">A <c>Collider</c> Unity component representing the collider of the object that it collides with.</param>
    private void OnTriggerEnter(Collider other)
    {
        PlayerStats playerStats = other.GetComponentInParent<PlayerStats>();

        if (playerStats)
        {
            playerStats.TakeDamage(damage);
        }
    }

    #endregion
}
