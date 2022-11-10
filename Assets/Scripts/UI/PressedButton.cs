using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UI
{
    public class PressedButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        #region Inspector Fields
        [SerializeField] private UnityEvent _buttonDown;
        [SerializeField] private UnityEvent _buttonUp;
        #endregion

        #region Events
        public event UnityAction ButtonDown
        {
            add => _buttonDown.AddListener(value);
            remove => _buttonDown.RemoveListener(value);
        }

        public event UnityAction ButtonUp
        {
            add => _buttonUp.AddListener(value);
            remove => _buttonUp.RemoveListener(value);
        }
        #endregion

        #region Functions
        public void OnPointerUp(PointerEventData eventData)
        {
            _buttonUp?.Invoke();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _buttonDown?.Invoke();
        }
        #endregion
    }
}
