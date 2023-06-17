using UnityEngine;
using Core.Player;

public class Level : MonoBehaviour
{
    [SerializeField] private UserController _usercontroller;
    [SerializeField] private PlayerMovement _playerMovement;

    private void Awake()
    {
        _playerMovement.SetController(_usercontroller);
    }
}

