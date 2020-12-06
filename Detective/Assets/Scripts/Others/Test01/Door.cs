using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public float Distance = 2;
    GameObject player;
    bool Enter = false;
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
        Enter = true;
    }

    void Update()
    {
        if (Enter)
        {
            if (Vector2.Distance(transform.position, player.transform.position) < Distance && transform.childCount == 0)
            {
                Destroy(player);
                StartCoroutine(WaitReplay());
                Enter = false;
            }

            if (Input.GetMouseButtonDown(0))
                Enter = false;
        }
    }

    IEnumerator WaitReplay()
    {
        yield return new WaitForSecondsRealtime(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}
