using UnityEngine;

namespace Core.Car
{
    public class SignalLamp : MonoBehaviour
    {
        public bool IsActive {  get; private set; }

        public void SetActive (bool active)
        {
            if(IsActive == active)
            {
                return;
            }

            IsActive = active;
            // light active ? on : off
        }
    }
}