using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void GoHome()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadLevel(int ind)
    {
        SceneManager.LoadScene(ind);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void OpenURL(string url)
    {
        Application.OpenURL(url);
    }
}
