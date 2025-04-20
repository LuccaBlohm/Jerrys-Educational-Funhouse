using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwitchManager : MonoBehaviour
{
    public delegate void StateChangeDelegate(bool a, bool b, bool c, bool d, bool e);
    public static event StateChangeDelegate OnStateChanged;

    public delegate void StateChangeDelegatePortals(bool a, bool b, bool c, bool d, bool e);
    public static event StateChangeDelegate OnStateChangedPortals;

    public bool a = false;
    public bool b = false;
    public bool c = false;
    public bool d = false;
    public bool e = false;

    public GameObject FinalDoor;


    // Start is called before the first frame update
    void Start()
    {
        FinalDoor.SetActive(a && b && c && d && e);
        OnStateChanged?.Invoke(a, b, c, d, e); //calls event to swap lights and doors to default position because all is 0
    }

    void Update()
    {
        if (a && b && c && d && e) //only here for debugging, normally done from leverflip
        {
            FinalDoor.SetActive(true);
            StartCoroutine(DelayedRefresh(FinalDoor));
        }
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
            StartCoroutine(DelayedRefresh(FinalDoor));
        }
        OnStateChanged?.Invoke(a, b, c, d, e);
        OnStateChangedPortals?.Invoke(a, b, c, d, e);
    }

    IEnumerator DelayedRefresh(GameObject obj)
    {
        yield return new WaitForSeconds(1f); //wait for a whole second to allow refresh; //waiting for fixed update wasnt long enough

        foreach (var col in obj.GetComponentsInChildren<Collider>(true))
        {
            RefreshCollider(col);
        }
    }

    void RefreshCollider(Collider col)  //forcing unity to recalculate the collider's physics because apparently its lazy and re-enabling doesnt change anything
    // So as a i understand it, a tree is used for raycasts, lost colliders are removed from the tree but not re-added even when enabled until we do this
    {
        // Forces an actual transform update Unity can't optimize away
        col.transform.localScale *= 1.0001f;
        col.transform.localScale *= 0.9999f;

        // Temporarily disables/enables again for good measure
        col.enabled = false;
        col.enabled = true;
    }

}