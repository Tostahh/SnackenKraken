using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiFollow : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private float Runspeed;
    [SerializeField] private float Roamspeed;
    [SerializeField] private float MinDistance;
    [SerializeField] private float MaxDistance;
    [SerializeField] private float DashDistance;
    [SerializeField] private bool NoMax;
    [SerializeField] private bool BDash;
    [SerializeField] private float DashCooldown = 0.2f;

    private Animator animator;
    private Rigidbody2D rb;
    private TrailRenderer trailRenderer;

    private Transform target;
    private Vector2 RandomDirection;
    private float ChangeTime = 2;

    private bool Hungry;
    private bool Stuned;
    private float Timer;
    private bool Dashing;

    private void Awake()
    {
        if(BDash)
        {
            trailRenderer = GetComponent<TrailRenderer>();
        }
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        target = FindObjectOfType<SeaCritterController>().gameObject.transform;
        RandomDirection = transform.up;
    }
    private void Update()
    {
        if (BDash)
        {
            Timer += Time.deltaTime;
        }

        if (!Stuned)
        {
            if (!Dashing)
            {
                if (!target.GetComponent<SeaCritterController>().SPower)
                {
                    if (Vector2.Distance(transform.position, target.position) < MaxDistance || NoMax)
                    {
                        Hungry = true;

                        if (BDash)
                        {
                            if (Vector2.Distance(transform.position, target.position) > MinDistance)
                            {
                                if (Vector2.Distance(transform.position, target.position) < DashDistance && Timer >= (1f / DashCooldown))
                                {
                                    StartCoroutine(Dash());
                                    Dashing = true;
                                    Timer = 0;
                                    DashCooldown = Random.Range(0.1f, 1f);
                                }
                                else
                                {
                                    if (Vector2.Distance(transform.position, target.position) > MinDistance)
                                    {
                                        transform.position = Vector2.MoveTowards(transform.position, target.position, Runspeed * Time.deltaTime);
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (Vector2.Distance(transform.position, target.position) > MinDistance)
                            {
                                transform.position = Vector2.MoveTowards(transform.position, target.position, Runspeed * Time.deltaTime);
                            }
                        }
                    }
                    else
                    {
                        Hungry = false;
                        ChangeTime -= Time.deltaTime;
                        if (ChangeTime <= 0)
                        {
                            float c = Random.Range(-90f, 90f);
                            Quaternion rotation = Quaternion.AngleAxis(c, transform.forward);
                            RandomDirection = rotation * RandomDirection;
                            ChangeTime = Random.Range(2f, 7f);
                        }
                    }
                }
                else
                {
                    Hungry = false;
                    ChangeTime -= Time.deltaTime;
                    if (ChangeTime <= 0)
                    {
                        float c = Random.Range(-90f, 90f);
                        Quaternion rotation = Quaternion.AngleAxis(c, transform.forward);
                        RandomDirection = rotation * RandomDirection;
                        ChangeTime = Random.Range(2f, 7f);
                    }
                }

                if (Hungry)
                {
                    if (target.transform.position.x > transform.position.x)
                    {
                        transform.localScale = new Vector3(1, 1, 1);
                    }
                    if (target.transform.position.x < transform.position.x)
                    {
                        transform.localScale = new Vector3(-1, 1, 1);
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
        }
    }

    private void FixedUpdate()
    {
        if (!Hungry)
        {
            rb.velocity = RandomDirection * Roamspeed * Time.deltaTime;
        }
    }
    public void Stun()
    {
        Stuned = true;
        animator.enabled = false;
        StartCoroutine(UnStun());
    }
    private IEnumerator UnStun()
    {
        yield return new WaitForSeconds(1);
        animator.enabled = true;
        Stuned = false;
    }

    private IEnumerator Dash()
    {
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(0.2f);
        trailRenderer.emitting = true;
        Vector2 targetGO = (target.position - transform.position).normalized;
        rb.velocity = targetGO*10;
        yield return new WaitForSeconds(0.6f);
        trailRenderer.emitting = false;
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(0.01f);
        Dashing = false;
    }
}
