using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaking : MonoBehaviour
{
    [SerializeField] private CinemachineImpulseSource source;
    private void Awake()
    {
        source = GetComponent<CinemachineImpulseSource>();
    }
    void Start()
    {
        source.GenerateImpulse();
    }
}
