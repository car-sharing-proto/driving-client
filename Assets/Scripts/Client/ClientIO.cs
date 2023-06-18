using Core.Car;
using UnityEngine;

public class ClientIO : MonoBehaviour
{
    [SerializeField] private Door _door;
    [SerializeField] private Transform _head;
    [SerializeField] private GameObject _cursor;
    [SerializeField] private UserController _userController;

    private Raycaster _raycaster;

    private bool _isPause = false;

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

        var seat = _raycaster.CheckHit<Seat>();
        if (seat != null && !seat.IsTaken && Input.GetKeyUp(KeyCode.F))
        {
            seat.Take(_userController);
        }

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            _isPause = !_isPause;

            MouseController.SetVisibility(_isPause);
            _userController.SetMoveAbility(!_isPause);
        }
    }
}
