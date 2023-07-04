using Core.Car;
using Core.GameManagment;
using Core.Player;
using Core.ViewProber;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private PlayerBody _playerBody;
    [SerializeField] private ClientUI _clientUI;
    [SerializeField] private ClientIO _clientIO;
    [SerializeField] private GameState _gameState;

    private UserController _userController;

    private void Awake()
    {
        // User Controller data.
        var carController = new CarController(_clientIO);
        var playerController = new PlayerController(_clientIO);

        // Client IO data.
        var rayCaster = new Raycaster(_playerBody.HeadTransform, 3f);
        var viewProbes = new ViewProbeHolder[]
        {
            // Check for interaction with some functional.
            new ViewProbe<IFunctional>(rayCaster,
                probe => probe.Interact(),
                probe => probe.IsInteractable),

            // Check for ability to sit in a seat.
            new ViewProbe<SeatController>(rayCaster,
                probe => probe.Take(_userController),
                probe => probe.IsInteractable(_userController))
        };

        // Game state set up.
        _gameState = new GameState();

        // User controller set up.
        _userController = new UserController(carController, playerController);
        _userController.PlayerController.SetPlayerBody(_playerBody);
        _userController.SetMoveAbility(true);

        // Client IO set up.
        _clientIO.Initialize(_gameState, viewProbes);
        _clientUI.Initialize(_gameState, _clientIO);
    }

    private void Update()
    {
        // Update statements.
        _clientIO.Update();
        _userController.Update();

        // Update dependencies.
        _userController.SetMoveAbility(_gameState.IsUnpause);
    }
}
