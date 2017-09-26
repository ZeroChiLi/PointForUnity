using System.Collections.Generic;
using UnityEngine;

public class Points : MonoBehaviour 
{
    public PointAppearance apperance = new PointAppearance();
    public PointBase[] points = new PointBase[0];

    public PointBase this[int index] {get{ return points[index]; } set { points[index] = value; } }

    private void OnDrawGizmos()
    {
        if (!apperance.showGizoms)
            return;
        Gizmos.color = apperance.pointColor;
        Gizmos.matrix = transform.localToWorldMatrix;
        for (int i = 0; i < points.Length; i++)
            Gizmos.DrawSphere(points[i].position, apperance.pointSize * 2f);
    }
}
