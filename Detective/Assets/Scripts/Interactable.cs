using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum TypeofItem
{
    Zoomer,
    Collectable,
    UiSelectable,
    OpenClose,
    Spin,
    Navigation,
    PlaceHolder,
    Coloring,
    Search,
    InputPuzzle,

}

[RequireComponent(typeof(BoxCollider2D))]
[ExecuteInEditMode]
public class Interactable : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]
    public List<TypeofItem> Types;

    Camera cam;
    public int TypesCount;

    #region VariablesTypes
    //zoom (item,pos) , OpenClose (isActive,pos,item), search(pos), Coloring(item)
    public GameObject Item;
    public Vector3 pos;
    public bool IsActive;

    //zoomer
    public float zoom;

    //Collectable (UiItems)
    public bool Hidden;

    //Spin
    public float speed;
    public int Value;

    //Navigtion
    public GameObject Objects;
    public Vector3[] Destinations;
    public int step;
    //  public int DestinationCounts;


    //PlaceHolder
    public GameObject UiItems;

    //OpenClose, Spin, Placeholder, Collectable
    public bool AffectedByZoom;

    //Search
    Vector3 InitialPos;

    //Coloring
    public Sprite ColorTool;
    public Color colorChoice;
    GameObject Target;
    GameObject ColorBuckets;

    //Input
    public string RightAnswer;
    public InputField MyInput;
    public GameObject CanvasIn;

    //OpenClose 
    public bool Locked;

    #endregion

    void Start()
    {
        cam = Camera.main;
        InitialPos = transform.position;
        Target = Item;
        UiItems = GameObject.Find("Ui").transform.Find("Items").gameObject;
        if(ColorTool != null)
            ColorBuckets = GameObject.Find("Change").transform.Find("Paint PlaceHolders").gameObject;
    }



    public void OnPointerDown(PointerEventData eventData)
    {
        for (int i = 0; i < TypesCount; i++)
        {
            switch (Types[i])
            {
                #region UiSelectable
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
                #endregion


                #region Navigation
                case TypeofItem.Navigation:

                    cam.orthographicSize = 5;

                    for (int k = 0; k < Destinations.Length; k++)
                    {
                        if (cam.transform.position == Destinations[k])
                        {
                            if (k + step < 0)
                            {
                                cam.transform.position = Destinations[Destinations.Length - 1];

                            }

                            else if (k + step < Destinations.Length)
                            {
                                cam.transform.position = Destinations[k + step];

                            }
                            else
                            {
                                cam.transform.position = Destinations[0];
                            }
                            cam.GetComponent<CameraReset>().pos = cam.transform.position;
                            break;

                        }

                    }


                    break;
                    #endregion

            }
        }
    }

    public void Openning()
    {
        if (!Locked)
        {
            IsActive = !IsActive;

            if (IsActive)
            {
                for (int j = 0; j < transform.childCount; j++)
                {
                    if (transform.GetChild(j).gameObject.GetComponent<Interactable>() != null && GetComponent<SpriteRenderer>() != null)
                    {
                        if (GetComponent<SpriteRenderer>().enabled)
                        {
                            transform.GetChild(j).gameObject.SetActive(true);
                        }
                    }
                    else
                        transform.GetChild(j).gameObject.SetActive(true);
                }
                GetComponent<Collider2D>().offset = pos;
                if (Item != null)
                {
                    if(Item.GetComponent<Interactable>() != null)
                    {
                        if (Item.GetComponent<Interactable>().Types[0] == TypeofItem.Collectable)
                            Item.GetComponent<Interactable>().Hidden = false;
                        else
                            Item.SetActive(true);
                    }
                    else
                    {
                        Item.SetActive(true);
                        Locked = true;
                    }
                        
                }
            }
            else
            {
                for (int j = 0; j < transform.childCount; j++)
                {
                    if (transform.GetChild(j).gameObject.GetComponent<Interactable>() != null && GetComponent<SpriteRenderer>() != null)
                    {
                        if (GetComponent<SpriteRenderer>().enabled)
                        {

                        }
                    }
                    else
                        transform.GetChild(j).gameObject.SetActive(false);
                }
                GetComponent<Collider2D>().offset = Vector2.zero;
                if (Item != null)
                {
                    if (Item.GetComponent<Interactable>().Types[0] == TypeofItem.Collectable)
                        Item.GetComponent<Interactable>().Hidden = true;
                    else
                        Item.SetActive(false);
                }
            }
        }
    }

    void OnMouseDown()
    {
        for (int i = 0; i < TypesCount; i++)
        {
            switch (Types[i])
            {
                #region Input
                case TypeofItem.InputPuzzle:
                    CanvasIn.SetActive(true);
                    break;
                #endregion

                #region Zoomer
                case TypeofItem.Zoomer:
                    cam.orthographicSize = zoom;
                    cam.transform.position = pos;
                    GetComponent<Collider2D>().enabled = false;
                    if (Item != null)
                    {
                        if (Item.GetComponent<Interactable>().Types[0] == TypeofItem.Collectable)
                            Item.GetComponent<Interactable>().Hidden = false;
                        else
                            Item.SetActive(true);
                    }
                    break;
                #endregion

                #region Collectable
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
                #endregion

                #region OpenClose
                case TypeofItem.OpenClose:
                    Openning();
                    break;
                #endregion

                #region Spin
                case TypeofItem.Spin:
                    if (enabled)
                    {
                        transform.Rotate(0, 0, -(360/speed));
                        Value++;
                        if (Value > speed)
                            Value = 1;
                    }
                    break;
                #endregion

                #region PlaceHolder
                case TypeofItem.PlaceHolder:
                    GameObject tmpitem = Item;
                    for (int j = 0; j < UiItems.transform.childCount; j++)
                    {
                        if (UiItems.transform.GetChild(j).GetComponent<Image>().sprite == GetComponent<SpriteRenderer>().sprite)
                        {
                            Item = UiItems.transform.GetChild(j).gameObject;
                            break;
                        }
                    }

                    if (ColorTool != null)
                    {
                        if (Item != null && Item.GetComponent<Image>().sprite != ColorTool)
                        {
                            if (Item.GetComponent<Interactable>().IsActive)
                            {
                                GetComponent<SpriteRenderer>().enabled = true;
                                Item.GetComponent<Image>().sprite = null;
                                Item.GetComponent<Interactable>().IsActive = false;
                            }
                        }
                    }
                    else
                    {
                        if (Item != null)
                        {
                            if (Item.GetComponent<Interactable>().IsActive)
                            {
                                GetComponent<SpriteRenderer>().enabled = true;
                                Item.GetComponent<Image>().sprite = null;
                                Item.GetComponent<Interactable>().IsActive = false;
                            }
                            if (GetComponent<Interactable>().Types[1] == TypeofItem.OpenClose)
                            {
                                Item = tmpitem;
                                Openning();
                            }     
                        }
                    }
                    
                    break;
                #endregion

                #region Search
                case TypeofItem.Search:
                    if (transform.position == InitialPos)
                    {
                        transform.position = new Vector3(pos.x, pos.y, 0f);

                        if (Item != null)
                            Item.GetComponent<Interactable>().Hidden = false;
                    }
                    else
                    {
                        transform.position = InitialPos;
                        if (Item != null)
                            Item.GetComponent<Interactable>().Hidden = true;
                    }
                    break;
                #endregion

                #region Coloring
                case TypeofItem.Coloring:
                    for (int j = 0; j < UiItems.transform.childCount; j++)
                    {
                        if (UiItems.transform.GetChild(j).GetComponent<Image>().sprite == ColorTool)
                        {
                            Item = UiItems.transform.GetChild(j).gameObject;
                            break;
                        }
                    }

                    if (Item != null)
                    {
                        if (Item.GetComponent<Interactable>().IsActive)
                        {
                            bool changed = false;
                            for (int j = 0; j < ColorBuckets.transform.childCount; j++)
                            {
                                Color c = ColorBuckets.transform.GetChild(j).GetComponent<Interactable>().colorChoice;
                                if (Target.GetComponent<SpriteRenderer>().color == c)
                                {
                                    Target.GetComponent<SpriteRenderer>().color *= colorChoice;
                                    changed = true;
                                    break;
                                }
                            }

                            if (!changed)
                            { Target.GetComponent<SpriteRenderer>().color = colorChoice; }

                        }
                    }

                    break;
                #endregion
            }
        }
    }

    void Update()
    {
        for (int i = 0; i < TypesCount; i++)
        {
            switch (Types[i])
            {
                #region Zoomer
                case TypeofItem.Zoomer:

                    break;
                #endregion

                #region Collectable
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
                #endregion

                #region UISelectable
                case TypeofItem.UiSelectable:
                    if (IsActive && transform.localScale != Vector3.one * 1.2f)
                        transform.localScale *= 1.2f;
                    if (!IsActive && transform.localScale != Vector3.one)
                        transform.localScale = Vector3.one;
                    break;
                #endregion

                #region AffectedByZoom
                case TypeofItem.OpenClose:
                case TypeofItem.Spin:
                case TypeofItem.PlaceHolder:
                case TypeofItem.Search:

                    if (AffectedByZoom)
                    {
                        if (Camera.main.orthographicSize == 5 && GetComponent<Collider2D>().enabled)
                        {
                            GetComponent<Collider2D>().enabled = false;
                            if (GetComponent<Interactable>().Types[0] == TypeofItem.OpenClose && IsActive)
                                Openning();
                        }
                            
                        if (Camera.main.orthographicSize != 5 && !GetComponent<Collider2D>().enabled)
                            GetComponent<Collider2D>().enabled = true;
                    }
                    break;
                #endregion

                #region Navigation
                case TypeofItem.Navigation:
                    if (AffectedByZoom)
                    {
                        if (Camera.main.orthographicSize != 5 && GetComponent<Collider2D>().enabled && GetComponent<Image>().enabled)
                        {
                            GetComponent<Collider2D>().enabled = false;
                            GetComponent<Image>().enabled = false;
                        }
                        if (Camera.main.orthographicSize == 5 && !GetComponent<Collider2D>().enabled && !GetComponent<Image>().enabled)
                        {
                            GetComponent<Collider2D>().enabled = true;
                            GetComponent<Image>().enabled = true;
                        }

                    }
                    break;
                #endregion

                #region Input
                case TypeofItem.InputPuzzle:

                    if (RightAnswer.ToString() == MyInput.text)
                    {
                        MyInput.DeactivateInputField();
                        Item.SetActive(true);
                        CanvasIn.SetActive(false);
                    }

                    break;
                    #endregion

            }
        }
    }
}
