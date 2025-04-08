using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }

    public void ResumeGame()
    {
        PlayerMovement.GamePaused = false;
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
