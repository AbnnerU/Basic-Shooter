using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorManeger : MonoBehaviour
{
    [SerializeField] private GameObject cursorImage;
    [SerializeField] private GameObject aimPoint;
    [SerializeField] private GameObject aimPrecion;

    [SerializeField] private GameObject scope;
    [SerializeField] private GameObject scopeCamera;
    private InputControler inputControler;

    private Vector2 aimPrecisionSize;
    private Vector2 cursorSize;

    private float imagesDiference;

    private bool activeScope;

  

    // Start is called before the first frame update
    void Awake()
    {
        Cursor.visible = false;
        inputControler = FindObjectOfType<InputControler>();
        aimPrecion.SetActive(false);

        aimPrecisionSize = aimPrecion.GetComponent<RectTransform>().sizeDelta;
        cursorSize = aimPoint.GetComponent<RectTransform>().sizeDelta;

        imagesDiference = aimPrecisionSize.x - cursorSize.x;

    }

    // Update is called once per frame
    void Update()
    {
        if (activeScope)
        {
            scopeCamera.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(inputControler.GetMousePosition().x, inputControler.GetMousePosition().y, 0));
        }

        cursorImage.transform.position = inputControler.GetMousePosition();
    }

    public void StartAim(float percetageOfAim)
    {
        float finalValue = (imagesDiference * percetageOfAim) / 100;

        //print(finalValue);
        aimPrecion.GetComponent<RectTransform>().sizeDelta = new Vector2(aimPrecisionSize.x - finalValue, aimPrecisionSize.y - finalValue);

        aimPrecion.SetActive(true);
    }

    public void StopAim()
    {
        aimPrecion.SetActive(false);
        aimPrecion.GetComponent<RectTransform>().sizeDelta = aimPrecisionSize;
    }

    public void Scope(bool useScope)
    {
        activeScope = useScope;

        scope.SetActive(useScope);

        scopeCamera.SetActive(useScope);
    }
}
