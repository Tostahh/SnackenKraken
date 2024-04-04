using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHeath : MonoBehaviour
{
    [SerializeField] private int MaxHeath;

    [SerializeField] private GameObject DeathPrefab;

    private int Heath;
    private Color sc;
    private void Awake()
    {
        Heath = MaxHeath;
        sc = GetComponentInChildren<SpriteRenderer>().color;
    }
    public void Takeheath()
    {
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
