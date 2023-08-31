using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
    [SerializeField] private float speed = 3f;

    private Camera mainCamera;
    private Vector3 mouseInput = Vector3.zero;

    private void Initialize()
    {
        mainCamera = Camera.main;
    }

    public override void OnNetworkSpawn(){
        base.OnNetworkSpawn();
        Initialize();
    }

    void Update()
    {
        if(!IsOwner || !Application.isFocused) return;
        //Movement
        Vector2 mousePositionScreen = (Vector2)Input.mousePosition;
        mouseInput.x = Input.mousePosition.x;
        mouseInput.y = Input.mousePosition.y;
        mouseInput.z = mainCamera.nearClipPlane;
        Vector3 mouseWorldCoordinates = mainCamera.ScreenToWorldPoint(mouseInput);
        mouseWorldCoordinates.z = 0f;
        transform.position = Vector3.MoveTowards(transform.position, mouseWorldCoordinates, 
        Time.deltaTime * speed);

        //Rotation
        if(mouseWorldCoordinates != transform.position){
            Vector3 targetDirection = mouseWorldCoordinates - transform.position;
            transform.up = targetDirection;
        }
    }
}
