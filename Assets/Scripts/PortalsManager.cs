using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalsManager : MonoBehaviour
{
    public Camera cameraB;
    public Camera cameraA;
    public Camera cameraC;
    public Camera cameraD;
    public Camera cameraE;
    public Camera cameraF;
    public Camera cameraG;
    public Camera cameraH;


    public Material materialB;
    public Material materialA;
    public Material materialC;
    public Material materialD;
    public Material materialE;
    public Material materialF;
    public Material materialG;
    public Material materialH;
    // Start is called before the first frame update
    void Start()
    {
        if (cameraB.targetTexture != null)
        {
            cameraB.targetTexture.Release();
        }
        cameraB.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        materialB.mainTexture = cameraB.targetTexture;

        if (cameraA.targetTexture != null)
        {
            cameraA.targetTexture.Release();
        }
        cameraA.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        materialA.mainTexture = cameraA.targetTexture;

        if (cameraC.targetTexture != null)
        {
            cameraC.targetTexture.Release();
        }
        cameraC.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        materialC.mainTexture = cameraC.targetTexture;

        if (cameraD.targetTexture != null)
        {
            cameraD.targetTexture.Release();
        }
        cameraD.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        materialD.mainTexture = cameraD.targetTexture;

        if (cameraE.targetTexture != null)
        {
            cameraE.targetTexture.Release();
        }
        cameraE.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        materialE.mainTexture = cameraE.targetTexture;

        if (cameraF.targetTexture != null)
        {
            cameraF.targetTexture.Release();
        }
        cameraF.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        materialF.mainTexture = cameraF.targetTexture;

        if (cameraG.targetTexture != null)
        {
            cameraG.targetTexture.Release();
        }
        cameraG.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        materialG.mainTexture = cameraG.targetTexture;

        if (cameraH.targetTexture != null)
        {
            cameraH.targetTexture.Release();
        }
        cameraH.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        materialH.mainTexture = cameraH.targetTexture;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
