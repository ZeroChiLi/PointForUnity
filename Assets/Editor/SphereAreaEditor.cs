using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SphereArea))]
public class SphereAreaEditor : AnnulusAreaEditor
{
    static private Matrix4x4 oldGizmoMatrix;
    static private Color oldGizmoColor;

    private void OnSceneGUI()
    {
        mOld = Handles.matrix;
        colorOld = Handles.color;

        DrawMinMaxScaleHandle();

        Handles.color = colorOld;
        Handles.matrix = mOld;
    }


    [DrawGizmo(GizmoType.Selected | GizmoType.Active)]
    static void RenderBoxGizmo(SphereArea sphereArea, GizmoType gizmoType)
    {
        oldGizmoColor = Gizmos.color;
        oldGizmoMatrix = Gizmos.matrix;

        Gizmos.color = sphereArea.enabled ? sphereArea.appearance.innerColor : Color.clear;
        Gizmos.DrawSphere(sphereArea.transform.position, sphereArea.MinRadius);

        Gizmos.color = sphereArea.enabled ? sphereArea.appearance.outerColor : Color.clear;
        Gizmos.DrawSphere(sphereArea.transform.position, sphereArea.MaxRadius);

        Gizmos.matrix = oldGizmoMatrix;
        Gizmos.color = oldGizmoColor;

    }
}
