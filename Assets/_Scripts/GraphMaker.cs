using System.Collections.Generic;
using UnityEngine;

public class GraphMaker : MonoBehaviour
{
    [SerializeField] public Vector2Int StartPos; // get set
    [SerializeField] public Vector2Int EndPos; // get set

    [SerializeField] GameObject _waypointPrefab;
    [SerializeField] LayerMask _mask;
    
    public Dictionary<Vector2Int, GameObject> PointDict = new Dictionary<Vector2Int, GameObject>();

    //singleton
    private static GraphMaker instance;

    public static GraphMaker Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject("Graph Maker");
                instance = go.AddComponent<GraphMaker>();
            }
            return instance;
        }
    }

    public List<GameObject> ActivePoints = new List<GameObject>();

    private void Awake()
    {
        instance = this;


        int ypos = StartPos.y;
        int xpos = StartPos.x;
        for (int i = 0; i < EndPos.y * 2; i++)
        {
            for (int j = 0; j < EndPos.x * 2; j++)
            {
                Vector2Int spawnPos = new Vector2Int(xpos + j, ypos + i);
                PointDict.Add(spawnPos, Instantiate(_waypointPrefab, (Vector2)spawnPos, Quaternion.identity));
            }
        }
        foreach (GameObject point in PointDict.Values)
        {
            if (Physics2D.OverlapPoint(point.transform.position, _mask.value)) point.SetActive(false); else ActivePoints.Add(point);
            if (point.TryGetComponent<WayPoint>(out WayPoint wayPoint))
            {
                Vector2Int actualPos = new Vector2Int((int)point.transform.position.x, (int)point.transform.position.y);

                WayPoint rightPoint = null;
                WayPoint leftPoint = null;
                WayPoint BottomPoint = null;
                WayPoint TopPoint = null;
                if (actualPos.x != EndPos.x) rightPoint = PointDict[new Vector2Int(actualPos.x + 1, actualPos.y)].GetComponent<WayPoint>();
                if (actualPos.x != StartPos.x) leftPoint = PointDict[new Vector2Int(actualPos.x - 1, actualPos.y)].GetComponent<WayPoint>();
                if (actualPos.y != StartPos.y) BottomPoint = PointDict[new Vector2Int(actualPos.x, actualPos.y - 1)].GetComponent<WayPoint>();
                if (actualPos.y != EndPos.y) TopPoint = PointDict[new Vector2Int(actualPos.x, actualPos.y + 1)].GetComponent<WayPoint>();

                if (!wayPoint.Neighbours.Contains(rightPoint)) wayPoint.Neighbours.Add(rightPoint);
                if (!wayPoint.Neighbours.Contains(leftPoint)) wayPoint.Neighbours.Add(leftPoint);
                if (!wayPoint.Neighbours.Contains(BottomPoint)) wayPoint.Neighbours.Add(BottomPoint);
                if (!wayPoint.Neighbours.Contains(TopPoint)) wayPoint.Neighbours.Add(TopPoint);
            }

        }
    }

    public void ActivatePoint(GameObject pointObject)
    {
        if (ActivePoints.Contains(pointObject)) return;
        ActivePoints.Add(pointObject);
        pointObject.SetActive(true);
    }
}
 