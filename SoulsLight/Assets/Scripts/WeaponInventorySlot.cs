using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class <c>WeaponInventorySlot</c> is a Unity component script used to control where the weapon is instantiated on her hands.
/// </summary>
public class WeaponInventorySlot : MonoBehaviour
{
    /// <summary>
    /// Instance variable <c>item</c> is a Unity <c>WeaponItem</c> scriptable object representing the data of the weapon item of the inventory slot.
    /// </summary>
    private WeaponItem _item;
    
    /// <summary>
    /// Instance variable <c>icon</c> is a Unity <c>Image</c> component representing the icon of weapon item of the inventory slot.
    /// </summary>
    public Image icon;

    /// <summary>
    /// This function is responsible for adding a given weapon item to the weapon inventory slot.
    /// </summary>
    /// <param name="newItem">An Unity <c>WeaponItem</c> scriptable object representing the weapon to add to the inventory slot.</param>
    public void AddItem(WeaponItem newItem)
    {
        _item = newItem;
        icon.sprite = _item.itemIcon;
        icon.enabled = true;
        gameObject.SetActive(true);
    }

    /// <summary>
    /// This function is responsible for clearing the inventory slot.
    /// </summary>
    public void ClearInventorySlot()
    {
        _item = null;
        icon.sprite = null;
        icon.enabled = false;
        gameObject.SetActive(false);
    }
}
