using System;
using Core.Car;
using UnityEngine;

[RequireComponent(typeof(Seatable))]
public class DriverSeat : MonoBehaviour
{
    [SerializeField] private Car _car;

    private Seatable _seatable;

    private void Awake()
    {
        _seatable = GetComponent<Seatable>();

        _seatable.OnUserSitting += ProvideCarHandling;
        _seatable.OnUserLeaving += DepriveCarHandling;

    }

    private void OnDestroy()
    {
        _seatable.OnUserSitting -= ProvideCarHandling;
        _seatable.OnUserLeaving -= DepriveCarHandling;
    }

    private void ProvideCarHandling(UserController userController)
    {
        Debug.Log($"[{DateTime.Now}] -> {userController}" +
            $" has access to control {_car}");

        userController.CarController.SetCar(_car);
    }

    private void DepriveCarHandling(UserController userController)
    {
        Debug.Log($"[{DateTime.Now}] -> {userController}" +
            $" has lost to control {_car}");

        userController.CarController.RemoveCar();
    }

}
