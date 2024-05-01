using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyHeath : MonoBehaviour
{
    public static Action SnakeDmg = delegate { };

    [SerializeField] public int MaxHeath;

    [SerializeField] private GameObject DeathPrefab;

    public bool Snake;

    public int Heath;
    private Color sc;
    private void Awake()
    {
        Heath = MaxHeath;
        sc = GetComponentInChildren<SpriteRenderer>().color;
    }
    private void OnEnable()
    {
        EnemyHeath.SnakeDmg += SnakeTakeDmg;
    }
    private void OnDisable()
    {
        EnemyHeath.SnakeDmg -= SnakeTakeDmg;
    }
    public void Takeheath()
    {
        if (!Snake)
        {
            if (FindObjectOfType<GameSystems>().BossNumb > 1)
            {
                Heath--;
            }
            Heath--;
            GetComponentInChildren<SpriteRenderer>().color = Color.black;
            Invoke(nameof(ResetColor), 0.1f);
            if (Heath <= 0)
            {
                if (DeathPrefab != null)
                {
                    Instantiate(DeathPrefab, transform.position, Quaternion.identity);
                }
                Destroy(gameObject);
            }
        }
        else
        {
            SnakeDmg();
        }
    }

    private void SnakeTakeDmg()
    {
        if (FindObjectOfType<GameSystems>().BossNumb > 1)
        {
            Heath--;
        }
        Heath--;
        GetComponentInChildren<SpriteRenderer>().color = Color.black;
        Invoke(nameof(ResetColor), 0.1f);
        if (Heath <= 0)
        {
            if (DeathPrefab != null)
            {
                Instantiate(DeathPrefab, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }

    private void ResetColor()
    {
        GetComponentInChildren<SpriteRenderer>().color = sc;
    }
}
