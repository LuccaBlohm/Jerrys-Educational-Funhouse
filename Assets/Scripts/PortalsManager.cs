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
    public Camera cameraI;
    public Camera cameraJ;
    public Camera cameraK;
    public Camera camera1;
    public Camera camera2;
    public Camera camera3;  
    public Camera camera4;
    public Camera camera5;
    public Camera camera6;
    public Camera camera7;
    public Camera camera8;

    public Material materialB;
    public Material materialA;
    public Material materialC;
    public Material materialD;
    public Material materialE;
    public Material materialF;
    public Material materialG;
    public Material materialH;
    public Material materialI;
    public Material materialJ;
    public Material materialK;
    public Material material1;
    public Material material2;
    public Material material3;
    public Material material4;
    public Material material5;
    public Material material6;
    public Material material7;
    public Material material8;

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

        if (cameraI.targetTexture != null)
        {
            cameraI.targetTexture.Release();
        }
        cameraI.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        materialI.mainTexture = cameraI.targetTexture;

        if (cameraJ.targetTexture != null)
        {
            cameraJ.targetTexture.Release();
        }
        cameraJ.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        materialJ.mainTexture = cameraJ.targetTexture;

        if (cameraK.targetTexture != null)
        {
            cameraK.targetTexture.Release();
        }
        cameraK.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        materialK.mainTexture = cameraK.targetTexture;

        if (camera1.targetTexture != null)
        {
            camera1.targetTexture.Release();
        }
        camera1.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        material1.mainTexture = camera1.targetTexture;

        if (camera2.targetTexture != null)
        {
            camera2.targetTexture.Release();
        }
        camera2.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        material2.mainTexture = camera2.targetTexture;

        if (camera3.targetTexture != null)
        {
            camera3.targetTexture.Release();
        }
        camera3.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        material3.mainTexture = camera3.targetTexture;

        if (camera4.targetTexture != null)
        {
            camera4.targetTexture.Release();
        }
        camera4.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        material4.mainTexture = camera4.targetTexture;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
