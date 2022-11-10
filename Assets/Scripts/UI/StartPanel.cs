using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Animator))]
    public class StartPanel : MonoBehaviour
    {
        private readonly int HideKey = Animator.StringToHash("Hide");
        private readonly int ShowKey = Animator.StringToHash("Idle");

        [SerializeField] private ToggleGroup _toggleGroup;
        [SerializeField] private Button _startButton;

        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void Hide()
        {
            _animator.SetTrigger(HideKey);
            _startButton.enabled = false;
        }

        public void Show()
        {
            _animator.SetTrigger(ShowKey);
            _startButton.enabled = true;
        }

        public float GetDifficult()
        {
            var difficult = _toggleGroup.GetFirstActiveToggle();

            return difficult.GetComponent<Difficult>().WallSpeed;
        }
    }
}
