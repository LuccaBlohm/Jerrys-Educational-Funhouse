using UnityEngine;

public class lr_testing : MonoBehaviour
{
    [SerializeField] private Transform[] points;
    [SerializeField] private LineController line;
    void Start()
    {
        line.SetUpLine(points);
    }
}