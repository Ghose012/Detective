using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;

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

[RequireComponent(typeof(BoxCollider2D))]
[ExecuteInEditMode]
public class Interactable : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]
    public List<TypeofItem> Types;

    Camera cam;
   public int TypesCount;


    //zoom (item,pos) , OpenClose (isActive,pos)
    public GameObject Item;
    public Vector3 pos;
    public bool IsActive;

    //zoomer
    public float zoom;

    //Collectable
    public bool Hidden;

    //Spin
    public float speed;
    public int Value;

    //Navigtion
    public GameObject Objects;

    //PlaceHolder
    public GameObject UiItems;

    void Start()
    {
        cam = Camera.main;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        for (int i = 0; i < TypesCount; i++)
        {
            switch (Types[i])
            {
                case TypeofItem.UiSelectable:
                    if (GetComponent<Image>().sprite != null)
                    {
                        IsActive = !IsActive;
                        for (int j = 0; j < UiItems.transform.childCount; j++)
                        {
                            if (UiItems.transform.GetChild(j).gameObject != gameObject)
                                UiItems.transform.GetChild(j).GetComponent<Interactable>().IsActive = false;
                        }
                    }
                    break;
            }
        }
    }

    void OnMouseDown()
    {
        for (int i = 0; i < TypesCount; i++)
        {
            switch (Types[i])
            {
                case TypeofItem.Zoomer:
                    cam.orthographicSize = zoom;
                    cam.transform.position = pos;
                    GetComponent<Collider2D>().enabled = false;
                    if (Item != null)
                        Item.GetComponent<Interactable>().Hidden = false;
                    break;

                case TypeofItem.Collectable:
                    for (int j = 0; j < UiItems.transform.childCount; j++)
                    {
                        if (UiItems.transform.GetChild(j).GetComponent<Image>().sprite == null)
                        {
                            UiItems.transform.GetChild(j).GetComponent<Image>().sprite = GetComponent<SpriteRenderer>().sprite;
                            break;
                        }
                    }
                    Destroy(gameObject);
                    break;

                case TypeofItem.OpenClose:
                    IsActive = !IsActive;

                    if (IsActive)
                    {
                        for (int j = 0; j < transform.childCount; j++)
                        {
                            transform.GetChild(j).gameObject.SetActive(true);
                        }
                        GetComponent<Collider2D>().offset = pos;
                        if (Item != null)
                            Item.GetComponent<Interactable>().Hidden = false;
                    }
                    else
                    {
                        for (int j = 0; j < transform.childCount; j++)
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

                    for (int j = 0; j < Objects.transform.childCount; j++)
                    {
                        Objects.transform.GetChild(j).GetComponent<Collider2D>().enabled = true;
                    }
                    break;

                case TypeofItem.PlaceHolder:
                    for (int j = 0; j < UiItems.transform.childCount; j++)
                    {
                        if (UiItems.transform.GetChild(j).GetComponent<Image>().sprite == GetComponent<SpriteRenderer>().sprite)
                        {
                            Item = UiItems.transform.GetChild(j).gameObject;
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
        for (int i = 0; i <TypesCount; i++)
        {
            switch (Types[i])
            {
                case TypeofItem.Zoomer:

                    break;

                case TypeofItem.Collectable:
                    if (Hidden)
                    {
                        var SpriteCollectable = GetComponent<SpriteRenderer>();
                        if (SpriteCollectable != null)
                        {
                            SpriteCollectable.enabled = false;
                            GetComponent<Collider2D>().enabled = false;
                        }
                        
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
