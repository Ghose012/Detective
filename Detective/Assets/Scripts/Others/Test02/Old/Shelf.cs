using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shelf : MonoBehaviour
{
    public Vector2 pos;
    public GameObject Item;
    bool IsActive = false;
    
    void Start()
    {
        
    }

    void OnMouseDown()
    {
        IsActive = !IsActive;

        if (IsActive)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
            GetComponent<Collider2D>().offset = pos;
            if (Item != null)
                Item.GetComponent<Item>().Hidden = false;
        }
        else
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
            GetComponent<Collider2D>().offset = Vector2.zero;
            if (Item != null)
                Item.GetComponent<Item>().Hidden = true;
        }
    }

    void Update()
    {
        if (Camera.main.orthographicSize == 5 && GetComponent<Collider2D>().enabled)
            GetComponent<Collider2D>().enabled = false;
        if (Camera.main.orthographicSize != 5 && !GetComponent<Collider2D>().enabled)
            GetComponent<Collider2D>().enabled = true;
    }
}
