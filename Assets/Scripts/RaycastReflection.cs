﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class RaycastReflection : MonoBehaviour
{
    public float MaxPower;
    public float MaxRayLenght;
    public float usePower;

    private LineRenderer _lineRenderer;
    private Ray _ray;
    private RaycastHit _hit;
    private Vector3 _direction;

    private MirrorCoefficient lastHitMirror;


    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        _ray = new Ray(transform.position, transform.up);

        _lineRenderer.positionCount = 1;
        _lineRenderer.SetPosition(0, transform.position);
        float remainingLength = MaxRayLenght;

        for (usePower = 0; usePower < MaxPower; usePower += lastHitMirror.Coefficient)
        {
            if (Physics.Raycast(_ray.origin, _ray.direction, out _hit, remainingLength))
            {
                lastHitMirror = _hit.collider.GetComponent<MirrorCoefficient>();

                _lineRenderer.positionCount += 1;
                _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, _hit.point);


                remainingLength -= Vector3.Distance(_ray.origin, _hit.point);
                _ray = new Ray(_hit.point, Vector3.Reflect(_ray.direction, _hit.normal));


                if (_hit.collider.tag != "Mirror")
                    break;
            }
            else
            {
                
                    _lineRenderer.positionCount += 1;
                    _lineRenderer.SetPosition(_lineRenderer.positionCount - 1,
                        _ray.origin + _ray.direction * remainingLength);
                    lastHitMirror = null;
                

                break;
            }
        }
    }
}