using UnityEngine;

[System.Serializable]
public class Point
{
    public Vector3 position;
    public Vector3 eulerAngles;
    public Quaternion rotation { get { return Quaternion.Euler(eulerAngles); } set { eulerAngles = value.eulerAngles; } }

    public Point() { }
    public Point(Vector3 position, Quaternion rotation) { this.position = position; this.rotation = rotation; }
    public Point(Vector3 position, Vector3 eulerAngles) { this.position = position; this.eulerAngles = eulerAngles; }


    /// <summary>
    /// 获取点位置的世界坐标
    /// </summary>
    /// <param name="parent">父对象</param>
    /// <returns>点位置的世界坐标</returns>
    public Vector3 GetWorldPosition(Transform parent)
    {
        return parent.TransformPoint(position);
    }

    /// <summary>
    /// 获取点旋转的世界坐标
    /// </summary>
    /// <param name="parent">父对象</param>
    /// <returns>点旋转的世界坐标</returns>
    public Quaternion GetWorldRotation(Transform parent)
    {
        return parent.rotation * rotation;
    }

    /// <summary>
    /// 获取世界坐标下的点
    /// </summary>
    /// <param name="parent">点的父对象</param>
    /// <returns>世界坐标下的点</returns>
    public Point GetWorldSpacePoint(Transform parent)
    {
        return new Point(GetWorldPosition(parent), GetWorldRotation(parent));
    }
}

