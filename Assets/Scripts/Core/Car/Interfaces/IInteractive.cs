namespace Core.Car
{
    public interface IInteractive
    {
        public bool IsInteractable { get; }
        public void Interact();
    }
}
