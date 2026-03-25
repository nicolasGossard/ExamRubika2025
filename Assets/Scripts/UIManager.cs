using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public TMPro.TMP_Text scoreText;
    public TMPro.TMP_Text livesText;
    public TMPro.TMP_Text timeText;
    public TMPro.TMP_Text bonusText;
    public TMPro.TMP_Text countdownText;

    private bool isSubscribed = false;

    void Start()
    {
        scoreText.enabled = true;
        livesText.enabled = true;
        timeText.enabled  = true;
        bonusText.enabled = false;

        StartCoroutine(SubscribeWhenReady());
    }

    ////////// ABONNEMENTS //////////

    private IEnumerator SubscribeWhenReady()
    {
        while (GameManager.Instance == null)
        {
            yield return null;
        }

        if (!isSubscribed)
        {
            GameManager.Instance.OnScoreChanged += UpdateScoreUI;
            Player player = FindAnyObjectByType<Player>();
            player.OnLivesChanged += UpdateLivesUI;
            GameManager.Instance.OnTimeChanged  += UpdateTimeUI;
            GameManager.Instance.OnBonusChanged += UpdateBonusUI;
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
            GameManager.Instance.OnTimeChanged  -= UpdateTimeUI;
            GameManager.Instance.OnBonusChanged -= UpdateBonusUI;
            isSubscribed = false;
        }
    }

    ////////// AFFICHAGE DU SCORE //////////

    private void UpdateScoreUI(int newScore)
    {
        scoreText.text = "SCORE: " + newScore;
    }

    ////////// AFFICHAGE DE LA VIE //////////

    private void UpdateLivesUI(int newLives)
    {
        string colorLives = newLives == 3 ? "<color=green>" : newLives == 2 ? "<color=yellow>" : "<color=red>"; 
        livesText.text = "LIVES: " + colorLives + newLives + "</color>";
    }

    ////////// AFFICHAGE DU TEMPS ÉCOULÉ //////////

    private void UpdateTimeUI(float gameTime)
    {
        int minutes = Mathf.FloorToInt(gameTime / 60);
        int seconds = Mathf.FloorToInt(gameTime % 60);
        timeText.text = string.Format("TIME: {0:00}:{1:00}", minutes, seconds);
    }

    ////////// AFFICHAGE DE BONUS //////////
    
    private void UpdateBonusUI(int bulletCount, int maxBulletCount)
    {
        bonusText.text = bulletCount == maxBulletCount ? "MAX WEAPON LEVEL!  +200 SCORE" : "WEAPON UPGRADED!  BULLETS: " + bulletCount;
        StartCoroutine(UpdateBonusUICoroutine());
    }

    private IEnumerator UpdateBonusUICoroutine()
    {
        bonusText.enabled = true;
        yield return new WaitForSeconds(2.0f);
        bonusText.enabled = false;
        yield return null;
    } 
}