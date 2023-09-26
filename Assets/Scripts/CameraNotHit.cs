using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraNotHit : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), 0.5f))
        {
            Debug.Log("bateu");
        }
    }
}
