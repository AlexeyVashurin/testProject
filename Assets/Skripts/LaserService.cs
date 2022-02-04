using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class LaserService : MonoBehaviour
{
    
    public List <Laser> lasers = new List<Laser>();
    private List<GameObject> lines = new List<GameObject>();

    public float maxRaycastDistance = 20f;

    public Laser Laser;
    
    public GameObject LinePrefab;

    

    // Start is called before the first frame update
    public void AddLaser(Laser laser)
    {
        lasers.Add(laser);
    }

    public void DeleteLaser(Laser laser)
    {
        lasers.Remove(laser);
    }

    void RemoveOldLines(int linesCount)
    {
        if (linesCount < lines.Count)
        {
            Destroy(lines[lines.Count-1]);
            lines.RemoveAt(lines.Count-1);
            RemoveOldLines(linesCount);
        }
    }

    void Start()
    {
        AddLaser(Laser);
    }

    // Update is called once per frame
    void Update()
    {
        
        int linesCount = 0;
        foreach (Laser laser in lasers)
        {
            linesCount += CalcLaserLine(laser.transform.position + laser.transform.forward*0.5f, laser.transform.forward,linesCount);
        }
        RemoveOldLines(linesCount);
    }

    int CalcLaserLine(Vector3 startPosition, Vector3 direction, int index)
    {
        int result = 1;
        RaycastHit hit;
        Ray ray = new Ray(startPosition, direction);
        bool intersect = Physics.Raycast(ray, out hit, maxRaycastDistance);

        Vector3 hitPosition = hit.point;
        
        DrawLine(startPosition, hitPosition, index);
        if (!intersect )
        {
            hitPosition = startPosition + direction * maxRaycastDistance;
        }
        else
        {
            
           result += CalcLaserLine(hitPosition, Vector3.Reflect(direction, hit.normal), index+ result);
        }

        return result;

    }

    private void DrawLine(Vector3 startPosition, Vector3 finishPosition, int index)
    {
        LineRenderer line = null;
        if (index<lines.Count)
        {
            line = lines[index].GetComponent<LineRenderer>();
        }
        else
        {
            GameObject go = Instantiate(LinePrefab, Vector3.zero, Quaternion.identity);
             line = go.GetComponent<LineRenderer>();
             lines.Add(go);
        }

        
        
        line.SetPosition(0, startPosition);
        line.SetPosition(1,finishPosition);
    }
}
