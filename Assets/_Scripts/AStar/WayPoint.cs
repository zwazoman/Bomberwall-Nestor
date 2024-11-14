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
    Color _color;

    public List<WayPoint> Neighbours = new List<WayPoint>();

    [HideInInspector] public WayPoint FormerPoint;

    [HideInInspector] public bool IsOpen = false;
    [HideInInspector] public bool IsClosed = false;

    [HideInInspector] public float H;
    [HideInInspector] public float G;
    [HideInInspector] public float F => G + H ;


    private void Awake()
    {
        TryGetComponent<SpriteRenderer>(out _SR);
        _color = _SR.color;
    }

    public void TravelThrough(ref List<WayPoint> openPoints,ref List<WayPoint> closedPoints, ref Stack<WayPoint> shorterPath, WayPoint endPoint, WayPoint startPoint)
    {
        if(this == endPoint)
        {
            Close(ref openPoints, ref closedPoints);
            WayPoint currentPoint = endPoint;
            while(currentPoint != startPoint)
            {
                shorterPath.Push(currentPoint);
                currentPoint = currentPoint.FormerPoint;
            }
            return;
        }

        Close(ref openPoints, ref closedPoints);

        foreach(WayPoint point in Neighbours)
        {
            if (point.IsClosed || point.IsOpen || !point.gameObject.activeSelf) continue;

            point.Open(this, endPoint,  ref openPoints);
        }

        if(openPoints.Count == 0)
        {
            print("Oh cong la target est pas dans le graph cagole");
            return;
        }

        WayPoint bestPoint = null;
        foreach (WayPoint point in openPoints)
        {
            if (bestPoint == null) bestPoint = point;
            else if (point.F < bestPoint.F) bestPoint = point;
        }

        bestPoint.TravelThrough(ref openPoints,ref closedPoints, ref shorterPath, endPoint, startPoint);
    }

    void Open(WayPoint formerPoint, WayPoint endPoint, ref List<WayPoint> openPoints)
    {
        IsOpen = true;

        openPoints.Add(this);

        FormerPoint = formerPoint;

        H = Vector2.Distance(transform.position, endPoint.transform.position);
        G ++;

        _SR.color = Color.green;
    }

    void Close(ref List<WayPoint> openPoints, ref List<WayPoint> closedPoints)
    {
        IsClosed = true;
        closedPoints.Add(this);
        if(openPoints.Contains(this)) openPoints.Remove(this);
        _SR.color = Color.blue;
    }

    public void ResetState()
    {
        FormerPoint = null;
        G = 0;
        H = 0;
        IsClosed = false;
        IsOpen = false;
        //_SR.color = _color;
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
/*;*/