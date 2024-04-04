using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class GameSystems : GameAction
{
    public static Action ActivateBoost = delegate { };
    public static Action DeActivateBoost = delegate { };

    [Header("Refs")]
    [SerializeField] private Slider SizeSilder;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Image PowerUpDisplay;
    [SerializeField] private GameObject StartScreen;
    [SerializeField] private GameObject PauseScreen;
    [SerializeField] private GameObject EndScreen;
    [SerializeField] private GameObject TryAgainB;
    [SerializeField] private GameObject ResumeB;
    [SerializeField] private TextMeshProUGUI Score;
    [SerializeField] private TextMeshProUGUI ScoreF;
    [SerializeField] private TextMeshProUGUI ScoreP;
    [SerializeField] private TextMeshProUGUI HighScoreE;
    [SerializeField] private TextMeshProUGUI SHighScore;
    [SerializeField] private TextMeshProUGUI HighPScore;
    [SerializeField] private AudioSource As;

    [SerializeField] private Sprite[] PowerSprites;

    public bool InPlay = false;
    public int ScoreNumb;
    public float HealthNumb;
    public float SizeNumb;

    public bool Paused;

    private PlayerController PC;
    private SeaCritterController Sc;

    private void OnEnable()
    {
        DealDamageGA.DealDmg += DecreaseHeath;
        GainBoostGA.GiveBoost += IncreaseSize;
        IncreaseScoreGA.IncreaseScore += IncreaseScore;
        Projectile.IncreasePointsHit += SmallIncreaseScore;
        IncreaseHeathGA.Heal += IncreaseHeath;
        PC.Player.PauseGame.performed += PauseGame;
    }
    private void OnDisable()
    {
        DealDamageGA.DealDmg -= DecreaseHeath;
        GainBoostGA.GiveBoost -= IncreaseSize;
        IncreaseScoreGA.IncreaseScore -= IncreaseScore;
        Projectile.IncreasePointsHit -= SmallIncreaseScore;
        IncreaseHeathGA.Heal -= IncreaseHeath;
        PC.Player.PauseGame.performed -= PauseGame;
    }
    private void Awake()
    {
        SHighScore.text = PlayerPrefs.GetInt("HighScore").ToString();
        Sc = FindObjectOfType<SeaCritterController>();
        PC = new PlayerController();
        PC.Enable();
        healthSlider.value = 10;
        HealthNumb = healthSlider.value;
        SizeSilder.value = 0;
        SizeNumb = SizeSilder.value;
        ScoreNumb = 0;
    }

    private void Update()
    {
        if (SizeSilder.value >= 10)
        {
            ActivateBoost();
        }
        else
        {
            DeActivateBoost();
        }
        if(Sc.SpecailIndicator == 1)
        {
            PowerUpDisplay.sprite = PowerSprites[0];
        }
        else if (Sc.SpecailIndicator == 2)
        {
            PowerUpDisplay.sprite = PowerSprites[1];
        }
        else if (Sc.SpecailIndicator == 3)
        {
            PowerUpDisplay.sprite = PowerSprites[2];
        }
        else
        {
            PowerUpDisplay.sprite = null;
        }
    }

    private void IncreaseSize()
    {
        SizeSilder.value++;
        SizeNumb = SizeSilder.value;
    }
    public void DecreaseSize()
    {
        SizeSilder.value--;
        if(SizeSilder.value<0)
        {
            SizeSilder.value = 0;
        }
        SizeNumb = SizeSilder.value;
    }

    private void IncreaseScore()
    {
        ScoreNumb += 100;
        Score.text = ScoreNumb.ToString();
    }
    private void SmallIncreaseScore()
    {
        ScoreNumb += 50;
        Score.text = ScoreNumb.ToString();
    }


    private void IncreaseHeath()
    {
        if(healthSlider.value++ != 11)
        {
            healthSlider.value++;
            HealthNumb = healthSlider.value;
        }
    }
    private void DecreaseHeath()
    {
        healthSlider.value--;
        HealthNumb = healthSlider.value;
        if (healthSlider.value <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        As.Play();
        InPlay = false;
        if (!PlayerPrefs.HasKey("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", ScoreNumb);
        }
        else if(PlayerPrefs.GetInt("HighScore") < ScoreNumb)
        {
            PlayerPrefs.SetInt("HighScore", ScoreNumb);
        }

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(TryAgainB);

        ScoreF.text = ScoreNumb.ToString();
        HighScoreE.text = PlayerPrefs.GetInt("HighScore").ToString();
        EndScreen.SetActive(true);
        Time.timeScale = 0;
        Debug.Log("Dead");
    }

    public void StartGame()
    {
        InPlay = true;
        StartScreen.SetActive(false);
        Time.timeScale = 1;
    }
    private void PauseGame(InputAction.CallbackContext PauseGame)
    {
        if (!Paused)
        {
            Paused = true;
            PauseScreen.SetActive(true);

            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(ResumeB);

            Time.timeScale = 0;
        }
        else
        {
            Paused = false;
            PauseScreen.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void TryAgain()
    {
        if(Paused)
        {
            Paused = false;
            PauseScreen.SetActive(false);
        }
        InPlay = true;
        SeaSpawner SS = FindObjectOfType<SeaSpawner>();
        MapController Mc = FindObjectOfType<MapController>();
        healthSlider.value = 10;
        HealthNumb = healthSlider.value;
        SizeSilder.value = 0;
        SizeNumb = SizeSilder.value;
        ScoreNumb = 0;
        SS.ResetGame();
        Mc.ResetGame();
        Score.text = ScoreNumb.ToString();
        EndScreen.SetActive(false);
        Time.timeScale = 1;
    }
    public void Quit()
    {
        Application.Quit();
    }
}
