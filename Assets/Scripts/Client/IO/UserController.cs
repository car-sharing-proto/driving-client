using Core.Player;
using Core.Car;
using Core.GameManagment;

public class UserController
{
    public CarController CarController { get; private set; }
    public PlayerController PlayerController { get; private set; }

    private readonly GameState _gameState;

    public UserController(GameState gameState,
        CarController carController,
        PlayerController playerController)
    {
        CarController = carController;
        PlayerController = playerController;

        this._gameState = gameState;
    }

    public void SetMoveAbility(bool state)
    {
        CarController.IsAvailable = state;
        PlayerController.IsAvailable = state;
    }

    public void Update()
    {
        SetMoveAbility(_gameState.IsUnpause);

        CarController.Update();
        PlayerController.Update();
    }
}
