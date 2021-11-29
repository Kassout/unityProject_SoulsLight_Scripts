using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class <c>StaminaBar</c> is a Unity component script used to control the player's UI stamina bar behaviour.
/// </summary>
public class StaminaBar : MonoBehaviour
{
    #region Fields / Properties

    /// <summary>
    /// Instance variable <c>slider</c> is a Unity <c>Slider</c> component representing UI slider of the player's stamina bar.
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
    /// This function is responsible for setting up the max stamina value of the player UI stamina bar.
    /// </summary>
    /// <param name="maxStamina">An integer value representing the max stamina of the player to setup.</param>
    public void SetMaxStamina(int maxStamina)
    {
        _slider.maxValue = maxStamina;
        _slider.value = maxStamina;
    }

    /// <summary>
    /// This function is responsible for rendering up the current player's stamina value on the UI stamina bar.
    /// </summary>
    /// <param name="currentStamina">An integer value representing the current stamina quantity of the player.</param>
    public void SetCurrentStamina(int currentStamina)
    {
        _slider.value = currentStamina;
    }

    #endregion
}
