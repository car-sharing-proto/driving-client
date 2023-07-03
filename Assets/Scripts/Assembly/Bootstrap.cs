using Core.Car;
using Core.Player;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private PlayerBody _playerBody;
    [SerializeField] private ClientUI _clientUI;
    [SerializeField] private ClientIO _clientIO;

    private UserController _userController;

    private void Awake()
    {
        _userController = new UserController(
            new CarController(_clientIO),
            new PlayerController(_clientIO)
            );
        _userController.PlayerController.SetPlayerBody(_playerBody);
        _userController.SetMoveAbility(true);

        _clientIO.Initialize(
            new Core.ViewProber.ViewProbeHolder[] {
                new Core.ViewProber.ViewProbe<IFunctional>(
                    _playerBody.HeadTransform,
                    3f, probe => probe.Interact(),
                    probe => probe.IsInteractable),
                new  Core.ViewProber.ViewProbe<SeatController>(
                    _playerBody.HeadTransform,
                    2f, probe => probe.Take(_userController),
                    probe => probe.IsInteractable(_userController))
            });

        _clientUI.Initialize(_clientIO);
    }

    private void Update()
    {
        // Update statements.
        _clientIO.Update();
        _userController.Update();

        // Update dependencies.
        _userController.SetMoveAbility(!_clientIO.IsPause);
    }
}
