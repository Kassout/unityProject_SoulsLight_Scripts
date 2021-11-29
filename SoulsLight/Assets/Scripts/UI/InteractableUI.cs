using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class <c>InteractableUI</c> is a Unity component script used to manage the interactable UI behaviour.
/// </summary>
public class InteractableUI : MonoBehaviour
{
    #region Fields / Properties

    /// <summary>
    /// Instance variable <c>interactableText</c> is a Unity <c>Text</c> component representing the interaction type text.
    /// </summary>
    public Text interactableText;
    
    /// <summary>
    /// Instance variable <c>itemNameText</c> is a Unity <c>Text</c> component representing the item name text.
    /// </summary>
    public Text itemNameText;

    /// <summary>
    /// Instance variable <c>itemImage</c> is a Unity <c>RawImage</c> component representing the item to pick up image.
    /// </summary>
    public RawImage itemImage;

    #endregion
}
