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
        if (_isWaiting)
        {
            PickupClosestBombPickup();
        }
    }

    async void PickupClosestBombPickup()
    {
        if(God.Instance.BombPickups.Count == 0)
        {
            _isWaiting = true;
            return;
        }
        _isWaiting = false;

        Vector2Int closestBombPickupPos = FindClosestBombPickup();

        WayPoint targetPoint = GraphMaker.Instance.PointDict[closestBombPickupPos].GetComponent<WayPoint>(); // point du graph correspondant à la position du pickup le plus proche
        Vector2Int posToVectorInt = new Vector2Int((int) transform.position.x, (int) transform.position.y);
        WayPoint currentPoint = GraphMaker.Instance.PointDict[posToVectorInt].GetComponent<WayPoint>(); // point du graph correspondant à la position du gameObject


        // a ranger dans une func a part ? chiant pour l'async ?
        Stack<WayPoint> bestPath = _aStar.FindBestPath(currentPoint, targetPoint); // fais la tambouille et parcours le graph

        while (bestPath.Count > 0)
        {
            Vector2Int searchClosest = FindClosestBombPickup();
            if (searchClosest != closestBombPickupPos && !_bombsHandler.HasABomb)
            {
                PickupClosestBombPickup();
                return;
            }

            WayPoint nextPoint = bestPath.Pop();
            //attendre la fin de la task
            _currentTask = _move.StartMoving(nextPoint.transform.position, _moveSpeed); // déplace le joueur jusqu'au pickup en passant par tous les former points du best path
            await _currentTask;
        }
        _currentTask = null;
        if (_bombsHandler.HasABomb) ExplodeTheGreatWallBetweenAmericaAndMexicoVoteTrump(); else PickupClosestBombPickup();
    }

    async void ExplodeTheGreatWallBetweenAmericaAndMexicoVoteTrump()
    {
        WayPoint targetPoint = GraphMaker.Instance.PointDict[new Vector2Int(16, -8)].GetComponent<WayPoint>();
        Vector2Int posToVectorInt = new Vector2Int((int)transform.position.x, (int)transform.position.y);
        WayPoint currentPoint = GraphMaker.Instance.PointDict[posToVectorInt].GetComponent<WayPoint>(); // point du graph correspondant à la position du gameObject

        Stack<WayPoint> bestPath = _aStar.FindBestPath(currentPoint, targetPoint); // fais la tambouille et parcours le graph

        while (bestPath.Count > 0)
        {
            WayPoint nextPoint = bestPath.Pop();
            //attendre la fin de la task
            _currentTask = _move.StartMoving(nextPoint.transform.position, _moveSpeed); // déplace le joueur jusqu'au pickup en passant par tous les former points du best path
            await _currentTask;
        }
        _currentTask = null;

        _bombsHandler.DeployBomb();

        PickupClosestBombPickup();

    }

    /// <summary>
    /// parcours les points autours des murs cassables, trouve le plus proche, y déplace le bot et lui fait poser une bombe
    /// </summary>
    void ExplodeClosestBreakable()
    {
        print("gros shlagar");
    }

    Vector2Int FindClosestBombPickup()
    {
        List<Vector2> bombPickupPos = new List<Vector2>();
        foreach (BombPickup BombPickup in God.Instance.BombPickups)
        {
            bombPickupPos.Add(BombPickup.transform.position);
        }
        Vector2Int closestBombPickupPos = FindClosest(bombPickupPos); //trouver le pickup de bombe le plus proche
        return closestBombPickupPos;
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
