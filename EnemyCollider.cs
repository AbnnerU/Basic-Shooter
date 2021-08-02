using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollider : MonoBehaviour, IDamageble
{
    [SerializeField] private EnemyManeger enemyManeger;

    //[SerializeField] private Transform damageSpawnReference;

    [SerializeField] private bool head;


    //private HitPopUp hitPopUp;


    private void OnEnable()
    {
        //hitPopUp = FindObjectOfType<HitPopUp>();
    }

    public void TakeDamage(int damageValue, float impactForce, Vector3 hitPosition, float hitAreaEffectRadius, bool exploxiveProjectile)
    {
        if(exploxiveProjectile && head)
        {
            return;
        }

        if (head)
        {
            damageValue *= 2;
        }

        enemyManeger.ModifyHealth(damageValue, impactForce, hitPosition, hitAreaEffectRadius, head);
    }


    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    //if (collision.gameObject.CompareTag("Bullet"))
    //    //{
    //    //    int damage = collision.gameObject.GetComponent<Bullet>().GetMaxDamage();
    //    //    int impactForce = collision.gameObject.GetComponent<Bullet>().GetImpactForce();

    //    //    PoolManager.ReleaseObject(collision.gameObject);

    //    //    if (head)
    //    //    {
    //    //        enemyManeger.ModifyHealth(damage * 2,impactForce, collision.transform, head,false);


    //    //        //if (enemyManeger.GetAlive() == false)
    //    //        //{
    //    //        //    Vector2 direction = collision.gameObject.transform.position - transform.position;
    //    //        //    rb.AddForceAtPosition(new Vector2(Mathf.Sign(direction.x) * impactForce, 3), transform.position, ForceMode2D.Impulse);
    //    //        //}
    //    //    }
    //    //    else
    //    //    {
    //    //        enemyManeger.ModifyHealth(damage,impactForce, collision.transform, head,false);

    //    //        //if (enemyManeger.GetAlive() == false)
    //    //        //{
    //    //        //    Vector2 direction = collision.gameObject.transform.position - transform.position;
    //    //        //    rb.AddForceAtPosition(direction.normalized * impactForce, transform.position, ForceMode2D.Impulse);
    //    //        //}
    //    //    }

    //    //}
    //    ///*else*/ if (collision.gameObject.CompareTag("Explosion"))
    //    //{
    //    //    enemyManeger.ModifyHealth(10000, 0, collision.transform, head,true);
    //    //}

    //}

    //public EnemyManeger GetEnemyManeger()
    //{
    //    return enemyManeger;
    //}

    //public bool Head()
    //{
    //    return head;
    //}


}
