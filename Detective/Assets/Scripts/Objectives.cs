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
}

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
    public bool finished;
    GameObject item;



    void Start()
    {
        //still need to disable interactable script from objects related to a completed puzzle
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
                            item.GetComponent<Image>().sprite = null;
                            item.GetComponent<Interactable>().IsActive = false;                     
                            SceneManager.LoadScene(0);

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
                        IsComplete = true;

                    if (Item != null)
                        Item.GetComponent<Interactable>().Hidden = false;
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
                    /* if (o == PlaceHolders.Length)
                         IsComplete = true;*/

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
                        }

                    }
                      for (int a = 0; a < UiItems.transform.childCount; a++)
                    {
                        if (UiItems.transform.GetChild(i).GetComponent<Image>().sprite == Open)
                        {
                            finished = true;
                            item = UiItems.transform.GetChild(i).gameObject;

                        }
                    }
                       
                    break;
                    #endregion
            }
        }
    }

}
