using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIControls : MonoBehaviour
{
    public void RestartButton()
    {
        SceneManager.LoadScene("Base");
    }
}
