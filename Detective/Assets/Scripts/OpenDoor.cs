using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OpenDoor : MonoBehaviour
{
    public Sprite key;
    GameObject item;
    void Start()
    {

    }

    void OnMouseDown()
    {
        GameObject Items = GameObject.Find("Ui").transform.Find("Items").gameObject;
        for (int i = 0; i < Items.transform.childCount; i++)
        {
            if (Items.transform.GetChild(i).GetComponent<Image>().sprite == key)
            {
                item = Items.transform.GetChild(i).gameObject;
                break;
            }
        }

        if (item != null)
        {
            if (item.GetComponent<Interactable>().IsActive)
            {
                item.GetComponent<Image>().sprite = null;
                item.GetComponent<Interactable>().IsActive = false;
                StartCoroutine(WaitReplay());
            }
        }
    }

    IEnumerator WaitReplay()
    {
        yield return new WaitForSecondsRealtime(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);
    }
}
