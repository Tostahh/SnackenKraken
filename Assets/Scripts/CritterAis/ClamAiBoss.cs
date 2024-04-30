using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class ClamAiBoss : AiBoss
{
    [SerializeField] private Animator animator;
    [SerializeField] private LayerMask SpawnPointMask;
    [SerializeField] private CircleCollider2D circleCollider;
    [SerializeField] private Sprite Phase2Sprite;

    [SerializeField] private GameObject HelpfulMinion;
    [SerializeField] private GameObject ShootingMinion;
    [SerializeField] private GameObject DashingMinion;
    [SerializeField] private GameObject ProjectilePrefab;
    [SerializeField] private GameObject BossShield;

    [SerializeField] private float RSpeed;
    [SerializeField] private float ProjectileSpeed;
    [SerializeField] private float AttackInterval;

    [SerializeField] private Transform FiringPoint1;
    [SerializeField] private Transform FiringPoint2;
    [SerializeField] private Transform FiringPoint3;
    [SerializeField] private Transform FiringPoint4;
    [SerializeField] private Transform FiringPoint5;

    [SerializeField] private ParticleSystem ParticleSystem;

    [SerializeField] private float BossWait;


    public List <Transform> spawnpoints = new List<Transform>();
    public List <GameObject> Minions = new List<GameObject>();

    private float AttackTimer;
    private Vector3 Direction;
    public bool Protected;
    public bool Active;

    public override void Awake()
    {
        base.Awake();
        ParticleSystem = GetComponentInChildren<ParticleSystem>();
        animator = GetComponentInChildren<Animator>();
        FindSpawnPoints();

        StartCoroutine(BossIntro());
    }
    private void Update()
    {
        if (Active)
        {
            CheckHeath();
            CheckMinions();
            if (Protected)
            {
                BossShield.SetActive(true);
                circleCollider.enabled = true;
            }
            else
            {
                BossShield.SetActive(false);
                circleCollider.enabled = false;
            }


            if (target)
            {
                Direction = transform.position - target.transform.position;
            }

            AttackTimer += Time.deltaTime;
            if (Minions.Count < 1)
            {
                Protected = false;
                if (AttackTimer >= AttackInterval)
                {
                    int x = UnityEngine.Random.Range(1, 5);
                    if (x == 1)
                    {
                        SpawnMinions();

                    }
                    if (x == 2)
                    {
                        SpawnHelp();
                    }
                    else
                    {
                        Shoot();
                    }
                    AttackTimer = 0;
                }
            }
            else
            {
                if (AttackTimer >= AttackInterval)
                {
                    int x = UnityEngine.Random.Range(1, 4);
                    if (x < 3)
                    {
                        Shoot();
                    }
                    else
                    {
                        SpawnHelp();
                    }
                    AttackTimer = 0;
                }
                Protected = true;
            }
        }
    }

    private void FixedUpdate()
    {
        if (Active)
        {
            quaternion toRotation = quaternion.LookRotation(Vector3.forward, Direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, RSpeed * Time.deltaTime);
        }
    }

    private void SpawnMinions()
    {
        if(Phase == 1)
        {
            foreach(Transform t in spawnpoints)
            {
                GameObject m = Instantiate(ShootingMinion, t.transform.position, quaternion.identity);
                Minions.Add(m);
            }
        }
        else
        {
            foreach (Transform t in spawnpoints)
            {
                GameObject m = Instantiate(DashingMinion, t.transform.position, quaternion.identity);
                Minions.Add(m);
            }
        }
    }

    private void SpawnHelp()
    {
        foreach (Transform t in spawnpoints)
        {
            Instantiate(HelpfulMinion, t.transform.position, quaternion.identity);
        }
    }
    private void Shoot()
    {
        if(Phase == 1)
        {
            if (Protected)
            {
                Projectile p = Instantiate(ProjectilePrefab, FiringPoint1).GetComponent<Projectile>();
                p.transform.localPosition = Vector3.zero;
                p.transform.parent = null;
                p.Activate(ProjectileSpeed, -transform.up);
            }
            else
            {
                Projectile p = Instantiate(ProjectilePrefab, FiringPoint1).GetComponent<Projectile>();
                p.transform.localPosition = Vector3.zero;
                p.transform.parent = null;
                p.Activate(ProjectileSpeed, -transform.up);

                Projectile p1 = Instantiate(ProjectilePrefab, FiringPoint2).GetComponent<Projectile>();
                p1.transform.localPosition = Vector3.zero;
                p1.transform.parent = null;
                p1.Activate(ProjectileSpeed, -transform.up);

                Projectile p2 = Instantiate(ProjectilePrefab, FiringPoint3).GetComponent<Projectile>();
                p2.transform.localPosition = Vector3.zero;
                p2.transform.parent = null;
                p2.Activate(ProjectileSpeed, -transform.up);
            }
        }
        else
        {
            if (Protected)
            {
                Projectile p = Instantiate(ProjectilePrefab, FiringPoint1).GetComponent<Projectile>();
                p.transform.localPosition = Vector3.zero;
                p.transform.parent = null;
                p.Activate(ProjectileSpeed, -transform.up);

                Projectile p1 = Instantiate(ProjectilePrefab, FiringPoint2).GetComponent<Projectile>();
                p1.transform.localPosition = Vector3.zero;
                p1.transform.parent = null;
                p1.Activate(ProjectileSpeed, -transform.up);

                Projectile p2 = Instantiate(ProjectilePrefab, FiringPoint3).GetComponent<Projectile>();
                p2.transform.localPosition = Vector3.zero;
                p2.transform.parent = null;
                p2.Activate(ProjectileSpeed, -transform.up);
            }
            else
            {
                Projectile p = Instantiate(ProjectilePrefab, FiringPoint1).GetComponent<Projectile>();
                p.transform.localPosition = Vector3.zero;
                p.transform.parent = null;
                p.Activate(ProjectileSpeed, -transform.up);

                Projectile p1 = Instantiate(ProjectilePrefab, FiringPoint2).GetComponent<Projectile>();
                p1.transform.localPosition = Vector3.zero;
                p1.transform.parent = null;
                p1.Activate(ProjectileSpeed, -transform.up);

                Projectile p2 = Instantiate(ProjectilePrefab, FiringPoint3).GetComponent<Projectile>();
                p2.transform.localPosition = Vector3.zero;
                p2.transform.parent = null;
                p2.Activate(ProjectileSpeed, -transform.up);

                Projectile p3 = Instantiate(ProjectilePrefab, FiringPoint4).GetComponent<Projectile>();
                p3.transform.localPosition = Vector3.zero;
                p3.transform.parent = null;
                p3.Activate(ProjectileSpeed, -transform.up);

                Projectile p4 = Instantiate(ProjectilePrefab, FiringPoint5).GetComponent<Projectile>();
                p4.transform.localPosition = Vector3.zero;
                p4.transform.parent = null;
                p4.Activate(ProjectileSpeed, -transform.up);
            }
        }
    }
    
    public override void CheckHeath()
    {
        base.CheckHeath();
        if(Phase == 2)
        {
            GetComponentInChildren<SpriteRenderer>().sprite = Phase2Sprite;
        }
    }
    private void CheckMinions()
    {
        List<GameObject> RemoveMinion = new List<GameObject>();

        foreach(GameObject m in Minions)
        {
            if (m == null)
            {
                RemoveMinion.Add(m);
            }
        }

        if(RemoveMinion.Count > 0)
        {
            foreach(GameObject m in RemoveMinion)
            {
                Minions.Remove(m);
            }
        }
    }
    private void FindSpawnPoints()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, 20, transform.up, SpawnPointMask);
        foreach(RaycastHit2D hit in hits)
        {
            if (hit.transform.gameObject.CompareTag("Point"))
            {
                spawnpoints.Add(hit.transform);
            }
        }
    }


    public override IEnumerator BossIntro()
    {
        yield return new WaitForSeconds(BossWait);
        animator.SetTrigger("Start");
        ParticleSystem.Play();
        Active = true;
    }
}
