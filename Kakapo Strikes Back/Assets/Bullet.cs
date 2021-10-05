using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 4f;

    void Update()
    {
        transform.Translate(transform.right * speed * Time.deltaTime, Space.World);
    }
}
