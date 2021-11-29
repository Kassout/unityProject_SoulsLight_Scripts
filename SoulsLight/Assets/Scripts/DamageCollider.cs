using UnityEngine;

/// <summary>
/// Class <c>DamageCollider</c> is a Unity component script used to manage damage collider behaviour.
/// </summary>
public class DamageCollider : MonoBehaviour
{
    #region Fields / Properties

    /// <summary>
    /// Instance variable <c>damageCollider</c> is a Unity <c>Collider</c> component object representing the damage collider of the associated game object.
    /// </summary>
    private Collider _damageCollider;
    
    /// <summary>
    /// Instance variable <c>currentWeaponDamage</c> represents the current weapon damage value of the damage collider.
    /// </summary>
    [SerializeField] private int currentWeaponDamage = 25;

    #endregion

    #region MonoBehaviour

    /// <summary>
    /// This function is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        _damageCollider = GetComponent<Collider>();
        _damageCollider.gameObject.SetActive(true);
        _damageCollider.isTrigger = true;
        _damageCollider.enabled = false;
    }
    
    /// <summary>
    /// This method is called when another object enters a trigger collider attached to this object.
    /// </summary>
    /// <param name="collision">A <c>Collider</c> Unity component representing the collider of the object that it collides with.</param>
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerStats playerStats = collision.GetComponent<PlayerStats>();

            if (playerStats)
            {
                playerStats.TakeDamage(currentWeaponDamage);
            }
        }
        
        if (collision.CompareTag("Enemy"))
        {
            EnemyStats enemyStats = collision.GetComponent<EnemyStats>();

            if (enemyStats)
            {
                enemyStats.TakeDamage(currentWeaponDamage);
            }
        }
    }

    #endregion

    #region Public

    /// <summary>
    /// This function is responsible for enabling the damage collider.
    /// </summary>
    public void EnableDamageCollider()
    {
        _damageCollider.enabled = true;
    }

    /// <summary>
    /// This function is responsible for disabling the damage collider.
    /// </summary>
    public void DisableDamageCollider()
    {
        _damageCollider.enabled = false;
    }

    #endregion
}
