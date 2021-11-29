using UnityEngine;

/// <summary>
/// Class <c>Item</c> is an Unity scriptable object used to defined and describe the general aspect constituting of an item game object.
/// </summary>
public class Item : ScriptableObject
{
    #region Fields / Properties

    /// <summary>
    /// Instance variable <c>itemIcon</c> is a Unity <c>Sprite</c> component representing the sprite of the item.
    /// </summary>
    [Header("Item Information")] 
    public Sprite itemIcon;
    
    /// <summary>
    /// Instance variable <c>itemName</c> represents the name of the item.
    /// </summary>
    public string itemName;

    #endregion
}
