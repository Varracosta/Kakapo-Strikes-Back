using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Moves Crosshair on level 4 according to position of the mouse
public class CrosshairMovement : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;

    private PlayerInputHandler inputHandler;
    void Start()
    {
        inputHandler = FindObjectOfType<PlayerInputHandler>();
    }
    void Update()
    {
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(inputHandler.MousePosition);
        mouseWorldPosition.z = 0f;
        transform.position = mouseWorldPosition;
    }
}
