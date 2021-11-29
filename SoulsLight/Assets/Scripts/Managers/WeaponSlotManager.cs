using UnityEngine;

/// <summary>
/// Class <c>WeaponSlotManager</c> is a Unity component script used to manage the weapon slots of the player character.
/// </summary>
public class WeaponSlotManager : MonoBehaviour
{
    #region Fields / Properties

    /// <summary>
    /// Instance variable <c>animator</c> is a Unity <c>Animator</c> component representing the player character animator.
    /// </summary>
    private Animator _animator;

    /// <summary>
    /// Instance variable <c>quickSlotsUI</c> is a Unity <c>QuickSlotsUI</c> component script representing the quick slots UI manager.
    /// </summary>
    private QuickSlotsUI _quickSlotsUI;

    /// <summary>
    /// Instance variable <c>playerStats</c> is a Unity <c>PlayerStats</c> component script representing the player stats manager.
    /// </summary>
    private PlayerStats _playerStats;
    
    /// <summary>
    /// Instance variable <c>leftHandSlot</c> is a Unity <c>WeaponHolderSlot</c> component representing the left hand weapon slot of the player character.
    /// </summary>
    private WeaponHolderSlot _leftHandSlot;
    
    /// <summary>
    /// Instance variable <c>rightHandSlot</c> is a Unity <c>WeaponHolderSlot</c> component representing the right hand weapon slot of the player character.
    /// </summary>
    private WeaponHolderSlot _rightHandSlot;

    /// <summary>
    /// Instance variable <c>leftHandDamageCollider</c> is a Unity <c>DamageCollider</c> component representing the left hand weapon damage collider of the player character.
    /// </summary>
    private DamageCollider _leftHandDamageCollider;
    
    /// <summary>
    /// Instance variable <c>rightHandDamageCollider</c> is a Unity <c>DamageCollider</c> component representing the right hand weapon damage collider of the player character.
    /// </summary>
    private DamageCollider _rightHandDamageCollider;

    /// <summary>
    /// Instance variable <c>attackingWeapon</c> is a Unity <c>WeaponItem</c> scriptable object representing the attacking weapon of the player.
    /// </summary>
    [HideInInspector] public WeaponItem attackingWeapon;

    #endregion

    #region MonoBehaviour

    /// <summary>
    /// This function is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _quickSlotsUI = FindObjectOfType<QuickSlotsUI>();
        _playerStats = GetComponentInParent<PlayerStats>();
        
        WeaponHolderSlot[] weaponHolderSlots = GetComponentsInChildren<WeaponHolderSlot>();
        foreach (WeaponHolderSlot weaponSlot in weaponHolderSlots)
        {
            if (weaponSlot.isLeftHandSlot)
            {
                _leftHandSlot = weaponSlot;
            }
            else if (weaponSlot.isRightHandSlot)
            {
                _rightHandSlot = weaponSlot;
            }
        }
    }

    #endregion

    #region Public

    /// <summary>
    /// This function is responsible for loading the given weapon on the target hand slot.
    /// </summary>
    /// <param name="weaponItem">An Unity <c>WeaponItem</c> scriptable object representing the weapon item to load.</param>
    /// <param name="isLeft">A boolean value representing the is weapon left hand status.</param>
    public void LoadWeaponOnSlot(WeaponItem weaponItem, bool isLeft)
    {
        if (isLeft)
        {
            _leftHandSlot.LoadWeaponModel(weaponItem);
            LoadLeftWeaponDamageCollider();
            
            _quickSlotsUI.UpdateWeaponQuickSlotsUI(true, weaponItem);

            #region Handle Left Weapon Idle Animations

            if (weaponItem)
            {
                _animator.CrossFade(weaponItem.leftHandIdle, 0.2f);
            }
            else
            {
                _animator.CrossFade("Left Arm Empty", 0.2f);
            }

            #endregion
        }
        else
        {
            _rightHandSlot.LoadWeaponModel(weaponItem);
            LoadRightWeaponDamageCollider();
            
            _quickSlotsUI.UpdateWeaponQuickSlotsUI(false, weaponItem);

            #region Handle Right Weapon Idle Animations

            if (weaponItem)
            {
                _animator.CrossFade(weaponItem.rightHandIdle, 0.2f);
            }
            else
            {
                _animator.CrossFade("Right Arm Empty", 0.2f);
            }

            #endregion
        }
    }

    #endregion
    
    #region Handle Weapon's Damage Collider

    /// <summary>
    /// This function is responsible for loading the damage collider of the left hand weapon.
    /// </summary>
    private void LoadLeftWeaponDamageCollider()
    {
        _leftHandDamageCollider = _leftHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
    }

    /// <summary>
    /// This function is responsible for loading the damage collider of the right hand weapon.
    /// </summary>
    private void LoadRightWeaponDamageCollider()
    {
        _rightHandDamageCollider = _rightHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
    }

    /// <summary>
    /// This function is responsible for enabling the damage collider of the right hand weapon.
    /// </summary>
    public void OpenRightDamageCollider()
    {
        _rightHandDamageCollider.EnableDamageCollider();
    }

    /// <summary>
    /// This function is responsible for enabling the damage collider of the left hand weapon.
    /// </summary>
    public void OpenLeftDamageCollider()
    {
        _leftHandDamageCollider.EnableDamageCollider();
    }

    /// <summary>
    /// This function is responsible for disabling the damage collider of the right hand weapon.
    /// </summary>
    public void CloseRightDamageCollider()
    {
        _rightHandDamageCollider.DisableDamageCollider();
    }

    /// <summary>
    /// This function is responsible for disabling the damage collider of the left hand weapon.
    /// </summary>
    public void CloseLeftDamageCollider()
    {
        _leftHandDamageCollider.DisableDamageCollider();
    }

    #endregion

    #region Handle Weapon's Stamina Drainage

    /// <summary>
    /// This function is responsible for draining player stamina related to a light attack action.
    /// </summary>
    public void DrainStaminaLightAttack()
    {
        _playerStats.TakeStaminaDamage(Mathf.RoundToInt(attackingWeapon.baseStaminaCost * attackingWeapon.lightAttackMultiplier));
    }

    /// <summary>
    /// This function is responsible for draining player stamina related to a heavy attack action.
    /// </summary>
    public void DrainStaminaHeavyAttack()
    {
        _playerStats.TakeStaminaDamage(Mathf.RoundToInt(attackingWeapon.baseStaminaCost * attackingWeapon.heavyAttackMultiplier));
    }

    #endregion
}
