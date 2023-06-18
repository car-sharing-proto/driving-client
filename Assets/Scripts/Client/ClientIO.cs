using Core.Car;
using System.Collections.Generic;
using UnityEngine;

public class ClientIO : MonoBehaviour
{
    [SerializeField] private Door _door;
    [SerializeField] private GameObject _cursor;
    [SerializeField] private UserController _userController;

    private List<ViewProbeHolder> _viewProbeHolders;
    private bool _isPause = false;

    private void Start()
    {
        _viewProbeHolders = new()
        {
            new ViewProbe<IFunctional>(_userController.PlayerMovement, 
                3f, probe => probe.Interact(),
                (probe, player) => probe.IsInteractable),
            new ViewProbe<Seat>(_userController.PlayerMovement, 
                2f, probe => probe.Take(_userController),
                (probe, player) => !probe.IsTaken && !player.IsSitting),
        };

        MouseController.SetVisibility(false);
    }

    private void Update()
    {
        CheckViewProbes();
        CheckPauseSwitch();
    }

    private void CheckViewProbes()
    {
        _cursor.SetActive(false);

        foreach (var holder in _viewProbeHolders)
        {
            var mode = Input.GetKeyUp(KeyCode.E) ? 
                ViewProbeHolder.QueryMode.INTERACT :
                ViewProbeHolder.QueryMode.CHECK;

            if (holder.CheckCondition(mode))
            {
                _cursor.SetActive(true);
            }
        }
    }

    private void CheckPauseSwitch()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            _isPause = !_isPause;

            MouseController.SetVisibility(_isPause);
            _userController.SetMoveAbility(!_isPause);
        }
    }
}
