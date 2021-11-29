using System;
using UnityEngine;

/// <summary>
/// Class <c>UIManager</c> is a Unity component script used to manage the general game UI behaviour.
/// </summary>
public class UIManager : MonoBehaviour
{
    #region Fields / Properties

    /// <summary>
    /// Instance variable <c>weaponInventorySlots</c> is a list of Unity <c>WeaponInventorySlot</c> component scripts representing the weapon inventory slot managers.
    /// </summary>
    private WeaponInventorySlot[] _weaponInventorySlots;

    /// <summary>
    /// Instance variable <c>equipmentWindowUI</c> is a Unity <c>EquipmentWindowUI</c> component scripts representing the equipment window UI manager.
    /// </summary>
    private EquipmentWindowUI _equipmentWindowUI;

    /// <summary>
    /// Instance variable <c>playerInventory</c> is a Unity <c>PlayerInventory</c> component script representing the player inventory manager.
    /// </summary>
    public PlayerInventory playerInventory;
    
    /// <summary>
    /// Instance variable <c>hudWindow</c> is a Unity <c>GameObject</c> component representing the HUD window game object.
    /// </summary>
    [Header("UI Windows")]
    public GameObject hudWindow;
    
    /// <summary>
    /// Instance variable <c>selectWindow</c> is a Unity <c>GameObject</c> component representing the window of UIs selection.
    /// </summary>
    public GameObject selectWindow;

    /// <summary>
    /// Instance variable <c>weaponInventoryWindow</c> is a Unity <c>GameObject</c> component representing the weapon inventory window game object.
    /// </summary>
    public GameObject weaponInventoryWindow;

    /// <summary>
    /// Instance variable <c>weaponInventorySlotPrefab</c> is a Unity <c>GameObject</c> component representing the weapon inventory slot prefab game object.
    /// </summary>
    [Header("Weapon Inventory")]
    public GameObject weaponInventorySlotPrefab;

    /// <summary>
    /// Instance variable <c>weaponInventorySlotsParent</c> is a Unity <c>Transform</c> component representing the weapon inventory slot parent transform.
    /// </summary>
    public Transform weaponInventorySlotsParent;

    #endregion

    #region MonoBehaviour

    /// <summary>
    /// This function is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        _equipmentWindowUI = FindObjectOfType<EquipmentWindowUI>();
    }

    /// <summary>
    /// This function is called on the frame when a script is enabled just before any of the Update methods are called the first time.
    /// </summary>
    private void Start()
    {
        _weaponInventorySlots = weaponInventorySlotsParent.GetComponentsInChildren<WeaponInventorySlot>();
        _equipmentWindowUI.LoadWeaponsOnEquipmentScreen(playerInventory);
    }

    #endregion
    
    #region Public

    /// <summary>
    /// This function is responsible for updating the UI display.
    /// </summary>
    public void UpdateUI()
    {
        #region Weapon Inventory Slots

        for (int i = 0; i < _weaponInventorySlots.Length; i++)
        {
            if (i < playerInventory.weaponsInventory.Count)
            {
                if (_weaponInventorySlots.Length < playerInventory.weaponsInventory.Count)
                {
                    Instantiate(weaponInventorySlotPrefab, weaponInventorySlotsParent);
                    _weaponInventorySlots = weaponInventorySlotsParent.GetComponentsInChildren<WeaponInventorySlot>();
                }
                
                _weaponInventorySlots[i].AddItem(playerInventory.weaponsInventory[i]);
            }
            else
            {
                _weaponInventorySlots[i].ClearInventorySlot();
            }
        }

        #endregion
    }

    /// <summary>
    /// This function is responsible for opening the window of UIs selection.
    /// </summary>
    public void OpenSelectWindow()
    {
        selectWindow.SetActive(true);
    }

    /// <summary>
    /// This function is responsible for closing the window of UIs selection.
    /// </summary>
    public void CloseSelectWindow()
    {
        selectWindow.SetActive(false);
    }

    /// <summary>
    /// This function is responsible for closing the all inventory windows.
    /// </summary>
    public void CloseAllInventoryWindows()
    {
        weaponInventoryWindow.SetActive(false);
    }
    
    #endregion
}
