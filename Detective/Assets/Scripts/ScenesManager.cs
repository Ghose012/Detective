using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScenesManager : MonoBehaviour
{
    public GameObject Buttons, ButtonsFB;
    public GameObject[] MailsOpened;
  


    private void Start()
    {
        if (gameObject.name == "Levels Menu")
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (PlayerPrefs.GetInt("CurrentLevel") >= i + 3)
                {
                    transform.GetChild(i).GetChild(2).gameObject.SetActive(false);
                }
            }
        }
    }

    public void StartGame()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(1);
    }
    public void SkipIntro()
    {
        SceneManager.LoadScene(2);


    }
    public void Menu()
    {
        SceneManager.LoadScene(0);

    }
    public void LoadSceneWithIndex(int index)
    {
        SceneManager.LoadScene(index);
    }
    public void Case(int index)
    {
        if (PlayerPrefs.GetInt("CurrentLevel") >= index)
            SceneManager.LoadScene(index);

    }

    public void Settings()
    {
        if (Buttons.activeInHierarchy)
        {
            Buttons.SetActive(false);
        }
        else if (!Buttons.activeInHierarchy)
        {
            Buttons.SetActive(true);

        }

        if (ButtonsFB.activeInHierarchy)
        {
            ButtonsFB.SetActive(false);
        }
        else if (!ButtonsFB.activeInHierarchy)
        {
            ButtonsFB.SetActive(true);

        }

    }
    public void QuitGame()
    {
        Application.Quit();

    }

    public void Repeat()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ButtonsMails(int index)
    {

        if (!MailsOpened[index].activeInHierarchy)
        {
            MailsOpened[index].SetActive(true);
            for (int i = 0; i < MailsOpened.Length; i++)
            {if (i!= index)
                MailsOpened[i].SetActive(false);
            }
        }
    }

    public void WrongRapist(GameObject WrongAnswer)
    {
        WrongAnswer.SetActive(true);
    }

    public void RightRapest(GameObject RightAnswer)
    {
        RightAnswer.SetActive(true);
    }



}
