using Core.GameManagment;
using UnityEngine;

public class ClientUI : MonoBehaviour
{
    [SerializeField] private GameObject _cursor;

    private ClientIO _clientIO;
    private GameState _gameState;

    public void Initialize(GameState _gameState, ClientIO clientIO)
    {
        this._gameState = _gameState;
        this._clientIO = clientIO;
    }

    private void Update()
    {
        _cursor.SetActive(_clientIO.IsFocused);

        MouseController.SetVisibility(_gameState.IsPause);
    }
}
