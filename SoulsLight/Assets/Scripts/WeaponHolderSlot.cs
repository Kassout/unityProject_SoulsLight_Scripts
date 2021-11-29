using UnityEngine;

/// <summary>
/// Class <c>WeaponHolderSlot</c> is a Unity component script used to control where the weapon is instantiated on her hands.
/// </summary>
public class WeaponHolderSlot : MonoBehaviour
{
    #region Fields / Properties

    /// <summary>
    /// Instance variable <c>parentOverride</c> is a Unity <c>Transform</c> component representing the position, rotation and scale of the weapon game object parent.
    /// </summary>
    [SerializeField] private Transform parentOverride;

    /// <summary>
    /// Instance variable <c>currentWeaponModel</c> is a Unity <c>GameObject</c> component representing the weapon game object.
    /// </summary>
    public GameObject currentWeaponModel;
    
    /// <summary>
    /// Instance variable <c>isLeftHandSlot</c> represents the left hand status of the weapon item.
    /// </summary>
    public bool isLeftHandSlot;

    /// <summary>
    /// Instance variable <c>isRightHandSlot</c> represents the right hand status of the weapon item.
    /// </summary>
    public bool isRightHandSlot;

    #endregion

    #region Public

    /// <summary>
    /// This function is responsible for unloading the current weapon model.
    /// </summary>
    public void UnloadWeapon()
    {
        if (currentWeaponModel)
        {
            currentWeaponModel.SetActive(false);
        }
    }

    /// <summary>
    /// This function is responsible for unloading and destroy the current weapon model.
    /// </summary>
    public void UnloadWeaponAndDestroy()
    {
        if (currentWeaponModel)
        {
            Destroy(currentWeaponModel);
        }
    }

    /// <summary>
    /// This function is responsible for loading the given <c>WeaponItem</c> object.
    /// </summary>
    /// <param name="weaponItem">An Unity <c>WeaponItem</c> scriptable object representing the weapon item to load.</param>
    public void LoadWeaponModel(WeaponItem weaponItem)
    {
        // Unload and destroy the current weapon item
        UnloadWeaponAndDestroy();

        if (!weaponItem)
        {
            UnloadWeapon();
            return;
        }

        GameObject model = Instantiate(weaponItem.modelPrefab) as GameObject;
        if (model)
        {
            model.transform.parent = parentOverride ? parentOverride : transform;

            model.transform.localPosition = Vector3.zero;
            model.transform.localRotation = Quaternion.identity;
            model.transform.localScale = Vector3.one;
        }

        currentWeaponModel = model;
    }

    #endregion
}
