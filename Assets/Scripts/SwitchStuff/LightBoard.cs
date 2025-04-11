using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBoard : MonoBehaviour
{
    public GameObject[] lights; // Should be 5 GameObjects on each board corresponding to A–E

    void OnEnable()
    {
        SwitchManager.OnStateChanged += UpdateLights;
    }

    void OnDisable()
    {
        SwitchManager.OnStateChanged -= UpdateLights;
    }

    public void UpdateLights(bool a, bool b, bool c, bool d, bool e)
    {
        lights[0].SetActive(a);
        lights[1].SetActive(b);
        lights[2].SetActive(c);
        lights[3].SetActive(d);
        lights[4].SetActive(e);
    }
}
