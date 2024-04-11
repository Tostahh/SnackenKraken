using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using UnityEngine.UIElements.Experimental;

public class AiMinionB : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private float Runspeed;
    [SerializeField] private float RSpeed;
    [SerializeField] private float MinDistance;
    [SerializeField] private float DashDistance;
    [SerializeField] private float DashCooldown = 0.2f;
    [SerializeField] private GameObject Help;
    [SerializeField] private GameObject Helparticles;

    private TrailRenderer trailRenderer;
    private Rigidbody2D rb;
    private Transform target;
    private float Dtimer;
    private bool Stuned;
    private bool Dashing;
    private Vector3 Direction;
    private Transform st;

    private void Awake()
    {
        target = FindObjectOfType<SeaCritterController>().gameObject.transform;
        rb = GetComponent<Rigidbody2D>();
        trailRenderer = GetComponent<TrailRenderer>();
    }

    private void Update()
    {   
        if(!Stuned && !Dashing)
        {
            Dtimer += Time.deltaTime;
        }

        if(target)
        {
            Direction = transform.position - target.transform.position;
        }

        if (Vector2.Distance(transform.position, target.position) > MinDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, Runspeed * Time.deltaTime);

            if (Dtimer >= DashCooldown && !Dashing)
            {
                StartCoroutine(Dash());
                Dashing = true;
                Dtimer = 0;
            }
        }
    }
    private void FixedUpdate()
    {
        if (!Stuned)
        {
            quaternion toRotation = quaternion.LookRotation(Vector3.forward, Direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, RSpeed * Time.deltaTime);
        }
    }
    public void Stun()
    {
        Stuned = true;
        StartCoroutine(UnStun());
    }
    private IEnumerator UnStun()
    {
        yield return new WaitForSeconds(1);
        Stuned = false;
    }
    private IEnumerator Dash()
    {
        Vector3 st = transform.position;
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(0.5f);
        trailRenderer.emitting = true;
        Vector2 targetGO = (target.position - transform.position).normalized;
        rb.velocity = targetGO * 10;
        yield return new WaitForSeconds(DashDistance);
        Instantiate(Helparticles, st, quaternion.identity);
        Instantiate(Help, st, quaternion.identity);
        trailRenderer.emitting = false;
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(0.5f);
        Dashing = false;
    }
}
