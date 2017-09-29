using System.Collections.Generic;
using UnityEngine;

public class PointCubeArea : PointAreaBase
{
    [System.Serializable]
    public class PointCubeAreaAppearance
    {
        public Color EdgeColor = Color.white;
    }

    public PointCubeAreaAppearance apperance = new PointCubeAreaAppearance();
    [SerializeField,HideInInspector]
    private Vector3 size = Vector3.one;

    public override Point GetRandomPointInArea()
    {
        return null;
    }

    public override Point GetRandomPointInEdge()
    {
        return null;
    }

    public override Vector3 GetRandomPositionInArea()
    {
        return new Vector3(Random.Range(0, size.x), Random.Range(0, size.y), Random.Range(0, size.z));
    }

    public override Vector3 GetRandomPositionInEdge()
    {
        return new Vector3();
    }
}
