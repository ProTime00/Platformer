using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject mario;

    private void FixedUpdate()
    {
        var vector3 = gameObject.transform.position;
        vector3.x = mario.transform.position.x;
        gameObject.transform.position = vector3;
    }
}
