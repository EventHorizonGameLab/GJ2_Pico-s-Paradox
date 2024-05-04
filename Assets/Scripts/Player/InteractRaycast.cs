using UnityEngine;


public class InteractRaycast : MonoBehaviour
{

    private void OnEnable()
    {
        InputManager.ActionMap.Player.Interact.performed += RaycastStart;
    }

    private void OnDisable()
    {
        InputManager.ActionMap.Player.Interact.performed -= RaycastStart;
    }

    private void RaycastStart(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 1f);

        if (hit.transform.TryGetComponent<IInteractable>(out var interactable))
        {
            interactable.Interact();
        }
    }
}
