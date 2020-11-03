using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movements : MonoBehaviour
{
    public float speed = 5;
    Vector3 point;
    Animator anim;

    void Start()
    {
        point = transform.position;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Input.mousePosition;
            point = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 0));

            anim.SetBool("Walk_R", false);
            anim.SetBool("Walk_L", false);
        }

        if (transform.position.x < point.x - 0.25f || transform.position.x > point.x + 0.25f)
        {
            if(point.x > transform.position.x)
            {
                transform.Translate(Vector2.right * speed * Time.deltaTime);
                anim.SetBool("Walk_R", true);
            }
                

            if (point.x < transform.position.x)
            {
                transform.Translate(Vector2.left * speed * Time.deltaTime);
                anim.SetBool("Walk_L", true);
            }
        }
        else
        {
            anim.SetBool("Walk_R", false);
            anim.SetBool("Walk_L", false);
        }
    }
}
