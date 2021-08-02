using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraManeger : MonoBehaviour
{
    [SerializeField] private float defaltCameraSize;

    [SerializeField] private GameObject postProcessing;

    private Camera cam;
    
    private bool fadeOut;
    private float newCameraSize;
    private float fadeOutTime;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void Update()
    {
        if (fadeOut)
        {
            if (cam.orthographicSize < newCameraSize)
            {
                cam.orthographicSize += (newCameraSize / fadeOutTime) * Time.deltaTime;
            }
            else
            {
                cam.orthographicSize = newCameraSize;
            }
           
        }
    }

    public void FadeOutEffect(float cameraSize,float time , bool useScopeAim)
    {
        fadeOut = true;
        fadeOutTime = time;
        newCameraSize = cameraSize;

        
        postProcessing.SetActive(useScopeAim);
    }

    public void DefaltCameraSize()
    {
        fadeOut = false;
        cam.orthographicSize = defaltCameraSize;

        postProcessing.SetActive(false);
    }

    public float GetCameraSize()
    {
        return cam.orthographicSize;
    }
}
