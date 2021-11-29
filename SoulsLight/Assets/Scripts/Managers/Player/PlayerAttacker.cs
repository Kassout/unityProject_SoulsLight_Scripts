using UnityEngine;

/// <summary>
/// Class <c>PlayerAttacker</c> is a Unity component script used to manage the player character attack behaviour.
/// </summary>
public class PlayerAttacker : MonoBehaviour
{
    #region Fields / Properties

    /// <summary>
    /// Instance variable <c>animatorHandler</c> is a Unity <c>AnimatorHandler</c> component object representing the player animator handler.
    /// </summary>
    private AnimatorHandler _animatorHandler;

    /// <summary>
    /// Instance variable <c>inputHandler</c> is a Unity <c>InputHandler</c> script component representing the player input handler.
    /// </summary>
    private InputHandler _inputHandler;

    /// <summary>
    /// Instance variable <c>weaponSlotManager</c> is a Unity <c>WeaponSlotManager</c> component representing the weapon slot manager of the player character.
    /// </summary>
    private WeaponSlotManager _weaponSlotManager;

    /// <summary>
    /// Instance variable <c>lastAttack</c> represents the animation name of the last player attack.
    /// </summary>
    private string _lastAttack;
    
    #endregion

    #region MonoBehaviour

    /// <summary>
    /// This function is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        _animatorHandler = GetComponentInChildren<AnimatorHandler>();
        _weaponSlotManager = GetComponentInChildren<WeaponSlotManager>();
        _inputHandler = GetComponent<InputHandler>();
    }

    #endregion

    #region Public

    /// <summary>
    /// This function is responsible for trigger a character combo of light attacks.
    /// </summary>
    /// <param name="weapon">An Unity <c>WeaponItem</c> scriptable object representing the weapon to trigger the attack combo animation from.</param>
    public void HandleWeaponCombo(WeaponItem weapon)
    {
        if (_inputHandler.comboFlag)
        {
            _animatorHandler.animator.SetBool("canDoCombo", false);
            
            if (_lastAttack == weapon.oneHandLightAttack1)
            {
                _animatorHandler.PlayTargetAnimation(weapon.oneHandLightAttack2, true);
                _lastAttack = weapon.oneHandLightAttack2;
            } else if (_lastAttack == weapon.oneHandLightAttack2)
            {
                _animatorHandler.PlayTargetAnimation(weapon.oneHandLightAttack3, true);
                _lastAttack = weapon.oneHandLightAttack3;
            }
        }
    }

    /// <summary>
    /// This function is responsible for trigger a character light attack.
    /// </summary>
    /// <param name="weapon">An Unity <c>WeaponItem</c> scriptable object representing the weapon to trigger the heavy attack animation from.</param>
    public void HandleLightAttack(WeaponItem weapon)
    {
        _weaponSlotManager.attackingWeapon = weapon;
        _animatorHandler.PlayTargetAnimation(weapon.oneHandLightAttack1, true);
        _lastAttack = weapon.oneHandLightAttack1;
    }

    /// <summary>
    /// This function is responsible for trigger a character heavy attack.
    /// </summary>
    /// <param name="weapon">An Unity <c>WeaponItem</c> scriptable object representing the weapon to trigger the heavy attack animation from.</param>
    public void HandleHeavyAttack(WeaponItem weapon)
    {
        _weaponSlotManager.attackingWeapon = weapon;
        _animatorHandler.PlayTargetAnimation(weapon.oneHandHeavyAttack1, true);
        _lastAttack = weapon.oneHandHeavyAttack1;
    }

    #endregion
}
