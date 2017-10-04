using System.Collections.Generic;
using UnityEngine;

public class SpawnAndNavigation : MonoBehaviour
{
    public Points points;
    public GameObject prefab;
    public bool isRandom;
    public float changeNextWayPointTime = 3f;

    private List<ChickenManager> chickenList = new List<ChickenManager>();
    private float elapsed = 1f;
    private Point nextPoint;

    private void Start()
    {
        Point worldSpacePoint;
        for (int i = 0; i < points.Count; i++)
        {
            chickenList.Add(Instantiate(prefab, transform).GetComponent<ChickenManager>());
            worldSpacePoint = points.GetWorldSpacePoint(i);
            chickenList[i].transform.position = worldSpacePoint.position;
            chickenList[i].transform.rotation = worldSpacePoint.rotation;
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
                    chickenList[i].SetNextWayPoint(points.GetRandomPoint().position);
                else if (nextPoint != null)
                    chickenList[i].SetNextWayPoint(nextPoint.position);
            }
            elapsed = changeNextWayPointTime;
        }
    }

}
