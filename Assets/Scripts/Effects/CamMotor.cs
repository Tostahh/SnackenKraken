using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMotor : MonoBehaviour
{
    [SerializeField] private Transform lookAt;
    [SerializeField] private float boundx = 0.15f;
    [SerializeField] private float boundy = 0.05f;

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
}
