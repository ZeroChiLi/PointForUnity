using UnityEngine;

public class AnnulusArea : AreaBase
{
    [System.Serializable]
    public class Appearance
    {
        public Color innerColor = new Color(0, 0, 0, 0.2f);
        public Color outerColor = new Color(1, 1, 1, 0.2f);
        public Color innerEdgeColor = Color.cyan;
        public Color outerEdgeColor = Color.magenta;
    }

    public Appearance appearance = new Appearance();
    [SerializeField, HideInInspector]
    protected float minRadius = 5f;
    [SerializeField, HideInInspector]
    protected float maxRadius = 10f;
    [Range(0.01f, 360f)]
    public float angle = 60f;

    public float MinRadius { get { return minRadius; } set { minRadius = Mathf.Max(0, Mathf.Min(value, maxRadius)); } }
    public float MaxRadius { get { return maxRadius; } set { maxRadius = Mathf.Max(value, minRadius); } }

    public Vector3 GetRandomPositionByRadius(float radius)
    {
        float f = Random.Range(0, angle) * Mathf.PI / 180;
        return GetWorldSpacePosition(new Vector3(radius * Mathf.Cos(f), 0, radius * Mathf.Sin(f)));
    }

    public override Vector3 GetRandomPositionInArea()
    {
        return GetRandomPositionByRadius(Random.Range(minRadius, maxRadius));
    }

    public override Vector3 GetRandomPositionInEdge()
    {
        return GetRandomPositionByRadius(maxRadius);
    }

    public virtual Vector3 GetRandomPositionInMinEdge()
    {
        return GetRandomPositionByRadius(minRadius);
    }

    public virtual Point GetRandomPointInMinEdge()
    {
        Vector3 pos = GetRandomPositionInMinEdge();
        return new Point(pos,GetAngleByPosition(GetLocalSpacePosition(pos)));
    }

    protected override Vector3 GetWorldSpacePosition(Vector3 pos)
    {
        return transform.rotation * pos + transform.position;
    }

    protected override Vector3 GetLocalSpacePosition(Vector3 pos)
    {
        return Quaternion.Inverse(transform.rotation) * (pos - transform.position);
    }
}
