using Player;
using UI;
using System;
using UnityEngine;
using Road;

public class Level : MonoBehaviour
{
    private readonly string AttempsKey = "Attemps";

    #region Inspector Fields
    [Header("Controllers")]
    [SerializeField] private StartPanel _startPanel;
    [SerializeField] private FinishPanel _finishPanel;
    [SerializeField] private WallSpawner _wallSpawner;

    [Header("Ball parameters")]
    [SerializeField] private BallUpMover _ballPrefab;
    [SerializeField] private Transform _ballSpawnPoint;
    [SerializeField] private PressedButton _pressedButton;
    #endregion

    private BallCollisionTrigger _ballCollisionTrigger;
    private BallUpMover _ball;
    private float _deltaTime;

    private void Awake()
    {
        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        if(_ballCollisionTrigger) _ballCollisionTrigger.BallCollision -= FinishLevel;
    }

    public void StartLevel(bool isGetingDifficult = true)
    {
        Time.timeScale = 1;
        _deltaTime = Time.time;

        if(isGetingDifficult) _wallSpawner.SetStartSpeed(_startPanel.GetDifficult());
        _wallSpawner.StartSpawn();

        _ball = Instantiate(_ballPrefab, _ballSpawnPoint);
        _ball.SetPressedButton(_pressedButton);

        _ballCollisionTrigger = _ball.GetComponent<BallCollisionTrigger>();
        _ballCollisionTrigger.BallCollision += FinishLevel;

        if (!_ballCollisionTrigger) throw new NullReferenceException("Ball Collision Trigger is null");
    }

    public void RestartLevel()
    {
        StartLevel(false);
    }

    private void FinishLevel()
    {
        Destroy(_ball.gameObject);
        _wallSpawner.DestroyAllWalls();

        var attemps = PlayerPrefs.GetInt(AttempsKey);
        var leftTime = Time.time - _deltaTime;
        attemps++;

        PlayerPrefs.SetInt(AttempsKey, attemps);

        _finishPanel.Show();
        _finishPanel.SetStatistic((int)leftTime, attemps);

        Time.timeScale = 0;
    }
}
