using System;
using UnityEngine;

public class MirrorCoefficient : MonoBehaviour
{
    public float Coefficient = 10;

    public bool checkHitRaycast;
    

    public delegate void hitTouch(int coof);

    public event hitTouch hitEvent;
    
    


    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    public void hitObject()
    {
        hitEvent?.Invoke(10);
    }



    // Update is called once per frame
    
}
