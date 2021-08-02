using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPropeties : MonoBehaviour
{
    [SerializeField] private Sprite sprite;

    [SerializeField] private Vector3 position;

    [SerializeField] private Vector3 FirePoint;

    [SerializeField] private float gunPrecision;
    [SerializeField] private float gunAimTime;
    [SerializeField] private float fadeOutValue;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed;

    [SerializeField] private int ammoAmount;
    [SerializeField] private float reloadTime;

    [SerializeField] private float fireRate;

    [SerializeField] private char fireType;


    public Sprite GetSprite()
    {
        return sprite;
    }

    public Vector3 GetPosition()
    {
        return position;
    }

    public Vector3 GetFirePoint()
    {
        return FirePoint;
    }

    public float GetPrecision()
    {
        return gunPrecision;
    }

    public float GetGunAimTime()
    {
        return gunAimTime;
    }

    public float GetFadeOutValue()
    {
        return fadeOutValue;
    }

    public GameObject GetBulletPrefab()
    {
        return bulletPrefab;
    }

    public float GetBulletSpeed()
    {
        return bulletSpeed;
    }

    public int GetAmmoAmount()
    {
        return ammoAmount;
    }

    public float GetReloadTime()
    {
        return reloadTime;
    }

    public float GetFireRate()
    {
        return fireRate;
    }

    public char GetFireType()
    {
        return fireType;
    }
}
