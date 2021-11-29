using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    #region Fields / Properties

    /// <summary>
    /// Instance variable <c>animator</c> is a Unity <c>Animator</c> component object representing the player animator.
    /// </summary>
    private Animator _animator;

    /// <summary>
    /// Instance variable <c>maxHealth</c> represents the max health value of the player character.
    /// </summary>
    private int _maxHealth;

    /// <summary>
    /// Instance variable <c>currentHealth</c> represents the current health value of the player character.
    /// </summary>
    private int _currentHealth;
    
    /// <summary>
    /// Instance variable <c>healthLevel</c> represents the health level value of the player character.
    /// </summary>
    public int healthLevel = 10;

    #endregion

    #region MonoBehaviour

    /// <summary>
    /// This function is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    /// <summary>
    /// This function is called on the frame when a script is enabled just before any of the Update methods are called the first time.
    /// </summary>
    private void Start()
    {
        _maxHealth = SetMaxHealthFromHealthLevel();
        _currentHealth = _maxHealth;
    }

    #endregion

    #region Private

    /// <summary>
    /// This function is responsible for converting health level to a max health value.
    /// </summary>
    /// <returns></returns>
    private int SetMaxHealthFromHealthLevel()
    {
        _maxHealth = healthLevel * 10;
        return _maxHealth;
    }

    #endregion

    #region Public

    /// <summary>
    /// This function is responsible for updating player character stats regarding a take damage action.
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage)
    {
        _currentHealth = _currentHealth - damage;
        
        _animator.Play("GetHit");

        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            _animator.Play("Death");
            // TODO: HANDLE ENEMY DEATH
        }
    }

    #endregion
}
