using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TenticleAi : MonoBehaviour
{
    [SerializeField] private Transform[] ControllerTargets;
    [SerializeField] private float RSpeed;
    [SerializeField] private float Runspeed;
    [SerializeField] private int CurrentTarget;

    //[SerializeField] private AiMinion[] BodySegments;

    private Vector3 Direction;
    private KrackenBossAI bossAI;

    private void Awake()
    {
        CurrentTarget = 0;
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
        }

        if (bossAI.Phase == 1)
        {
            Direction = transform.position - ControllerTargets[CurrentTarget].position;
            float angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, RSpeed * Time.deltaTime);

            transform.position = Vector2.MoveTowards(transform.position, ControllerTargets[CurrentTarget].position, Runspeed * Time.deltaTime);
        }
        else
        {
            Direction = transform.position - ControllerTargets[CurrentTarget].position;
            float angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, RSpeed * Time.deltaTime);

            transform.position = Vector2.MoveTowards(transform.position, ControllerTargets[CurrentTarget].position, Runspeed * 1.5f * Time.deltaTime);
        }
    }
}
