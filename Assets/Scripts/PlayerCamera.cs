using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float Sensitivity = 2f;
    public float x;
    public float y;
    public Transform head;

    void Start()
    {
        head = transform.Find("Head");
        PlayerPrefs.SetInt("CursorViewMode", 0);
        PlayerPrefs.SetString("CursorLockMode", "Lock");
    }
    void Update()
    {
        if(PlayerPrefs.GetInt("CursorViewMode") == 1)
            Cursor.visible = true;
        else
            Cursor.visible = false;
        x = Input.GetAxis("Mouse X") * Sensitivity * 1.5f;
        y = -Input.GetAxis("Mouse Y") * Sensitivity;

        transform.localEulerAngles += new Vector3(0, x, 0);
        head.localEulerAngles += new Vector3(y, 0, 0);
    }
}
