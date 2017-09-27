using UnityEngine;

public class PointAnnulusArea : PointAreaBase
{
    [HideInInspector]
    private float minRadius = 5f;
    [HideInInspector]
    private float maxRadius = 10f;

    public float MinRadius { get { return minRadius; } }
    public float MaxRadius { get { return maxRadius; } }

    private float LerpFromAnnulus(float value)
    {
        return value >= 0 ? Mathf.Lerp(MinRadius, MaxRadius, value) : Mathf.Lerp(-MaxRadius, -MinRadius, value);
    }

    public override Vector3 GetRandomPositionInArea()
    {
        Vector2 randomPos = Random.insideUnitCircle;
        return new Vector3(LerpFromAnnulus(randomPos.x), 0, LerpFromAnnulus(randomPos.y));
    }

    public override Vector3 GetRandomPositionInEdge()
    {
        float x = Random.value;
        x *= Random.value > 0.5 ? 1 : -1;
        return new Vector3(x * maxRadius, 0, Mathf.Sqrt(1 - x * x) * maxRadius);
    }

    public Vector3 GetRandomPositionInMinEdge()
    {
        float x = Random.value;
        x *= Random.value > 0.5 ? 1 : -1;
        return new Vector3(x * minRadius, 0, Mathf.Sqrt(1 - x * x) * minRadius);
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
