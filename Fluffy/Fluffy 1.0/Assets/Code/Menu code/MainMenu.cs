using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartScene ()
    {
        SceneManager.LoadScene(1);
    }

    public void StartGame ()
    {
        SceneManager.LoadScene(2);
    }

    public void QuitGame ()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}
