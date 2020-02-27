using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{
    public int currentScore; // Changed to public
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
        scoreText.text = currentScore.ToString("D9");
    }
}
