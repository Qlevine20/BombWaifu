using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    void Start()
    {
        MusicManager.instance.PlayMainMenuMusic();
    }

    public void StartButton()
    {
        MusicManager.instance.PlayGameMusic();
        SceneManager.LoadScene("Michael");
    }

    public void CreditsButton()
    {
        SceneManager.LoadScene("Credits");
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}