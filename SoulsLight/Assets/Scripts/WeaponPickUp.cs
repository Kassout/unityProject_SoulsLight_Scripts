using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class <c>WeaponPickUp</c> is a Unity component script used to manage interactable weapon item picks up behaviour.
/// </summary>
public class WeaponPickUp : Interactable
{
    #region Fields / Properties

    /// <summary>
    /// Instance variable <c>weapon</c> is a Unity <c>WeaponItem</c> scriptable object representing the interactable weapon to pick up.
    /// </summary>
    public WeaponItem weapon;

    #endregion

    #region Public

    /// <summary>
    /// This function is responsible for managing the pick up weapon item behaviour.
    /// </summary>
    /// <param name="playerManager">A Unity <c>PlayerManagement</c> component representing the general player game behaviour manager.</param>
    public override void Interact(PlayerManager playerManager)
    {
        base.Interact(playerManager);
        
        PickUpItem(playerManager);
    }

    #endregion

    #region Private

    /// <summary>
    /// This function is responsible for picking up the item to player inventory, on call.
    /// </summary>
    /// <param name="playerManager">A Unity <c>PlayerManagement</c> component representing the general player game behaviour manager.</param>
    private void PickUpItem(PlayerManager playerManager)
    {
        PlayerInventory playerInventory = playerManager.GetComponent<PlayerInventory>();
        PlayerLocomotion playerLocomotion = playerManager.GetComponent<PlayerLocomotion>();
        AnimatorHandler animatorHandler = playerManager.GetComponentInChildren<AnimatorHandler>();
        
        // Stops the player from moving whilst picking up items
        playerLocomotion.rigidbody.velocity = Vector3.zero;
        animatorHandler.PlayTargetAnimation("Pick Up Item", true);
        playerInventory.weaponsInventory.Add(weapon);

        playerManager.itemInteractableGameObject.GetComponentInChildren<Text>().text = weapon.itemName;
        playerManager.itemInteractableGameObject.GetComponentInChildren<RawImage>().texture = weapon.itemIcon.texture;
        playerManager.itemInteractableGameObject.SetActive(true);
        Destroy(gameObject);
    }

    #endregion
}
