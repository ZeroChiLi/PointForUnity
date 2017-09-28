using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PointAnnulusArea))]
public class PointAnnulusAreaEditor : Editor
{
    private PointAnnulusArea Target;

    private void OnEnable()
    {
        Target = target as PointAnnulusArea;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        DrawPropertiesExcluding(serializedObject, "m_Script");
        GetProperties();
        serializedObject.ApplyModifiedProperties();
    }

    private void GetProperties()
    {
        Target.MinRadius = EditorGUILayout.FloatField("MinRadius", Target.MinRadius);
        Target.MaxRadius = EditorGUILayout.FloatField("MaxRadius", Target.MaxRadius);
    }

    private void OnSceneGUI()
    {
        Matrix4x4 mOld = Handles.matrix;
        Color colorOld = Handles.color;

        DrawAnnulusArea();
        DrawScaleHandle();

        Handles.color = colorOld;
        Handles.matrix = mOld;
    }

    private void DrawAnnulusArea()
    {
        Handles.color = Target.appearance.outerCircleColor;
        Handles.DrawSolidDisc(Target.transform.position, Target.transform.up, Target.MaxRadius);
        Handles.color = Target.appearance.innerCircleColor;
        Handles.DrawSolidDisc(Target.transform.position, Target.transform.up, Target.MinRadius);
    }

    private void DrawScaleHandle()
    {
        Handles.color = Target.appearance.edgeColor;
        Handles.CircleHandleCap(0, Target.transform.position, Quaternion.LookRotation(Target.transform.up), Target.MinRadius, EventType.Repaint);
        Handles.CircleHandleCap(0, Target.transform.position, Quaternion.LookRotation(Target.transform.up), Target.MaxRadius, EventType.Repaint);
    }
}
