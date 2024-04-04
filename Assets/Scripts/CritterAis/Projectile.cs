using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public static Action IncreasePointsHit = delegate { };

    [Header("Refs")]
    [SerializeField] private GameObject PopPrefab;
    [SerializeField] private SpriteRenderer SRenderer;
    [SerializeField] private CircleCollider2D cCollider;
    [SerializeField] private float lifetime = 5;
    [SerializeField] private bool Bplayer;
    [SerializeField] private bool BLife;
    [SerializeField] private bool Power;

    private float speed = 5;
    private Vector3 direction = Vector3.zero;
    private float timer;

    void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        if (timer >= lifetime)
        {
            if (!BLife)
            {
                DisableSelf();
            }
        }

        transform.position += direction * Time.fixedDeltaTime * speed;
    }
    public void Activate(float spd, Vector3 dir)
    {
        direction = dir;
        speed = spd;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && Bplayer)
        {
            return;
        }
        if (other.CompareTag("Friend"))
        {
            return;
        }

        if (other.CompareTag("Projectile"))
        {
            return;
        }

        if(other.CompareTag("Player"))
        {
            SeaCritterController Sc = other.GetComponent<SeaCritterController>();
            Sc.ResetSpeed();
            Sc.gameObject.transform.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.blue;
            DisableSelf();
        }

        if (other.CompareTag("Enemy") && Bplayer)
        {
            AiFollow B = other.GetComponent<AiFollow>();
            AiShoot A = other.GetComponent<AiShoot>();
            EnemyHeath EH = other.GetComponent<EnemyHeath>();
            IncreasePointsHit();
            if (B != null)
            {
                B.Stun();
            }
            if (A != null)
            {
                A.Stun();
            }
            EH.Takeheath();
            DisableSelf();
        }
    }
    private void DisableSelf()
    {
        if (!Power)
        {
            Instantiate(PopPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
