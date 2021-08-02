using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projetile : ProjectileMovement, IProjectile
{  
    //Bullet damage
    protected int maxDamage;
    protected int minDamage;
    protected int currentDamage;
    protected int impactForce;

    //Bullet falloff
    protected float bulletDistance;

    protected float dropDamageRange;
    protected float distanceRange;

    protected int dropOffStart;
    protected int dropOffEnd;

    protected bool actived;

    public abstract void ProjectileDamageCharacteristcs(int maxDamage, int minDamage, int damageDropStart, int damageDropEnd, int impactForce);
}

public abstract class ProjectileMovement : MonoBehaviour, IHaveProjectileMovement
{
    protected float predictionPerFrame = 6f;

    protected Vector3 gravityValue = Physics.gravity;

    protected Vector3 bulletVelocity;

    protected Vector3 originPoint;

    protected Vector3 point1;

    protected bool useRigidbody;

    public abstract void ProjectileMovementCharacteristcs(Vector3 speed);

    public abstract void UseRigidbody(bool value);

}
