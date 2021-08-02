
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EnemyManeger : MonoBehaviour
{
    [SerializeField] private Slider healtBarSlider;
    [SerializeField] private int maxHealt;

    [SerializeField] private GameObject damageSpawnReference;

    [SerializeField] private GameObject enemyCanvas;

    [SerializeField] private GameObject[] whenAliveObjects;

    [SerializeField] private GameObject[] allRagdollParts;

    [SerializeField] private Animator anim;

   

    private Vector3 teste;
    private float coisa;

    private Rigidbody2D rb;

    private HitPopUp hitPopUp;

    private bool alive;

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();

        hitPopUp = FindObjectOfType<HitPopUp>();

        enemyCanvas.SetActive(true);

        alive = true;

        healtBarSlider.maxValue = maxHealt;
        healtBarSlider.value = maxHealt;

        foreach (GameObject g in allRagdollParts)
        {
            g.GetComponent<Rigidbody2D>().isKinematic = true;

            if (g.GetComponent<Collider2D>())
            {
                g.GetComponent<Collider2D>().enabled = false;
            }
        }

    }

    public void ModifyHealth(int damageValue, float impactForce, Vector3 hitPosition, float hitAreaEffectRadius, bool headShoot)
    {
        healtBarSlider.value -= damageValue;

        teste = hitPosition;

        coisa = hitAreaEffectRadius;

        ////Damage popUp
        hitPopUp.CreatePopUp(damageSpawnReference, damageValue, headShoot);

        if (healtBarSlider.value <= 0 && alive == true)
        {
            alive = false;
            enemyCanvas.SetActive(false);

            Die(hitPosition, impactForce, hitAreaEffectRadius);

        }

        hitPopUp.HitMark(alive);
    }

    public void Die(Vector3 hitPosition, float impactForce, float hitAreaRadius)
    {
        anim.enabled = false;

        rb.isKinematic = true;

        
        // Active ragdoll
        foreach (GameObject g in whenAliveObjects)
        {
            g.GetComponent<Collider2D>().enabled = false;
        }

        foreach (GameObject g in allRagdollParts)
        {
            g.GetComponent<Rigidbody2D>().isKinematic = false;

            if (g.GetComponent<Collider2D>())
            {
                g.GetComponent<Collider2D>().enabled = true;
            }
        }
        

        //Aply force to ragdoll
        teste = hitPosition;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(hitPosition, hitAreaRadius);
              
           if (colliders[0].GetComponent<Rigidbody2D>() != null)
           {
               Rigidbody2D hitRb = colliders[0].GetComponent<Rigidbody2D>();

               
                Vector3 direction = hitRb.position - (Vector2)hitPosition;
                print(direction.normalized);
                hitRb.AddForce(direction * impactForce, ForceMode2D.Impulse);
           }
     

    }

    //public void ModifyHealth(int value, float impactForce, Vector3 hitPosition, bool headShoot, bool explosive)
    //{

    //    healtBarSlider.value -= value;

    //    //Damage popUp
    //    hitPopUp.CreatePopUp(damageSpawnReference.position, value, headShoot);

    //    if (healtBarSlider.value <= 0 && alive==true)
    //    {           
    //        alive = false;
    //        enemyCanvas.SetActive(false);

    //        Die(hitPosition,impactForce,explosive);

    //    }



    //    hitPopUp.HitMark(alive);

    //}



    public bool GetAlive()
    {
        return alive;
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawSphere(teste, coisa);
    }

}
