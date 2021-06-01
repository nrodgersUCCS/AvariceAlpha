using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    Animator playerAnimator;        // Player animator

    public Weapon LeftWeapon { get; set; }  // Left hand weapon
    public Weapon RightWeapon { get; set; }  // Right hand weapon

    void Start()
    {
        // Set animator to the component
        playerAnimator = GetComponent<Animator>();

        // Get left and right hand weapons
        GameObject temp = GetComponent<PlayerInventory>().LeftHand;
        if (temp != null)
            LeftWeapon = temp.GetComponent<Weapon>();
        temp = GetComponent<PlayerInventory>().RightHand;
        if (temp != null)
            RightWeapon = temp.GetComponent<Weapon>();
    }

    void Update()
    {
        // Attack with left hand weapon
        if (Input.GetKeyDown(KeyCode.Mouse0) && LeftWeapon != null)
        {
            // Instantiate weapon
            Weapon weaponInstance = Instantiate<Weapon>(LeftWeapon, transform);

            // Weapon attack behavior
            weaponInstance.Attack();

            // Play player animation associated with weapon
            playerAnimator.SetTrigger(ConfigurationUtils.PlayerWeaponAnimationNames[weaponInstance.Name]);
        }

        // Attack with right hand weapon
        if (Input.GetKeyDown(KeyCode.Mouse1) && RightWeapon != null)
        {
            // Instantiate weapon
            Weapon weaponInstance = Instantiate<Weapon>(RightWeapon, transform);

            // Weapon attack behavior
            weaponInstance.Attack();

            // Play player animation associated with weapon
            playerAnimator.SetTrigger(ConfigurationUtils.PlayerWeaponAnimationNames[weaponInstance.Name]);
        }
    }
}
