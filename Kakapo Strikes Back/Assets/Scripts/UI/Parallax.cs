using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private Vector2 parallaxMovement;
    private Transform currentCameraPosition;
    private Vector3 lastCameraPosition;
    private float texturePerUnitsOnX;

    void Start()
    {
        currentCameraPosition = Camera.main.transform;
        lastCameraPosition = currentCameraPosition.position;
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        texturePerUnitsOnX = texture.width / sprite.pixelsPerUnit;
    }

    private void FixedUpdate()
    {
        Vector3 deltaMove = currentCameraPosition.position - lastCameraPosition;
        transform.position += new Vector3(deltaMove.x * parallaxMovement.x, deltaMove.y * parallaxMovement.y);
        lastCameraPosition = currentCameraPosition.position;

        if (Mathf.Abs(currentCameraPosition.position.x - transform.position.x) >= texturePerUnitsOnX)
        {
            float offsetPositionX = (currentCameraPosition.position.x - transform.position.x) % texturePerUnitsOnX;
            transform.position = new Vector3(currentCameraPosition.position.x + offsetPositionX, transform.position.y);
        }
    }

    void Update()
    {

    }



}
