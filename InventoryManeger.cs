using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManeger : MonoBehaviour
{

    [SerializeField] private Image primaryWeaponImage;

    [SerializeField] private GameObject primaryWeaponView;

    [SerializeField] private GameObject secondaryWeaponView;

    [Header("Inventory:")]

    [SerializeField] private AnimationManeger playerAnim;

    [SerializeField] private List<GameObject> PgunsOnInventory;
    [SerializeField] private List<GameObject> SgunsOnInventory;

    [SerializeField] private GameObject primarySlot;
    [SerializeField] private GameObject secondarySlot;

    [SerializeField] private GameObject gunInventory;


    private PlayerInputControls controls;

    private CameraManeger cam;

    public enum GunType { Primary, Secondary }

    private GunType equippedGun;

    private int primaryGun;

    private int secondaryGun;

    private void Start()
    {
        cam = FindObjectOfType<CameraManeger>();

        controls = new PlayerInputControls();

        controls.Gameplay.Enable();

        controls.Gameplay.PrimaryGun.performed += _ => OnChoosePrimary();
        controls.Gameplay.SecondaryGun.performed += _ => OnChooseSecondary();

        primaryGun = 0;
        secondaryGun = 0;

        PgunsOnInventory[primaryGun].SetActive(true);
        SgunsOnInventory[secondaryGun].SetActive(true);

        equippedGun = GunType.Primary;

        SetGunsOnSlot(equippedGun);

        SetGunsEquipped(equippedGun);

    }

    public void SwitchEquippedGun(GunType type)
    {
        if (equippedGun != type)
        {
            if (playerAnim.GetCanSwitch() == true)
            {
                cam.DefaltCameraSize();

                equippedGun = type;

                if (equippedGun == GunType.Primary)
                {
                    //SetGunsOnSlot(GunType.Primary);

                    //PgunsOnInventory[primaryGun].GetComponent<GunControls>().enabled = true;
                    SgunsOnInventory[secondaryGun].GetComponent<GunControls>().enabled = false;

                    playerAnim.SwitchArmsHoldType(PgunsOnInventory[primaryGun].GetComponent<GunControls>().GetGunType());
                }
                else
                {
                    //SetGunsOnSlot(GunType.Secondary);

                    //SgunsOnInventory[secondaryGun].GetComponent<GunControls>().enabled = true;
                    PgunsOnInventory[primaryGun].GetComponent<GunControls>().enabled = false;

                    playerAnim.SwitchArmsHoldType(SgunsOnInventory[secondaryGun].GetComponent<GunControls>().GetGunType());
                }
            }
        }
    }

    private void SetGunsOnSlot(GunType type)
    {
        if (type == GunType.Primary)
        {
            //Secondary guns on slot
            foreach (GameObject g in SgunsOnInventory)
            {
                g.transform.SetParent(secondarySlot.transform);
                g.transform.localPosition = Vector3.zero;

                g.transform.localRotation = Quaternion.Euler(0, 0, 90);

            }

            SgunsOnInventory[secondaryGun].GetComponent<GunControls>().enabled = false;
            ////primary guns on equipped position
            //foreach (GameObject g in PgunsOnInventory)
            //{
            //    g.transform.SetParent(gunInventory.transform);
            //    g.transform.localPosition = Vector3.zero;

            //    g.transform.localRotation = Quaternion.Euler(0, 0, 0);

            //}
        }
        else
        {
            //Primary guns on slot
            foreach (GameObject g in PgunsOnInventory)
            {
                g.transform.SetParent(primarySlot.transform);
                g.transform.localPosition = Vector3.zero;

                g.transform.localRotation = Quaternion.Euler(0, 0, 90);

            }

            PgunsOnInventory[primaryGun].GetComponent<GunControls>().enabled = false;
            ////Secondary guns on equipped position
            //foreach (GameObject g in SgunsOnInventory)
            //{
            //    g.transform.SetParent(gunInventory.transform);
            //    g.transform.localPosition = Vector3.zero;

            //    g.transform.localRotation = Quaternion.Euler(0, 0, 0);

            //}
        }


    }

    private void SetGunsEquipped(GunType type)
    {
        if (type == GunType.Primary)
        {
            //primary guns on equipped position
            foreach (GameObject g in PgunsOnInventory)
            {
                g.transform.SetParent(gunInventory.transform);
                g.transform.localPosition = Vector3.zero;

                g.transform.localRotation = Quaternion.Euler(0, 0, 0);

            }

            PgunsOnInventory[primaryGun].GetComponent<GunControls>().enabled = true;

        }
        else
        {
            //Secondary guns on equipped position
            foreach (GameObject g in SgunsOnInventory)
            {
                g.transform.SetParent(gunInventory.transform);
                g.transform.localPosition = Vector3.zero;

                g.transform.localRotation = Quaternion.Euler(0, 0, 0);

            }

            SgunsOnInventory[secondaryGun].GetComponent<GunControls>().enabled = true;
        }
    }


        #region Perfomed

        private void OnChoosePrimary()
    {
        SwitchEquippedGun(GunType.Primary);
    }

    private void OnChooseSecondary()
    {
        SwitchEquippedGun(GunType.Secondary);
    }


    #endregion

    #region Weapons Views
    public void PrimaryWeaponView()
    {
        primaryWeaponView.SetActive(!primaryWeaponView.activeSelf);

        secondaryWeaponView.SetActive(false);
    }

    public void SecondaryWeaponView()
    {
        secondaryWeaponView.SetActive(!primaryWeaponView.activeSelf);

        primaryWeaponView.SetActive(false);
    }

    #endregion

    #region ChangeWeapon(Primary)

    public void ChangePrimaryWeapon(GameObject newGunReference)
    {
        controls = new PlayerInputControls();


        int objIndex = FindGunIndex(newGunReference, GunType.Primary);
        print(objIndex);
        if (objIndex != -1)
        {
            if (objIndex != primaryGun)
            {
                PgunsOnInventory[primaryGun].SetActive(false);

                PgunsOnInventory[objIndex].SetActive(true);

                primaryGun = objIndex;
            }
        }
        else
        {
            CreateGun(newGunReference, GunType.Primary);
        }

    }

    #endregion

    #region ChangeWeapon(Secondary)

    public void ChangeSecondaryWeapon(GameObject newGunReference)
    {
        int objIndex = FindGunIndex(newGunReference, GunType.Secondary);

        if (objIndex != -1)
        {
            if (objIndex != secondaryGun)
            {
                SgunsOnInventory[secondaryGun].SetActive(false);

                secondaryGun = objIndex;
            }
        }
        else
        {
            CreateGun(newGunReference, GunType.Secondary);
        }

    }

    #endregion

    private void CreateGun(GameObject obj, GunType type)
    {
        GameObject gun = Instantiate(obj, gunInventory.transform);
        gun.transform.SetParent(gunInventory.transform);

        if (type == GunType.Primary)
        {
            PgunsOnInventory.Add(gun);

            PgunsOnInventory[primaryGun].SetActive(false);

            primaryGun = FindGunIndex(gun, GunType.Primary);
        }
        else
        {
            SgunsOnInventory.Add(gun);

            secondaryGun = FindGunIndex(gun, GunType.Secondary);
        }
    }

    private int FindGunIndex(GameObject obj, GunType type)
    {
        print(obj);
        if (type == GunType.Primary)
        {
            return PgunsOnInventory.FindIndex(x => x.gameObject.CompareTag(obj.tag));
        }
        else
        {
            return SgunsOnInventory.FindIndex(x => x.gameObject.CompareTag(obj.tag));
        }
    }

    public void ChangeGunOnSlot()
    {
        if (equippedGun == GunType.Primary)
        {
            SetGunsOnSlot(GunType.Primary);
        }
        else
        {
            SetGunsOnSlot(GunType.Secondary);          
        }
    }

    public void ChangeGunEquipped()
    {
        if (equippedGun == GunType.Primary)
        {
            SetGunsEquipped(GunType.Primary);
        }
        else
        {
            SetGunsEquipped(GunType.Secondary);
        }
    }
}