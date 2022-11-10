using System;
using TMPro;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(Animator))]
    public class FinishPanel : MonoBehaviour
    {
        private readonly int ShowKey = Animator.StringToHash("Show");
        private readonly int HideKey = Animator.StringToHash("Hide");

        [SerializeField] private TMP_Text _duration;
        [SerializeField] private TMP_Text _attempts;

        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void Show()
        {
            _animator.SetTrigger(ShowKey);
        }

        public void Hide()
        {
            _animator.SetTrigger(HideKey);
        }

        public void SetStatistic(int duration, int attempts)
        {
            var minutes = (duration / 60).ToString("00");
            var seconds = (duration % 60).ToString("00");

            _duration.text = $"{minutes}:{seconds}";
            _attempts.text = attempts.ToString();
        }
    }
}
