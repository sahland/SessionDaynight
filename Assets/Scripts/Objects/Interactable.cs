using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius;
    public Transform interactionTransform;

    private bool _isFocus;
    private bool _hasInteracted;
    private Transform _player;

    public virtual void Interact()
    {
        //Данный метод необходим для переопределения
    }

    void Start()
    {
        radius = 3f;
        _isFocus = false;
        _hasInteracted = false;
    }

    void Update()
    {
       if (_isFocus && !_hasInteracted)
        {
            float distance = Vector3.Distance(_player.position, interactionTransform.position);
            if(distance <= radius)
            {
                Interact();
                _hasInteracted = true;
            }
        }
    }

    public void OnFocused(Transform playerTransform)
    {
        _isFocus = true;
        _player = playerTransform;
        _hasInteracted = false;
    }

    public void OnDefocused()
    {
        _isFocus = false;
        _player = null;
        _hasInteracted = false;
    }

    private void OnDrawGizmosSelected()
    {
        if (interactionTransform == null)
        {
            interactionTransform = transform;
        }

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }

}
