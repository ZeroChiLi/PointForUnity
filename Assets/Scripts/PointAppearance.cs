using UnityEngine;

[System.Serializable]
public sealed class PointAppearance 
{
    public bool showGizoms = true;
    public bool useScreenSize;
    public Color selectedColor = Color.yellow;
    [Range(0.1f, 1f)]
    public float pointSize = 0.6f;
    public Color pointColor = Color.white;
    [Range(10, 30)]
    public int indexFontSize = 20;
    public Color indexFontColor = Color.black;
    [Range(0.1f, 5f)]
    public float axisLength = 3f;
}
