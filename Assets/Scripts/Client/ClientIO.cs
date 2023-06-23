using Core.Car;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClientIO : MonoBehaviour
{
    [SerializeField] private GameObject _cursor;
    [SerializeField] private UserCharacterController _userController;

    private List<ViewProbeHolder> _viewProbeHolders;
    private bool _isPause = false;

    //test
    [SerializeField] private Text _speed;
    [SerializeField] private Car _car;
    private readonly SmoothPressing gasSmoothPressing = new(0.7f, 0.5f);
    private readonly SmoothPressing breakSmoothPressing = new(1f, 2.0f);

    private void Start()
    {
        _viewProbeHolders = new()
        {
            new ViewProbe<IFunctional>(
                _userController.CharacterBody.HeadTransform, 
                3f, probe => probe.Interact(),
                probe => probe.IsInteractable),
            new ViewProbe<Core.Character.SeatPlace>(
                _userController.CharacterBody.HeadTransform, 
                2f, probe => probe.Take(_userController.CharacterBody),
                probe => probe.IsInteractable(_userController.CharacterBody)),
        };

        MouseController.SetVisibility(false);
    }

    private void Update()
    {
        CheckViewProbes();
        CheckPauseSwitch();

        // test

        _speed.text = $"{(int)(_car.GetVelocity() * 3.6f)} km/h \n" +
            $"{_car.Transmission.CurrentGear + 1}";
    
        if(Input.GetKey(KeyCode.W))
        {
            gasSmoothPressing.Press();
        }
        else
        {
            gasSmoothPressing.Release();
        }

        if (Input.GetKey(KeyCode.Q))
        {
            breakSmoothPressing.Press();
        }
        else
        {
            breakSmoothPressing.Release();
        }

        gasSmoothPressing.FullPush =
            breakSmoothPressing.FullPush =
            Input.GetKey(KeyCode.LeftControl);

        _car.GasPedal.Value = gasSmoothPressing.Value;
        _car.BreakPedal.Value = breakSmoothPressing.Value;
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
