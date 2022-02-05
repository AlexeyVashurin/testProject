using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class RaycastReflection : MonoBehaviour
{

    public float StartPower;
    public float CurrentPower;
    public float MaxRayLenght;

    private LineRenderer _lineRenderer;
    private Ray _ray;
    private RaycastHit _hit;
    private Vector3 _direction;

    public MirrorCoefficient MirrorCoefficient;

   

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _ray = new Ray(transform.position, transform.up);

        _lineRenderer.positionCount = 1;
        _lineRenderer.SetPosition(0, transform.position);
        float remainingLength = MaxRayLenght;

        for (int i = 0; i < CurrentPower; i++)
        {
            if (Physics.Raycast(_ray.origin, _ray.direction, out _hit, remainingLength))
            {
                if (_hit.collider.GetComponent<MirrorCoefficient>())
                {
                    

                    // CurrentPower -= _hit.collider.GetComponent<MirrorCoefficient>().Coefficient;
                    // MirrorCoefficient = _hit.collider.GetComponent<MirrorCoefficient>();
                }
                _lineRenderer.positionCount += 1;
                _lineRenderer.SetPosition(_lineRenderer.positionCount -1, _hit.point);
                
                    remainingLength -= Vector3.Distance(_ray.origin, _hit.point);
                    if (CurrentPower>10)
                    {
                        _ray = new Ray(_hit.point, Vector3.Reflect(_ray.direction, _hit.normal)); 
                    }
                
                if (_hit.collider.tag != "Mirror")
                break;
                
            }
            else
            {
                

                if (_lineRenderer.positionCount == 1)
                {
                    _lineRenderer.positionCount += 1;
                    _lineRenderer.SetPosition(_lineRenderer.positionCount-1, _ray.origin+_ray.direction*remainingLength);
                    CurrentPower = StartPower;
                    
                } 
                
                break;
                
                
            }
        }
        
    }


    void minusIndex(int index)
    {
        CurrentPower -= index;
    }
}
