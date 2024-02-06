using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ObjectMovements : MonoBehaviour
{
    public Transform[] waypoints;
    public float moveDuration = 3f;
    public bool useCurvedPath = false;

    private int currentIndex = 0;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        for (int i = 0; i < waypoints.Length - 1; i++)
        {
            Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
        }

        if (useCurvedPath)
        {
            Gizmos.color = Color.red;
            for (float t = 0.05f; t < 1; t += 0.05f)
            {
                Gizmos.DrawLine(GetCurvedPathPoint(t), GetCurvedPathPoint(t + 0.05f));
            }
        }
    }

    private void Start()
    {
        MoveObject();
    }

    private void MoveObject()
    {
        if (currentIndex >= waypoints.Length)
        {
            currentIndex = 0;
        }

        Vector3 targetPosition = waypoints[currentIndex].position;

        // 이동 방식 선택
        if (useCurvedPath)
        {
            transform.DOMove(GetCurvedPathPoint(0.5f), moveDuration).SetEase(Ease.Linear).OnComplete(() => MoveObject());
        }
        else
        {
            transform.DOMove(targetPosition, moveDuration).SetEase(Ease.Linear).OnComplete(() => MoveObject());
        }

        currentIndex++;
    }

    private Vector3 GetCurvedPathPoint(float t)
    {
        int index0 = (currentIndex - 1 + waypoints.Length) % waypoints.Length;
        int index1 = currentIndex % waypoints.Length;
        int index2 = (currentIndex + 1) % waypoints.Length;

        Vector3 p0 = waypoints[index0].position;
        Vector3 p1 = waypoints[index1].position;
        Vector3 p2 = waypoints[index2].position;

        Vector3 point = 0.5f * ((2 * p1) +
                     (-p0 + p2) * t +
                     (2 * p0 - 5 * p1 + 4 * p2 - p2) * t * t +
                     (-p0 + 3 * p1 - 3 * p2 + p2) * t * t * t);

        return point;
    }
}
