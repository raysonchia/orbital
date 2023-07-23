using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseManager : Singleton<PauseManager>
{
    [SerializeField]
    private InputActionReference pauseInput;
    private GameObject pauseMenu;

    public bool isPaused = false;

    private void Start()
    {
        Hide();
    }

    private void Update()
    {
        if (pauseInput.action.WasPressedThisFrame())
        {
            if (!isPaused)
            {
                PauseGame();
            }
            else
            {
                UnpauseGame();
            }
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void UnpauseGame()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Hide()
    {
        if (pauseMenu == null)
        {
            pauseMenu = GameObject.FindWithTag("Pause");
        }
        pauseMenu.SetActive(false);
    }
}