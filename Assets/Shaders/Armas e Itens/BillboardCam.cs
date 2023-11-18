using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BillboardCam : MonoBehaviour
{
    //move a barra de vida para a camera.
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private Camera currentCamera;

    void Awake()
    {
        //Seta a camera
        CheckCamera();
    }
    void LateUpdate()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - currentCamera.transform.position);
    }

    void CheckCamera()
    {
        mainCamera = GameObject.Find("MainCamera");
        currentCamera = mainCamera.GetComponent<Camera>();
    }
}