using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMotor : MonoBehaviour
{
    [SerializeField] private Transform lookAt;
    [SerializeField] private float boundx = 0.15f;
    [SerializeField] private float boundy = 0.05f;

    private float Sx;
    private float Sy;
    private void Awake()
    {
        Sx = boundx;
        Sx = boundy;
    }
    private void OnEnable()
    {
        GameSystems.EnterBoss += ChangeTargetBoss;
        GameSystems.ExitBoss += ChangeTargetPlayer;
        CameraShake.ResCam += ResetPos;
    }
    private void OnDisable()
    {
        GameSystems.EnterBoss -= ChangeTargetBoss;
        GameSystems.ExitBoss -= ChangeTargetPlayer;
        CameraShake.ResCam -= ResetPos;
    }

    private void LateUpdate()
    {
        Vector3 delta = Vector3.zero;
        float deltaX = lookAt.position.x - transform.position.x;
        if (deltaX > boundx || deltaX < -boundx)
        {
            if (transform.position.x < lookAt.position.x)
            {
                delta.x = deltaX - boundx;
            }
            else
            {
                delta.x = deltaX + boundx;
            }
        }
        float deltaY = lookAt.position.y - transform.position.y;
        if (deltaY > boundy || deltaY < -boundy)
        {
            if (transform.position.y < lookAt.position.y)
            {
                delta.y = deltaY - boundy;
            }
            else
            {
                delta.y = deltaY + boundy;
            }
        }
        transform.position += new Vector3(delta.x, delta.y, 0);
    }

    private void ChangeTargetBoss()
    {
        lookAt = FindObjectOfType<BossAreana>().transform;
        transform.position = new Vector3(lookAt.position.x, lookAt.position.y, transform.position.z);
        boundx = 0.1f;
        boundy = 0.1f;
    }

    private void ChangeTargetPlayer()
    {
        lookAt = FindObjectOfType<SeaCritterController>().transform;
        boundx = Sx;
        boundy = Sy;
    }

    private void ResetPos()
    {
        transform.position = new Vector3(lookAt.position.x, lookAt.position.y, transform.position.z);
    }
}