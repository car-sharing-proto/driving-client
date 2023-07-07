using Core.GameManagment;
using TMPro;
using UnityEngine;

public class ClientUI : MonoBehaviour
{
    [SerializeField] private GameObject _cursor;
    [SerializeField] private GameObject _pauseBackground;
    [SerializeField] private TextMeshProUGUI _hintKey;
    [SerializeField] private TextMeshProUGUI _hintText;

    private InteractiveRaycast _interactiveRaycast;
    private GameState _gameState;
    // TODO: fix this.
    private readonly string _key = "E";

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

        _hintKey.text = $"[{_key}]";
        _hintText.text = _interactiveRaycast.Hint;

        MouseController.SetVisibility(_gameState.IsPause);
    }
}
