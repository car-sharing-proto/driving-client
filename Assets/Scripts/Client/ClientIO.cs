using Core.Car;
using UnityEngine;

public class ClientIO : MonoBehaviour
{
    [SerializeField] private Door _door;
    [SerializeField] private Transform _head;

    [SerializeField] private GameObject _cursor;

    private Raycaster _raycaster;

    private void Start()
    {
        _raycaster = new Raycaster(_head, 3f);

        MouseController.SetVisibility(false);
    }

    private void Update()
    {
        var interactable = _raycaster.CheckHit<IInteractable>();
        if (interactable != null && Input.GetKeyUp(KeyCode.E))
        {
            interactable.Interact();
        }

        _cursor.SetActive(interactable != null);

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            MouseController.SwitchState();
        }
    }
}
