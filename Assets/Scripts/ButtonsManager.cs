using System.Collections;
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

    [SerializeField] private AudioManager _aM;

    public void StartGame()
    {
        StartCoroutine(ScaryTitle());
        _aM.audioSource.clip = null;
    }

    public void QuitGame()
    {
        Application.Quit();
        print("Quit Game");
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(1);
        print("Reseted Game");
    }

    public void MainScene()
    {
        SceneManager.LoadScene(0);
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