using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text scoreText;
    [SerializeField] private TMPro.TMP_Text livesText;
    [SerializeField] private TMPro.TMP_Text timeText;
    [SerializeField] private TMPro.TMP_Text bonusText;

    [SerializeField] private GameObject[]   bonusStates;
    [SerializeField] private Slider[]       sliderStates;

    private int score;

    void Start()
    {
        score = 0;
        scoreText.enabled = true;
        livesText.enabled = true;
        timeText.enabled  = true;
        bonusText.enabled = false;

        for (int i = 0; i < bonusStates.Length; i++)
        {
            bonusStates[i].SetActive(false);
            sliderStates[i].value = 1f;
        }

        UpdateScoreUI(0);
    }

    ////////// ABONNEMENTS //////////
    
    private void OnEnable()
    {
        Player.OnLivesChanged += UpdateLivesUI;

        BonusShield.OnBonusShieldCreated += UpdateBonusShieldUI;
        Shield.OnShieldBreaked += RemoveBonusShield;

        BonusBullet.OnBonusBulletCreated += UpdateBonusBulletUI;
        BonusLives.OnBonusLivesCreated += UpdateBonusLivesUI;

        Enemy.AddScore += UpdateScoreUI;

        ManagerTest.OnTimeChanged += UpdateTimeUI;
    }

    private void OnDisable()
    {
        Player.OnLivesChanged -= UpdateLivesUI;

        BonusShield.OnBonusShieldCreated -= UpdateBonusShieldUI;
        Shield.OnShieldBreaked -= RemoveBonusShield;

        BonusBullet.OnBonusBulletCreated -= UpdateBonusBulletUI;
        BonusLives.OnBonusLivesCreated   -= UpdateBonusLivesUI;

        Enemy.AddScore -= UpdateScoreUI;

        ManagerTest.OnTimeChanged -= UpdateTimeUI;
    }

    private void RemoveBonusShield()
    {
        bonusStates[0].SetActive(false);
    }

    ////////// AFFICHAGE DU SCORE //////////

    private void UpdateScoreUI(int amount)
    {
        score += amount;
        scoreText.text = "SCORE: " + score;
    }

    ////////// AFFICHAGE DE LA VIE //////////

    private void UpdateLivesUI(int playerLives)
    {
        string colorLives = "";

        if (playerLives >= 3)
        {
            colorLives = "<color=green>";
        }
        else if (playerLives == 2)
        {
            colorLives = "<color=yellow>";
        }
        else if (playerLives == 1)
        {
            colorLives = "<color=red>";
        }
        else
        {
            colorLives = "<color=red>";

            for (int i = 0; i < bonusStates.Length; i++)
            {
                bonusStates[i].SetActive(false);
                sliderStates[i].value = 0f;
            }
        }

        livesText.text = "LIVES: " + colorLives + playerLives + "</color>";
    }

    ////////// AFFICHAGE DU TEMPS ÉCOULÉ //////////

    private void UpdateTimeUI(float gameTime)
    {
        int minutes = Mathf.FloorToInt(gameTime / 60);
        int seconds = Mathf.FloorToInt(gameTime % 60);
        timeText.text = string.Format("TIME: {0:00}:{1:00}", minutes, seconds);
    }

    ////////// AFFICHAGE DES BONUS //////////
    
    private void UpdateBonusLivesUI()
    {
        bonusText.text = "LIFE UP! +1 LIFE";
        StartCoroutine(UpdateBonusTextCoroutine());
    }
    
    private void UpdateBonusShieldUI()
    {
        bonusText.text = "SHIELD ACTIVATED!";
        StartCoroutine(UpdateBonusTextCoroutine());
        StartCoroutine(UpdateBonusSliderCoroutine(0, 5.0f));
    }

    private void UpdateBonusBulletUI()
    {
        Player player = FindFirstObjectByType<Player>();
        bonusText.text = player.bulletCount == player.GetBulletMaxCount ?
                         "MAX WEAPON LEVEL!  +200 SCORE" :
                         "WEAPON UPGRADED!  BULLETS: " + player.bulletCount;
        StartCoroutine(UpdateBonusTextCoroutine());
        StartCoroutine(UpdateBonusSliderCoroutine(1, 15.0f));
    }

    private IEnumerator UpdateBonusSliderCoroutine(int num, float effectTimer)
    {
        bonusStates[num].SetActive(true);
        sliderStates[num].value = 1.0f;

        float duration = effectTimer;
        float elapsedTime = 0f;
        
        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            sliderStates[num].value = Mathf.Lerp(1f, 0f, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        sliderStates[num].value = 0f;
        bonusStates[num].SetActive(false);
    } 

    private IEnumerator UpdateBonusTextCoroutine()
    {
        bonusText.enabled = true;
        yield return new WaitForSeconds(2.0f);
        bonusText.enabled = false;
        yield return null;
    } 
}