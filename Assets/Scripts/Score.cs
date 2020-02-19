using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{
    [SerializeField] int currentScore;
    // public Text scoreText;
    public TextMeshProUGUI scoreText;

    // Start is called before the first frame update
    void Start()
    {
        OnScoreChanged();
    }

    public void AddToScore(int scoreAmount) {
        currentScore += scoreAmount;
        OnScoreChanged();
    }

    void OnScoreChanged() {
        //scoreText.text = "Score: " + currentScore.ToString("D9");
        scoreText.text = currentScore.ToString("D9");
    }
}
