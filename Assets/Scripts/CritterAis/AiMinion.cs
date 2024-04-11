using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class AiMinion : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private float RSpeed;
    [SerializeField] private float Fps;
    [SerializeField] private float Bps;
    [SerializeField] private float FiringDuration;
    [SerializeField] private float ProjectileSpeed;
    [SerializeField] private GameObject ProjectilePrefab;
    [SerializeField] private Transform FiringPoint;

    private float BpsTimer;
    private Transform target;

    private bool Stuned;
    private Vector3 Direction;

    private void Awake()
    {
        target = FindObjectOfType<SeaCritterController>().gameObject.transform;
    }

    private void Update()
    {
        if(target)
        {
            Direction = transform.position - target.transform.position;
        }

        BpsTimer += Time.deltaTime;
        if (!Stuned)
        {
            if (BpsTimer >= Bps)
            {
                BpsTimer = 0;
                Projectile bs;
                bs = Instantiate(ProjectilePrefab, FiringPoint.position, quaternion.identity).GetComponent<Projectile>();
                bs.Activate(ProjectileSpeed, -transform.up);
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
}
