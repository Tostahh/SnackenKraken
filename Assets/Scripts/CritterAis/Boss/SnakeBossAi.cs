using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using System;
using System.IO;
using System.Threading;

public class SnakeBossAi : AiBoss
{
    public static Action Shake = delegate { };

    [SerializeField] private Transform[] ControllerTargets;

    [SerializeField] private Transform[] ControllerTargets2;

    [SerializeField] private GameObject[] Warnings;

    [SerializeField] private GameObject[] Warnings2;

    [SerializeField] private GameObject HelpFullSquid;

    [SerializeField] private AiMinion[] BodySegments;
    [SerializeField] private float RSpeed;
    [SerializeField] private float Runspeed;
    [SerializeField] private float TiredTime;
    [SerializeField] private int CurrentTarget;

    public bool Main;
    public bool Active;

    private Vector3 Direction;
    private bool Fired;
    private bool Done;
    private bool Phased;
    private bool dashing;
    private bool BTired;
    private int x;
    public override void Awake()
    {
        base.Awake();
    }

    void Update()
    {
        CheckHeath();
        if (target)
        {
            if (Phase == 1)
            {
                if (!BTired)
                {
                    if (CurrentTarget == 0)
                    {
                        Warnings[0].SetActive(true);
                    }
                    else
                    {
                        Warnings[0].SetActive(false);
                    }

                    if (CurrentTarget == 3)
                    {
                        Warnings[3].SetActive(true);
                    }
                    else
                    {
                        Warnings[3].SetActive(false);
                    }

                    if (CurrentTarget == 5 && dashing)
                    {
                        Warnings[1].SetActive(true);
                    }
                    else
                    {
                        Warnings[1].SetActive(false);
                    }

                    if (CurrentTarget == 2 && dashing)
                    {
                        Warnings[2].SetActive(true);
                    }
                    else
                    {
                        Warnings[2].SetActive(false);
                    }


                    if (Vector2.Distance(ControllerTargets[CurrentTarget].position, transform.position) <= 0.1f)
                    {

                        if (CurrentTarget == 6)
                        {
                            BTired = true;
                            StartCoroutine(Tired());
                            CurrentTarget = 0; /////
                        }

                        if (CurrentTarget == 7)
                        {
                            BTired = true;
                            StartCoroutine(Tired());
                            CurrentTarget = 3; /////
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
                            else if (CurrentTarget != 6 && CurrentTarget != 7) ////////
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
                                    dashing = true;
                                    Done = true;
                                    Runspeed = 24;
                                    CurrentTarget = 5;
                                    StartCoroutine(WarningOff());
                                }
                                else
                                {
                                    Done = false;
                                    Runspeed = 18;
                                    CurrentTarget = 7; //
                                }
                            }
                            else if (CurrentTarget == 5)
                            {
                                if (!Done)
                                {
                                    dashing = true;
                                    Done = true;
                                    Runspeed = 24;
                                    CurrentTarget = 2;
                                    StartCoroutine(WarningOff());
                                }
                                else
                                {
                                    Done = false;
                                    Runspeed = 18;
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
            else
            {
                if (!BTired)
                {

                    if (CurrentTarget == 5 && dashing)
                    {
                        Warnings2[0].SetActive(true);
                    }
                    else
                    {
                        Warnings2[0].SetActive(false);
                    }

                    if (CurrentTarget == 0 && dashing)
                    {
                        Warnings2[1].SetActive(true);
                    }
                    else
                    {
                        Warnings2[1].SetActive(false);
                    }

                    if (Vector2.Distance(ControllerTargets2[CurrentTarget].position, transform.position) <= 0.1f)
                    {
                        if (CurrentTarget == 10)
                        {
                            BTired = true;
                            StartCoroutine(Tired());
                            CurrentTarget = 4;

                        }
                        if (CurrentTarget == 11)
                        {
                            BTired = true;
                            StartCoroutine(Tired());
                            CurrentTarget = -1;
                        }

                        if (CurrentTarget == 4 || CurrentTarget == 9)
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
                            if (CurrentTarget == ControllerTargets2.Length - 3)
                            {
                                CurrentTarget = 0;
                            }
                            else if (CurrentTarget != 10 && CurrentTarget != 11)
                            {
                                CurrentTarget++;
                            }
                        }
                        else
                        {
                            if (CurrentTarget == 4)
                            {
                                 CurrentTarget = 10; 
                            }
                            else if (CurrentTarget == 9)
                            {
                                 CurrentTarget = 11; 
                            }
                            x = 1;
                        }

                        if(CurrentTarget == 5 || CurrentTarget == 0)
                        {
                            dashing = true;
                            Runspeed = 24;
                            StartCoroutine(WarningOff());
                        }
                    }

                    Direction = transform.position - ControllerTargets2[CurrentTarget].position;
                    float angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;
                    Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                    transform.rotation = Quaternion.Slerp(transform.rotation, rotation, RSpeed * Time.deltaTime);

                    transform.position = Vector2.MoveTowards(transform.position, ControllerTargets2[CurrentTarget].position, Runspeed * Time.deltaTime);
                }
                else
                {
                    Direction = transform.position - target.position;
                    float angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;
                    Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                    transform.rotation = Quaternion.Slerp(transform.rotation, rotation, RSpeed * Time.deltaTime);
                }
            }
        }

        if (Phase == 1)
        {
            if (CurrentTarget == 2 || CurrentTarget == 5)
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
    }

    public override void CheckHeath()
    {
        if (Main)
        {
            base.CheckHeath();
            if(Phase == 2 && !Phased)
            {
                Phased = true;
                StopAllCoroutines();
                BTired = false;
                BodySegments[0].Firing = true;
                CurrentTarget = 0;
            }
        }
    }

    private IEnumerator Tired()
    {
        BodySegments[0].Firing = false;
        Instantiate(HelpFullSquid, transform.position, quaternion.identity);

        yield return new WaitForSeconds(0.5f);

        Instantiate(HelpFullSquid, transform.position, quaternion.identity);

        yield return new WaitForSeconds(TiredTime);
        BodySegments[0].Firing = true;
        BTired = false;
    }

    private IEnumerator WarningOff()
    {
        yield return new WaitForSeconds(1f);
        dashing = false;
        Shake();
    }
}
