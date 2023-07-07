using Core.Raycasting;

public class InteractiveRaycast
{
    private readonly UserController _userController;
    private readonly Raycaster _raycaster;

    private IInteractive _interactive;

    public string Hint { get; private set; }
    public bool IsFocused { get; private set; }

    public InteractiveRaycast(Raycaster raycaster,
        UserController userController)
    {
        _userController = userController;
        _raycaster = raycaster;

        _interactive = null;
    }

    public void Update()
    {
        _interactive = CastInteractive();

        Hint = _interactive?.Hint ?? string.Empty;

        IsFocused = _interactive is not null;
    }

    public void TryInteract()
    {
        if (_interactive is null)
        {
            return;
        }

        _interactive.Interact(_userController);
    }

    private IInteractive CastInteractive()
    {
        var raycastHit = _raycaster.CheckHit<IInteractive>();

        if (!raycastHit?.IsInteractable(_userController) ?? false)
        {
            return null;
        }

        return raycastHit;
    }
}
