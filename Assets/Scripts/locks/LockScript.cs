using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LockScript : MonoBehaviour
{
    [SerializeField] private int[] num;

    [SerializeField] private int[] correctNums;
    [SerializeField] private TMP_Text[] numTexts;

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


    void Update()
    {
        
    }
}
