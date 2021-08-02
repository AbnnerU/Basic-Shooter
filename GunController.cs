using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class GunController : MonoBehaviour
{
    //[SerializeField] private GameObject bulletContainer;

    [SerializeField] private GameObject rotationReference;

    //[Header("Gun propeties")]

    //[SerializeField] private GameObject currentGun;

    //[SerializeField] private Transform FirePoint;

    //[SerializeField] private float gunPrecision;
    //[SerializeField] private float gunAimTime;
    //[SerializeField] private float fadeOutValue;

    //[SerializeField] private GameObject bulletPrefab;
    //[SerializeField] private float bulletSpeed;

    //[SerializeField] private CameraManeger cam;

    private InputControler inputControler;

    private PlayerInputControls controls;

    private CursorManeger cursorManeger;

    private string[] statesAim = new string[] { "Aiming", "NAiming" };
    private string aimState;

    private float actualFadeOut=0;

    private void Awake()
    {
        //OnGunChanged(currentGun);
        inputControler = FindObjectOfType<InputControler>();
        cursorManeger = FindObjectOfType<CursorManeger>();
        //controls = new PlayerInputControls();     
    }

    //private void OnEnable()
    //{
    //    controls.Gameplay.Enable();

    //    controls.Gameplay.Shoot.performed += ctx => OnShoot(ctx.ReadValue<float>());

    //    controls.Gameplay.Aim.performed += ctx => OnAim(ctx.ReadValue<float>());
    //}

    private void Update()
    {
        Vector2 refernceToScreen = Camera.main.WorldToScreenPoint(rotationReference.transform.position);

        Vector2 dir = inputControler.GetMousePosition() - refernceToScreen;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;


        rotationReference.transform.eulerAngles = new Vector3(0, 0, angle);





        ////Aim

        //if(aimState == statesAim[0])
        //{
        //    if (cam.GetCameraSize() < fadeOutValue)
        //    {
        //        actualFadeOut += (fadeOutValue*2 / gunAimTime) * Time.deltaTime;

        //        float percentageOfAim = (actualFadeOut * 100) / fadeOutValue;

        //        cursorManeger.StartAim(percentageOfAim);

        //    }
        //    else
        //    {
        //        actualFadeOut = fadeOutValue;
        //        cursorManeger.StartAim(100);
        //    }
        //}
    }

    private void OnDisable()
    {
        rotationReference.transform.eulerAngles = Vector3.zero;
    }

    //public void OnGunChanged(GameObject newGun)
    //{
    //    //GunPropeties properties = newGun.GetComponent<GunPropeties>();
    //    currentGun = newGun;

    //    //FirePoint = properties.GetFirePoint().transform;
    //    //gunPrecision = properties.GetnormalPrecision();
    //    //gunAimTime = properties.GetGunAimTime();
    //    //fadeOutValue = properties.GetFadeOutValue();

    //    //bulletPrefab = properties.GetBulletPrefab();
    //    //bulletSpeed = properties.GetBulletSpeed();
    //}


    //#region Perfomed

    //private void OnShoot(float ctx)
    //{
    //    if (ctx >= 1)
    //    {
    //        if (aimState == statesAim[0])
    //        {
    //            float precisionValue = Random.Range(-gunPrecision, gunPrecision);

    //            float percentageOfAim = (actualFadeOut * 100) / fadeOutValue;

    //            float valueToSubstract = (precisionValue * percentageOfAim) / 100;

    //            precisionValue -= valueToSubstract;

    //            Shoot(precisionValue);
    //        }
    //        else
    //        {
    //            float precisionValue = Random.Range(-gunPrecision, gunPrecision);
    //            Shoot(precisionValue);
    //        }
    //    }
    //}

    //private void Shoot(float precisionValue)
    //{        
    //    GameObject obj = PoolManager.SpawnObject(bulletPrefab, FirePoint.position, FirePoint.rotation);
    //    obj.transform.SetParent(bulletContainer.transform);
    //    obj.GetComponent<Rigidbody2D>().AddForce((FirePoint.right * bulletSpeed) + (FirePoint.up * precisionValue)  ,ForceMode2D.Impulse);
    //}

    //private void OnAim(float ctx)
    //{
    //    if (ctx >= 1)
    //    {
    //        cam.FadeOutEffect(fadeOutValue,gunAimTime);
    //        aimState = statesAim[0];         
    //    }
    //    else
    //    {
    //        cam.DefaltCameraSize();
    //        actualFadeOut = 0;
    //        aimState = statesAim[1];

    //        cursorManeger.StopAim();
    //    }
    //}

    //#endregion



}
