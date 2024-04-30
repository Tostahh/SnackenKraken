using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using System.IO;
using System.Threading;

public class SnakeBossAi : AiBoss
{
    [SerializeField] private Transform[] ControllerTargets;
    [SerializeField] private AiMinion[] BodySegments;
    [SerializeField] private float RSpeed;
    [SerializeField] private float Runspeed;
    [SerializeField] private float TiredTime;
    [SerializeField] private int CurrentTarget;

    private Vector3 Direction;
    private bool Fired;
    private bool Done;
    private bool BTired;
    private int x;
    public override void Awake()
    {
        base.Awake();
    }

    void Update()
    {
        if (target)
        {
            if (!BTired)
            {
                if (Vector2.Distance(ControllerTargets[CurrentTarget].position, transform.position) <= 0.1f)
                {

                    if (CurrentTarget == 6)
                    {
                        BTired = true;
                        StartCoroutine(Tired());
                        CurrentTarget = 0;
                    }

                    if (CurrentTarget == 7)
                    {
                        BTired = true;
                        StartCoroutine(Tired());
                        CurrentTarget = 3;
                    }

                    if (CurrentTarget == 2 || CurrentTarget == 5)
                    {
                        x = UnityEngine.Random.Range(1, 3);
                    }
                    else
                    {
                        x = 1;
                    }

                    if (x == 1)
                    {
                        Runspeed = 18;
                        if (CurrentTarget == ControllerTargets.Length - 3)
                        {
                            CurrentTarget = 0;
                        }
                        else if (CurrentTarget != 6 && CurrentTarget != 7)
                        {
                            CurrentTarget++;
                        }
                    }
                    else
                    {
                        if (CurrentTarget == 2)
                        {
                            if (!Done)
                            {
                                Done = true;
                                Runspeed = 24;
                                CurrentTarget = 5;
                            }
                            else
                            {
                                Done = false;
                                Runspeed = 5;
                                CurrentTarget = 7; //
                            }
                        }
                        else if (CurrentTarget == 5)
                        {
                            if (!Done)
                            {
                                Done = true;
                                Runspeed = 24;
                                CurrentTarget = 2;
                            }
                            else
                            {
                                Done = false;
                                Runspeed = 5;
                                CurrentTarget = 6; //
                            }
                        }
                        x = 1;
                    }
                }

                Direction = transform.position - ControllerTargets[CurrentTarget].position;
                float angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;
                Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, RSpeed * Time.deltaTime);

                transform.position = Vector2.MoveTowards(transform.position, ControllerTargets[CurrentTarget].position, Runspeed * Time.deltaTime);
            }
            else
            {
                Direction = transform.position - target.position;
                float angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;
                Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, RSpeed * Time.deltaTime);
            }
        }

        if(CurrentTarget == 2 || CurrentTarget == 5)
        {
            if (!Fired)
            {
                Fired = true;
                foreach (AiMinion Seg in BodySegments)
                {
                    Seg.Fire();
                }
            }
        }
        else
        {
            Fired = false;
        }

    }

    private IEnumerator Tired()
    {
        yield return new WaitForSeconds(TiredTime);
        BTired = false;
    }
}
