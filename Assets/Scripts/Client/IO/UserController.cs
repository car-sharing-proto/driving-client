using Core.Player;
using Core.Car;

public class UserController
{
    public CarController CarController { get; set; }
    public PlayerController PlayerController { get; set; }

    public UserController (
        CarController carController,
        PlayerController playerController)
    {
        CarController = carController;
        PlayerController = playerController;
    }

    public void SetMoveAbility(bool state)
    {
        CarController.IsAvailable = state;
        PlayerController.IsAvailable = state;
    }

    public void Update()
    {
        CarController.Update();
        PlayerController.Update();
    }
}
