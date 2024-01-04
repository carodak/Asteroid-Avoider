using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverHandler : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Button continueButton;
    [SerializeField] private GameObject gameOverDisplay;
    [SerializeField] private AsteroidSpawner asteroidSpawner;
    [SerializeField] private TMP_Text gameOverText;
    [SerializeField] private ScoreSystem scoreSystem;
    public void EndGame(){
        asteroidSpawner.enabled = false;

        string finalScore = scoreSystem.OnDestroy().ToString();
        string highScore = PlayerPrefs.GetInt(ScoreSystem.HighScoreKey, 0).ToString();

        gameOverText.text = $"Your score: {finalScore}" +
        System.Environment.NewLine + $"Highest score: {highScore}";

        gameOverDisplay.gameObject.SetActive(true);

    }
    public void PlayAgain(){
        SceneManager.LoadScene(1);
    }

    public void ContinueButton(){
        AdManager.Instance.ShowAd(this);

        continueButton.interactable = false;
    }

    public void ReturnToMenu(){
        SceneManager.LoadScene(0);
    }

    public void ContinueGame()
    {
        scoreSystem.StartTimer();

        player.transform.position = Vector3.zero;
        player.SetActive(true);
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;

        asteroidSpawner.enabled = true;

        gameOverDisplay.gameObject.SetActive(false);
    }
}
