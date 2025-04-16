using UnityEngine;

public class Port : MonoBehaviour
{
    public MatchEntity _ownerMatchEntity;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out MovablePair CollidedMovable))
        {
            _ownerMatchEntity.PairObjectInteraction(IsEnter: true, CollidedMovable);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out MovablePair CollidedMovable))
        {
            _ownerMatchEntity.PairObjectInteraction(IsEnter: false, CollidedMovable);
        }
    }
}