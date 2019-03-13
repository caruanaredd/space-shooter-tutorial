using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    // A variable that makes this class a singleton.
    // i.e. only one instance can exist at any time.
    public static ScoreManager main { get; private set; }

    // The UI text showing the score.
    public TextMeshProUGUI scoreUI;

    // The score value for this game.
    public int score { get; private set; }

    // The all-time high score for this machine.
    public int highScore { get; private set; }

    // For singletons - set the main value to this
    // if it doesn't exist, or destroy the object.
    private void Awake()
    {
        if (main == null)
        {
            main = this;
        }
        else
        {
            DestroyImmediate(this);
        }
    }

    private void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    // Adds points to the score.
    public void AddPoints(int points)
    {
        score += points;
        scoreUI.SetText(score.ToString());
    }

    // Will clear the score in case of new play sessions.
    public void Clear()
    {
        // Check if the current score is greater than the highest one.
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
        }

        // Reset the score and UI
        score = 0;
        scoreUI.SetText(score.ToString());
    }
}
