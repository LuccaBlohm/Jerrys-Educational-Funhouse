using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwitchManager : MonoBehaviour
{
    public delegate void StateChangeDelegate(bool a, bool b, bool c, bool d, bool e);
    public static event StateChangeDelegate OnStateChanged;

    public bool a = false;
    public bool b = false;
    public bool c = false;
    public bool d = false;
    public bool e = false;

    public GameObject FinalDoor;
    public GameObject FinalExit;

    // Start is called before the first frame update
    void Start()
    {
        FinalDoor.SetActive(a && b && c && d && e);
        OnStateChanged?.Invoke(a, b, c, d, e);
        FinalExit.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LeverFlip(string switchNumeral)
    {
        switch (switchNumeral)
        {
            case "I":
                a = !a;
                b = !b;
                // lights[0].SetActive(a); //setting first light based on bool a
                // lights[1].SetActive(b); //setting second light based on bool b
                //When implemented this would shift which doorframe the first/second door renders and teleports to
                //doors[0].State == a; 
                //doors[1].State == b; //same but for the second door
                break;

            case "II":
                c = !c;
                b = !b;
                // lights[2].SetActive(c); //setting third light based on bool c
                // lights[1].SetActive(b); //setting second light based on bool b
                //doors[2].State == c; 
                //doors[1].State == b; 
                break;

            case "III":
                c = !c;
                d = !d;
                // lights[2].SetActive(c); //setting third light based on bool c
                // lights[3].SetActive(d); //setting fourth light based on bool d
                //doors[2].State == c; 
                //doors[3].State == d; 
                break;

            case "IV":
                a = !a;
                c = !c;
                e = !e;
                // lights[2].SetActive(c); //setting first light based on bool a
                // lights[4].SetActive(e); //setting second light based on bool b
                //doors[2].State == c; 
                //doors[3].State == d; 
                break;
        }
        if (a && b && c && d && e)
        {
            FinalDoor.SetActive(true);
            StartCoroutine(WaitAndActivateExit());
        }
        OnStateChanged?.Invoke(a, b, c, d, e);
    }

    IEnumerator WaitAndActivateExit()
    {
        Debug.Log("Coroutine started!");
        yield return new WaitForSeconds(1.5f);
        FinalExit.SetActive(true);
    }
}
