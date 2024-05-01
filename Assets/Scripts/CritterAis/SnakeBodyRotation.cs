using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SnakeBodyRotation : MonoBehaviour
{
    [SerializeField] private float RSpeed;

    public Transform target;
    private Vector3 Direction;
    void Update()
    {
        if (target)
        {
            Direction = transform.position - target.transform.position;
            float angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, RSpeed * Time.deltaTime);
        }
    }
}
