using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PointCubeArea))]
public class PointCubeAreaEditor : Editor 
{
    private PointCubeArea Target;

    static private Matrix4x4 mOld;
    static private Color colorOld;

    private void OnEnable()
    {
        Target = target as PointCubeArea;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        DrawPropertiesExcluding(serializedObject, "m_Script");
        GetProperties();
        serializedObject.ApplyModifiedProperties();
        SceneView.RepaintAll();
    }

    private void GetProperties()
    {
        Target.Extents = EditorGUILayout.Vector3Field("Extents", Target.Extents);
    }

    [DrawGizmo(GizmoType.Selected | GizmoType.Active)]
    static void RenderBoxGizmo(PointCubeArea cubeArea, GizmoType gizmoType)
    {
        colorOld = Gizmos.color;
        mOld = Gizmos.matrix;

        Gizmos.matrix = cubeArea.transform.localToWorldMatrix;

        Gizmos.color = cubeArea.enabled ? cubeArea.apperance.surfaceColor : Color.clear;
        Gizmos.DrawCube(Vector3.zero, cubeArea.Size);

        Gizmos.color = cubeArea.apperance.edgeColor;
        Gizmos.DrawWireCube(Vector3.zero, cubeArea.Size);

        Gizmos.matrix = mOld;
        Gizmos.color = colorOld;

    }

}
