using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManeger : MonoBehaviour
{
    [SerializeField] private Animator anim;

    [Header("Arms")]

    [SerializeField] private GunController gunController;

    [SerializeField] private BodyManager bodyManager;

    [SerializeField] private InventoryManeger inventory;

    private bool canSwitch=true;

    public enum AnimationState { Stopped, Move, ReverseMove}
    private AnimationState state = AnimationState.Stopped;

  

    public void StartMove()
    {
        anim.Play("Torso",1,0);

        anim.Play("Player Move Loop",0,0.25f);
    }

    public void StartIdle()
    {
        anim.Play("Reverse Torso", 1, 0);

        anim.Play("Player Idle",0,0);
    }


    public void StartReverseMove()
    {
        anim.Play("Reverse Torso", 1, 0);
        anim.Play("Player Reverse Move");
    }

    public void StartFly()
    {
        anim.Play("Player Flying");
    }

    public void StartReverseFly()
    {
        anim.Play("Player Fall");
    }

    //Arms

    public void SwitchArmsHoldType(GunControls.GunType gunType)
    {
        if(gunType == GunControls.GunType.Rifle)
        {
            anim.SetInteger("Type", 1);
            anim.SetBool("Switch", true);

            BeginSwitch();
        }
        else if(gunType == GunControls.GunType.Pistol)
        {
            anim.SetInteger("Type", 2);
            anim.SetBool("Switch", true);

            BeginSwitch();
        }       
    }

    private void BeginSwitch()
    {
        bodyManager.SetFlipRotationCenter(false);

        gunController.enabled = false;

        canSwitch = false;
    }

    public void EndSwitch()
    {
        anim.SetBool("Switch", false);

        bodyManager.SetFlipRotationCenter(true);

        gunController.enabled = true;

        canSwitch = true;
    }

    public void ChangeGunOnSlot()
    {
        inventory.ChangeGunOnSlot();
    }

    public void ChangeEquippedGun()
    {
        inventory.ChangeGunEquipped();
    }


    public bool GetCanSwitch()
    {
        return canSwitch;
    }
}
