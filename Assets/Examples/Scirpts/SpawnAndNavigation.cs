using System.Collections.Generic;
using UnityEngine;

public class SpawnAndNavigation : MonoBehaviour
{
    public Points points;
    public GameObject prefab;
    public bool isRandom;
    public float changeNextWayPointTime = 3f;

    private List<ChickenManager> chickenList = new List<ChickenManager>();
    private float elapsed = 3f;
    private Point nextPoint;

    private void Start()
    {
        for (int i = 0; i < points.Count; i++)
        {
            chickenList.Add(Instantiate(prefab, transform).GetComponent<ChickenManager>());
            chickenList[i].transform.position = points.GetWorldPosition(points[i]);
            chickenList[i].transform.rotation = points.GetWorldRotation(points[i]);
        }
    }

    private void Update()
    {
        elapsed -= Time.deltaTime;
        if (elapsed <= 0)
        {
            nextPoint = points.GetNextPoint();
            for (int i = 0; i < chickenList.Count; i++)
            {
                if (isRandom)
                    chickenList[i].SetNextWayPoint(points.GetWorldPosition(points.GetRandomPoint()));
                else if (nextPoint != null)
                    chickenList[i].SetNextWayPoint(points.GetWorldPosition(nextPoint));
            }
            elapsed = changeNextWayPointTime;
        }
    }

}
