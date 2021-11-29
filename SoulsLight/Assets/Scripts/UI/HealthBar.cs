using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class <c>HealthBar</c> is a Unity component script used to control the player's UI health bar behaviour.
/// </summary>
public class HealthBar : MonoBehaviour
{
    #region Fields / Properties

    /// <summary>
    /// Instance variable <c>slider</c> is a Unity <c>Slider</c> component representing UI slider of the player's health bar.
    /// </summary>
    private Slider _slider;

    #endregion

    #region MonoBehaviour

    /// <summary>
    /// This function is called on the frame when a script is enabled just before any of the Update methods are called the first time.
    /// </summary>
    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    #endregion

    #region Public

    /// <summary>
    /// This function is responsible for setting up the max health value of the player UI health bar.
    /// </summary>
    /// <param name="maxHealth">An integer value representing the max health of the player to setup.</param>
    public void SetMaxHealth(int maxHealth)
    {
        _slider.maxValue = maxHealth;
        _slider.value = maxHealth;
    }
    
    /// <summary>
    /// This function is responsible for rendering up the current player's health value on the UI health bar.
    /// </summary>
    /// <param name="currentHealth">An integer value representing the current health quantity of the player.</param>
    public void SetCurrentHealth(int currentHealth)
    {
        _slider.value = currentHealth;
    }

    #endregion
}
