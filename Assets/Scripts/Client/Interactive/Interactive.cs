using System;
using UnityEngine;

[RequireComponent(typeof(Core.Car.IInteractive))]
public class Interactive : MonoBehaviour, IInteractive
{
    [SerializeField] private string _hint;

    private Core.Car.IInteractive _interactive;

    public string Hint => _hint;

    private void Awake()
    {
        _interactive = GetComponent<Core.Car.IInteractive>();
    }

    public void Interact(UserController userController)
    {
        Debug.Log($"[{DateTime.Now}] -> {userController} interacts {this}.");

        _interactive.Interact();
    }

    public bool IsInteractable(UserController userController)
    {
        return _interactive.IsInteractable;
    }
}
