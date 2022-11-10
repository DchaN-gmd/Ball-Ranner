using UnityEngine;

namespace UI
{
    public class Difficult : MonoBehaviour
    {
        [SerializeField] [Min(0)] private float _wallSpeed;

        public float WallSpeed => _wallSpeed;
    }
}
