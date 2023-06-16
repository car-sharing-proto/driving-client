using UnityEngine;

public class ClientIO : MonoBehaviour
{
    [SerializeField] private Door _door;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            _door.Interact();
        }
    }
}
