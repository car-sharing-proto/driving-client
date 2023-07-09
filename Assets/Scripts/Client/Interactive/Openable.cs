using System;
using UnityEngine;

[RequireComponent(typeof(Core.Car.IOpenable))]
public class Openable : MonoBehaviour, IInteractive
{
    [SerializeField] private string _openHintText;
    [SerializeField] private string _closeHintText;

    private Core.Car.IOpenable _interactive;

    public string Hint
    {
        get => _interactive.State switch
        {
            Core.Car.IOpenable.OpenState.CLOSED => _openHintText,
            Core.Car.IOpenable.OpenState.OPEN => _closeHintText,
            _ => string.Empty
        };
    }

    private void Awake()
    {
        _interactive = GetComponent<Core.Car.IOpenable>();
    }

    public bool IsInteractable(UserController userController)
    {
        return _interactive.IsInteractable;
    }

    public void Interact(UserController userController)
    {
        Debug.Log($"[{DateTime.Now}] -> {userController} opens {this}.");

        _interactive.Interact
            ();
    }
}
