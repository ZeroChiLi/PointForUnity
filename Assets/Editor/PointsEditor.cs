using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[CustomEditor(typeof(Points))]
internal class PointsEditor : Editor
{
    private Points Target { get { return target as Points; } }
    private static string[] excludeFields = null;

    private ReorderableList pointList;
    static private bool pointsExpanded;
    static bool preferHandleSelection = true;

    private void OnEnable()
    {
        pointList = null;
    }

    public override void OnInspectorGUI()
    {
        if (pointList == null)
            SetupPointList();

        serializedObject.Update();
        pointList.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
    }

    private void SetupPointList()
    {
        pointList = new ReorderableList(serializedObject, serializedObject.FindProperty("points"), true, true, true, true);
        pointList.elementHeight *= 2;

        pointList.drawHeaderCallback = (Rect rect) => { GUI.Label(rect, "Point List"); };
        pointList.drawElementCallback = DrawPointEditor;
    }

    private void DrawPointEditor(Rect rect, int index, bool selected, bool focused)
    {
        PointBase def = new PointBase();

        Vector2 numberDimension = GUI.skin.button.CalcSize(new GUIContent("999"));
        Vector2 labelDimension = GUI.skin.label.CalcSize(new GUIContent("Rotation "));
        float vSpace = 2;
        float hSpace = 3;

        SerializedProperty element = pointList.serializedProperty.GetArrayElementAtIndex(index);
        rect.y += vSpace / 2;

        Rect r = new Rect(rect.position, numberDimension);
        r.y += numberDimension.y - r.height / 2;
        Color color = GUI.color;
        // GUI.color = Target.m_Appearance.pathColor;
        if (GUI.Button(r, new GUIContent(index.ToString(), "Go to the waypoint in the scene view")))
        {
            pointList.index = index;
            //SceneView.lastActiveSceneView.pivot = Target.EvaluatePosition(index);
            //SceneView.lastActiveSceneView.size = 3;
            //SceneView.lastActiveSceneView.Repaint();
        }
        GUI.color = color;

        r = new Rect(rect.position, labelDimension);
        r.x += hSpace + numberDimension.x;
        EditorGUI.LabelField(r, "Position");
        r.x += hSpace + r.width;
        r.width = rect.width - (numberDimension.x + hSpace + r.width + hSpace);
        EditorGUI.PropertyField(r, element.FindPropertyRelative("position"), GUIContent.none);

        r = new Rect(rect.position, labelDimension);
        r.y += numberDimension.y + vSpace;
        r.x += hSpace + numberDimension.x; r.width = labelDimension.x;
        EditorGUI.LabelField(r, "Rotation");
        r.x += hSpace + r.width;
        r.width = rect.width - (numberDimension.x + hSpace + r.width + hSpace );
        EditorGUI.PropertyField(r, element.FindPropertyRelative("rotate"), GUIContent.none);
        r.x += r.width + hSpace;
    }
}
