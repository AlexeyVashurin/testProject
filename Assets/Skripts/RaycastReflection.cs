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

    public List<MirrorCoefficient> listMirrors;



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
                
                if (_hit.collider.GetComponent<MirrorCoefficient>().checkHitRaycast == false)
                {
                    CurrentPower -= _hit.collider.GetComponent<MirrorCoefficient>().Coefficient;
                    
                    _hit.collider.GetComponent<MirrorCoefficient>().checkHitRaycast = true;
                    listMirrors.Add(_hit.collider.GetComponent<MirrorCoefficient>());
                    // MirrorCoefficient = _hit.collider.GetComponent<MirrorCoefficient>();
                }
                _lineRenderer.positionCount += 1;
                _lineRenderer.SetPosition(_lineRenderer.positionCount -1, _hit.point);
                
                
                
                    remainingLength -= Vector3.Distance(_ray.origin, _hit.point);
                    _ray = new Ray(_hit.point, Vector3.Reflect(_ray.direction, _hit.normal)); 
                    
                    if (CurrentPower<10)
                    {
                        break;
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
                    for (int j = 0; j < listMirrors.Count; j++)
                    {
                        listMirrors[i].checkHitRaycast = false;
                        listMirrors.RemoveAt(i);
                    }
                        
                    
                    
                } 
                
                break;
                
                
            }
        }
        
    }
}
