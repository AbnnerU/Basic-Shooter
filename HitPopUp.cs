using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HitPopUp : MonoBehaviour
{
    [SerializeField] private GameObject popUpPrefab;

    [SerializeField] private int valueToRandomizePositionX;

    [SerializeField] private int valueToRandomizePositionY;

    [SerializeField] private int DamageValueToMaxFontSize;

    [SerializeField] private int minFontSize;
    [SerializeField] private int maxFontSize;


    [SerializeField] private Image hitMarkImage;
    [SerializeField] private Animator hitMarkAnim;

    public void CreatePopUp(GameObject reference, int value, bool headShoot)
    {
        
        GameObject obj = PoolManager.SpawnObject(popUpPrefab);

        obj.transform.SetParent(gameObject.transform,false);

        obj.transform.position = Camera.main.WorldToScreenPoint(reference.transform.position);

        //obj.GetComponent<TextMeshPro>().text = value.ToString();
        int currentFontSize;

        int positionRandomizeX = Random.Range(-valueToRandomizePositionX, valueToRandomizePositionX);
        int positionRandomizeY = Random.Range(-valueToRandomizePositionY, valueToRandomizePositionY);


        if (value > DamageValueToMaxFontSize)
        {
            currentFontSize = maxFontSize;
        }
        else
        {
            currentFontSize = minFontSize;
        }


        if (headShoot)
        {
            //Color textColor = Color.yellow;
            //obj.GetComponent<TextMeshPro>().color = textColor;
            //obj.GetComponent<TextMeshPro>().sortingOrder = 1;


            obj.GetComponent<Hit>().ChangeText(reference, value, Color.yellow, currentFontSize, positionRandomizeX, positionRandomizeY);
        }
        else
        {
            //Color textColor = Color.red;
            //obj.GetComponent<TextMeshPro>().color = textColor;
            //obj.GetComponent<TextMeshPro>().sortingOrder = 0;

            obj.GetComponent<Hit>().ChangeText(reference, value, Color.red, currentFontSize, positionRandomizeX, positionRandomizeY);
        }
    }

    public void HitMark(bool alive)
    {
        if (alive)
        {
            hitMarkImage.color = Color.white; ;
        }
        else
        {
            hitMarkImage.color = Color.red;
        }

        hitMarkAnim.Play("Hit", 0, 0);
    }
}
