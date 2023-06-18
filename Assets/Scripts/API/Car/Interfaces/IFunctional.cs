using UnityEngine;

namespace Core.Car
{
    public interface IFunctional
    {
        public bool IsInteractable { get; }
        public void Interact();
    }
}
