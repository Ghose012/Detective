using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoomer : MonoBehaviour
{
    public float zoom; 
    public Vector3 pos;
    public GameObject Item;
    Camera cam;
    void Start()
    {
        cam = Camera.main;
    }

    void OnMouseDown()
    {
        cam.orthographicSize = zoom;
        cam.transform.position = pos;
        GetComponent<Collider2D>().enabled = false;
        if(Item != null)
        Item.GetComponent<Item>().Hidden = false;
    }
}
