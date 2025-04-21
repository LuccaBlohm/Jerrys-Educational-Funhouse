using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalSwitcher : MonoBehaviour
{
    //connection A
    public GameObject ClassroomtoPiano;
    public GameObject ClassroomtoClassroom;
    //public GameObject PianotoClassroom; //this portal exists but never needs to be toggled, just always goes to classroom

    //connection B
    public GameObject BallroomtoClassroom;
    public GameObject BStatuetoClassroom; //one that would go to ballroom
    public GameObject BallroomtoStatue;
    public GameObject StatuetoBallroom;

    //Connection C
    public GameObject CStatuetoClassroom; //one that would go to piano
    public GameObject CPianotoClassroom;
    public GameObject PianotoStatue;
    public GameObject StatuetoPiano;

    //Connection D
    public GameObject PianotoAquarium;
    public GameObject AquariumtoPiano;
    public GameObject AquariumtoClassroom;
    public GameObject DPianotoClassroom;

    //Connection E
    public GameObject EStatuetoClassroom; //one that would go to infinite hallway
    public GameObject HallwaytoClassroom;
    public GameObject StatuetoHallway;
    public GameObject HallwaytoStatue;


    // Start is called before the first frame update
    void Start()
    {
        UpdatePortals(false, false, false, false, false);
    }
    void OnEnable()
    {
        SwitchManager.OnStateChangedPortals += UpdatePortals;
    }

    void OnDisable()
    {
        SwitchManager.OnStateChangedPortals -= UpdatePortals;
    }

    public void UpdatePortals(bool a, bool b, bool c, bool d, bool e)
    {
        Debug.Log("Updating Portals");
        //ClassroomtoClassroom.SetActive(!a);//note this goes from classroom portal1 to dump portal (classroom 2)
        ClassroomtoPiano.SetActive(a);//this goes from classroom portal1 to piano
        //PianotoClassroom.SetActive(a); //the piano portal to classroom just always goes to the same place in classroom

        //BallroomtoClassroom.SetActive(!b); Add later
        //BStatuetoClassroom.SetActive(!b);
        BallroomtoStatue.SetActive(b);
        Debug.Log("b is" + b);
        StatuetoBallroom.SetActive(b);

        PianotoStatue.SetActive(c);
        StatuetoPiano.SetActive(c);
        // CPianotoClassroom.SetActive(!c); Add Later
        // CStatuetoClassroom.SetActive(!c);

        PianotoAquarium.SetActive(d);
        AquariumtoPiano.SetActive(d);
        // AquariumtoClassroom.SetActive(!d); Add Later
        // DPianotoClassroom.SetActive(!d);

        StatuetoHallway.SetActive(e);
        HallwaytoStatue.SetActive(e);
        // HallwaytoClassroom.SetActive(!e); Add Later
        // EStatuetoClassroom.SetActive(!e);


    }
}
