using UnityEngine;

public abstract class PointAreaBase : MonoBehaviour
{
    public enum OrientationType { Same, Random,RandomY, Rule }
    public OrientationType orientationType;
    public Quaternion subAngle;

    public virtual Quaternion GetAngleByPosition(Vector3 offset)
    {
        switch (orientationType)
        {
            case OrientationType.Same:
                return subAngle;
            case OrientationType.Random:
                return Random.rotation;
            case OrientationType.RandomY:
                return Quaternion.Euler(0, Random.Range(0f,360f), 0);
            case OrientationType.Rule:
                return Quaternion.Euler(subAngle * offset);
        }
        Debug.LogError("Get Error Qrientation.");
        return Quaternion.identity;
    }

    public abstract Vector3 GetRandomPositionInArea();
    public abstract Vector3 GetRandomPositionInEdge();
    public abstract Point GetRandomPointInArea();
    public abstract Point GetRandomPointInEdge();
}