using UnityEngine;

[System.Serializable]
public class PointBase
{
    public Vector3 position;
    public Vector3 eulerAngles;
    public Quaternion Rotation { get { return Quaternion.Euler(eulerAngles); } set { eulerAngles = value.eulerAngles; } }

    public PointBase() { }
    public PointBase(Vector3 position, Quaternion rotation) { this.position = position; Rotation = rotation; }
    public PointBase(Vector3 position, Vector3 eulerAngles) { this.position = position; this.eulerAngles = eulerAngles; }
}

