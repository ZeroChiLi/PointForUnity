using UnityEngine;

public abstract class PointAreaBase : Point
{
    public enum Orientation { Same, Random, Rule }
    public Orientation orientation;
    public Quaternion subAngle;

    public virtual Quaternion GetAngleByPosition(Vector3 pos)
    {
        switch (orientation)
        {
            case Orientation.Same:
                return subAngle;
            case Orientation.Random:
                return Random.rotation;
            case Orientation.Rule:
                return Quaternion.Euler(subAngle * pos);
            default:
                Debug.LogError("Get Error Qrientation.");
                return Quaternion.identity;
        }
    }

    public abstract Vector3 GetRandomPositionInArea();
    public abstract Vector3 GetRandomPositionInEgde();
    public abstract Point GetRandomPointInArea();
    public abstract Point GetRandomPointInEgde();
}