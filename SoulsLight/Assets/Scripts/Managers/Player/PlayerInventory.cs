using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class <c>PlayerInventory</c> is a Unity component script used to manage the player character inventory.
/// </summary>
public class PlayerInventory : MonoBehaviour
{
    #region Fields / Properties

    /// <summary>
    /// Instance variable <c>weaponSlotManager</c> is a Unity <c>WeaponSlotManager</c> component representing the weapon slot manager of the player character.
    /// </summary>
    private WeaponSlotManager _weaponSlotManager;
        
    /// <summary>
    /// Instance variable <c>currentRightWeaponIndex</c> represents the index value of the current equipped right hand weapon.
    /// </summary>
    private int _currentRightWeaponIndex = -1;
    
    /// <summary>
    /// Instance variable <c>currentLeftWeaponIndex</c> represents the index value of the current equipped left hand weapon.
    /// </summary>
    private int _currentLeftWeaponIndex = -1;    
    
    /// <summary>
    /// Instance variable <c>rightWeapon</c> is a Unity <c>WeaponItem</c> scriptable object representing the right hand weapon to load.
    /// </summary>
    [HideInInspector] public WeaponItem rightWeapon;
    
    /// <summary>
    /// Instance variable <c>leftWeapon</c> is a Unity <c>WeaponItem</c> scriptable object representing the left hand weapon to load.
    /// </summary>
    [HideInInspector] public WeaponItem leftWeapon;
    
    /// <summary>
    /// Instance variable <c>weaponsInventory</c> is a Unity <c>WeaponItem</c> list of scriptable objects representing the weapons stored in the player character inventory.
    /// </summary>
    [HideInInspector] public List<WeaponItem> weaponsInventory;

    /// <summary>
    /// Instance variable <c>unarmedWeapon</c> is a Unity <c>WeaponItem</c> scriptable object representing the unarmed weapon to load.
    /// </summary>
    public WeaponItem unarmedWeapon;
    
    /// <summary>
    /// Instance variable <c>weaponsInRightHandSlots</c> is an array of Unity <c>WeaponItem</c> scriptable objects representing the weapons accessible in right hand slots.
    /// </summary>
    public WeaponItem[] weaponsInRightHandSlots = new WeaponItem[1];
    
    /// <summary>
    /// Instance variable <c>weaponsInLeftHandSlots</c> is an array of Unity <c>WeaponItem</c> scriptable objects representing the weapons accessible in left hand slots.
    /// </summary>
    public WeaponItem[] weaponsInLeftHandSlots = new WeaponItem[1];

    #endregion

    #region MonoBehaviour

    /// <summary>
    /// This function is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        _weaponSlotManager = GetComponentInChildren<WeaponSlotManager>();
    }

    /// <summary>
    /// This function is called on the frame when a script is enabled just before any of the Update methods are called the first time.
    /// </summary>
    private void Start()
    {
        rightWeapon = unarmedWeapon;
        leftWeapon = unarmedWeapon;
    }

    #endregion

    #region Public

    /// <summary>
    /// This function is responsible for changing the equipped right hand weapon.
    /// </summary>
    public void ChangeRightWeapon()
    {
        _currentRightWeaponIndex += 1;

        if (_currentRightWeaponIndex == 0 && weaponsInRightHandSlots[0])
        {
            rightWeapon = weaponsInRightHandSlots[_currentRightWeaponIndex];
            _weaponSlotManager.LoadWeaponOnSlot(weaponsInRightHandSlots[_currentRightWeaponIndex], false);
        }
        else if (_currentRightWeaponIndex == 0 && !weaponsInRightHandSlots[0])
        {
            _currentRightWeaponIndex += 1;
        }
        else if (_currentRightWeaponIndex == 1 && weaponsInRightHandSlots[1])
        {
            rightWeapon = weaponsInRightHandSlots[_currentRightWeaponIndex];
            _weaponSlotManager.LoadWeaponOnSlot(weaponsInRightHandSlots[_currentRightWeaponIndex], false);
        }
        else if (_currentRightWeaponIndex == 1 && !weaponsInRightHandSlots[1])
        {
            _currentRightWeaponIndex += 1;
        }
        
        if (_currentRightWeaponIndex > weaponsInRightHandSlots.Length - 1)
        {
            _currentRightWeaponIndex = -1;
            rightWeapon = unarmedWeapon;
            _weaponSlotManager.LoadWeaponOnSlot(unarmedWeapon, false);
        }
    }
    
    /// <summary>
    /// This function is responsible for changing the equipped left hand weapon.
    /// </summary>
    public void ChangeLeftWeapon()
    {
        _currentLeftWeaponIndex += 1;

        if (_currentLeftWeaponIndex == 0 && weaponsInLeftHandSlots[0])
        {
            leftWeapon = weaponsInLeftHandSlots[_currentLeftWeaponIndex];
            _weaponSlotManager.LoadWeaponOnSlot(weaponsInLeftHandSlots[_currentLeftWeaponIndex], true);
        }
        else if (_currentLeftWeaponIndex == 0 && !weaponsInLeftHandSlots[0])
        {
            _currentLeftWeaponIndex += 1;
        }
        else if (_currentLeftWeaponIndex == 1 && weaponsInLeftHandSlots[1])
        {
            leftWeapon = weaponsInLeftHandSlots[_currentLeftWeaponIndex];
            _weaponSlotManager.LoadWeaponOnSlot(weaponsInLeftHandSlots[_currentLeftWeaponIndex], true);
        }
        else if (_currentLeftWeaponIndex == 1 && !weaponsInLeftHandSlots[1])
        {
            _currentLeftWeaponIndex += 1;
        }
        
        if (_currentLeftWeaponIndex > weaponsInLeftHandSlots.Length - 1)
        {
            _currentLeftWeaponIndex = -1;
            leftWeapon = unarmedWeapon;
            _weaponSlotManager.LoadWeaponOnSlot(unarmedWeapon, true);
        }
    }

    #endregion
}