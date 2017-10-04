using System;
using System.Collections.Generic;
using UnityEngine;

public class CubeArea : AreaBase
{
    [System.Serializable]
    public class Appearance
    {
        public Color edgeColor = Color.white;
        public Color surfaceColor = new Color(1, 1, 1, 0.5f);
    }

    public Appearance appearance = new Appearance();
    [SerializeField, HideInInspector]
    private Vector3 size = Vector3.one * 10f;
    public Vector3 Size { get { return size; } set { size = new Vector3(Mathf.Max(0, value.x), Mathf.Max(0, value.y), Mathf.Max(0, value.z)); } }
    public Vector3 Extents { get { return Size / 2f; } set { Size = value * 2f; } }

    public Vector3 ActualSize { get { return new Vector3(Size.x * transform.lossyScale.x, Size.y * transform.lossyScale.y, Size.z * transform.lossyScale.z); } }
    public Vector3 ActualExtents { get { return ActualSize / 2f; } }

    public override Vector3 GetRandomPositionInArea()
    {
        return GetWorldSpacePosition(new Vector3(RandomUtility.Extents(Extents.x), RandomUtility.Extents(Extents.y), RandomUtility.Extents(Extents.z)));
    }

    public override Vector3 GetRandomPositionInEdge()
    {
        Vector3 pos = Vector3.zero;
        switch (RandomUtility.Index(ActualSize.x, ActualSize.y, ActualSize.z))
        {
            case 0:
                pos =  new Vector3(RandomUtility.Extents(Extents.x), RandomUtility.sign * Extents.y, RandomUtility.sign * Extents.z);
                break;
            case 1:
                pos = new Vector3(RandomUtility.sign * Extents.x, RandomUtility.Extents(Extents.y), RandomUtility.sign * Extents.z);
                break;
            case 2:
                pos = new Vector3(RandomUtility.sign * Extents.x, RandomUtility.sign * Extents.y, RandomUtility.Extents(Extents.z));
                break;
        }
        return GetWorldSpacePosition(pos);
    }

    /// <summary>
    /// 获取表面上随机的位置
    /// </summary>
    public Vector3 GetRandomPositionInSurface()
    {
        Vector3 actualSize = ActualSize;
        Vector3 pos = Vector3.zero;
        switch (RandomUtility.Index(actualSize.y * actualSize.z, actualSize.x * actualSize.z, actualSize.x * actualSize.y))
        {
            case 0:
                pos = new Vector3(RandomUtility.sign * Extents.x, RandomUtility.Extents(Extents.y), RandomUtility.Extents(Extents.z));
                break;
            case 1:
                pos = new Vector3(RandomUtility.Extents(Extents.x), RandomUtility.sign * Extents.y, RandomUtility.Extents(Extents.z));
                break;
            case 2:
                pos = new Vector3(RandomUtility.Extents(Extents.x), RandomUtility.Extents(Extents.z), RandomUtility.sign * Extents.z);
                break;
        }
        return GetWorldSpacePosition(pos);
    }

    /// <summary>
    /// 获取表面上随机的点
    /// </summary>
    public Point GetRandomPointInSurface()
    {
        Vector3 pos = GetRandomPositionInSurface();
        return new Point(pos, GetAngleByPosition(GetLocalSpacePosition(pos)));
    }

}
