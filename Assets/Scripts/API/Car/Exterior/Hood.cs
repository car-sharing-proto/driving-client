using UnityEngine;

namespace Core.Car
{
    public class Hood : Door
    {
        [SerializeField] private GameObject _engineCompartment;

        private void Update()
        {
            _engineCompartment.SetActive(State != IOpenable.OpenState.CLOSED);
        }
    }
}
