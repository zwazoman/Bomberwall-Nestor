using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class AIController : MonoBehaviour
{
    public event Action OnStep;

    public WayPoint Destination;

    [SerializeField] float _moveSpeed = 6;
    [SerializeField] Vector2Int specificBombingSpot;

    Move _move;
    BombsHandler _bombsHandler;
    AStar _aStar;
    Stack<WayPoint> _destinationPath;

    Task _currentTask = null;
    bool _isWaiting = true;

    private void Awake()
    {
        TryGetComponent<Move>(out _move);
        TryGetComponent<BombsHandler>(out _bombsHandler);
        TryGetComponent<AStar>(out _aStar);
    }

    private void Start()
    {
        transform.position = new Vector2(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y));
    }

    private void Update()
    {
        if (Destination == null) return;
        if(_currentTask == null || _currentTask.IsCompleted)
        {
            if (transform.position == Destination.transform.position)
            {
                _currentTask = null;
                return;
            }
            _currentTask = _move.StartMoving(_destinationPath.Pop().transform.position, _moveSpeed);
            OnStep?.Invoke();
        }
    }

    public void SetDestination(WayPoint target)
    {
        Destination = target;
        Vector2Int posToVectorInt = new Vector2Int((int)transform.position.x, (int)transform.position.y);
        WayPoint currentPoint = GraphMaker.Instance.PointDict[posToVectorInt].GetComponent<WayPoint>(); // point du graph correspondant à la position du gameObject
        _destinationPath = _aStar.FindBestPath(currentPoint, Destination); // fais la tambouille et parcours le graph
    }

    public Vector2Int FindClosest(List<Vector2> points)
    {
        Vector2 closestPoint = Vector2.positiveInfinity;
        foreach(Vector2 point in points)
        {
            float distanceToClosest = Vector2.Distance(transform.position, closestPoint);
            float distanceToPoint = Vector2.Distance(transform.position, point);
            if(distanceToPoint < distanceToClosest) closestPoint = point;
        }
        return new Vector2Int((int)closestPoint.x, (int)closestPoint.y);
    }


}
