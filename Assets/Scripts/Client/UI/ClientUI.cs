using Core.GameManagment;
using UnityEngine;

public class ClientUI : MonoBehaviour
{
    [SerializeField] private GameObject _cursor;
    [SerializeField] private GameObject _pauseBackground;

    private InteractiveRaycast _interactiveRaycast;
    private GameState _gameState;

    public void Initialize(GameState gameState,
        InteractiveRaycast interactiveRaycast)
    {
        this._gameState = gameState;
        this._interactiveRaycast = interactiveRaycast;
    }

    private void Update()
    {
        _cursor.SetActive(_interactiveRaycast.IsFocused
            && _gameState.IsUnpause);
        _pauseBackground.SetActive(_gameState.IsPause);

        MouseController.SetVisibility(_gameState.IsPause);
    }
}
