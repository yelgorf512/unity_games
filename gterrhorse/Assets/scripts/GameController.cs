using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    public TextMesh scoreText;
    private int score;

    void Start()
    {
        score = 0;
        UpdateScore();
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    public void ResetScore()
    {
        score = 0;
        UpdateScore();
    }

    void UpdateScore()
    {
        Debug.Log("SCORE " + score);
        scoreText.text = "Score: " + score;
    }
}