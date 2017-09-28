using System.Collections.Generic;
using UnityEngine;

public class SpawnFromAnnulusArea : MonoBehaviour 
{
    public PointAnnulusArea area;
    public GameObject prefab;
    public int amount = 10;
    public float changeTime = 3f;

    private List<GameObject> objList = new List<GameObject>();
    private float elapsed;
    private Point temP;

    private void Start()
    {
        elapsed = changeTime;
        for (int i = 0; i < amount; i++)
        {
            objList.Add(Instantiate(prefab));
            temP = area.GetRandomPointInArea().GetWorldSpacePoint(transform);
            objList[i].transform.position = temP.position;
            objList[i].transform.rotation = temP.rotation;
        }
    }

    private void Update()
    {
        elapsed -= Time.deltaTime;
        if (elapsed <= 0)
        {
            for (int i = 0; i < objList.Count; i++)
            {
                temP = area.GetRandomPointInArea().GetWorldSpacePoint(transform);
                objList[i].transform.position = temP.position;
                objList[i].transform.rotation = temP.rotation;
            }
            elapsed = changeTime;
        }
    }


}
