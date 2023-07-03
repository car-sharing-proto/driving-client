using UnityEngine;

public class ClientUI : MonoBehaviour
{
    [SerializeField] private GameObject _cursor;

    private ClientIO _clientIO;

    public void Initialize(ClientIO clientIO)
    {
        this._clientIO = clientIO;
    }

    private void Update()
    {
        _cursor.SetActive(_clientIO.IsFocused);

        MouseController.SetVisibility(_clientIO.IsPause);
    }
}
