using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[CustomEditor(typeof(Points))]
internal class PointsEditor : Editor
{
    //private Points Target { get { return target as Points; } }
    private Points Target;
    private static string[] excludeFields = null;
    private ReorderableList pointList;

    private Vector2 numberDimension;
    private Vector2 labelDimension;
    private const float vSpace = 2;
    private const float hSpace = 3;

    private void OnEnable()
    {
        Target = target as Points;
        pointList = null;
    }

    public override void OnInspectorGUI()
    {
        if (pointList == null)
            SetupPointList();
        serializedObject.Update();
        DrawPropertiesExcluding(serializedObject, "m_Script", "points");
        pointList.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
    }

    /// <summary>
    /// 配置好点对象列表
    /// </summary>
    private void SetupPointList()
    {
        pointList = new ReorderableList(serializedObject, serializedObject.FindProperty("points"), true, true, true, true);
        pointList.elementHeight *= 2;

        pointList.drawHeaderCallback = (Rect rect) => { GUI.Label(rect, "Point List"); };
        pointList.drawElementCallback = DrawPointElement;
    }

    /// <summary>
    /// 绘制点对象元素
    /// </summary>
    /// <param name="rect">矩形</param>
    /// <param name="index">索引</param>
    /// <param name="selected">是否选择了</param>
    /// <param name="focused">是否聚焦了</param>
    private void DrawPointElement(Rect rect, int index, bool selected, bool focused)
    {
        PointBase def = new PointBase();
        numberDimension = GUI.skin.button.CalcSize(new GUIContent("999"));
        labelDimension = GUI.skin.label.CalcSize(new GUIContent("Rotation "));

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
        r.width = rect.width - (numberDimension.x + hSpace + r.width + hSpace);
        EditorGUI.PropertyField(r, element.FindPropertyRelative("eulerAngles"), GUIContent.none);
        r.x += r.width + hSpace;
    }

    private void OnSceneGUI()
    {
        if (pointList == null)
            SetupPointList();

        if (Tools.current == Tool.Move)
        {
            Matrix4x4 mOld = Handles.matrix;
            Color colorOld = Handles.color;

            Handles.matrix = Target.transform.localToWorldMatrix;
            for (int i = 0; i < Target.points.Length; ++i)
            {
                DrawSelectionHandle(i);
                if (pointList.index == i)
                    DrawPositionControl(i);
            }
            Handles.color = colorOld;
            Handles.matrix = mOld;
        }
    }

    /// <summary>
    /// 绘制选择对象的控制
    /// </summary>
    /// <param name="i">点对象索引值</param>
    private void DrawSelectionHandle(int i)
    {
        if (Event.current.button == 0)
        {
            Vector3 pos = Target.points[i].position;
            float size = HandleUtility.GetHandleSize(pos) * Target.apperance.pointSize;
            Handles.color = Target.apperance.pointColor;
            if (Handles.Button(pos, Quaternion.identity, size, size, Handles.SphereHandleCap)
                && pointList.index != i)
            {
                pointList.index = i;
                InternalEditorUtility.RepaintAllViews();
            }

            Handles.BeginGUI();
            Vector2 labelSize = new Vector2(EditorGUIUtility.singleLineHeight * 2, EditorGUIUtility.singleLineHeight * 2);
            Vector2 labelPos = HandleUtility.WorldToGUIPoint(pos);
            labelPos.y -= labelSize.y / 2;
            labelPos.x -= labelSize.x / 2;
            GUILayout.BeginArea(new Rect(labelPos, labelSize));
            GUIStyle style = new GUIStyle();
            style.normal.textColor = Target.apperance.indexFontColor;
            style.fontSize = Target.apperance.indexFontSize;
            style.alignment = TextAnchor.MiddleCenter;
            GUILayout.Label(new GUIContent(i.ToString(), "Point " + i), style);
            GUILayout.EndArea();
            Handles.EndGUI();
        }
    }

    /// <summary>
    /// 绘制位置控制
    /// </summary>
    /// <param name="i">点对象索引值</param>
    private void DrawPositionControl(int i)
    {
        PointBase wp = Target.points[i];
        EditorGUI.BeginChangeCheck();
        Quaternion rotation = (Tools.pivotRotation == PivotRotation.Local) ? Quaternion.identity : Quaternion.Inverse(Target.transform.rotation);
        float size = HandleUtility.GetHandleSize(wp.position) * 0.1f;
        Handles.SphereHandleCap(0, wp.position, rotation, size, EventType.Repaint);
        Vector3 pos = Handles.PositionHandle(wp.position, rotation);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Move Point");
            wp.position = pos;
            Target.points[i] = wp;
        }
    }
}
