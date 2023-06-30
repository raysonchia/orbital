using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIControls : MonoBehaviour
{
    public void RestartButton()
    {
        PauseManager.Instance.UnpauseGame();
        SceneManager.LoadScene("Base");
    }

    public void ResumeButton()
    {
        PauseManager.Instance.UnpauseGame();
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
