using UnityEngine;

public class FallReset : MonoBehaviour
{
    private Vector3 startPosition = new Vector3(-3462, -1, 1934);
    public float fallThresholdY = -10f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (transform.position.y < fallThresholdY)
        {
            ResetPlayer();
        }
    }

    void ResetPlayer()
    {
        transform.position = startPosition;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        Debug.Log("Player reset to classroom.");
    }
}
