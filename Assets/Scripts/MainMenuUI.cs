using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MainMenuUI : MonoBehaviour
{
    [Inject] private GameManager _gameManager;

    [SerializeField] private Button[] difficultyButtons;

    public void OnStartGameClick()
    {
        _gameManager.StartGame();
    }

    /// <param name="difficulty">0 - Easy, 1 - Meduim, 2 - Hard</param>
    public void OnDifficultyButtonClick(int difficulty)
    {
        difficultyButtons[(int)_gameManager.currentDifficulty].interactable = true;
        difficultyButtons[difficulty].interactable = false;
        _gameManager.SetDifficulty((Difficulty)difficulty);
    }
}
