using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class God : MonoBehaviour
{
    [SerializeField] int _maxBombs;

    List<GameObject> ennemyBombs = new List<GameObject>();

    private void Start()
    {
        for(int i =0;  i < _maxBombs; i++)
        {
            //summon a gauche
            SummonBombPickup(new Vector2(Random.Range(-16, 8), Random.Range(-8, 9)));
            //summon a droite
            SummonBombPickup(new Vector2(Random.Range(9, 17), Random.Range(9, -8)));
        }
    }

    void SummonBombPickup(Vector2 spawnPos)
    {
        
    }
}
