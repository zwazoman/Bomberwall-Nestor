using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar : MonoBehaviour
{
    [SerializeField] WayPoint _startPoint;
    public WayPoint EndPoint;

    private void Start()
    {
        ASTAR(_startPoint, EndPoint);
    }
    void ASTAR(WayPoint startPoint, WayPoint endPoint)
    {
        List<WayPoint> openWayPoints = new List<WayPoint>();

        startPoint.TravelThrough(ref openWayPoints, endPoint);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            EndPoint = Physics2D.OverlapCircle(Camera.main.ScreenToWorldPoint(Input.mousePosition), 1).gameObject.GetComponent<WayPoint>();
        }
    }
}
