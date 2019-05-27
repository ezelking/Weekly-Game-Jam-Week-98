using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            if (transform.position.x < 190)
                transform.position += new Vector3(1, 0, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            if (transform.position.z > -275)
                transform.position += new Vector3(0, 0, -1);
        }
        if (Input.GetKey(KeyCode.W))
        {
            if (transform.position.z < 0)
            transform.position += new Vector3(0, 0, 1);
        }
        if (Input.GetKey(KeyCode.A))
        {
            if (transform.position.x > -140)
                transform.position += new Vector3(-1, 0, 0);
        }
    }
}
