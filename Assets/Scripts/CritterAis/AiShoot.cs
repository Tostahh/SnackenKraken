using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class AiShoot : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private float Runspeed;
    [SerializeField] private float Roamspeed;
    [SerializeField] private float MinDistance;
    [SerializeField] private float MaxDistance;
    [SerializeField] private float Bulletspeed;
    [SerializeField] private float Fps;
    [SerializeField] private float Bps;
    [SerializeField] private float FiringDuration;
    [SerializeField] private GameObject ProjectilePrefab;
    [SerializeField] private Transform FiringPoint1;
    [SerializeField] private Transform FiringPoint2;
    [SerializeField] private Transform FiringPoint3;
    [SerializeField] private Transform FiringPoint4;
    [SerializeField] private Transform FiringPoint5;

    private float BpsTimer;
    private float FpsTimer;
    private Transform target;
    private Rigidbody2D rb;
    private Vector2 RandomDirection;
    private float ChangeTime = 0;

    private bool Stuned;
    private bool Shooting;
    private bool Scared;
    private bool FarAway;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        target = FindObjectOfType<SeaCritterController>().gameObject.transform;
        RandomDirection = transform.up;
    }

    private void Update()
    {
        FpsTimer += Time.deltaTime;

        if (!Stuned)
        {
            if (!Shooting)
            {
                if (!target.GetComponent<SeaCritterController>().SPower)
                {
                    if (Vector2.Distance(transform.position, target.position) < MinDistance)
                    {
                        Scared = true;
                        transform.position = Vector2.MoveTowards(transform.position, target.position, (-Runspeed) * Time.deltaTime);
                    }
                    else if (Vector2.Distance(transform.position, target.position) > MaxDistance)
                    {
                        FarAway = true;
                        transform.position = Vector2.MoveTowards(transform.position, target.position, (Runspeed) * Time.deltaTime);
                    }
                    else if (Vector2.Distance(transform.position, target.position) < MaxDistance && Vector2.Distance(transform.position, target.position) > MinDistance)
                    {
                        if (FpsTimer >= (1f / Fps))
                        {
                            Shooting = true;
                            StartCoroutine(Shoot(FiringDuration));
                            Scared = false;
                            FarAway = false;
                            FpsTimer = 0;
                        }
                    }
                    else
                    {
                        Scared = false;
                        FarAway = false;
                        ChangeTime -= Time.deltaTime;
                        if (ChangeTime <= 0)
                        {
                            float c = Random.Range(-90f, 90f);
                            Quaternion rotation = Quaternion.AngleAxis(c, transform.forward);
                            RandomDirection = rotation * RandomDirection;
                            ChangeTime = Random.Range(0.5f, 4f);
                        }
                    }
                }
                else
                {
                    Scared = false;
                    FarAway = false;
                    ChangeTime -= Time.deltaTime;
                    if (ChangeTime <= 0)
                    {
                        float c = Random.Range(-90f, 90f);
                        Quaternion rotation = Quaternion.AngleAxis(c, transform.forward);
                        RandomDirection = rotation * RandomDirection;
                        ChangeTime = Random.Range(0.5f, 4f);
                    }
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (!Stuned || !Scared || !Shooting || !FarAway)
        {
            rb.velocity = RandomDirection * Roamspeed * Time.deltaTime;
        }
    }
    public void Stun()
    {
        Stuned = true;
        StartCoroutine(UnStun());
    }
    IEnumerator Shoot(float duration)
    {
        float startRotation = transform.eulerAngles.z;
        float endRotation = startRotation + 360.0f;
        float t = 0.0f;
        BpsTimer = (1f / Bps);
        while (t < duration)
        {
            rb.velocity = Vector2.zero;
            t += Time.deltaTime;
            BpsTimer += Time.deltaTime;
            float zRotation = Mathf.Lerp(startRotation, endRotation, t / duration) % 360.0f;
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, zRotation);
            if (BpsTimer >= (1f / Bps))
            {
                BpsTimer = 0;
                Projectile p = Instantiate(ProjectilePrefab, FiringPoint1).GetComponent<Projectile>();
                p.transform.localPosition = Vector3.zero;
                p.transform.parent = null;
                p.Activate(Bulletspeed, transform.up);

                Projectile p1 = Instantiate(ProjectilePrefab, FiringPoint2).GetComponent<Projectile>();
                p1.transform.localPosition = Vector3.zero;
                p1.transform.parent = null;
                p1.Activate(Bulletspeed, -transform.right);

                Projectile p2 = Instantiate(ProjectilePrefab, FiringPoint3).GetComponent<Projectile>();
                p2.transform.localPosition = Vector3.zero;
                p2.transform.parent = null;
                p2.Activate(Bulletspeed, transform.right);

                Projectile p3 = Instantiate(ProjectilePrefab, FiringPoint4).GetComponent<Projectile>();
                p3.transform.localPosition = Vector3.zero;
                p3.transform.parent = null;
                p3.Activate(Bulletspeed, -transform.up);
            }
            yield return null;
        }
        Shooting = false;
    }
    private IEnumerator UnStun()
    {
        yield return new WaitForSeconds(1);
        Stuned = false;
    }
}
