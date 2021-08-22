using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public enum UI
{
    None,
    MainMenu,
    GameOver
}

public class GuiController : MonoBehaviour
{
    [Inject] private GameManager _gameManager;

    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _gameOverMenu;
    [SerializeField] private GameObject _menuBackground;

    private GameObject[] _uiMenus;
    private UI _activeUI = UI.MainMenu;

    private Dictionary<UI, GameObject> UIPanel;

    private void Start()
    {
        _uiMenus = new GameObject[2] { _mainMenu, _gameOverMenu };
        UIPanel = new Dictionary<UI, GameObject>()
        {
            [UI.None] = null,
            [UI.MainMenu] = _mainMenu,
            [UI.GameOver] = _gameOverMenu
        };
        _gameManager.OnGameStart += OnGameStart;
        _gameManager.OnGameOver += OnGameOver;
    }

    private void OnDestroy()
    {
        _gameManager.OnGameStart -= OnGameStart;
        _gameManager.OnGameOver -= OnGameOver;
    }

    private void OnGameStart()
    {
        SetActiveUI(UI.None);
    }

    private void OnGameOver()
    {
        SetActiveUI(UI.GameOver);
    }

    public void SetActiveUI(UI ui)
    {
        UIPanel[_activeUI]?.SetActive(false);
        _activeUI = ui;
        UIPanel[ui]?.SetActive(true);
        _menuBackground.SetActive(_activeUI != UI.None);
    }

    public void SetActiveUI(int uiInt) => SetActiveUI((UI)uiInt);
    public void SetActiveUI(GameObject ui) => SetActiveUI(UIPanel.FirstOrDefault(x => x.Value == ui).Key);
    
}
