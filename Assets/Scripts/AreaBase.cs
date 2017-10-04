using UnityEngine;

public abstract class AreaBase : MonoBehaviour
{
    public enum OrientationType { Same, Rule, Random, RandomX, RandomY, RandomZ, }
    public OrientationType orientationType;
    public Vector3 orientationEulerAngle;
    public Quaternion orientationAngle { get { return Quaternion.Euler(orientationEulerAngle); } set { orientationEulerAngle = value.eulerAngles; } }

    /// <summary>
    /// 通过位置偏移量计算旋转角
    /// </summary>
    /// <param name="localPos">相对位置</param>
    public virtual Quaternion GetAngleByPosition(Vector3 localPos)
    {
        Quaternion angle = Quaternion.identity;
        switch (orientationType)
        {
            case OrientationType.Same:
                angle = orientationAngle;
                break;
            case OrientationType.Rule:
                angle = orientationAngle * Quaternion.LookRotation(localPos);
                break;
            case OrientationType.Random:
                angle = Random.rotation;
                break;
            case OrientationType.RandomX:
                angle = Quaternion.Euler(Random.Range(0f, 360f), 0, 0);
                break;
            case OrientationType.RandomY:
                angle = Quaternion.Euler(0, Random.Range(0f, 360f), 0);
                break;
            case OrientationType.RandomZ:
                angle = Quaternion.Euler(0, 0, Random.Range(0f, 360f));
                break;
            default:
                Debug.LogError("Get Error Qrientation.");
                break;
        }
        return GetWorldSpaceRotation(angle);
    }

    /// <summary>
    /// 获取区域内随机位置
    /// </summary>
    public abstract Vector3 GetRandomPositionInArea();

    /// <summary>
    /// 获取边缘上的随机位置
    /// </summary>
    public abstract Vector3 GetRandomPositionInEdge();

    /// <summary>
    /// 获取区域内随机的点
    /// </summary>
    public virtual Point GetRandomPointInArea()
    {
        Vector3 pos = GetRandomPositionInArea();
        return new Point(pos, GetAngleByPosition(GetLocalSpacePosition(pos)));
    }

    /// <summary>
    /// 获取边缘上随机的点
    /// </summary>
    public virtual Point GetRandomPointInEdge()
    {
        Vector3 pos = GetRandomPositionInEdge();
        return new Point(pos, GetAngleByPosition(GetLocalSpacePosition(pos)));
    }

    /// <summary>
    /// 获取世界空间下点的位置
    /// </summary>
    protected virtual Vector3 GetWorldSpacePosition(Vector3 pos)
    {
        return transform.TransformPoint(pos);
    }

    /// <summary>
    /// 获取本地空间下点的位置
    /// </summary>
    protected virtual Vector3 GetLocalSpacePosition(Vector3 pos)
    {
        return transform.InverseTransformPoint(pos);
    }

    /// <summary>
    /// 获取世界空间下点的旋转
    /// </summary>
    protected virtual Quaternion GetWorldSpaceRotation(Quaternion rotation)
    {
        return transform.rotation * rotation;
    }

}