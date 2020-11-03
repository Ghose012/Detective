using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockHand : MonoBehaviour
{
    public float speed;
    public int Value;
    void Start()
    {
        
    }

    void OnMouseDown()
    {
        if (enabled)
        {
            transform.Rotate(0, 0, -speed);
            Value++;
            if (Value > 12)
                Value = 1;
        }
    }

    void Update()
    {
        if (Camera.main.orthographicSize == 5 && GetComponent<Collider2D>().enabled)
            GetComponent<Collider2D>().enabled = false;
        if (Camera.main.orthographicSize != 5 && !GetComponent<Collider2D>().enabled)
            GetComponent<Collider2D>().enabled = true;
    }
}
