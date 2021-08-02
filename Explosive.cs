using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive : ProjectileMovement
{
    [SerializeField] private int groundLayer;

    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private Collider2D objectCollider;

    [SerializeField] private ParticleSystem explosiveParticles;

    [SerializeField] private CircleCollider2D explosionColider;

    [SerializeField] private float explosionDuration; 

    [SerializeField] private SpriteRenderer spriteRenderer;

    private bool active;

    private void OnEnable()
    {
        explosionColider.enabled = false;

        objectCollider.enabled = true;

        spriteRenderer.enabled = true;
    }

    private void OnDisable()
    {
        rb.constraints = RigidbodyConstraints2D.None;
    }

    private void Update()
    {
        if (useRigidbody == true)
        {
            return;
        }
        else
        {
            if (active)
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
                        OnHit();
                        active = false;
                        break;
                    }

                    point1 = point2;

                }
            }
            transform.position = point1;           
        }

    }

    private void OnHit()
    {
        //print("play");
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        objectCollider.enabled = false;

        spriteRenderer.enabled = false;

        explosionColider.enabled = true;

        explosiveParticles.Play();

        StartCoroutine(ExplosionTime(explosionDuration));
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnHit();
    }

    IEnumerator ExplosionTime(float time)
    {
        yield return new WaitForSeconds(time);

        PoolManager.ReleaseObject(gameObject);

        StopCoroutine(ExplosionTime(time));
    }

    #region Abstract class

    public override void ProjectileMovementCharacteristcs(Vector3 speed)
    {
        bulletVelocity = speed;

        originPoint = transform.position;

        point1 = originPoint;

        active = true;
    }

    public override void UseRigidbody(bool value)
    {
        useRigidbody = value;

        rb.isKinematic = !value;

        objectCollider.enabled = value;
    }

    #endregion
}
