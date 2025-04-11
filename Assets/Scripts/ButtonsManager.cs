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

    [SerializeField] private GameObject[] scaryButtons;
    [SerializeField] private GameObject[] buttons;
    [SerializeField] private GameObject scaryTitle;
    [SerializeField] private GameObject title;
    [SerializeField] private GameObject scaryBackground;
    [SerializeField] private GameObject jerry;
    [SerializeField] private GameObject scerry;

    public void StartGame()
    {
        StartCoroutine(ScaryTitle());
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

    IEnumerator ScaryTitle()
    {
        for (int l = 0; l < buttons.Length; l++)
        {
            buttons[l].SetActive(false);
        }
        for (int i = 0; i < scaryButtons.Length; i++)
        {
            scaryButtons[i].SetActive(true); 
        }
        title.SetActive(false);
        scaryTitle.SetActive(true);
        scaryBackground.SetActive(true);
        jerry.SetActive(false);
        scerry.SetActive(true);
        yield return new WaitForSeconds(0.07f);
        SceneManager.LoadScene(1);
    }
}
