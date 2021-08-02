using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunControls : MonoBehaviour
{
    [Header("Gun propeties")]

    //[SerializeField] private GameObject FirePoint;

    [Header("Aim")]
    [SerializeField] private bool useScopeAim = false;
    [SerializeField] private float spreadAngle;
    [SerializeField] private float gunAimTime;
    [SerializeField] private float fadeOutValue;

    //[Header("Bullet")]
    //[SerializeField] private GameObject bulletPrefab;
    //[SerializeField] private float bulletSpeed;
    //[SerializeField] private int maxDamage;
    //[SerializeField] private int minDamage;
    //[SerializeField] private int dropDamageStart;
    //[SerializeField] private int dropDamageEnd;
    //[SerializeField] private int bulletImpactForce;
    //[SerializeField] private bool useBulletRigidbody;
    //[SerializeField] private bool useAnotherScritptToShoot;

    [Header("Gun features")]
    [SerializeField] private int ammoAmount;
    [SerializeField] private float reloadTime;



    //public enum FireType { Auto, SemiAuto }

    //[SerializeField] private FireType fireType;
    
    public enum GunType {Rifle, Pistol}

    [SerializeField] private GunType gunType;


    //Events

    public event EventHandler<OnShootArgs> OnShootEvent;

    public class OnShootArgs: EventArgs
    {
        public bool shooting;
    }

    //__________________

    private PlayerInputControls controls;

    private CursorManeger cursorManeger;

    private CameraManeger cam;

    private ReloadImage reloadImage;
    
    //private GameObject bulletContainer;

    private enum AimStates{ Aiming, NAiming };

    private AimStates aimState;

    private float actualFadeOut = 0;

    private float currentSpreadAngle;

    private int currentAmmo;

    //private bool canShoot;

    private bool shooting;

    private bool reloading;

    private bool gunActive;
   
    void Update()
    {

        //Aim
        if (aimState == AimStates.Aiming)
        {
            if (cam.GetCameraSize() < fadeOutValue)
            {
                actualFadeOut += (fadeOutValue * 2 / gunAimTime) * Time.deltaTime;

                float percentageOfAim = (actualFadeOut * 100) / fadeOutValue;

                cursorManeger.StartAim(percentageOfAim);

            }
            else
            {
                actualFadeOut = fadeOutValue;
                cursorManeger.StartAim(100);
            }
        }


        //Shoot Type : Hold

        //if (shooting && fireType == FireType.Auto)
        //{
        //    if (canShoot && !useAnotherScritptToShoot)
        //    {
        //        InitiateShoot();
        //    }
        //}
    }


    private void OnEnable()
    {

        controls = new PlayerInputControls();

        controls.Gameplay.Enable();

        //controls.Gameplay.ShootHold.performed += ctx => OnShoot(ctx.ReadValue<float>());
        //controls.Gameplay.ShootHold.canceled += ctx => OnShoot(ctx.ReadValue<float>());
        controls.Gameplay.ShootClick.performed += ctx => OnShoot(ctx.ReadValue<float>());
        controls.Gameplay.ShootClick.canceled += ctx => OnShoot(ctx.ReadValue<float>());


        controls.Gameplay.Aim.performed += ctx => OnAim(ctx.ReadValue<float>());

        controls.Gameplay.Reload.performed += _ => OnReload();



        cursorManeger = FindObjectOfType<CursorManeger>();

        cam = FindObjectOfType<CameraManeger>();

        reloadImage = FindObjectOfType<ReloadImage>();

        //bulletContainer = GameObject.FindGameObjectWithTag("BulletConteiner");

        currentAmmo = ammoAmount;

        shooting = false;

        //canShoot = true;

        reloading = false;

        gunActive = true;

        cursorManeger.Scope(useScopeAim);


    }

    private void OnDisable()
    {

        controls.Gameplay.Disable();

    }

    #region Perfomed/Cancel

    private void OnShoot(float ctx)
    {
        if (ctx >= 1)
        {
            shooting = true;
            
            SpreadAngle();

            if (reloading == false)
            {
                OnShootEvent?.Invoke(this, new OnShootArgs { shooting = shooting });
            }
                     
        }
        else
        {        
            shooting = false;
           
            OnShootEvent?.Invoke(this, new OnShootArgs { shooting = shooting });

           
        }
        
    }

    private void OnAim(float ctx)
    {

        if (ctx >= 1)
        {
            cam.FadeOutEffect(fadeOutValue, gunAimTime, useScopeAim);
            aimState = AimStates.Aiming;
        }
        else
        {
            cam.DefaltCameraSize();
            actualFadeOut = 0;
            aimState = AimStates.NAiming;

            cursorManeger.StopAim();
        }

    }

    private void OnReload()
    {
        if (gunActive)
        {
            if (currentAmmo < ammoAmount && reloading == false)
            {
                reloading = true;

                OnShootEvent?.Invoke(this, new OnShootArgs { shooting = false });

                StartCoroutine(Reload(reloadTime));
            }
        }
    }

    #endregion

    private void SpreadAngle()
    {
        if (aimState == AimStates.Aiming)
        {
            float spreadAngleValue = UnityEngine.Random.Range(-spreadAngle, spreadAngle);

            float percentageOfAim = (actualFadeOut * 100) / fadeOutValue;

            float valueToSubstract = (spreadAngleValue * percentageOfAim) / 100;

            spreadAngleValue -= valueToSubstract;

            currentSpreadAngle = spreadAngleValue;

        }
        else
        {
            float spreadAngleValue = UnityEngine.Random.Range(-spreadAngle, spreadAngle);

            currentSpreadAngle = spreadAngleValue;

        }

    }

    //private void InitiateShoot()
    //{
    //    //print("entrou");
    //    if (currentAmmo > 0 && reloading == false)
    //    {
    //        canShoot = false;
    //        //print("oie");
    //        if (aimState == AimStates.Aiming)
    //        {
    //            float spreadAngleValue = UnityEngine.Random.Range(-gunPrecision, gunPrecision);

    //            float percentageOfAim = (actualFadeOut * 100) / fadeOutValue;

    //            float valueToSubstract = (spreadAngleValue * percentageOfAim) / 100;

    //            spreadAngleValue -= valueToSubstract;

    //            currentSpreadAngle = spreadAngleValue;

    //            StartCoroutine(DelayToShoot(fireRate, spreadAngleValue));
    //        }
    //        else
    //        {
    //            float spreadAngleValue = UnityEngine.Random.Range(-gunPrecision, gunPrecision);

    //            currentSpreadAngle = spreadAngleValue;

    //            StartCoroutine(DelayToShoot(fireRate, precisionValue));
    //        }
    //    }
    //}

    //private void Shoot(float precisionValue)
    //{
    //    currentAmmo--;

    //    GameObject obj = PoolManager.SpawnObject(bulletPrefab, FirePoint.transform.position, FirePoint.transform.rotation);
    //    obj.transform.SetParent(bulletContainer.transform);

    //    if (useBulletRigidbody)
    //    {
    //        obj.GetComponent<Rigidbody2D>().AddForce((FirePoint.transform.right * bulletSpeed) + (FirePoint.transform.up * precisionValue), ForceMode2D.Impulse);
    //    }

    //    if (obj.GetComponent<Bullet>() != null)
    //    {

    //        Bullet bulletScript = obj.GetComponent<Bullet>();

    //        bulletScript.SetBulletVelocity(FirePoint.transform.right * bulletSpeed);
    //        bulletScript.SetBulletMaxDamage(maxDamage);
    //        bulletScript.SetBulletMinDamage(minDamage);
    //        bulletScript.SetDamageDropStart(dropDamageStart);
    //        bulletScript.SetDamageDropEnd(dropDamageEnd);
    //        bulletScript.SetBulletForce(bulletImpactForce);
    //        bulletScript.UseRigidbody(useBulletRigidbody);


    //    }
    //    else if(obj.GetComponentInChildren<ExplosiveArea>() != null)
    //    {

    //        ExplosiveArea explosiveScript = obj.GetComponentInChildren<ExplosiveArea>();

    //        explosiveScript.SetBulletMaxDamage(maxDamage);
    //        explosiveScript.SetBulletMinDamage(minDamage);
    //        explosiveScript.SetDamageDropStart(dropDamageStart);
    //        explosiveScript.SetDamageDropEnd(dropDamageEnd);

    //    }
    //}


    //public void ChangeGun(GunPropeties newGun)
    //{
    //    gameObject.GetComponent<SpriteRenderer>().sprite = newGun.GetSprite();
    //    //gameObject.transform.position = new Vector3(newGun.GetPosition().x, 0, 0);

    //    //FirePoint.transform.position = new Vector3(newGun.GetFirePoint().x, 0, 0);

    //    gunPrecision = newGun.GetPrecision();
    //    gunAimTime = newGun.GetGunAimTime();
    //    fadeOutValue = newGun.GetFadeOutValue();

    //    bulletPrefab = newGun.GetBulletPrefab();
    //    bulletSpeed = newGun.GetBulletSpeed();

    //    ammoAmount = newGun.GetAmmoAmount();
    //    currentAmmo = ammoAmount;
    //    reloadTime = newGun.GetReloadTime();

    //    fireRate = newGun.GetFireRate();

    //    fireType = newGun.GetFireType();

    //}


    //IEnumerator DelayToShoot(float time,float precisionValue)
    //{
    //    Shoot(precisionValue);

    //    yield return new WaitForSeconds(time);

    //    canShoot = true;

    //    StopCoroutine(DelayToShoot(time, precisionValue));
    //}

    IEnumerator Reload(float time)
    {
        reloadImage.StartReload(time);

        yield return new WaitForSeconds(time);

        reloading = false;
    
        currentAmmo = ammoAmount;

        StopCoroutine(Reload(time));
    }

    #region get/set

    public int GetCurrentAmmo()
    {
        return currentAmmo;
    }

    public void DescreaseAmmo(int value)
    {

        currentAmmo -= value;
    }


    public float GetCurrentSpreadAngle()
    {
        return currentSpreadAngle;
    }

    public GunControls.GunType GetGunType()
    {
        return gunType;
    }

    #endregion



    //public GameObject FirePoint;

    //public enum FireType { Auto, SemiAuto }

    //public FireType fireType;

    //public float fireRate;

    //[Header("Bullet")]

    //public GameObject bulletPrefab;
    //public float bulletSpeed;
    //public int maxDamage;
    //public int minDamage;
    //public int dropDamageStart;
    //public int dropDamageEnd;
    //public int bulletImpactForce;
    //public bool useBulletRigidbody;

    //[Header("Burt Config")]

    //[SerializeField] private int bulletsPerShoot;
    //[SerializeField] private float timeBetweenBullets;





    //    gunControls.DescreaseAmmo(1);

    //        GameObject obj = PoolManager.SpawnObject(bulletPrefab, FirePoint.transform.position, FirePoint.transform.rotation);
    //    obj.transform.SetParent(bulletContainer.transform);

    //        if (useBulletRigidbody)
    //        {
    //            obj.GetComponent<Rigidbody2D>().AddForce((FirePoint.transform.right* bulletSpeed) + (FirePoint.transform.up* precisionValue), ForceMode2D.Impulse);
    //}

    //        if (obj.GetComponent<Bullet>() != null)
    //        {

    //            Bullet bulletScript = obj.GetComponent<Bullet>();

    //bulletScript.SetBulletVelocity(FirePoint.transform.right* bulletSpeed);
    //            bulletScript.SetBulletMaxDamage(maxDamage);
    //            bulletScript.SetBulletMinDamage(minDamage);
    //            bulletScript.SetDamageDropStart(dropDamageStart);
    //            bulletScript.SetDamageDropEnd(dropDamageEnd);
    //            bulletScript.SetBulletForce(bulletImpactForce);
    //            bulletScript.UseRigidbody(useBulletRigidbody);


    //        }
    //        else if (obj.GetComponentInChildren<ExplosiveArea>() != null)
    //        {

    //            ExplosiveArea explosiveScript = obj.GetComponentInChildren<ExplosiveArea>();

    //explosiveScript.SetBulletMaxDamage(maxDamage);
    //            explosiveScript.SetBulletMinDamage(minDamage);
    //            explosiveScript.SetDamageDropStart(dropDamageStart);
    //            explosiveScript.SetDamageDropEnd(dropDamageEnd);

    //        }
}
