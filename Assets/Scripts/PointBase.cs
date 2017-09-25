using UnityEngine;

[System.Serializable]
public class PointBase
{
    public Vector3 position;
    public Vector3 rotate;
    public Quaternion rotation;
    public Vector3 eulerAngles { get { return rotation.eulerAngles; } set { rotation.eulerAngles = value; } }

    public PointBase() { }
    public PointBase(Vector3 position, Quaternion rotation) { this.position = position; this.rotation = rotation; }
    public PointBase(Vector3 position, Vector3 eulerAngles) { this.position = position; this.eulerAngles = eulerAngles; }
}

