using Core.GameManagment;
using UnityEngine;

public class ClientUI : MonoBehaviour
{
    [SerializeField] private GameObject _cursor;
    [SerializeField] private GameObject _pauseBackground;

    private ClientIO _clientIO;
    private GameState _gameState;

    public void Initialize(GameState gameState, ClientIO clientIO)
    {
        this._gameState = gameState;
        this._clientIO = clientIO;
    }

    private void Update()
    {
        _cursor.SetActive(_clientIO.IsFocused && _gameState.IsUnpause);
        _pauseBackground.SetActive(_gameState.IsPause);

        MouseController.SetVisibility(_gameState.IsPause);
    }
}
