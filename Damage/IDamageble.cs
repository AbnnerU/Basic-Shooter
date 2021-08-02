using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageble
{
    void TakeDamage(int damageValue, float impactForce, Vector3 hitPosition, float hitAreaEffectRadius, bool exploxiveProjectile);
}
