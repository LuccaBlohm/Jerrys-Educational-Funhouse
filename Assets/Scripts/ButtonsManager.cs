using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsManager : MonoBehaviour
{
    [SerializeField] private GameObject mainScene;
    [SerializeField] private GameObject creditsScene;
    [SerializeField] private GameObject howToPlayScene;

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Credits()
    {
        mainScene.SetActive(false);
        howToPlayScene.SetActive(false);
        creditsScene.SetActive(true);
    }

    public void HowToPlay()
    {
        mainScene.SetActive(false);
        howToPlayScene.SetActive(true);
        creditsScene.SetActive(false);
    }

    public void Back()
    {
        mainScene.SetActive(true);
        howToPlayScene.SetActive(false);
        creditsScene.SetActive(false);
    }
}
