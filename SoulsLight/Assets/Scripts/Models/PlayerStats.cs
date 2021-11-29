using UnityEngine;

/// <summary>
/// Class <c>PlayerStats</c> is a Unity component script used to manage the player character stats.
/// </summary>
public class PlayerStats : MonoBehaviour
{
    #region Fields / Properties

    /// <summary>
    /// Instance variable <c>animatorHandler</c> is a Unity <c>AnimatorHandler</c> component object representing the player animator handler.
    /// </summary>
    private AnimatorHandler _animatorHandler;
    
    /// <summary>
    /// Instance variable <c>healthBar</c> is a Unity <c>HealthBar</c> component representing the UI health bar manager.
    /// </summary>
    private HealthBar _healthBar;

    /// <summary>
    /// Instance variable <c>staminaBar</c> is a Unity <c>StaminaBar</c> component representing the UI stamina bar manager.
    /// </summary>
    private StaminaBar _staminaBar;
     
    /// <summary>
    /// Instance variable <c>healthLevel</c> represents the health level value of the player character.
    /// </summary>
    public int healthLevel = 10;

    /// <summary>
    /// Instance variable <c>maxHealth</c> represents the max health value of the player character.
    /// </summary>
    private int _maxHealth;

    /// <summary>
    /// Instance variable <c>currentHealth</c> represents the current health value of the player character.
    /// </summary>
    private int _currentHealth;

    /// <summary>
    /// Instance variable <c>staminaLevel</c> represents the stamina level value of the player character.
    /// </summary>
    public int staminaLevel = 10;
    
    /// <summary>
    /// Instance variable <c>maxStamina</c> represents the max stamina value of the player character.
    /// </summary>
    private int _maxStamina;
    
    /// <summary>
    /// Instance variable <c>currentStamina</c> represents the current stamina value of the player character.
    /// </summary>
    private int _currentStamina;

    #endregion

    #region MonoBehaviour

    /// <summary>
    /// This function is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        _healthBar = FindObjectOfType<HealthBar>();
        _staminaBar = FindObjectOfType<StaminaBar>();
        _animatorHandler = GetComponentInChildren<AnimatorHandler>();
    }

    /// <summary>
    /// This function is called on the frame when a script is enabled just before any of the Update methods are called the first time.
    /// </summary>
    private void Start()
    {
        _maxHealth = SetMaxHealthFromHealthLevel();
        _currentHealth = _maxHealth;
        _healthBar.SetMaxHealth(_maxHealth);
        _healthBar.SetCurrentHealth(_currentHealth);

        _maxStamina = SetMaxStaminaFromStaminaLevel();
        _currentStamina = _maxStamina;
        _staminaBar.SetMaxStamina(_maxStamina);
        _staminaBar.SetCurrentStamina(_currentStamina);
    }

    #endregion

    #region Private

    /// <summary>
    /// This function is responsible for converting health level to a max health value.
    /// </summary>
    /// <returns>An integer value representing the computed max health value of the player character.</returns>
    private int SetMaxHealthFromHealthLevel()
    {
        _maxHealth = staminaLevel * 10;
        return _maxHealth;
    }
    
    /// <summary>
    /// This function is responsible for converting stamina level to a max stamina value.
    /// </summary>
    /// <returns>An integer value representing the computed max stamina value of the player character.</returns>
    private int SetMaxStaminaFromStaminaLevel()
    {
        _maxStamina = healthLevel * 10;
        return _maxStamina;
    }

    #endregion

    #region Public

    /// <summary>
    /// This function is responsible for updating player character health stats regarding a take damage action.
    /// </summary>
    /// <param name="damage">An integer value representing the damage value taken by the player.</param>
    public void TakeDamage(int damage)
    {
        _currentHealth = _currentHealth - damage;
        
        _healthBar.SetCurrentHealth(_currentHealth);
        
        _animatorHandler.PlayTargetAnimation("GetHit", true);

        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            _animatorHandler.PlayTargetAnimation("Death", true);
            // TODO: HANDLE PLAYER DEATH
        }
    }

    /// <summary>
    /// This function is responsible for updating player character stamina stats regarding an attack action.
    /// </summary>
    /// <param name="damage">An integer value representing the stamina value cost of the attack action.</param>
    public void TakeStaminaDamage(int damage)
    {
        _currentStamina = _currentStamina - damage;
        
        _staminaBar.SetCurrentStamina(_currentStamina);
    }

    #endregion
}
