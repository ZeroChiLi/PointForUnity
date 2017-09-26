using UnityEngine;

[System.Serializable]
public sealed class PointAppearance 
{
    [Range(0.1f, 0.5f)]
    public float pointSize = 0.3f;
    public Color pointColor = Color.white;
    [Range(10,20)]
    public int indexFontSize = 13;
    public Color indexFontColor = Color.black;
    [Range(1,20)]
    public float axisLength = 10f;
}
