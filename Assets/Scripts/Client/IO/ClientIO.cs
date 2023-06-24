using Core.Car;
using System.Collections.Generic;
using UnityEngine;

public class ClientIO : MonoBehaviour
{
    [SerializeField] private GameObject _cursor;
    [SerializeField] private UserCharacterController _userController;

    private List<ViewProbeHolder> _viewProbeHolders;
    private bool _isPause = false;

    private void Start()
    {
        _viewProbeHolders = new()
        {
            new ViewProbe<IFunctional>(
                _userController.CharacterBody.HeadTransform,
                3f, probe => probe.Interact(),
                probe => probe.IsInteractable),
            new ViewProbe<SeatController>(
                _userController.CharacterBody.HeadTransform,
                2f, probe => probe.Take(_userController),
                probe => probe.IsInteractable(_userController)),
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
            var mode = Input.GetKeyDown(KeyCode.E) ?
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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _isPause = !_isPause;

            MouseController.SetVisibility(_isPause);
            _userController.SetMoveAbility(!_isPause);
        }
    }
}
