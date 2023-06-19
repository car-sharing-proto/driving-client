using UnityEngine;

// Just in case I decide composition is better than implementing an interface.
namespace Core.Character
{
    [RequireComponent(typeof(CharacterController))]
    public class Sitable : MonoBehaviour
    {
        private CharacterController _characterController;

        public bool IsSitting { get; set; }

        private void Start()
        {
            _characterController = GetComponent<CharacterController>();
        }

        public void SitDown(Transform placePoint)
        {
            IsSitting = true;

            SetParent(placePoint);
        }

        public void StandUp(Transform leavePoint)
        {
            RemoveParent();
            Translate(leavePoint.position);

            IsSitting = false;
        }

        public void Translate(Vector3 position)
        {
            _characterController.enabled = false;
            transform.position = position;
            _characterController.enabled = true;
        }

        private void SetParent(Transform parentTransform)
        {
            transform.SetParent(parentTransform);
            transform.localPosition = Vector3.zero;
        }

        private void RemoveParent()
        {
            transform.SetParent(null);
        }
    }
}