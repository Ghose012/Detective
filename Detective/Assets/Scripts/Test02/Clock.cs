using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public GameObject HandM;
    public GameObject HandH;
    public Vector2 Time;
    public bool IsComplete = false;
    public GameObject Item;

    void Start()
    {
        
    }

    void Update()
    {
        if (HandM.GetComponent<Interactable>().Value == Time.y && HandH.GetComponent<Interactable>().Value == Time.x)
        {
            HandM.GetComponent<Interactable>().enabled = false;
            HandH.GetComponent<Interactable>().enabled = false;
            IsComplete = true;
            if(Item != null)
                Item.GetComponent<Interactable>().Hidden = false;
        }
    }
}
