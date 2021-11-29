using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class <c>HandEquipmentSlotUI</c> is a Unity component script used to manage the hand equipment slot UI behaviour.
/// </summary>
public class HandEquipmentSlotUI : MonoBehaviour
{
    #region Fields / Properties

    /// <summary>
    /// Instance variable <c>weapon</c> is a Unity <c>WeaponItem</c> scriptable object representing the data of the item in the hand equipment slot UI.
    /// </summary>
    private WeaponItem _weapon;
    
    /// <summary>
    /// Instance variable <c>icon</c> is a Unity <c>Image</c> component representing the hand equipment slot item image.
    /// </summary>
    public Image icon;
    
    /// <summary>
    /// Instance variable <c>rightHandSlot01</c> represents the status of the first right hand slot.
    /// </summary>
    public bool rightHandSlot01;
    
    /// <summary>
    /// Instance variable <c>rightHandSlot02</c> represents the status of the second right hand slot.
    /// </summary>
    public bool rightHandSlot02;

    /// <summary>
    /// Instance variable <c>leftHandSlot01</c> represents the status of the first left hand slot.
    /// </summary>
    public bool leftHandSlot01;
    
    /// <summary>
    /// Instance variable <c>leftHandSlot02</c> represents the status of the second left hand slot.
    /// </summary>
    public bool leftHandSlot02;

    #endregion

    #region Public

    /// <summary>
    /// This function is responsible for adding an item to the hand equipment slot UI object.
    /// </summary>
    /// <param name="newWeapon">An Unity <c>WeaponItem</c> scriptable object representing the weapon to put in the hand equipment slot UI.</param>
    public void AddItem(WeaponItem newWeapon)
    {
        _weapon = newWeapon;
        icon.sprite = _weapon.itemIcon;
        icon.enabled = true;
        gameObject.SetActive(true);
    }

    /// <summary>
    /// This function is responsible for clearing the hand equipment slot UI.
    /// </summary>
    public void ClearItem()
    {
        _weapon = null;
        icon.sprite = null;
        icon.enabled = false;
        gameObject.SetActive(false);
    }

    #endregion
}
