using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class KrackenBossHead : MonoBehaviour
{
    [SerializeField] private Transform[] ControllerTargets;
    [SerializeField] private float RSpeed;
    [SerializeField] private float Runspeed;
    [SerializeField] private float ProjectileSpeed;
    [SerializeField] private GameObject ProjectilePrefab;
    [SerializeField] private GameObject ProjectilePrefab1;
    [SerializeField] private Transform FiringPoint;
    [SerializeField] private Transform FiringPoint1;
    [SerializeField] private Transform FiringPoint2;

    private int CurrentTarget;
    private Vector3 DirectionR;
    public Transform target;
    private KrackenBossAI bossAI;
    private void Awake()
    {
        CurrentTarget = 0;
        target = FindObjectOfType<SeaCritterController>().gameObject.transform;
        bossAI = FindObjectOfType<KrackenBossAI>();
    }
    private void Update()
    {
        if (Vector2.Distance(ControllerTargets[CurrentTarget].position, transform.position) <= 0.1f)
        {
            if (CurrentTarget == ControllerTargets.Length - 1)
            {
                CurrentTarget = 0;
            }
            else
            {
                CurrentTarget++;
            }
            Fire();
        }
        if (bossAI.Phase == 1)
        {
            transform.position = Vector2.MoveTowards(transform.position, ControllerTargets[CurrentTarget].position, Runspeed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, ControllerTargets[CurrentTarget].position, Runspeed* 1.5f * Time.deltaTime);
        }

        DirectionR = transform.position - target.transform.position;
    }

    private void FixedUpdate()
    {
        quaternion toRotation = quaternion.LookRotation(Vector3.forward, DirectionR);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, RSpeed * Time.deltaTime);
    }

    public void Fire()
    {
        if (bossAI.Phase == 1)
        {
            Projectile bs;
            bs = Instantiate(ProjectilePrefab, FiringPoint.position, quaternion.identity).GetComponent<Projectile>();
            bs.Activate(ProjectileSpeed, -transform.up);

            Projectile bs1;
            bs1 = Instantiate(ProjectilePrefab, FiringPoint1.position, quaternion.identity).GetComponent<Projectile>();
            bs1.Activate(ProjectileSpeed, -transform.up);

            Projectile bs2;
            bs2 = Instantiate(ProjectilePrefab, FiringPoint2.position, quaternion.identity).GetComponent<Projectile>();
            bs2.Activate(ProjectileSpeed, -transform.up);
        }
        else
        {
            Projectile bs;
            bs = Instantiate(ProjectilePrefab1, FiringPoint.position, quaternion.identity).GetComponent<Projectile>();
            bs.Activate(ProjectileSpeed, -transform.up);

            Projectile bs1;
            bs1 = Instantiate(ProjectilePrefab1, FiringPoint1.position, quaternion.identity).GetComponent<Projectile>();
            bs1.Activate(ProjectileSpeed, -transform.up);

            Projectile bs2;
            bs2 = Instantiate(ProjectilePrefab1, FiringPoint2.position, quaternion.identity).GetComponent<Projectile>();
            bs2.Activate(ProjectileSpeed, -transform.up);
        }
    }
}
