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
                3f, probe => probe.Interact()),
            new ViewProbe<Seat>(_userController.PlayerMovement, 
                1.5f, probe => probe.Take(_userController),
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
            if (holder.CheckCondition())
            {
                _cursor.SetActive(true);
            }
            if (Input.GetKeyUp(KeyCode.E))
            {
                holder.TakeProbeAndDoAction();
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
