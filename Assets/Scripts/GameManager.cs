using System;
using UnityEngine;
using Zenject;

public enum Difficulty
{
    Easy,
    Medium,
    Hard
}
public class GameManager : IInitializable, IDisposable
{
    [Inject] private WallsManager _wallsManager;
    [Inject] private PlayerBall _playerBall;

    private DateTime _startTime;

    public Difficulty currentDifficulty = Difficulty.Medium;
    public int totalAttempts;
    public TimeSpan GameTime
    {
        get
        {
            return DateTime.Now - _startTime;
        }
    }

    public event Action OnGameStart = delegate { };
    public event Action OnGameOver = delegate { };

    public void Initialize()
    {
        _wallsManager.SetupWalls();
        totalAttempts = PlayerPrefs.GetInt("totalAttempts");
        _playerBall.OnHitWall += GameOver;
    }
    public void Dispose()
    {
        _playerBall.OnHitWall -= GameOver;
    }

    public void StartGame()
    {
        _playerBall.ResetBall();
        totalAttempts++;
        PlayerPrefs.SetInt("totalAttempts", totalAttempts);
        _startTime = DateTime.Now;
        OnGameStart.Invoke();
        _wallsManager.StartMoving();
        _playerBall.StartMoving(currentDifficulty);
    }

    private void GameOver()
    {
        OnGameOver.Invoke();
        _wallsManager.StopMoving();
        _playerBall.StopMoving();
    }

    ///<param name="direction">(1) - up, (-1) - down</param>
    public void SetVerticalDirection(int direction)
    {
        _playerBall.verticalDirection = direction;
    }

    public void SetDifficulty(Difficulty difficulty)
    {
        currentDifficulty = difficulty;
    }
}
