using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
    public Transform kakapo;
    private Vector3 tempVector3;

    private void LateUpdate()
    {
        tempVector3 = new Vector3(kakapo.position.x, transform.position.y, transform.position.z);
        transform.position = tempVector3;
    }
}
