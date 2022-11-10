using Road;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    [RequireComponent(typeof(Collider))]
    public class BallCollisionTrigger : MonoBehaviour
    {
        [SerializeField] private UnityEvent _ballCollision;

        public event UnityAction BallCollision
        {
            add => _ballCollision.AddListener(value);
            remove => _ballCollision.RemoveListener(value);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.TryGetComponent(out WallMover wall))
            {
                _ballCollision?.Invoke();
            }
        }
    }
}
