using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public int TypesCount;

    //ColoringPuzzle, SpinCheck
    public GameObject Item;
    public bool IsComplete;

    //ColoringPuzzle
    public Color DesiredColor;

    //SpinCheck
    public GameObject[] Hands;
    public int[] Time;

    //SlotPuzzle
    public GameObject[] PlaceHolders;

    //Key
    public Objectives[] objectives;

    void Start()
    {
        //still need to disable interactable script from objects related to a completed puzzle
    }

    void Update()
    {
        for (int i = 0; i < TypesCount; i++)
        {
            switch (Types[i])
            {
                case TypeofObjective.SpinCheck:
                    int x = 0;
                    for(int j = 0; j < Hands.Length; j++)
                    {
                        if (Hands[j].GetComponent<Interactable>().Value == Time[j])
                            x++;
                    }
                    if (x == Hands.Length)
                        IsComplete = true;

                    if (Item != null)
                        Item.GetComponent<Interactable>().Hidden = false;
                    break;

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

                case TypeofObjective.ColoringPuzzle:
                    if (Item.GetComponent<SpriteRenderer>().color == DesiredColor)
                        IsComplete = true;

                    if (Item != null)
                        Item.GetComponent<Interactable>().Hidden = false;
                    break;

                case TypeofObjective.Key:
                    int s = 0;
                    for (int j = 0; j < objectives.Length; j++)
                    {
                        if (objectives[j].IsComplete)
                            s++;
                    }
                    if (s == objectives.Length)
                    {
                        //Give Key to open door and win case
                    }
                    break;
            }
        }
    }
}