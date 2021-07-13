using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] private float backroundScrollSpeed = 0.5f;
    private Material material;
    private Vector2 offset;

    private void Start()
    {
        material = GetComponent<Renderer>().material;
        offset = new Vector2(backroundScrollSpeed, 0f);
    }

    private void Update()
    {
        material.mainTextureOffset += offset * Time.deltaTime;
    }
}
