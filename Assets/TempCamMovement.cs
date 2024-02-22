using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempCamMovement : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Vector3 mov = Vector3.zero;
            mov.x = 1;
            transform.position += mov;
        }
        
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            Vector3 mov = Vector3.zero;
            mov.x = -1;
            transform.position += mov;
        }
    }
}
