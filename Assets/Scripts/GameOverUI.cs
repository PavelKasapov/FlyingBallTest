using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GameOverUI : MonoBehaviour
{
    [Inject] private GameManager _gameManager;

    [SerializeField] private Text _timeText;
    [SerializeField] private Text _totalAttemptsText;
    private void OnEnable()
    {
        _timeText.text = _gameManager.GameTime.ToString(@"m\:ss");
        _totalAttemptsText.text = _gameManager.totalAttempts.ToString();
    }

    public void OnTryAgainBtnClick()
    {
        _gameManager.StartGame();
    }
}
