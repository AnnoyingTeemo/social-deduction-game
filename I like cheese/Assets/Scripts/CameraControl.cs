using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Bolt;

public class CameraControl : MonoBehaviour
{
    public float Axis;
    public float maxRotation;
    public float minRotation;

    private bool mouselocked;

    public float rotationY; public float sensitivityY = 1;
    // Use this for initialization
    void Start()
    {
        mouselocked = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        transform.rotation = Quaternion.identity;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (mouselocked) {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                mouselocked = false;
            }
            else {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                mouselocked = true;
            }
        }
        LockedRotation();
    }

    void LockedRotation()
    {
        rotationY += Input.GetAxis("Mouse Y") * sensitivityY * 10;
        rotationY = Mathf.Clamp(rotationY, -minRotation, maxRotation);

        transform.localEulerAngles = new Vector3(-rotationY, 0, transform.localEulerAngles.z);
    }
}
