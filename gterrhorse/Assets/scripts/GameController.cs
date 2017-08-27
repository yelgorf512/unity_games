using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    public TextMesh scoreText;
    private int score;
    private int highscore;
    float start_time;
    float time_elapsed;
    public int max_time = 0;

    void Start()
    {
        start_time = Time.time;
        score = 0;
        highscore = 0;
        UpdateScore();
        AudioListener.volume = 0.0F;
    }

    private void Update()
    {
        time_elapsed = Time.time - start_time;
        UpdateScore();   
    }
    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    public void ResetScore()
    {
        if (score > highscore)
        {
            highscore = score;
        }
        score = 0;
        start_time = Time.time;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "High: " + highscore
            + "\nScore: " + score
            + "\nTimer: " + time_elapsed;
    }
}