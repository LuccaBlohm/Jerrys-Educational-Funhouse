using UnityEngine;

public class ItemInteract : MonoBehaviour, IInteractable
{
    [SerializeField] private ItemSprite key;
    [SerializeField] private PlayerMovement player;
    [SerializeField] protected AudioSource _rejectSound;
    [SerializeField] protected AudioSource _acceptSound;

    public void OnInteract()
    {
        if (key == player.itemHeld)
        {
            if (_acceptSound != null)
            {
                _acceptSound.Play();
                _acceptSound = null;
                _rejectSound = null;
            }

            interactableBehavior();
        }
        else if(_rejectSound != null)
        {
            _rejectSound.Play();
        }
    }

    protected virtual void interactableBehavior()
    {
        Destroy(gameObject);
        Debug.Log("Interacted successfully");
    }

}