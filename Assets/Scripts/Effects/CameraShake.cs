using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private float shakeAmount = 0.7f;
    [SerializeField] private float decreaseFactor = 1.0f;

    private float shakeDuration = 0f;
    private void OnEnable()
    {
        ShakeCamGA.Shake += Go;
    }

    private void OnDisable()
    {
        ShakeCamGA.Shake -= Go;
    }
    private void Update()
    {
        if (shakeDuration > 0)
        {
            transform.position = transform.position + Random.insideUnitSphere * shakeAmount;

            shakeDuration -= Time.unscaledDeltaTime * decreaseFactor;
        }
        else
        {
            shakeDuration = 0f;
            transform.position = new Vector3(transform.position.x, transform.position.y, -5);
        }
    }

    private void Go()
    {
        shakeDuration = 0.2f;
    }
}
