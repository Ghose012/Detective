using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;

[ExecuteInEditMode]
internal class Interactable : MonoBehaviour, IPointerDownHandler
{
    public List<TypeofItem> Types;

    //zoom (item,pos) , OpenClose (isActive,pos)
    public GameObject Item;
    public Vector3 pos;
    public bool IsActive;
    Camera cam;

    //zoomer
    public bool IsZoomer;
    public float zoom;

    //Collectable
    //public bool IsCollectable;
    public bool Hidden;

    //Spin
   // public bool IsSpin;
    public float speed;
    public int Value;

    //Navigtion
   // public bool IsNavigtion;
    public GameObject Objects;

    //PlaceHolder
    //public bool IsPlaceHolder;
    public GameObject UiItems;

    public enum TypeofItem
    {
        Zoomer,
        Collectable,
        UiSelectable,
        OpenClose,
        Spin,
        Navigation,
        PlaceHolder
    }
    void Start()
    {
        cam = Camera.main;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        foreach (var type in Types)
        {
            switch (type)
            {
                case TypeofItem.UiSelectable:
                    if (GetComponent<Image>().sprite != null)
                    {
                        IsActive = !IsActive;
                        for (int i = 0; i < UiItems.transform.childCount; i++)
                        {
                            if (UiItems.transform.GetChild(i).gameObject != gameObject)
                                UiItems.transform.GetChild(i).GetComponent<Interactable>().IsActive = false;
                        }
                    }
                    break;
            }
        }
    }

    void OnMouseDown()
    {
        foreach(var type in Types)
        {
            switch (type)
            {
                case TypeofItem.Zoomer:
                    cam.orthographicSize = zoom;
                    cam.transform.position = pos;
                    GetComponent<Collider2D>().enabled = false;
                    if (Item != null)
                        Item.GetComponent<Interactable>().Hidden = false;
                    break;

                case TypeofItem.Collectable:
                    for (int i = 0; i < UiItems.transform.childCount; i++)
                    {
                        if (UiItems.transform.GetChild(i).GetComponent<Image>().sprite == null)
                        {
                            UiItems.transform.GetChild(i).GetComponent<Image>().sprite = GetComponent<SpriteRenderer>().sprite;
                            break;
                        }
                    }
                    Destroy(gameObject);
                    break;

                case TypeofItem.OpenClose:
                    IsActive = !IsActive;

                    if (IsActive)
                    {
                        for (int i = 0; i < transform.childCount; i++)
                        {
                            transform.GetChild(i).gameObject.SetActive(true);
                        }
                        GetComponent<Collider2D>().offset = pos;
                        if (Item != null)
                            Item.GetComponent<Interactable>().Hidden = false;
                    }
                    else
                    {
                        for (int i = 0; i < transform.childCount; i++)
                        {
                            transform.GetChild(i).gameObject.SetActive(false);
                        }
                        GetComponent<Collider2D>().offset = Vector2.zero;
                        if (Item != null)
                            Item.GetComponent<Interactable>().Hidden = true;
                    }
                    break;

                case TypeofItem.Spin:
                    if (enabled)
                    {
                        transform.Rotate(0, 0, -speed);
                        Value++;
                        if (Value > 12)
                            Value = 1;
                    }
                    break;

                case TypeofItem.Navigation:
                    Vector3 x = cam.GetComponent<CameraReset>().pos;
                    cam.orthographicSize = 5;
                    cam.transform.position = pos;
                    cam.GetComponent<CameraReset>().pos = pos;
                    pos = x;

                    for (int i = 0; i < Objects.transform.childCount; i++)
                    {
                        Objects.transform.GetChild(i).GetComponent<Collider2D>().enabled = true;
                    }
                    break;

                case TypeofItem.PlaceHolder:
                    for (int i = 0; i < UiItems.transform.childCount; i++)
                    {
                        if (UiItems.transform.GetChild(i).GetComponent<Image>().sprite == GetComponent<SpriteRenderer>().sprite)
                        {
                            Item = UiItems.transform.GetChild(i).gameObject;
                            break;
                        }
                    }

                    if (Item != null)
                    {
                        if (Item.GetComponent<Interactable>().IsActive)
                        {
                            GetComponent<SpriteRenderer>().enabled = true;
                            Item.GetComponent<Image>().sprite = null;
                            Item.GetComponent<Interactable>().IsActive = false;
                        }
                    }
                    break;
            }
        } 
    }

    void Update()
    {
        foreach (var type in Types)
        {
            switch (type)
            {
                case TypeofItem.Zoomer:

                    break;

                case TypeofItem.Collectable:
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
                    break;

                case TypeofItem.UiSelectable:
                    if (IsActive && transform.localScale != Vector3.one * 1.2f)
                        transform.localScale *= 1.2f;
                    if (!IsActive && transform.localScale != Vector3.one)
                        transform.localScale = Vector3.one;
                    break;

                case TypeofItem.OpenClose: case TypeofItem.Spin: case TypeofItem.PlaceHolder:
                    if (Camera.main.orthographicSize == 5 && GetComponent<Collider2D>().enabled)
                        GetComponent<Collider2D>().enabled = false;
                    if (Camera.main.orthographicSize != 5 && !GetComponent<Collider2D>().enabled)
                        GetComponent<Collider2D>().enabled = true;
                    break;
            }
        }
    }
}
