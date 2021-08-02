using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaltShoot : Gun
{
    [SerializeField] private GameObject FirePoint;

    [Header("Bullet")]

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed;
    public int maxDamage;
    public int minDamage;
    public int dropDamageStart;
    public int dropDamageEnd;
    public int bulletImpactForce;
    public bool useBulletRigidbody;


    public override void Shoot(float precisionValue)
    {
        gunControls.DescreaseAmmo(1);

        GameObject obj = PoolManager.SpawnObject(bulletPrefab, FirePoint.transform.position, FirePoint.transform.rotation);
        obj.transform.SetParent(bulletContainer.transform);

        Vector3 speed = (FirePoint.transform.right * bulletSpeed) + (FirePoint.transform.up * precisionValue);

        obj.GetComponent<IHaveProjectileMovement>().UseRigidbody(useBulletRigidbody);

        if (useBulletRigidbody)
        {           
            obj.GetComponent<Rigidbody2D>().AddForce(speed, ForceMode2D.Impulse);
        }


        obj.GetComponent<IHaveProjectileDamage>()?.ProjectileDamageCharacteristcs(maxDamage, minDamage, dropDamageStart, dropDamageEnd, bulletImpactForce);

        obj.GetComponentInChildren<IHaveProjectileDamage>()?.ProjectileDamageCharacteristcs(maxDamage, minDamage, dropDamageStart, dropDamageEnd, bulletImpactForce);

        obj.GetComponent<IHaveProjectileMovement>()?.ProjectileMovementCharacteristcs(speed);



        //if (obj.GetComponent<Bullet>() != null)
        //{
        //    Bullet bulletScript = obj.GetComponent<Bullet>();

        //    Vector3 speed = FirePoint.transform.right * bulletSpeed;

        //    bulletScript.UseRigidbody(useBulletRigidbody);

        //    if (useBulletRigidbody)
        //    {            
        //        obj.GetComponent<Rigidbody2D>().AddForce((speed) + (FirePoint.transform.up * precisionValue), ForceMode2D.Impulse);
        //    }

        //    bulletScript.SetBulletCharacteristcs(useBulletRigidbody, speed, maxDamage, minDamage, dropDamageStart, 
        //        dropDamageEnd, bulletImpactForce);

        //}
        //else if (obj.GetComponentInChildren<ExplosiveArea>() != null)
        //{
        //    if (useBulletRigidbody)
        //    {
        //        obj.GetComponent<Rigidbody2D>().AddForce((FirePoint.transform.right * bulletSpeed) + (FirePoint.transform.up * precisionValue), ForceMode2D.Impulse);
        //    }

        //    ExplosiveArea explosiveScript = obj.GetComponentInChildren<ExplosiveArea>();

        //    explosiveScript.SetBulletMaxDamage(maxDamage);
        //    explosiveScript.SetBulletMinDamage(minDamage);
        //    explosiveScript.SetDamageDropStart(dropDamageStart);
        //    explosiveScript.SetDamageDropEnd(dropDamageEnd);

        //}
    }
}
