using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemUi : MonoBehaviour, IPointerDownHandler
{
    public bool IsActive;
    void Start()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (GetComponent<Image>().sprite != null)
        {
            IsActive = !IsActive;
            GameObject Items = GameObject.Find("Ui").transform.Find("Items").gameObject;
            for (int i = 0; i < Items.transform.childCount; i++)
            {
                if(Items.transform.GetChild(i).gameObject != gameObject)
                    Items.transform.GetChild(i).GetComponent<ItemUi>().IsActive = false;
            }
        }
    }

    void Update()
    {
        if (IsActive && transform.localScale != Vector3.one * 1.2f)
            transform.localScale *= 1.2f;
        if (!IsActive && transform.localScale != Vector3.one)
            transform.localScale = Vector3.one;
    }
}
