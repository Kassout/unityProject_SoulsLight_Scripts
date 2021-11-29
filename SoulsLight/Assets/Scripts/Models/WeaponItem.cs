using UnityEngine;

/// <summary>
/// Class <c>WeaponItem</c> is an Unity <c>Item</c> scriptable object used to defined and describe the general aspect constituting of a weapon item game object.
/// </summary>
[CreateAssetMenu(menuName = "Items/Weapon Item")]
public class WeaponItem : Item
{
    #region Fields / Properties

    /// <summary>
    /// Instance variable <c>modelPrefab</c> is a Unity <c>GameObject</c> component representing the model of the weapon item.
    /// </summary>
    public GameObject modelPrefab;

    /// <summary>
    /// Instance variable <c>isUnarmed</c> represents the unarmed status of the weapon item.
    /// </summary>
    public bool isUnarmed;

    /// <summary>
    /// Instance variable <c>rightHandIdle</c> represents the name of the target animation for the right hand idle.
    /// </summary>
    [Header("Idle Animations")] 
    public string rightHandIdle;

    /// <summary>
    /// Instance variable <c>leftHandIdle</c> represents the name of the target animation for the left hand idle.
    /// </summary>
    public string leftHandIdle;

    /// <summary>
    /// Instance variable <c>oneHandLightAttack1</c> represents the name of the target animation for the first one hand light attack.
    /// </summary>
    [Header("Attack Animations")]
    public string oneHandLightAttack1;

    /// <summary>
    /// Instance variable <c>oneHandLightAttack2</c> represents the name of the target animation for the second one hand light attack.
    /// </summary>
    public string oneHandLightAttack2;

    /// <summary>
    /// Instance variable <c>oneHandLightAttack3</c> represents the name of the target animation for the third one hand light attack.
    /// </summary>
    public string oneHandLightAttack3;

    /// <summary>
    /// Instance variable <c>oneHandHeavyAttack1</c> represents the name of the target animation for the first one hand heavy attack.
    /// </summary>
    public string oneHandHeavyAttack1;
    
    /// <summary>
    /// Instance variable <c>baseStaminaCost</c> represents the stamina cost value of an attack action realised with this weapon.
    /// </summary>
    [Header("Stamina Costs")]
    public int baseStaminaCost;
    
    /// <summary>
    /// Instance variable <c>lightAttackMultiplier</c> represents the stamina cost value multiplier for a light attack action realised with this weapon.
    /// </summary>
    public float lightAttackMultiplier;
    
    /// <summary>
    /// Instance variable <c>heavyAttackMultiplier</c> represents the stamina cost value multiplier for a heavy attack action realised with this weapon.
    /// </summary>
    public float heavyAttackMultiplier;

    #endregion
}
