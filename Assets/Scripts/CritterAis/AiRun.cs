using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiRun : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private float Runspeed;
    [SerializeField] private float Roamspeed;
    [SerializeField] private float MinDistance;

    private Transform target;
    private Rigidbody2D rb;
    private Vector2 RandomDirection;
    private float ChangeTime = 0;

    private bool Scared;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        target = FindObjectOfType<SeaCritterController>().gameObject.transform;
        RandomDirection = transform.up;
    }
    private void Update()
    {
        if (!target.GetComponent<SeaCritterController>().SPower)
        {
            if (Vector2.Distance(transform.position, target.position) < MinDistance)
            {
                Scared = true;
                transform.position = Vector2.MoveTowards(transform.position, target.position, (-Runspeed) * Time.deltaTime);
            }
            else
            {
                Scared = false;
                ChangeTime -= Time.deltaTime;
                if (ChangeTime <= 0)
                {
                    float c = UnityEngine.Random.Range(-90f, 90f);
                    Quaternion rotation = Quaternion.AngleAxis(c, transform.forward);
                    RandomDirection = rotation * RandomDirection;
                    ChangeTime = UnityEngine.Random.Range(0.5f, 4f);
                }
            }
        }
        else
        {
            Scared = false;
            ChangeTime -= Time.deltaTime;
            if (ChangeTime <= 0)
            {
                float c = UnityEngine.Random.Range(-90f, 90f);
                Quaternion rotation = Quaternion.AngleAxis(c, transform.forward);
                RandomDirection = rotation * RandomDirection;
                ChangeTime = UnityEngine.Random.Range(0.5f, 4f);
            }
        }

        if (Scared)
        {
            if (target.transform.position.x > transform.position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            if (target.transform.position.x < transform.position.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
        else
        {
            if (rb.velocity.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            if (rb.velocity.x > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }
    private void FixedUpdate()
    {
        if(!Scared)
        {
            rb.velocity = RandomDirection * Roamspeed * Time.deltaTime;
        }
    }
}
