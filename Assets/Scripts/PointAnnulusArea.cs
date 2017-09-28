using UnityEngine;

public class PointAnnulusArea : PointAreaBase
{
    public PointAnnulusAreaAppearance appearance = new PointAnnulusAreaAppearance();
    [SerializeField, HideInInspector]
    private float minRadius;
    [SerializeField, HideInInspector]
    private float maxRadius;

    public float MinRadius { get { return minRadius; } set { minRadius = Mathf.Max(0, Mathf.Min(value, maxRadius)); } }
    public float MaxRadius { get { return maxRadius; } set { maxRadius = Mathf.Max(value, minRadius); } }


    public Vector3 GetRandomPositionByRadius(float radius)
    {
        float angle = Random.Range(0, 360);
        return new Vector3(radius * Mathf.Cos(angle), 0, radius * Mathf.Sin(angle));
    }

    public override Vector3 GetRandomPositionInArea()
    {
        return GetRandomPositionByRadius(Random.Range(minRadius, maxRadius));
    }

    public override Vector3 GetRandomPositionInEdge()
    {
        return GetRandomPositionByRadius(maxRadius);
    }

    public Vector3 GetRandomPositionInMinEdge()
    {
        return GetRandomPositionByRadius(minRadius);
    }

    public override Point GetRandomPointInArea()
    {
        Vector3 pos = GetRandomPositionInArea();
        return new Point(pos, GetAngleByPosition(pos));
    }

    public override Point GetRandomPointInEdge()
    {
        Vector3 pos = GetRandomPositionInEdge();
        return new Point(pos, GetAngleByPosition(pos));
    }

    public Point GetRandomPointInMinEdge()
    {
        Vector3 pos = GetRandomPositionInMinEdge();
        return new Point(pos, GetAngleByPosition(pos));
    }
}
