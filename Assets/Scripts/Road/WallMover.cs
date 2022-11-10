using UnityEngine;
using System;
using UnityEngine.Events;

namespace Road
{
    public class WallMover : MonoBehaviour
    {
        #region Inspector Fields
        [Header("Parameters")]
        [SerializeField] private float _speed;
        [SerializeField] private float _distanceToCall;

        [Header("Events")]
        [SerializeField] private UnityEvent<WallMover> _distanceCovered;
        #endregion

        #region Fields
        private Transform _transform;
        private float _startPositionZ;
        private bool _isDistanceCovered = false;
        #endregion

        #region Events
        public event UnityAction<WallMover> DistanceCovered
        {
            add => _distanceCovered.AddListener(value);
            remove => _distanceCovered.RemoveListener(value);
        }
        #endregion

        #region Unity Functions
        private void Awake()
        {
            _transform = transform;
            _startPositionZ = _transform.position.z;
        }

        private void Update()
        {
            _transform.Translate(Vector3.forward * -_speed * Time.deltaTime);

            if(_distanceToCall > _transform.position.z && !_isDistanceCovered)
            {
                _distanceCovered?.Invoke(this);
                _isDistanceCovered = true;
            }
        }
        #endregion

        public void SetMoveSpeed(float speed)
        {
            if (speed <= 0) throw new ArgumentException("Speed can't be less then 0");

            _speed = speed;
        }
    }
}
