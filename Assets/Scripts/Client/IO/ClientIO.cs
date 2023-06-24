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
    private readonly SmoothPressing breakSmoothPressing = new(1f, 5.0f);

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

        // test

        _speed.text = $"{(int)(_car.GetVelocity() * 3.6f)} km/h \n" +
            $"{_car.Transmission.CurrentGear + 1}";

        // temporary testing, I promise
        var inCar = _userController.CarController != null;
        if (inCar) 
        {
            if (Input.GetKey(KeyCode.W))
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

            if (Input.GetKeyDown(KeyCode.O))
            {
                _userController.CarController.SetDrivingMode();
            }

            if (Input.GetKeyDown(KeyCode.I))
            {
                _userController.CarController.SetReverseMode();
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                _userController.CarController.SetParkingMode();
            }

            if (Input.GetKey(KeyCode.A))
            {
                _userController.CarController.SteerLeft(Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.D))
            {
                _userController.CarController.SteerRight(Time.deltaTime);
            }

            if (Input.GetKeyDown(KeyCode.T))
            {
                _userController.CarController.EngineSwitch();
            }

            gasSmoothPressing.FullPush =
                breakSmoothPressing.FullPush =
                Input.GetKey(KeyCode.LeftControl);

            _userController.CarController.GasPedalPress(gasSmoothPressing.Value);
            _userController.CarController.BreakPedalPress(breakSmoothPressing.Value);
        }

       
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
