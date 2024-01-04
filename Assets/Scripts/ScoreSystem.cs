using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private float scoreMultiplier;

    public const string HighScoreKey = "HighScore";
    
    private float score;
    private bool shouldCount = true;

    // Update is called once per frame
    void Update()
    {
        if (!shouldCount) return;

        score += Time.deltaTime * scoreMultiplier;

        scoreText.text = Mathf.FloorToInt(score).ToString();
    }

    public int OnDestroy(){
        shouldCount = false;
        int currentHighScore = PlayerPrefs.GetInt(HighScoreKey, 0);
        if (score > currentHighScore){
            PlayerPrefs.SetInt(HighScoreKey, Mathf.FloorToInt(score));
        }
        scoreText.text = "";
        return Mathf.FloorToInt(score);
    }

    public void StartTimer()
    {
        shouldCount = true;
    }
}
