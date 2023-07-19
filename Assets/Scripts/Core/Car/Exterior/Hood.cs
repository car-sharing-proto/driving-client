using UnityEngine;
using System.Collections.Generic;

namespace Core.Car
{
    public class Hood : Door
    {
        [SerializeField] private List<GameObject> _engineCompartments;

        private void Update()
        {
            _engineCompartments.ForEach(item => 
                item.SetActive(State != IOpenable.OpenState.CLOSED));
            _collider.enabled = IsInteractable;
        }
    }
}
