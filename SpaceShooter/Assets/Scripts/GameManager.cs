using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [Header("Player Objects")]
    public PlayerControls playerControls;

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
        SetGameState(GameState.TitleScreen);
    }

    public void StartGame()
    {
        SetGameState(GameState.GameScreen);
        _asteroidManager.GenerateBigAsteroids();
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

                gameOverScreen.SetActive(true);
                break;

            case GameState.GameScreen:
                titleScreen.SetActive(false);
                gameOverScreen.SetActive(false);

                _asteroidManager.Clear();
                gameUI.SetActive(true);
                playerControls.SetActive(true);
                break;
        }

        _state = state;
    }

}
