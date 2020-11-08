using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationArrow : MonoBehaviour 
{
    public Vector3 pos;
    public GameObject Objects;
    Camera cam;
    void Start()
    {
        cam = Camera.main;
    }

    void OnMouseDown()
    {
        //Vector3 x = cam.GetComponent<CameraReset>().pos;
        cam.orthographicSize = 5;
        cam.transform.position = pos;
        //cam.GetComponent<CameraReset>().pos = pos;
        //pos = x;

        for (int i = 0; i < Objects.transform.childCount; i++)
        {
            Objects.transform.GetChild(i).GetComponent<Collider2D>().enabled = true;
        }
    }
}
