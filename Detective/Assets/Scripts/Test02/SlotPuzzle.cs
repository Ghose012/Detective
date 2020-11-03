using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotPuzzle : MonoBehaviour
{
    public GameObject puzzle;
    public bool IsComplete = false;
    void Start()
    {
        
    }

    void Update()
    {
        if (IsComplete == false)
        {
            bool x = true;
            for (int i = 0; i < puzzle.transform.childCount; i++)
            {
                if (!puzzle.transform.GetChild(i).GetComponent<SpriteRenderer>().enabled)
                {
                    x = false;
                    break;
                }
            }

            IsComplete = x;

            if (IsComplete)
            {
                for (int i = 0; i < puzzle.transform.childCount; i++)
                {
                    puzzle.transform.GetChild(i).GetComponent<Collider2D>().enabled = false;
                }
            }
        }
    }
}
