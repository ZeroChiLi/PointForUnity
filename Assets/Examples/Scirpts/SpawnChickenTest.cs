using System.Collections.Generic;
using UnityEngine;

public class SpawnChickenTest : MonoBehaviour 
{
    public bool inEgde;
    public bool autoMove;
    public PointAreaBase area;
    public GameObject chickenPrefab;
    public int amount = 10;
    public float changeTime = 3f;

    private List<ChickenManager> objList = new List<ChickenManager>();
    private float elapsed;
    private Point temP;

    private void Start()
    {
        elapsed = changeTime;
        for (int i = 0; i < amount; i++)
        {
            objList.Add(Instantiate(chickenPrefab).GetComponent<ChickenManager>());
            ResetChicken(objList[i]);
        }
    }

    private void Update()
    {
        elapsed -= Time.deltaTime;
        if (elapsed <= 0)
        {
            for (int i = 0; i < objList.Count; i++)
                ResetChicken(objList[i]);
            elapsed = changeTime;
        }
    }

    private void ResetChicken(ChickenManager c)
    {
        temP = inEgde ? area.GetRandomPointInEdge().GetWorldSpacePoint(transform) : area.GetRandomPointInArea().GetWorldSpacePoint(transform);
        c.transform.position = temP.position;
        c.transform.rotation = temP.rotation;
        c.autoMoveForward = autoMove;
    }


}
