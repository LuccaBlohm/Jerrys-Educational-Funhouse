using UnityEngine;
using UnityEngine.UI;

public class Wire : MonoBehaviour
{
    [SerializeField] private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }
    
    public void SetColor(Color color)
    {
        _image.color = color;
    }
}