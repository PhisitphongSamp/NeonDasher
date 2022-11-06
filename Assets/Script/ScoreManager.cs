using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public Text hiScoreText;
    public TextMeshProUGUI showScore;

    public float scoreCount;
    public float hiScoreCount;

    public float pointsPerSecond;

    public bool scoreIncreasing;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(scoreIncreasing)
        {
            scoreCount += pointsPerSecond * Time.deltaTime;
        }
        if(scoreCount > hiScoreCount)
        {
            hiScoreCount = scoreCount;
        }
        
        scoreText.text = "" + Mathf.Round (scoreCount);

        showScore.text = "Your Score : " + Mathf.Round(scoreCount);
        
    }

    public void AddScore(int pointsToAdd)
    {
        scoreCount += pointsToAdd;
    }

    public void LastScore()
    {
        PlayerPrefs.SetInt("score", (int)scoreCount);
    }
}
