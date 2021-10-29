using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Plays water splash sound if anything hits the water
public class WaterSplash : MonoBehaviour
{
    [SerializeField] private AudioClip waterSplash;

    private void OnTriggerEnter2D(Collider2D other)
    {
        AudioSource.PlayClipAtPoint(waterSplash, Camera.main.transform.position);
    }
}
