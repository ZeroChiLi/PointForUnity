using System.Collections.Generic;
using UnityEngine;

public class SpawnFromAnnulusArea : MonoBehaviour 
{
    public PointAnnulusArea area;
    public GameObject prefab;
    public int amount = 10;

    private List<GameObject> objList = new List<GameObject>();

    private void Start()
    {
        Point p;
        for (int i = 0; i < amount; i++)
        {
            objList.Add(Instantiate(prefab));
            //p = area.GetRandomPointInArea()/*.GetWorldSpacePoint(transform)*/;
            objList[i].transform.position = area.GetRandomPositionInArea();
            //objList[i].transform.rotation = p.rotation;
            //Debug.Log(p.position + "  " + p.eulerAngles);
        }
    }


}
