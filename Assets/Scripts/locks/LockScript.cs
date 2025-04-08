using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LockScript : WindowUIManager
{
    [SerializeField] private int[] num;

    [SerializeField] private bool[] isCorrect = { false, false, false };
    [SerializeField] private int[] correctNums;
    [SerializeField] private TMP_Text[] numTexts;

    private LockedDoor lockedDoor;

    private void Awake()
    {
        parent = gameObject.transform.parent.GetComponent<Canvas>();
        rectTransform = GetComponent<RectTransform>();
    }

    public override void ConnectToOrigin(GameObject origin)
    {
        base.ConnectToOrigin(origin);

        if (origin != null)
        {
            LockedDoor door = origin.GetComponent<LockedDoor>();

            if (door != null)
            {
                lockedDoor = door.GetComponent<LockedDoor>();
            }
        }
    }

    // allows locked door that spawned this pop up to access methods
    // also sets lock based on locked door
    public void PassLockCombo(int[] combo)
    {
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

        if (lockedDoor != null)
        {
            lockedDoor.ComboCorrect();
        }

    }

    void Update()
    {
        
    }
}
