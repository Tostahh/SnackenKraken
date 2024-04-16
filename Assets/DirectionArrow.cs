using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using Unity.VisualScripting.AssemblyQualifiedNameParser;
using UnityEngine;

public class DirectionArrow : MonoBehaviour
{
    private Transform TransformTarget;
    private Transform RotationTarget;

    private void Awake()
    {
        TransformTarget = FindObjectOfType<SeaCritterController>().transform;
        RotationTarget = FindObjectOfType<BossAreana>().transform;
    }

    private void Update()
    {
        if (TransformTarget && RotationTarget)
        {
            transform.position = TransformTarget.position;
            Vector3 direction = RotationTarget.position - transform.position;
            transform.rotation = Quaternion.LookRotation(direction, transform.forward);
            transform.rotation = new Quaternion(0, 0, transform.rotation.z, transform.rotation.w);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
