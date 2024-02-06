using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierCurves : MonoBehaviour
{
    public Transform _target;
    public Transform _p1, _p2, _p3;
    public float _speed;

    private void Start()
    {
        StartCoroutine(COR_BezierCurves(_speed));
        //BezierCurve(_speed);
    }

    IEnumerator COR_BezierCurves(float speed)
    {
        float time = 0f;

        while (true)
        {
            if (time > 1f)
            {
                time = 0f;
            }

            Vector3 p4 = Vector3.Lerp(_p1.position, _p2.position, time);
            Vector3 p5 = Vector3.Lerp(_p2.position, _p3.position, time);
            _target.position = Vector3.Lerp(p4, p5, time);

            time += Time.deltaTime * _speed;

            yield return null;
        }
    }

    void BezierCurve(float speed)
    {

        float time = 0f;
        if (time > 1f)
        {
            time = 0f;
        }

        Vector3 p4 = Vector3.Lerp(_p1.position, _p2.position, time);
        Vector3 p5 = Vector3.Lerp(_p2.position, _p3.position, time);
        _target.position = Vector3.Lerp(p4, p5, time);

        time += Time.deltaTime * speed;

    }
}
