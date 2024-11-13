using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class BotBehaviour : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 6;

    Move _move;
    BombsHandler _bombsHandler;
    AStar _aStar;

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
        print("joue");
        God.Instance.OnSummonBomb += HandleWaiting;
        transform.position = new Vector2(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y));
    }

    public void HandleWaiting()
    {
        print("joue");
        if (_isWaiting)
        {
            GetClosestBombPickup();
        }
        else print("not waiting");
    }

    async void GetClosestBombPickup()
    {
        if(God.Instance.BombPickups.Count == 0)
        {
            print("wait");
            _isWaiting = true;
            return;
        }
        _isWaiting = false;
        List<Vector2> bombPickupPos = new List<Vector2>();
        foreach(BombPickup BombPickup in God.Instance.BombPickups)
        {
            bombPickupPos.Add(BombPickup.transform.position);
        }
        Vector2Int closestBombPickupPos = FindClosest(bombPickupPos); //trouver le pickup de bombe le plus proche

        WayPoint targetPoint = GraphMaker.Instance.PointDict[closestBombPickupPos].GetComponent<WayPoint>(); // point du graph correspondant � la position du pickup le plus proche
        Vector2Int posToVectorInt = new Vector2Int((int) transform.position.x, (int) transform.position.y);
        WayPoint currentPoint = GraphMaker.Instance.PointDict[posToVectorInt].GetComponent<WayPoint>(); // point du graph correspondant � la position du gameObject


        // a ranger dans une func a part ? chiant pour l'async ?
        Stack<WayPoint> bestPath = _aStar.FindBestPath(currentPoint, targetPoint); // fais la tambouille et parcours le graph

        while (bestPath.Count > 0)
        {
            WayPoint nextPoint = bestPath.Pop();
            //attendre la fin de la task
            _currentTask = _move.StartMoving(nextPoint.transform.position, _moveSpeed); // d�place le joueur jusqu'au pickup en passant par tous les former points du best path
            await _currentTask;
        }
        _currentTask = null;
        if (_bombsHandler.HasABomb) ExplodeTheGreatWallBetweenAmericaAndMexicoVoteTrump(); else GetClosestBombPickup();
    }

    async void ExplodeTheGreatWallBetweenAmericaAndMexicoVoteTrump()
    {
        WayPoint targetPoint = GraphMaker.Instance.PointDict[new Vector2Int(16, 0)].GetComponent<WayPoint>();
        Vector2Int posToVectorInt = new Vector2Int((int)transform.position.x, (int)transform.position.y);
        WayPoint currentPoint = GraphMaker.Instance.PointDict[posToVectorInt].GetComponent<WayPoint>(); // point du graph correspondant � la position du gameObject

        Stack<WayPoint> bestPath = _aStar.FindBestPath(currentPoint, targetPoint); // fais la tambouille et parcours le graph

        int cpt = 0;
        while (bestPath.Count > 0)
        {
            WayPoint nextPoint = bestPath.Pop();
            //attendre la fin de la task
            _currentTask = _move.StartMoving(nextPoint.transform.position, _moveSpeed); // d�place le joueur jusqu'au pickup en passant par tous les former points du best path
            await _currentTask;
        }
        _currentTask = null;

        _bombsHandler.DeployBomb();

        GetClosestBombPickup();

    }

    /// <summary>
    /// parcours les points autours des murs cassables, trouve le plus proche, y d�place le bot et lui fait poser une bombe
    /// </summary>
    void ExplodeClosestBreakable()
    {
        print("gros shlagar");
    }

    Vector2Int FindClosest(List<Vector2> points)
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
