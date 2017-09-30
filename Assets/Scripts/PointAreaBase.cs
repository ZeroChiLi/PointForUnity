using UnityEngine;

public abstract class PointAreaBase : MonoBehaviour
{
    public enum OrientationType { Same, Rule, Random, RandomX, RandomY, RandomZ, }
    public OrientationType orientationType;
    public Vector3 orientationEulerAngle;
    public Quaternion orientationAngle { get { return Quaternion.Euler(orientationEulerAngle); } set { orientationEulerAngle = value.eulerAngles; } }

    /// <summary>
    /// 通过位置偏移量计算旋转角
    /// </summary>
    /// <param name="offset">偏移量</param>
    public virtual Quaternion GetAngleByPosition(Vector3 offset)
    {
        switch (orientationType)
        {
            case OrientationType.Same:
                return orientationAngle;
            case OrientationType.Rule:
                return orientationAngle * Quaternion.LookRotation(offset);
            case OrientationType.Random:
                return Random.rotation;
            case OrientationType.RandomX:
                return Quaternion.Euler(Random.Range(0f, 360f), 0, 0);
            case OrientationType.RandomY:
                return Quaternion.Euler(0, Random.Range(0f, 360f), 0);
            case OrientationType.RandomZ:
                return Quaternion.Euler(0, 0, Random.Range(0f, 360f));
        }
        Debug.LogError("Get Error Qrientation.");
        return Quaternion.identity;
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
    public abstract Point GetRandomPointInArea();

    /// <summary>
    /// 获取边缘上随机的点
    /// </summary>
    public abstract Point GetRandomPointInEdge();
}