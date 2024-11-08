using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click : MonoBehaviour
{
    PlayerMove _playerMove;

    private void Awake()
    {
        _playerMove = GetComponent<PlayerMove>();
    }

    void Update()
    {
        // tout droit
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Vector2 targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //    _playerMove.Move(targetPos);
        //}
        if (Input.GetMouseButtonDown(0))
        {
            WayPoint pathEnd = Physics2D.OverlapCircle(Camera.main.ScreenToWorldPoint(Input.mousePosition), 1).GetComponent<WayPoint>();
        }
    }
}
