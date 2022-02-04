using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewsHolder : MonoBehaviour
{

    public static ViewsHolder instance;


    void Awake()
    {
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public LaserControlView laserControlView;
}
