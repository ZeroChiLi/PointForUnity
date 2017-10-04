using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SphereArea))]
public class SphereAreaEditor : AnnulusAreaEditor
{
    static private Matrix4x4 oldGizmoMatrix;
    static private Color oldGizmoColor;

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        DrawPropertiesExcluding(serializedObject, "m_Script","angle");
        GetProperties();
        serializedObject.ApplyModifiedProperties();
        SceneView.RepaintAll();
    }

    protected new void OnSceneGUI()
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

        Gizmos.color = sphereArea.appearance.innerColor;
        Gizmos.DrawSphere(sphereArea.transform.position, sphereArea.MinRadius);

        Gizmos.color = sphereArea.appearance.outerColor;
        Gizmos.DrawSphere(sphereArea.transform.position, sphereArea.MaxRadius);

        Gizmos.color = sphereArea.appearance.innerEdgeColor;
        Gizmos.DrawWireSphere(sphereArea.transform.position, sphereArea.MinRadius);

        Gizmos.color = sphereArea.appearance.outerEdgeColor;
        Gizmos.DrawWireSphere(sphereArea.transform.position, sphereArea.MaxRadius);

        Gizmos.matrix = oldGizmoMatrix;
        Gizmos.color = oldGizmoColor;

    }
}
