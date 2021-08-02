using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hands : MonoBehaviour
{
    [SerializeField] private GameObject hand1;
    [SerializeField] private GameObject hand2;

    public void ChangeHandsHoldType(GunControls.GunType gunType)
    {
        if(gunType == GunControls.GunType.Rifle)
        {

        }

        if(gunType == GunControls.GunType.Pistol)
        {

        }
    }
}
