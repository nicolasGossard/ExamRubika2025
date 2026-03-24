using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public TMPro.TMP_Text scoreText;
    public TMPro.TMP_Text livesText;
    public TMPro.TMP_Text timeText;
    public TMPro.TMP_Text countdownText;
    public TMPro.TMP_Text powerupMessageText;

    private bool isSubscribed = false;

    void Start()
    {
        scoreText.enabled = true;
        livesText.enabled = true;
        timeText.enabled = true;
        powerupMessageText.enabled = false;

        StartCoroutine(SubscribeWhenReady());
    }

    private IEnumerator SubscribeWhenReady()
    {
        while (GameManager.Instance == null)
        {
            yield return null;
        }

        if (!isSubscribed)
        {
            GameManager.Instance.OnScoreChanged += UpdateScoreUI;
            GameManager.Instance.OnLivesChanged += UpdateLivesUI;
            isSubscribed = true;
        }

        UpdateScoreUI(GameManager.Instance.score);
        UpdateLivesUI(GameManager.Instance.lives);
    }

    private void OnDisable()
    {
        if (isSubscribed && GameManager.Instance != null)
        {
            GameManager.Instance.OnScoreChanged -= UpdateScoreUI;
            GameManager.Instance.OnLivesChanged -= UpdateLivesUI;
            isSubscribed = false;
        }
    }

    private void UpdateScoreUI(int newScore)
    {
        scoreText.text = "SCORE: " + newScore;
    }

    private void UpdateLivesUI(int newLives)
    {
        livesText.text = "LIVES: " + newLives;
    }
}