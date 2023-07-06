public interface IInteractive
{
    public string Hint { get; }

    public bool IsInteractable (UserController userController);
    public void Interact(UserController userController);
}
