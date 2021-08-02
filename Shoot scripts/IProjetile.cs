using UnityEngine;


public interface IProjectile: IHaveProjectileDamage, IHaveProjectileMovement
{

}

public interface IHaveProjectileDamage
{
    void ProjectileDamageCharacteristcs(int maxDamage, int minDamage, int damageDropStart, int damageDropEnd, int impactForce);
}

public interface IHaveProjectileMovement
{
    void UseRigidbody(bool value);

    void ProjectileMovementCharacteristcs( Vector3 speed);
}