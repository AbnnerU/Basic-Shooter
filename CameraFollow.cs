using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject player;

    [SerializeField] private float cameraSpeed;

    private InputControler inputControler;
    private Vector2 mousePosition;

    private Vector3 velocity = Vector3.zero;

    private Vector2 midlePoint;
    private void Awake()
    {
        inputControler = FindObjectOfType<InputControler>();
        
    }

    // Update is called once per frame
    void Update()
    {
            
               
    }

    private void FixedUpdate()
    {
        mousePosition = inputControler.GetMousePosition();
        Vector2 playerPosition = Camera.main.WorldToScreenPoint(player.transform.position);

        midlePoint = (playerPosition - mousePosition) / 2;

        Vector2 midlePoitToWord = Camera.main.ScreenToWorldPoint(new Vector2(playerPosition.x + (-midlePoint.x), playerPosition.y + (-midlePoint.y)));
    
        transform.position = Vector3.SmoothDamp(new Vector3(transform.position.x, transform.position.y, -10), new Vector3(midlePoitToWord.x, midlePoitToWord.y, -10),ref velocity,cameraSpeed );
    }


 
}
