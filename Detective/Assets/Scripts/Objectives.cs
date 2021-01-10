using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public enum TypeofObjective
{
    SpinCheck,
    SlotPuzzle,
    ColoringPuzzle,
    Key,
    Save,
    Load,
    SwitchersCheck,
}

[RequireComponent(typeof(AudioSource))]
[ExecuteInEditMode]
public class Objectives : MonoBehaviour
{
    [SerializeField]
    public List<TypeofObjective> Types;
    public int TypesCkeckCount;

    //ColoringPuzzle, SpinCheck
    public GameObject Item;
    public bool IsComplete;

    //ColoringPuzzle
    public Color DesiredColor;
    //ColoringPuzzle , key
    public Sprite ColorTool;

    //SpinCheck
    public GameObject[] Hands;
    public int[] Time;

    //SlotPuzzle
    public GameObject[] PlaceHolders;

    //Key
    public Objectives[] AllObjectives;
    public GameObject UiItems;
    public Sprite Open;
    public GameObject KeyOpen;
    public static bool finished;
    GameObject item;
    public int indexScene;

    AudioSource audioSource;

    //Save
    public int levelInd;

    private void Awake()
    {
        for (int i = 0; i < TypesCkeckCount; i++)
        {
            switch (Types[i])
            {
                #region Load
                case TypeofObjective.Load:
                    if (PlayerPrefs.HasKey("CurrentLevel"))
                    {
                        PlayerPrefs.GetInt("CurrentLevel");

                    }
                    else
                    {
                        PlayerPrefs.SetInt("CurrentLevel", 3);

                    }

                    break;
                    #endregion
            }
        }
    }


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        //still need to disable interactable script from objects related to a completed puzzle
        if (GameObject.Find("Ui") != null)
            UiItems = GameObject.Find("Ui").transform.Find("Items").gameObject;
        finished = false;

        
    }



    public void OnMouseDown()
    {
        for (int i = 0; i < TypesCkeckCount; i++)
        {
            switch (Types[i])
            {
                #region key
                case TypeofObjective.Key:
                    if (item != null)
                    {

                        if (item.GetComponent<Interactable>().IsActive)
                        {
                            if(item.tag == "End")
                            {
                                item.GetComponent<Image>().sprite = null;
                                item.GetComponent<Interactable>().IsActive = false;
                                finished = true;
                                Debug.Log(finished);

                            }
                            else
                            {
                                item.GetComponent<Image>().sprite = null;
                                item.GetComponent<Interactable>().IsActive = false;
                                GetComponent<Interactable>().Locked = false;
                                GetComponent<Interactable>().Openning();

                                // temp
                                if (transform.parent.GetComponent<Animator>() != null)
                                    transform.parent.GetComponent<Animator>().enabled = true;
                            }
                        }
                    }
                    break;
                #endregion
            }
        }
    }


    
    void Update()
    {
        for (int i = 0; i < TypesCkeckCount; i++)
        {
            switch (Types[i])
            {
                #region SpinCheck
                case TypeofObjective.SpinCheck:
                    int x = 0;
                    for (int j = 0; j < Hands.Length; j++)
                    {
                        if (Hands[j].GetComponent<Interactable>().Value == Time[j])
                            x++;
                    }
                    if (x == Hands.Length)
                    {
                        IsComplete = true;
                        if (Item != null)
                        {
                            if (Item.GetComponent<Interactable>() != null)
                            {
                                if (Item.GetComponent<Interactable>().Types[0] == TypeofItem.Collectable)
                                    Item.GetComponent<Interactable>().Hidden = false;
                                else
                                    Item.SetActive(true);
                            }
                            else
                                Item.SetActive(true);
                        }
                    }     
                    break;
                #endregion

                #region SlotPuzzle
                case TypeofObjective.SlotPuzzle:
                    int o = 0;
                    for (int j = 0; j < PlaceHolders.Length; j++)
                    {
                        if (PlaceHolders[j].GetComponent<SpriteRenderer>().enabled)
                            o++;
                    }
                     if (o == PlaceHolders.Length)
                         IsComplete = true;

                    if (Item != null)
                        Item.GetComponent<Interactable>().Hidden = false;
                    break;
                #endregion

                #region Coloring
                case TypeofObjective.ColoringPuzzle:
                    if (Item.GetComponent<SpriteRenderer>().color == DesiredColor)
                    {
                        IsComplete = true;
                    }

                    break;
                #endregion

                #region key
                case TypeofObjective.Key:

                    for (int j = 0; j < AllObjectives.Length; j++)
                    {
                        if (AllObjectives[j].IsComplete)
                        {

                            KeyOpen.GetComponent<Interactable>().Hidden = false;
                            
                            //audioSource.Play();
                        }

                    }

                    for (int a = 0; a < UiItems.transform.childCount; a++)
                    {

                        if (UiItems.transform.GetChild(a).GetComponent<Image>().sprite == Open)
                        {
                            finished = true;
                            item = UiItems.transform.GetChild(a).gameObject;
                        }

                    }

                    break;
                #endregion

            }
        }
    }

}
