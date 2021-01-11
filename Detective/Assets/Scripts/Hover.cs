using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hover : MonoBehaviour
{
    public string Alt;
    Text AltText;
    GameObject panel;
    
    void Start()
    {
        panel = GameObject.Find("Ui").transform.Find("AltPanel").gameObject;
        AltText = panel.transform.Find("AltText").GetComponent<Text>();
    }

    void OnMouseOver()
    {
        panel.SetActive(true);
        panel.transform.position = Input.mousePosition + Vector3.up * 10;
        AltText.text = Alt;
    }

    void OnMouseExit()
    {
        panel.SetActive(false);
    }
}
