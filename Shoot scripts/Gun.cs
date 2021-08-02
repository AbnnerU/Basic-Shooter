using System.Collections;
using UnityEngine;

public  abstract class Gun : MonoBehaviour
{
    [SerializeField] protected float fireRate;

    public enum FireType { Auto, SemiAuto }

    [SerializeField] protected FireType fireType;   

    protected GameObject bulletContainer;

    protected GunControls gunControls;

    protected bool canShoot=true;

    protected bool shooting=false;

    public virtual void Awake()
    {
        bulletContainer = GameObject.FindGameObjectWithTag("BulletConteiner");

        gunControls = GetComponent<GunControls>();

        gunControls.OnShootEvent += GunControls_OnShootEvent;
    }

    public virtual void OnEnable()
    {
        canShoot = true;
    }

    public virtual void Update()
    {
        if (fireType != FireType.Auto)
        {
            return;
        }
        else
        {
            if (shooting)
            {
                if (canShoot == true)
                {
                    canShoot = false;

                    Shoot(gunControls.GetCurrentSpreadAngle());

                    StartCoroutine(ShootFireRate(fireRate));

                }
            }
        }
    }

    public virtual void GunControls_OnShootEvent(object sender, GunControls.OnShootArgs shootArgs)
    {
        shooting = shootArgs.shooting;

        if (fireType == FireType.SemiAuto && shooting == true) 
        {
            if (canShoot == true)
            {
                canShoot = false;           

                StartCoroutine(ShootFireRate(fireRate));

            }
        }
    }

    public virtual IEnumerator ShootFireRate(float time)
    {
        Shoot(gunControls.GetCurrentSpreadAngle());

        yield return new WaitForSeconds(time);
        canShoot = true;

        StopCoroutine(ShootFireRate(time));
    }

    public abstract void Shoot(float precisionValue);
}
