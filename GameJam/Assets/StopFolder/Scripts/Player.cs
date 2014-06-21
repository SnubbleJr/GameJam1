using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }
    float x, y;
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
            Screen.lockCursor = !Screen.lockCursor;

        if (Screen.lockCursor)
        {
            x += Input.GetAxis("Mouse X") * 5;
            y -= Input.GetAxis("Mouse Y") * 4;

            var rotation = Quaternion.Euler(y, x, 0);
            transform.rotation = rotation;
        }
    }
}
