using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDoorCrash : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(2);
        Cursor.lockState = CursorLockMode.Confined;
    }
}
