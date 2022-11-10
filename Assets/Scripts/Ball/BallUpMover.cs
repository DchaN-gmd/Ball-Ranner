using UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class BallUpMover : MonoBehaviour
    {
        #region Inspector Fields
        [Header("Parameters")]
        [SerializeField] private float _speed;
        [SerializeField] private Button button;

        [Header("Events")]
        [SerializeField] private UnityEvent _onStartedMove;
        [SerializeField] private UnityEvent _onStoppedMove;
        #endregion

        #region Fields
        private Rigidbody _rigidbody;
        private PressedButton _pressedButton;
        private bool _isMove;
        #endregion

        #region Events
        public event UnityAction OnStartedMove
        {
            add => _onStartedMove.AddListener(value);
            remove => _onStartedMove.RemoveListener(value);
        }

        public event UnityAction OnStoppedMove
        {
            add => _onStoppedMove.AddListener(value);
            remove => _onStoppedMove.RemoveListener(value);
        }
        #endregion

        #region Unity Functions
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnDisable()
        {
            if (_pressedButton)
            {
                _pressedButton.ButtonDown -= OnButtonDown;
                _pressedButton.ButtonUp -= OnButtonUp;
            }
        }

        private void Update()
        {
            if (_isMove)
            {
                MoveUp();
            }
        }
        #endregion

        #region Public Functions
        public void MoveUp()
        {
            _rigidbody.AddForce(Vector3.up * _speed * Time.deltaTime, ForceMode.Impulse);
        }

        public void SetPressedButton(PressedButton button)
        {
            _pressedButton = button;

            _pressedButton.ButtonDown += OnButtonDown;
            _pressedButton.ButtonUp += OnButtonUp;
        }
        #endregion

        #region Private Functions
        private void OnButtonDown()
        {
            _isMove = true;
        }

        private void OnButtonUp()
        {
            _isMove = false;
        }
        #endregion
    }
}
