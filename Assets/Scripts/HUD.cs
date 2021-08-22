using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class HUD : MonoBehaviour
{
    [Inject] private GameManager _gameManager;

    [SerializeField] private Text _timer;

    private void Start()
    {
        _gameManager.OnGameStart += StartTimer;
        _gameManager.OnGameOver += StopTimer;
    }

    private void OnDestroy()
    {
        _gameManager.OnGameStart -= StartTimer;
        _gameManager.OnGameOver -= StopTimer;
    }

    public void OnUpBtnClick(int direction)
    {
        _gameManager.SetVerticalDirection(direction);
    }
    private void StartTimer() 
    {
        StartCoroutine(DisplayTimer());
    }

    private void StopTimer()
    {
        StopAllCoroutines();
    }

    IEnumerator DisplayTimer()
    {
        while (true)
        {
            _timer.text = _gameManager.GameTime.ToString(@"m\:ss");
            yield return new WaitForSeconds(1);
        }
    }
}
