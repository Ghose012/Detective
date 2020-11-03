using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public bool Hidden;
    void Start()
    {
        
    }

    void OnMouseDown()
    {
        GameObject Items = GameObject.Find("Ui").transform.Find("Items").gameObject;
        for(int i = 0; i < Items.transform.childCount; i++)
        {
            if (Items.transform.GetChild(i).GetComponent<Image>().sprite == null)
            {
                Items.transform.GetChild(i).GetComponent<Image>().sprite = GetComponent<SpriteRenderer>().sprite;
                break;
            }
        }
        Destroy(gameObject);
    }

    void Update()
    {
        if (Hidden)
        {
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
        }
        else
        {
            GetComponent<SpriteRenderer>().enabled = true;
            GetComponent<Collider2D>().enabled = true;
        }
    }
}
