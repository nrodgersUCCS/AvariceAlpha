using UnityEngine;

public class Dagger : Weapon
{
    // Constants
    protected float SPIN_TORQUE;          // Torque used to spin dagger

    /// <summary>
    /// Called before start
    /// </summary>
    protected override void Awake()
    {
        // Call parent method
        base.Awake();

        // Set override values
        SPIN_TORQUE = ((DaggerValues)Constants.Values(ContainerType.DAGGER)).SpinTorque;
        throwMaterial = Resources.Load<PhysicsMaterial2D>("Materials/PhysicsMaterials/Dagger");
        ThrowAnimation = ThrowAnimation.DAGGER;
        PickupSound = AudioClipName.DAGGER_PICKUP;
        if (IsTempered)
            throwSound = AudioClipName.TEMPERED_DAGGER_THROW;
        else if (Type == DamageType.FLAME)
            throwSound = AudioClipName.FIRE_DAGGER_THROW;
        else if (Type == DamageType.FROST)
            throwSound = AudioClipName.FROST_DAGGER_THROW;
        else
            throwSound = AudioClipName.DAGGER_THROW;
        wallHitSound = AudioClipName.DAGGER_HIT_WALL;
        enemyHitSound = AudioClipName.DAGGER_HIT_ENEMY;
        SprHUD = Resources.Load<Sprite>("Sprites/UI/spr_daggerIcon");
        ShadowSprHUD = Resources.Load<Sprite>("Sprites/UI/spr_daggerIconShadow");
    }

    /// <summary>
    /// Detach the dagger from the player and adds its spin
    /// </summary>
    public override void Detach()
    {
        base.Detach();

        // Start dagger spin
        GetComponent<Rigidbody2D>().AddTorque(SPIN_TORQUE, ForceMode2D.Impulse);
    }
}
