using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraReset : MonoBehaviour
{
    public Vector3 pos;
    public GameObject Objects;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            GetComponent<Camera>().orthographicSize = 5;
            transform.position = pos;

            audioSource.Play();

            for (int i = 0; i < Objects.transform.childCount; i++)
            {
                Objects.transform.GetChild(i).GetComponent<Collider2D>().enabled = true;
            }
        }
    }
}
