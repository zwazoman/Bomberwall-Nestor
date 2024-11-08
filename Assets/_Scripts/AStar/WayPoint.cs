using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Playables;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class WayPoint : MonoBehaviour
{
    SpriteRenderer _SR;

    public List<WayPoint> Neighbours = new List<WayPoint>();

    public WayPoint FormerPoint;

    public bool IsOpen = false;
    public bool IsClosed = false;

    [HideInInspector] public float h;
    [HideInInspector] public float g = 0;
    [HideInInspector] public float f => g + h ;


    private void Awake()
    {
        TryGetComponent<SpriteRenderer>(out _SR);
    }

    public void TravelThrough(ref List<WayPoint> openPoints, WayPoint endPoint)
    {
        if(this == endPoint)
        {
            print("finito");
            return;
        }

        Close(ref openPoints);

        foreach(WayPoint point in Neighbours)
        {
            if (point.IsClosed || point.IsOpen) continue;

            point.Open(point,endPoint,  ref openPoints);
        }

        WayPoint bestPoint = null;
        foreach (WayPoint point in openPoints)
        {
            if (bestPoint == null) bestPoint = point;
            else if (point.f < bestPoint.f) bestPoint = point;
        }

        bestPoint.TravelThrough(ref openPoints, endPoint);
    }

    void Open(WayPoint formerPoint, WayPoint endPoint, ref List<WayPoint> openPoints)
    {
        IsOpen = true;

        openPoints.Add(this);

        FormerPoint = formerPoint;

        h = Vector2.Distance(transform.position, endPoint.transform.position);
        g = g + 1;

        _SR.color = Color.white;
    }

    void Close(ref List<WayPoint> openPoints)
    {
        IsClosed = true;
        print("close");
        if(openPoints.Contains(this)) openPoints.Remove(this);
        _SR.color = Color.black;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        foreach (WayPoint point in Neighbours) //il dessine 2 fois mais t'inquietes
        {
            Gizmos.DrawLine(transform.position, point.transform.position);
        }
    }
}
