using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public void StartGame()
    {
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
    public void Case1()
    {
        SceneManager.LoadScene(3);

    }
}
