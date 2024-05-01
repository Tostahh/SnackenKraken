using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tail : MonoBehaviour
{
    public int length;
    public LineRenderer lineRenderer;
    public Vector3[] segementPoses;
    private Vector3[] segmentV;

    public Transform TargetDir;
    public float TargetDist;
    public float SmoothSpeed;

    public float WiggleSpeed;
    public float WiggleAmount;
    public Transform WiggleDir;

    public Transform[] BodyParts;

    private void Awake()
    {
        lineRenderer.positionCount = length;
        segementPoses = new Vector3[length];
        segmentV = new Vector3[length];

        for(int i = 0; i < BodyParts.Length; i++)
        {
            if (i == 0)
            {
                BodyParts[i].GetComponent<SnakeBodyRotation>().target = TargetDir.transform;
            }
            else
            {
                BodyParts[i].GetComponent<SnakeBodyRotation>().target = BodyParts[i - 1].transform;
            }
        }

        segementPoses[0] = TargetDir.position;
        for(int i = 1; i < length; i++)
        {
            segementPoses[i] = segementPoses[i - 1] + TargetDir.right * TargetDist;
        }
    }

    private void Update()
    {
        WiggleDir.localRotation = Quaternion.Euler(0, 0, Mathf.Sin(Time.time * WiggleSpeed) * WiggleAmount);

        segementPoses[0] = TargetDir.position;

        for(int i = 1; i < segementPoses.Length; i++)
        {
            Vector3 targetPos = segementPoses[i - 1] + (segementPoses[i] - segementPoses[i - 1]).normalized * TargetDist;
            segementPoses[i] = Vector3.SmoothDamp(segementPoses[i], targetPos, ref segmentV[i], SmoothSpeed);
            BodyParts[i - 1].transform.position = segementPoses[i];
        }
        lineRenderer.SetPositions(segementPoses);
    }
}
