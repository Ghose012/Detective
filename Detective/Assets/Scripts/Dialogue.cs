using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public string[] sentences;
    private int index;
    public float typingSpeed;
    public GameObject continueButon;

    public int indexScene;

    string sceneName;
    Scene currentScene;

    public GameObject NavigationUI;
    public GameObject Itemsui;
    public GameObject DialogueUI;
    public GameObject Flashback;
    public GameObject CurrentCase;

    public bool EndScene;
    public bool FlahBackBool;

    void Start()
    {
        StartCoroutine(Type());
        currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
        DialogueUI.SetActive(true);
        NavigationUI.SetActive(false);
        Itemsui.SetActive(false);


    }
    private void Update()
    {
        if (textDisplay.text == sentences[index])
        {
            continueButon.SetActive(true);

        }

    }
    //affichage lettre par lettre
    IEnumerator Type()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    //affichage de chaque phrase
    public void NextSentence()
    {
        continueButon.SetActive(false);

        if (index < sentences.Length - 1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        }
        else
        {
            textDisplay.text = "";
            continueButon.SetActive(false);
            //nbadel e scene ki youfa dialogue w nebda kamalt l case

            if (EndScene)
            {
                SceneManager.LoadScene(indexScene);
                Debug.Log("next");
            }

            else if (!EndScene )
            {
                Debug.Log("not end");
                
                if (FlahBackBool == true)
                {
                    if (CurrentCase)
                    {
                        DialogueUI.SetActive(false);
                        NavigationUI.SetActive(true);
                        Itemsui.SetActive(true);
                    }
                    Debug.Log("flash true");
                        //mel case bech nemchi lel flashback
                        if (!Flashback.activeInHierarchy)
                        {
                            Debug.Log("slah tru");
                            Flashback.SetActive(true);
                            CurrentCase.SetActive(false);
                        }
                        //mel flashback bech anrjaa lel case
                        else if (Flashback.activeInHierarchy)
                        {

                            Debug.Log("active");
                            Flashback.SetActive(false);
                            CurrentCase.SetActive(true);
                        
                        }
                    }
                else if (FlahBackBool == false)
                {
                        Debug.Log("not flashback");
                        DialogueUI.SetActive(false);
                        NavigationUI.SetActive(true);
                        Itemsui.SetActive(true);
                }

                Destroy(gameObject);

            }

        }
    }

}
