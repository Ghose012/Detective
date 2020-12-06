using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pick : MonoBehaviour
{
    public float Distance = 2;
    public GameObject target;
    GameObject player;
    bool pick = false;
    void Start()
    {
        player = GameObject.Find("Player");
    }

    void OnMouseDown()
    {
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForEndOfFrame();
        pick = true;
    }

    void Update()
    {
        if (pick)
        {
            if (Vector2.Distance(transform.position, player.transform.position) < Distance)
            {
                Destroy(gameObject);
                Destroy(target);
                pick = false;
            }

            if (Input.GetMouseButtonDown(0))
                pick = false;
        }
    }
}
