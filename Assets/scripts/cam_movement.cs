using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cam_movement : MonoBehaviour
{
    public float mouse_sensitivity = 100f;
    public Transform Playerbody;
    float xrotation = 0f;
    // Start is called before the first frame update
    /*void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    */

    // Update is called once per frame
    void Update()
    {
        if (pausemenu.isOn)
        {
            if (Cursor.lockState!=CursorLockMode.None)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            return;
        }
        if (Cursor.lockState != CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        float mouseX = Input.GetAxis("Mouse X") * mouse_sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouse_sensitivity * Time.deltaTime;

        xrotation -= mouseY;
        xrotation = Mathf.Clamp(xrotation, -90f, 60f);

        transform.localRotation = Quaternion.Euler(xrotation, 0f, 0f);
        Playerbody.Rotate(Vector3.up * mouseX);
    }
}