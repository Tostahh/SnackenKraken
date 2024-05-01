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
    public static Action EnterBoss = delegate { };
    public static Action ExitBoss = delegate { };

    [Header("PlayerRefs")]
    [SerializeField] public Slider InkSilder;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Image PowerUpDisplay;

    [Header("UIRefs")]
    [SerializeField] private GameObject StartScreen;
    [SerializeField] private GameObject PauseScreen;
    [SerializeField] private GameObject EndScreen;
    [SerializeField] private GameObject VictoryScreen;
    [SerializeField] private GameObject GoAgainB;
    [SerializeField] private GameObject TryAgainB;
    [SerializeField] private GameObject ResumeB;
    [SerializeField] private TextMeshProUGUI BossUP;
    [SerializeField] private TextMeshProUGUI Score;
    [SerializeField] private TextMeshProUGUI ScoreF;
    [SerializeField] private TextMeshProUGUI ScoreV;
    [SerializeField] private TextMeshProUGUI ScoreP;
    [SerializeField] private TextMeshProUGUI HighScoreE;
    [SerializeField] private TextMeshProUGUI HighScoreV;
    [SerializeField] private TextMeshProUGUI SHighScore;
    [SerializeField] private TextMeshProUGUI HighPScore;
    [SerializeField] private AudioSource AsLose;
    [SerializeField] private AudioSource AsWin;
    [SerializeField] private AudioSource BossAs;

    [Header("BossRefs")]
    [SerializeField] private GameObject BossUI;
    [SerializeField] private TextMeshProUGUI BossName;
    [SerializeField] private Slider BossHeathBar;
    [SerializeField] private GameObject Boss1;
    [SerializeField] private GameObject Boss2;
    [SerializeField] private GameObject Boss3;

    [SerializeField] private Sprite[] PowerSprites;
    [SerializeField] private Animator Animator;
    [SerializeField] private Animator AnimatorB;

    public bool InPlay = false;
    public int BossNumb;
    public int ScoreNumb;
    public float HealthNumb;
    public float InkNumb;

    public bool Paused;
    public bool BossState;

    public bool TestB;

    private PlayerController PC;
    private SeaCritterController Sc;
    private EnemyHeath BossenemyHeath;
    private bool BVictory;

    private void OnEnable()
    {
        DealDamageGA.DealDmg += DecreaseHeath;
        GainBoostGA.GiveBoost += IncreaseSize;
        IncreaseScoreGA.IncreaseScore += IncreaseScore;
        Projectile.IncreasePointsHit += SmallIncreaseScore;
        IncreaseHeathGA.Heal += IncreaseHeath;
        PC.Player.PauseGame.performed += PauseGame;
        BossAreana.Startboss += TriggerBoss;
        BossAreana.StopBoss += FinishBoss;
    }
    private void OnDisable()
    {
        DealDamageGA.DealDmg -= DecreaseHeath;
        GainBoostGA.GiveBoost -= IncreaseSize;
        IncreaseScoreGA.IncreaseScore -= IncreaseScore;
        Projectile.IncreasePointsHit -= SmallIncreaseScore;
        IncreaseHeathGA.Heal -= IncreaseHeath;
        PC.Player.PauseGame.performed -= PauseGame;
        BossAreana.Startboss -= TriggerBoss;
        BossAreana.StopBoss -= FinishBoss;
    }
    private void Awake()
    {
        SHighScore.text = PlayerPrefs.GetInt("HighScore").ToString();
        Sc = FindObjectOfType<SeaCritterController>();
        PC = new PlayerController();
        PC.Enable();
        healthSlider.value = 10;
        HealthNumb = healthSlider.value;
        InkSilder.value = InkSilder.maxValue;
        InkNumb = InkSilder.value;
        ScoreNumb = 0;
    }

    private void Update()
    {
        if(TestB)
        {
            if (!BossState)
            {
                TriggerBoss();
            }
            else
            {
                FinishBoss();
            }
            TestB = false;
        }

        if(BossState)
        {
            BossHeathBar.value = BossenemyHeath.Heath;
        }

        if (InkSilder.value >= 10)
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
        InkSilder.value++;
        InkNumb = InkSilder.value;
    }
    public void DecreaseSize()
    {
        InkSilder.value--;
        if(InkSilder.value<0)
        {
            InkSilder.value = 0;
        }
        InkNumb = InkSilder.value;
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

    private void TriggerBoss()
    {
        Animator.SetTrigger("Trans");
        BossState = true;
        BossName.text = FindObjectOfType<AiBoss>().BossName;
        BossenemyHeath = FindObjectOfType<AiBoss>().gameObject.GetComponentInChildren<EnemyHeath>();
        BossHeathBar.maxValue = BossenemyHeath.MaxHeath;
        BossHeathBar.value = BossHeathBar.maxValue;
        BossUI.SetActive(true);
        EnterBoss();
    }

    private void FinishBoss()
    {
        Animator.SetTrigger("Trans");
        BossAs.Play();
        BossState = false;
        BossNumb++;
        if (BossNumb == 1)
        {
            BossUP.text = "MAX INK INCREASED";
            InkSilder.maxValue += 5;
            AnimatorB.SetTrigger("Crown1");
        }
        else if (BossNumb == 2)
        {
            BossUP.text = "DAMAGE INCREASED";
            AnimatorB.SetTrigger("Crown2");
        }
        else
        {
            BossUP.text = "MAX HEALTH INCREASED";
            healthSlider.maxValue += 10;
            AnimatorB.SetTrigger("Crown3");
        }
        BossUI.SetActive(false);
        ExitBoss();
    }

    private void FakeFinishBoss()
    {
        BossState = false;
        BossUI.SetActive(false);
        ExitBoss();
    }
    private void Die()
    {
        AsLose.Play();
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

    private void Victory()
    {
        AsWin.Play();
        InPlay = false;
        if (!PlayerPrefs.HasKey("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", ScoreNumb);
        }
        else if (PlayerPrefs.GetInt("HighScore") < ScoreNumb)
        {
            PlayerPrefs.SetInt("HighScore", ScoreNumb);
        }

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(GoAgainB);

        ScoreV.text = ScoreNumb.ToString();
        HighScoreV.text = PlayerPrefs.GetInt("HighScore").ToString();
        VictoryScreen.SetActive(true);
        Time.timeScale = 0;
    }

    public void Continue()
    {
        InPlay = true;
        VictoryScreen.SetActive(false);
        Time.timeScale = 1;
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
        FakeFinishBoss();
        if (FindObjectOfType<BossAreana>())
        {
            Destroy(FindObjectOfType<BossAreana>().gameObject);
        }
        InPlay = true;
        SeaSpawner SS = FindObjectOfType<SeaSpawner>();
        MapController Mc = FindObjectOfType<MapController>();
        healthSlider.maxValue = 10;
        healthSlider.value = 10;
        HealthNumb = healthSlider.value;
        InkSilder.maxValue = 10;
        InkSilder.value = 10;
        InkNumb = InkSilder.value;
        ScoreNumb = 0;
        SS.ResetGame();
        Mc.ResetGame();
        BossNumb = 0;
        Score.text = ScoreNumb.ToString();
        AnimatorB.SetTrigger("Reset");
        EndScreen.SetActive(false);
        Time.timeScale = 1;
    }
    public void Quit()
    {
        Application.Quit();
    }
}
