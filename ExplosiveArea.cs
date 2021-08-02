using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveArea : MonoBehaviour, IHaveProjectileDamage
{
    [SerializeField] private GameObject parent;

    [SerializeField] private CircleCollider2D explosionColider;

    private Vector3 originPoint;

    private int maxDamage;
    private int minDamage;
    private int dropOffStart;
    private int dropOffEnd;
    private int impactForce;

    private int currentDamage;

    private float hitDistance;

    private float dropDamageRange;
    private float distanceRange;


    private void OnDisable()
    {
        hitDistance = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageble damageble = collision.gameObject.GetComponent<IDamageble>();

        //print(damageble);
        if (damageble != null)
        {
            originPoint = parent.transform.position;

            ////Take enemy colider reference
            //EnemyCollider enemy = collision.gameObject.GetComponent<EnemyCollider>();

            //EnemyManeger enemyManeger = enemy.GetEnemyManeger();

            //print(collision.gameObject.transform.position);
            //print(parent.transform.position);
           
            //Calculate the damage

            hitDistance = Vector3.Distance(originPoint, collision.gameObject.transform.position);
            //print(hitDistance);


            if (hitDistance <= dropOffStart)
            {
                currentDamage = maxDamage;

            }
            else if (hitDistance >= dropOffEnd)
            {
                currentDamage = minDamage;

            }
            else 
            {
                float currentDistance = Mathf.Abs(dropOffStart - hitDistance);

                float droppedDamageValue = currentDistance * (dropDamageRange / distanceRange);

                currentDamage = Mathf.Clamp(maxDamage - (int)droppedDamageValue, minDamage, maxDamage);
            }

            damageble.TakeDamage(currentDamage, impactForce, parent.transform.position, explosionColider.radius, true);

        }
    }

    //public void SetBulletMaxDamage(int value)
    //{
    //    maxDamage = value;
    //    originPoint = parent.transform.position;       
    //}

    //public void SetBulletMinDamage(int value)
    //{
    //    minDamage = value;
    //}

    //public void SetDamageDropStart(int value)
    //{
    //    dropOffStart = value;
    //}

    //public void SetDamageDropEnd(int value)
    //{
    //    dropOffEnd = value;
    //}

    

    public void ProjectileDamageCharacteristcs(int maxDamage, int minDamage, int damageDropStart, int damageDropEnd, int impactForce)
    {
        //Damage
        this.maxDamage = maxDamage;
        currentDamage = this.maxDamage;

        this.minDamage = minDamage;

        dropDamageRange = this.maxDamage - this.minDamage;

        //Damager drop
        dropOffStart = damageDropStart;
        dropOffEnd = damageDropEnd;

        distanceRange = dropOffEnd - dropOffStart;

        //Impact force
        this.impactForce = impactForce;
    }
}
