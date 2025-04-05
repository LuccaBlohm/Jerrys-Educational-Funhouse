using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LockScript : MonoBehaviour
{
    [SerializeField] private int[] num;

    [SerializeField] private int[] correctNums;
    [SerializeField] private TMP_Text[] numTexts;
    [SerializeField] private bool[] isCorrect = { false, false, false };
    [SerializeField] private bool isunlocked;

    void Start()
    {

    }

    public void IncreaseNum(int x)
    {
        if (num[x] >= 0 && num[x] < 9)
        {
            num[x]++;
            ChangeText(x);
        }
    }

    public void DecreaseNum(int x)
    {
        if (num[x] > 0 && num[x] <= 9)
        {
            num[x]--;
            ChangeText(x);
        }
    }

    private void ChangeText(int x)
    {
        numTexts[x].text = num[x].ToString();
    }

    public void CheckLock()
    {
        Debug.Log(num.Length);
        for (int i = 0; i <= num.Length - 1 ; i++)
        {
            if (num[i] == correctNums[i])
            {
                isCorrect[i] = true;
            }
            else
            {
                isCorrect[i] = false;
            }
        }
    }


    void Update()
    {
        
    }
}
