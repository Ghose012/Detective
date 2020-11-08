using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraReset : MonoBehaviour
{
    public Vector3 pos;
    public GameObject Objects;

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            GetComponent<Camera>().orthographicSize = 5;
            transform.position = pos;
            
            for(int i = 0; i < Objects.transform.childCount; i++)
            {
                Objects.transform.GetChild(i).GetComponent<Collider2D>().enabled = true;
            }
        }
    }
}
