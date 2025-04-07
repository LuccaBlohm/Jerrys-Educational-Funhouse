using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LockScript : MonoBehaviour
{
    [SerializeField] private int[] num;

    [SerializeField] private bool[] isCorrect = { false, false, false };
    [SerializeField] private int[] correctNums;
    [SerializeField] private TMP_Text[] numTexts;

    private LockedDoor lockedDoor;

    void Start()
    {

    }

    // allows locked door that spawned this pop up to access methods
    // also sets lock based on locked door
    public void connectDoor(LockedDoor door, int[] combo)
    {
        lockedDoor = door;
        correctNums = combo;
    }

    public void IncreaseNum(int x)
    {
        if (num[x] >= 0 && num[x] < 9)
        {
            num[x]++;
            ChangeText(x);
        }
        else
        {
            num[x] = 0;
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
        else
        {
            num[x] = 9;
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
        for (int i = 0; i <= num.Length - 1; i++)
        {
            if (num[i] == correctNums[i])
            {
                isCorrect[i] = true;
            }
            else
            {
                isCorrect[i] = false;
                return;
            }
        }

        Destroy(lockedDoor.gameObject);
    }

    private void OnDestroy()
    {
        if(lockedDoor != null)
        {
            lockedDoor.disconnectPopUp();
        }

    }

    void Update()
    {
        
    }
}
