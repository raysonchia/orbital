using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadTestDungeonGenerator()
    {
        SceneManager.LoadScene(3);
    }

    public void LoadTestEnemy()
    {
        SceneManager.LoadScene(2);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

}
