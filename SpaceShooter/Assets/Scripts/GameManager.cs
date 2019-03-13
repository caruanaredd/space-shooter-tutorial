using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Enumerators - represent states/numbers with keywords
public enum GameState
{
    TitleScreen, GameScreen, GameOverScreen
}

[RequireComponent(typeof(AsteroidManager), typeof(ScoreManager))]
public class GameManager : MonoBehaviour
{
    // Singleton concept:
    public static GameManager main { get; private set; }

    // Referenced so we can manipulate the game objects.
    private AsteroidManager _asteroidManager;

    // Referenced so we can manipulate the game objects.
    private ScoreManager _scoreManager;

    // The mode this game is currently on.
    private GameState _state = GameState.TitleScreen;

    // A header command will add a bold header in the
    // Inspector within Unity.
    [Header("Game UI Elements")]
    public GameObject titleScreen;

    public GameObject gameUI;

    public GameObject gameOverScreen;

    public TextMeshProUGUI finalScoreText;

    public TextMeshProUGUI levelUIText;

    [Header("Player Objects")]
    public PlayerControls playerControls;

    [Header("Level Settings")]
    public int startingLevel = 1;

    private int _currentLevel;

    public LevelSettings[] settings;

    private void Awake()
    {
        if (main == null)
        {
            main = this;
        }
        else
        {
            DestroyImmediate(this);
            return;
        }

        _asteroidManager = GetComponent<AsteroidManager>();
        _scoreManager = GetComponent<ScoreManager>();
    }

    private void Start()
    {
        if (settings.Length == 0)
        {
            Debug.LogWarning("No settings data was found.");
            enabled = false;
            return;
        }

        SetGameState(GameState.TitleScreen);
    }

    public void StartGame()
    {
        _currentLevel = startingLevel;
        if (_currentLevel > settings.Length) _currentLevel = 1;
        levelUIText.SetText(string.Format("LV {0}", _currentLevel));

        SetGameState(GameState.GameScreen);
        _asteroidManager.SetSettings(settings[_currentLevel - 1]);
    }

    public void SetGameState(GameState state)
    {
        switch (state)
        {
            case GameState.TitleScreen:
                gameUI.SetActive(false);
                gameOverScreen.SetActive(false);
                playerControls.SetActive(false);

                titleScreen.SetActive(true);
                break;

            case GameState.GameOverScreen:
                gameUI.SetActive(false);
                titleScreen.SetActive(false);
                playerControls.SetActive(false);

                finalScoreText.SetText(
                    string.Format("High Score: {0}\nThis Game: {1}",
                    _scoreManager.highScore,
                    _scoreManager.score)
                );
                gameOverScreen.SetActive(true);
                break;

            case GameState.GameScreen:
                titleScreen.SetActive(false);
                gameOverScreen.SetActive(false);

                _asteroidManager.Clear();
                _scoreManager.Clear();
                gameUI.SetActive(true);
                playerControls.SetActive(true);
                break;
        }

        _state = state;
    }

    public void NextLevel()
    {
        _currentLevel++;
        if (_currentLevel > settings.Length) _currentLevel = 1;
        levelUIText.SetText(string.Format("LV {0}", _currentLevel));
        
        _asteroidManager.SetSettings(settings[_currentLevel - 1]);
    }

}
