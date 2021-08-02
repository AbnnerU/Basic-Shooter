using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HitAnimation : MonoBehaviour
{
    [SerializeField] private float firtDesappearTime;
    [SerializeField] private float secondDesappearTime;

    private TextMeshPro textMesh;
    private Color color;

    private float actualDesappearTime;

    private float moveInX;
    private float moveInY;

    private bool startAnimation;

    private void OnEnable()
    {
        actualDesappearTime = firtDesappearTime;

        textMesh = GetComponent<TextMeshPro>();
        color = textMesh.color;
        color.a = 1;
        textMesh.color = color;

        moveInX = Random.Range(-2, 2);
        moveInY = Random.Range(1, 3);
    }

    private void OnDisable()
    {
        actualDesappearTime = firtDesappearTime;

        textMesh = GetComponent<TextMeshPro>();
        color = textMesh.color;
        color.a = 1;
        textMesh.color = color;
    }

    private void Update()
    {
        actualDesappearTime -= Time.deltaTime;
        transform.position += new Vector3(moveInX * Time.deltaTime, moveInY * Time.deltaTime);

        if (actualDesappearTime <= 0)
        {
            color = textMesh.color;
            color.a -= secondDesappearTime * Time.deltaTime;
            textMesh.color = color;

            if (color.a <= 0)
            {
                PoolManager.ReleaseObject(gameObject);
            }
        }
    }
}
