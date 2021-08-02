using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyManager : MonoBehaviour
{
    [SerializeField] private GameObject body;

    [SerializeField] private GameObject rotationCenter;

    [SerializeField] private GameObject head;

    [SerializeField] private float maxHeadAngle;

    [SerializeField] private GameObject[] allRagdollParts;


    //private Vector2 hand1Position;
    //private Vector2 hand2Position;

    [SerializeField]private InputControler inputControler;

    private float currentFlipValue=1;

    private bool lookRigth;

    private bool flipRotationCenter=true;

    // Start is called before the first frame update
    void Awake()
    {
        lookRigth = true;

       foreach(GameObject g in allRagdollParts)
        {
            g.GetComponent<Rigidbody2D>().isKinematic = true;

            if (g.GetComponent<Collider2D>())
            {
                g.GetComponent<Collider2D>().enabled = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Head
        Vector2 headToScreen = Camera.main.WorldToScreenPoint(head.transform.position);

        Vector2 dirHead = inputControler.GetMousePosition() - headToScreen;
        float angleHead = Mathf.Atan2(dirHead.y, Mathf.Abs(dirHead.x)) * Mathf.Rad2Deg;
        head.transform.eulerAngles = new Vector3(0, 0, Mathf.Clamp(angleHead * body.transform.localScale.x, -maxHeadAngle, maxHeadAngle));

        //Flip Player
        Vector2 playerToScren = Camera.main.WorldToScreenPoint(transform.position);

        if (body.transform.localScale.x == 1 && inputControler.GetMousePosition().x < playerToScren.x)
        {
            lookRigth = false;

            currentFlipValue = -1;

            Flip();
        }
        else if(body.transform.localScale.x == -1 && inputControler.GetMousePosition().x > playerToScren.x)
        {
            lookRigth = true;

            currentFlipValue = 1;

            Flip();
        }

    }

    private void Flip()
    {
       body.transform.localScale = new Vector3(currentFlipValue, 1, 1);

        RotationCenter();
        //rotationCenter.transform.rotation = Quaternion.Euler(rotationCenter.transform.rotation.x, rotationCenter.transform.rotation.y, rotationCenter.transform.rotation.z * value);


        ////hands
        //hand1.transform.localPosition = new Vector2(hand1.transform.localPosition.x, -hand1.transform.localPosition.y);
        //hand2.transform.localPosition = new Vector2(hand2.transform.localPosition.x, -hand2.transform.localPosition.y);
    }

    private void RotationCenter()
    {
        if (flipRotationCenter == true)
        {
            rotationCenter.transform.localScale = new Vector3(currentFlipValue, currentFlipValue, 1);
        }
        else
        {
            rotationCenter.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public bool PlayerlookToRigth()
    {
        return lookRigth;
    }

    public void SetFlipRotationCenter(bool value)
    {
        flipRotationCenter = value;

        RotationCenter();
    }

}
