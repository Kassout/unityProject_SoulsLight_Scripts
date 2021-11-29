using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class <c>QuickSlotsUI</c> is a Unity component script used to manage the quick slots UI behaviour.
/// </summary>
public class QuickSlotsUI : MonoBehaviour
{
    #region Fields / Properties

    /// <summary>
    /// Instance variable <c>leftWeaponIcon</c> is a Unity <c>Image</c> component representing the left hand weapon icon.
    /// </summary>
    [SerializeField] private Image leftWeaponIcon;
    
    /// <summary>
    /// Instance variable <c>rightWeaponIcon</c> is a Unity <c>Image</c> component representing the right hand weapon icon.
    /// </summary>
    [SerializeField] private Image rightWeaponIcon;
    
    #endregion

    #region Public

    /// <summary>
    /// This function is responsible for updating the weapon quick slots UI components.
    /// </summary>
    /// <param name="isLeft">A boolean value representing the is left hand status of the weapon item.</param>
    /// <param name="weapon">An Unity <c>WeaponItem</c> scriptable object representing the weapon object to load the icon in the UI.</param>
    public void UpdateWeaponQuickSlotsUI(bool isLeft, WeaponItem weapon)
    {
        if (isLeft)
        {
            if (weapon.itemIcon)
            {
                leftWeaponIcon.sprite = weapon.itemIcon;
                leftWeaponIcon.enabled = true;
            }
            else
            {
                leftWeaponIcon.sprite = null;
                leftWeaponIcon.enabled = false;
            }
        }
        else
        {
            if (weapon.itemIcon)
            {
                rightWeaponIcon.sprite = weapon.itemIcon;
                rightWeaponIcon.enabled = true;
            }
            else
            {
                rightWeaponIcon.sprite = null;
                rightWeaponIcon.enabled = false;
            }
        }
    }

    #endregion
}
