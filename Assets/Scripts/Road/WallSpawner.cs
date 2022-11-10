using UnityEngine;
using TimeSystem;
using System;
using System.Collections.Generic;

namespace Road
{
    public class WallSpawner : MonoBehaviour
    {
        #region Inspector Fields
        [Header("Parameters")]
        [SerializeField] private WallMover _wall;
        [SerializeField] private bool _startSpawnOnStart;

        [Header("Spawn Parameters")]
        [SerializeField] private Transform[] _spawnPoints;
        [SerializeField] private float _timeToSpawnNewWall;
        [SerializeField] private float _timeToDestroyWall;

        [Header("Speed Parameters")]
        [SerializeField] [Min(0)] private float _startWallSpeed;
        [SerializeField] [Min(0)] private float _speedIncreaseSize;
        [SerializeField] [Min(0)] private float _timeToIncreaseSpeed;

        [Header("Timers")]
        [SerializeField] private Timer _timerForIncreaseSpeed;
        #endregion

        #region Fields
        private float _wallSpeed;

        private List<WallMover> _walls = new List<WallMover>();
        #endregion

        #region Unity Functions
        private void Awake()
        {
            _wallSpeed = _startWallSpeed;
        }

        private void OnEnable()
        {
            _timerForIncreaseSpeed.Stopped += IncreaseSpeed;
        }

        private void OnDisable()
        {
            _timerForIncreaseSpeed.Stopped -= IncreaseSpeed;
        }

        private void Start()
        {
            if (_startSpawnOnStart) StartSpawn();
        }
        #endregion

        #region Puplic Functions
        public void SetStartSpeed(float speed)
        {
            if (speed <= 0) throw new ArgumentException("Speed can't be less then 0");

            _wallSpeed = speed;
        }

        public void DestroyAllWalls()
        {
            foreach (var wall in _walls)
            {
                Destroy(wall.gameObject);
            }

            _walls.Clear();
        }

        public void StartSpawn()
        {
            _timerForIncreaseSpeed.Play(_timeToIncreaseSpeed);
            SpawnWall();
        }
        #endregion

        #region Private Functions
        private void SpawnWall()
        {
            var spawnPoint = _spawnPoints[UnityEngine.Random.Range(0, _spawnPoints.Length)];

            var wall = Instantiate(_wall, spawnPoint.position, Quaternion.identity);
            wall.SetMoveSpeed(_wallSpeed);
            wall.DistanceCovered += OnWallDistanceCovered;
            _walls.Add(wall);

            Destroy(wall.gameObject, _timeToDestroyWall);
        }

        private void IncreaseSpeed()
        {
            _wallSpeed += _speedIncreaseSize;
        }

        private void OnWallDistanceCovered(WallMover wall)
        {
            SpawnWall();
            wall.DistanceCovered -= OnWallDistanceCovered;
        }
        #endregion

        
    }
}
