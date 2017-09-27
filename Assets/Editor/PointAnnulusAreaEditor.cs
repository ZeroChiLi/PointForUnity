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
        DrawPropertiesExcluding(serializedObject, "m_Script", "subAngle", "radius");
        serializedObject.ApplyModifiedProperties();
    }


}
