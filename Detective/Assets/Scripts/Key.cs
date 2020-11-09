using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public SlotPuzzle sp;
    public Clock c;

    void Start()
    {
        
    }

    void Update()
    {
        if (c.IsComplete && sp.IsComplete && !GetComponent<SpriteRenderer>().enabled)
        {
            GetComponent<Interactable>().Hidden = false;
        }
    }
    //class Interectable, thot fiha string name mtaa l objet , nhot e zoomer, mbaad naamel des instance mtaa kol haja wa7adha, instance key, instance clock..
}
