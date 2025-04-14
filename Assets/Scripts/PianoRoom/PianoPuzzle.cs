using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoPuzzle : MonoBehaviour
{
    public List<string> puzzleWords = new List<string> { "BED", "FACE", "CAGE", "DEAD" };
    public List<GameObject> spotlights;
    private int currentRound = 0;
    private string currentInput = "";

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ActivateSpotlights());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RegisterKeyPress(string key)
    {
        currentInput += key.ToUpper();

        if (currentInput.Length >= puzzleWords[currentRound].Length)
        {
            string lastFew = currentInput.Substring(currentInput.Length - puzzleWords[currentRound].Length);
            if (lastFew == puzzleWords[currentRound])
            {
                currentRound++;
                if (currentRound < puzzleWords.Count)
                {
                    currentInput = "";
                    StartCoroutine(ActivateSpotlights());
                }
                else
                {
                    // Code to trigger lever wall and spotlights rotation
                }
            }
        }
    }

    IEnumerator ActivateSpotlights()
    {
        yield return new WaitForSeconds(1f);

        for (int i = 0; i < 3; i++)
        {
            spotlights[i].SetActive(false);
        }

        yield return new WaitForSeconds(1.5f);
        spotlights[currentRound].SetActive(true);
    }
}
