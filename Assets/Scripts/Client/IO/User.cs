using Core.Player;
using Core.Car;

public class User
{
    private bool _canMove = true;

    public CarController CarController { get; set; }
    public PlayerController PlayerController { get; set; }


    public void SetMoveAbility(bool state)
    {
        _canMove = state;
    }

    public void CarControl(Core.Car.IControls controls)
    {
        if (CarController == null)
        {
            return;
        }

        CarController.IsAvailable = _canMove;
        CarController.Update(controls);
    }

    public void PlayerControl(Core.Player.IControls controls)
    {
        if(PlayerController == null)
        {
            return;
        }

        PlayerController.IsAvailable = _canMove;   
        PlayerController.Update(controls);
    }
}
