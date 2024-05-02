using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraShake : MonoBehaviour
{
    public static Action ResCam = delegate { };

    [SerializeField] private float shakeAmount = 0.7f;
    [SerializeField] private float decreaseFactor = 1.0f;

    private bool Done;

    private float shakeDuration = 0f;
    private void OnEnable()
    {
        ShakeCamGA.Shake += Go;
        SnakeBossAi.Shake += GroundRumble;
    }

    private void OnDisable()
    {
        ShakeCamGA.Shake -= Go;
        SnakeBossAi.Shake -= GroundRumble;
    }

    private void Awake()
    {
        Done = true;
    }
    private void Update()
    {
        if (shakeDuration > 0)
        {
            transform.position = transform.position + UnityEngine.Random.insideUnitSphere * shakeAmount;

            shakeDuration -= Time.unscaledDeltaTime * decreaseFactor;
        }
        else
        {
            shakeDuration = 0f;
            transform.position = new Vector3(transform.position.x, transform.position.y, -5);

            if (!Done)
            {
                ResCam();
                Done = true;
            }
        }
    }

    private void Go()
    {
        Done = false;
        if (shakeDuration <= 0)
        {
            shakeDuration = 0.2f;
        }
    }

    private void GroundRumble()
    {
        Done = false;
        if (shakeDuration <= 0)
        {
            shakeDuration = 3f;
        }
    }
}
