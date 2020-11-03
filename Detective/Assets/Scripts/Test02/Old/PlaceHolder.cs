using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceHolder : MonoBehaviour
{
    GameObject item;
    void Start()
    {
        
    }

    void OnMouseDown()
    {
        GameObject Items = GameObject.Find("Ui").transform.Find("Items").gameObject;
        for (int i = 0; i < Items.transform.childCount; i++)
        {
            if (Items.transform.GetChild(i).GetComponent<Image>().sprite == GetComponent<SpriteRenderer>().sprite)
            {
                item = Items.transform.GetChild(i).gameObject;
                break;
            }
        }

        if (item != null)
        {
            if (item.GetComponent<ItemUi>().IsActive)
            {
                GetComponent<SpriteRenderer>().enabled = true;
                item.GetComponent<Image>().sprite = null;
                item.GetComponent<ItemUi>().IsActive = false;
            }
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
