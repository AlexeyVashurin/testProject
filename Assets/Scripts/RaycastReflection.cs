using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class RaycastReflection : MonoBehaviour
{
    [SerializeField] private float _maxPower;
    [SerializeField] private float _maxRayLenght;
    [SerializeField] private float _usePower;

    private LineRenderer lineRenderer;
    private Ray ray;
    private RaycastHit hit;
    private Vector3 direction;
    private MirrorCoefficient lastHitMirror;
    
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        ray = new Ray(transform.position, transform.up);
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, transform.position);
        float remainingLength = _maxRayLenght;

        for (_usePower = 0; _usePower < _maxPower; _usePower += lastHitMirror.Coefficient)
        {
            if (Physics.Raycast(ray.origin, ray.direction, out hit, remainingLength))
            {
                lastHitMirror = hit.collider.GetComponent<MirrorCoefficient>();
                lineRenderer.positionCount += 1;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, hit.point);
                remainingLength -= Vector3.Distance(ray.origin, hit.point);
                ray = new Ray(hit.point, Vector3.Reflect(ray.direction, hit.normal));
                if (hit.collider.tag != "Mirror")
                    break;
            }
            else
            {
                lineRenderer.positionCount += 1;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1,
                    ray.origin + ray.direction * remainingLength);
                lastHitMirror = null;
                break;
            }
        }
    }
}