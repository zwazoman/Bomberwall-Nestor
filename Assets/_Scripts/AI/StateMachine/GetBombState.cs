using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetBombState : AIState
{
    public override void OnEnter()
    {
        FindClosestBombPickup();
    }

    public override void Update()
    {
        throw new System.NotImplementedException();
    }

    public override void Exit(AIState nextState)
    {
        Machine.TransitionTo(nextState);
    }

    void FindClosestBombPickup()
    {
        List<Vector2> bombPickupPos = new List<Vector2>();
        foreach (BombPickup BombPickup in God.Instance.BombPickups)
        {
            bombPickupPos.Add(BombPickup.transform.position);
        }
        Vector2Int closestBombPickupPos = Machine.Controller.FindClosest(bombPickupPos); //trouver le pickup de bombe le plus proche
        WayPoint closestBombPickup = GraphMaker.Instance.PointDict[closestBombPickupPos].GetComponent<WayPoint>(); // point du graph correspondant à la position du pickup le plus proche
        Machine.Controller.SetDestination(closestBombPickup);
    }

    public override bool CanEnter()
    {
        throw new System.NotImplementedException();
    }
}
