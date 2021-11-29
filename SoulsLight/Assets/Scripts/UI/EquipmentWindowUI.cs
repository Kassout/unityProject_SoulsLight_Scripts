using UnityEngine;

/// <summary>
/// Class <c>EquipmentWindowUI</c> is a Unity component script used to manage the equipment window UI behaviour.
/// </summary>
public class EquipmentWindowUI : MonoBehaviour
{
    #region Fields / Properties
    
    /// <summary>
    /// Instance variable <c>handEquipmentSlotUIs</c> is an array of Unity <c>HandEquipmentSlotUI</c> script components representing the hand equipment slot UI elements manager.
    /// </summary>
    private HandEquipmentSlotUI[] _handEquipmentSlotUIs;

    /// <summary>
    /// Instance variable <c>rightHandSlot01Selected</c> represents the status of selection of the first right hand slot.
    /// </summary>
    public bool rightHandSlot01Selected;
    
    /// <summary>
    /// Instance variable <c>rightHandSlot02Selected</c> represents the status of selection of the second right hand slot.
    /// </summary>
    public bool rightHandSlot02Selected;
    
    /// <summary>
    /// Instance variable <c>leftHandSlot01Selected</c> represents the status of selection of the first left hand slot.
    /// </summary>
    public bool leftHandSlot01Selected;
    
    /// <summary>
    /// Instance variable <c>leftHandSlot02Selected</c> represents the status of selection of the second left hand slot.
    /// </summary>
    public bool leftHandSlot02Selected;

    #endregion

    #region MonoBehaviour

    /// <summary>
    /// This function is called on the frame when a script is enabled just before any of the Update methods are called the first time.
    /// </summary>
    private void Start()
    {
        _handEquipmentSlotUIs = GetComponentsInChildren<HandEquipmentSlotUI>();
    }
    
    #endregion

    #region Public

    /// <summary>
    /// This function is responsible for loading the weapons in inventory to the equipment window UI.
    /// </summary>
    /// <param name="playerInventory">A Unity <c>PlayerInventory</c> script component representing the player inventory manager to fill the equipment window UI with.</param>
    public void LoadWeaponsOnEquipmentScreen(PlayerInventory playerInventory)
    {
        foreach (HandEquipmentSlotUI uiSlot in _handEquipmentSlotUIs)
        {
            if (uiSlot.rightHandSlot01)
            {
                uiSlot.AddItem(playerInventory.weaponsInRightHandSlots[0]);
            } 
            else if (uiSlot.rightHandSlot02)
            {
                uiSlot.AddItem(playerInventory.weaponsInRightHandSlots[1]);
            } 
            else if (uiSlot.leftHandSlot01)
            {
                uiSlot.AddItem(playerInventory.weaponsInLeftHandSlots[0]);
            }
            else
            {
                uiSlot.AddItem(playerInventory.weaponsInLeftHandSlots[1]);
            }
        }
    }

    /// <summary>
    /// This function is responsible for selecting the first right hand slot.
    /// </summary>
    public void SelectRightHandSlot01()
    {
        rightHandSlot01Selected = true;
    }

    /// <summary>
    /// This function is responsible for selecting the second right hand slot.
    /// </summary>
    public void SelectRightHandSlot02()
    {
        rightHandSlot02Selected = true;
    }
    
    /// <summary>
    /// This function is responsible for selecting the first left hand slot.
    /// </summary>
    public void SelectLeftHandSlot01()
    {
        leftHandSlot01Selected = true;
    }
    
    /// <summary>
    /// This function is responsible for selecting the second left hand slot.
    /// </summary>
    public void SelectLeftHandSlot02()
    {
        leftHandSlot02Selected = true;
    }

    #endregion

}
