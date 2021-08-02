using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet :Projetile
{
     [SerializeField] private int groundLayer;

     [SerializeField] private Rigidbody2D rb;

     [SerializeField] private BoxCollider2D bulletCollider; 

     //private float predictionPerFrame = 6f;

     //private Vector3 gravityValue = Physics.gravity;

    // private int maxDamage;
    // private int minDamage;
    // private int currentDamage;
    // private int impactForce;

    ////Damage fall over distance

    //private Vector3 bulletVelocity;

    //private Vector3 originPoint;

    //private Vector3 point1;

    //private float bulletDistance;

    //private float dropDamageRange;
    //private float distanceRange;

    //private int dropOffStart;
    //private int dropOffEnd;

    //private bool actived;

    //private bool useRigidbody;


    private void OnDisable()
    {
        bulletDistance = 0;

        actived = false;
    }

    private void Update()
    {
        if (useRigidbody == true)
        {
            return;
        }
        else
        {
            if (actived)
            {

                float currentDeltaTime = Time.deltaTime;

                point1 = transform.position;

                float totalPredictions = 1f / predictionPerFrame;

                for (float i = 0; i < 1f; i += totalPredictions)
                {
                    bulletVelocity += gravityValue * (totalPredictions * currentDeltaTime);

                    Vector3 point2 = point1 + bulletVelocity * (totalPredictions * currentDeltaTime);

                    //Vector3 direction = point2 - point1;

                    RaycastHit2D raycast = Physics2D.Raycast(point1, point2 - point1, (point2 - point1).magnitude);

                    Collider2D collider = raycast.collider;

                    if (collider != null)
                    {

                        if (collider.gameObject.layer == groundLayer)
                        {
                            PoolManager.ReleaseObject(gameObject);
                            break;
                        }
                        else
                        {
                            OnHit(collider.gameObject, raycast.point);
                            PoolManager.ReleaseObject(gameObject);

                            break;
                        }
                    }


                    point1 = point2;

                }

                transform.position = point1;

            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == groundLayer)
        {
            PoolManager.ReleaseObject(gameObject);
        }
        else 
        {
            OnHit(collision.gameObject, gameObject.transform.position);
            PoolManager.ReleaseObject(gameObject);
        }
    }

    #region Gets/Sets

    public override void ProjectileDamageCharacteristcs( int maxDamage, int minDamage, int damageDropStart, int damageDropEnd, int impactForce)
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

    public override void ProjectileMovementCharacteristcs(Vector3 speed)
    {
        bulletVelocity = speed;

        originPoint = transform.position;

        point1 = originPoint;

        actived = true;

    }

    public override void UseRigidbody(bool value)
    {
        useRigidbody = value;
     
        rb.isKinematic = !value;

        bulletCollider.enabled = value;
    }



    public int GetMaxDamage()
    {
        return currentDamage;
    }

    public int GetImpactForce()
    {
        return impactForce;
    }

    #endregion

    private void OnHit(GameObject hitGameObject, Vector3 hitPosition)
    {
        IDamageble damageble = hitGameObject.GetComponent<IDamageble>();

        //print(damageble);
        ////Take enemy colider reference
        //EnemyCollider enemy = hitGameObject.GetComponent<EnemyCollider>();

        //EnemyManeger enemyManeger = enemy.GetEnemyManeger();

        if (damageble != null)
        {
            //Calculate the damage of bullet

            bulletDistance = Vector3.Distance(originPoint, transform.position);

            if (bulletDistance <= dropOffStart)
            {
                currentDamage = maxDamage;

            }
            else if (bulletDistance >= dropOffEnd)
            {
                currentDamage = minDamage;

            }
            else
            {

                float currentDistance = Mathf.Abs(dropOffStart - bulletDistance);

                float droppedDamageValue = currentDistance * (dropDamageRange / distanceRange);

                currentDamage = Mathf.Clamp(maxDamage - (int)droppedDamageValue, minDamage, maxDamage);
            }


            damageble.TakeDamage(currentDamage, impactForce, hitPosition, 0.3f, false);

        }
    }


}
