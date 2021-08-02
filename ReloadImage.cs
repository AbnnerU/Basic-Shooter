using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReloadImage : MonoBehaviour
{
    [SerializeField] private Image reload;

    private float time;

    private bool animation=false;

    private void Awake()
    {
        reload.enabled = false;
    }

    private void Update()
    {
        if (animation)
        {
            float value = (1 / time) * Time.deltaTime;
            reload.fillAmount += value;

            if (reload.fillAmount == 1)
            {
                animation = false;
                reload.enabled = false;
            }
        }
    }

    public void StartReload(float reloadTime)
    {
        reload.enabled = true;

        reload.fillAmount = 0;

        time = reloadTime;

        animation = true;
    }
}


