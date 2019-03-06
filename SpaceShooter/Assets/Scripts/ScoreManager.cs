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

    // Adds points to the score.
    public void AddPoints(int points)
    {
        score += points;
        scoreUI.SetText(score.ToString());
    }
}
