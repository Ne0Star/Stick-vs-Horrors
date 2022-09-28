using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(PatronCreator))]
public class TrajectoryEditor : Editor
{
    private PatronCreator creator;
    public override void OnInspectorGUI()
    {
        creator = (PatronCreator)target;

        var onReach = serializedObject.FindProperty("reach");
        var Ttarget = serializedObject.FindProperty("target");
        var endPosition = serializedObject.FindProperty("endPosition");
        //var OnReach = EditorGUILayout.PropertyField(onReach);

        GUIStyle scrollSkin = new GUIStyle(GUI.skin.box);
        GUIStyle toggleSkin = new GUIStyle(GUI.skin.button);

        serializedObject.Update();


        GUILayout.BeginVertical();

        GUILayout.Label("");
        GUILayout.BeginHorizontal(scrollSkin);
        GUILayout.Label("Дистанция до окончаниия: ");
        creator.reachRange = EditorGUILayout.FloatField(creator.reachRange);
        GUILayout.EndHorizontal();

        GUILayout.Label("");
        GUILayout.BeginHorizontal(scrollSkin);
        GUILayout.Label("Скорость перемещения: ");
        creator.moveSpeed = EditorGUILayout.FloatField(creator.moveSpeed);
        GUILayout.EndHorizontal();


        GUILayout.Label("");
        GUILayout.BeginHorizontal(scrollSkin);
        creator.usePursue = GUILayout.Toggle(creator.usePursue, "Приследовать цель ?");
        if (creator.usePursue)
        {
            GUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(Ttarget);
            GUILayout.EndHorizontal();
        } else
        {
            EditorGUILayout.PropertyField(endPosition);
        }
        GUILayout.EndHorizontal();


        GUILayout.Label("");
        GUILayout.BeginHorizontal(scrollSkin);
        creator.useZRotation = GUILayout.Toggle(creator.useZRotation, "Вращать источник в сторону движения ?");
        if (creator.useZRotation)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Смещение: ");
            creator.rotateOffset = EditorGUILayout.FloatField(creator.rotateOffset);
            GUILayout.EndHorizontal();
        }
        GUILayout.EndHorizontal();

        GUILayout.Label("");
        GUILayout.BeginHorizontal(scrollSkin);
        creator.useCurves = GUILayout.Toggle(creator.useCurves, "Использовать смещение траектории ?");
        GUILayout.EndHorizontal();


        GUILayout.EndVertical();
        GUILayout.Label("");

        GUIStyle curveStyle = new GUIStyle();
        curveStyle.stretchHeight = true;

        if (creator.useCurves)
        {
            GUILayout.BeginHorizontal(scrollSkin);
            GUILayout.Label("Сила смещения: ");
            creator.allCurvesPower = EditorGUILayout.FloatField(creator.allCurvesPower);
            GUILayout.EndHorizontal();

            GUILayout.BeginVertical(scrollSkin);
            GUILayout.BeginHorizontal();
            GUILayout.Label("Смещение по X");
            GUILayout.Label("Смещение по Y");
            GUILayout.Label("Общая скорость");
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            EditorGUILayout.CurveField(creator.missilesType.XOffset);
            EditorGUILayout.CurveField(creator.missilesType.YOffset);
            EditorGUILayout.CurveField(creator.missilesType.SpeedInterpolator);
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();
            GUILayout.Label("");
        }

        EditorGUILayout.HelpBox("Когда патрон достиг цели <Источник, Цель>", MessageType.Info);
        EditorGUILayout.PropertyField(onReach);
        //if (data.showList)
        //{
        //    GUILayout.BeginScrollView(new Vector2(200f, 0.5f), scrollSkin);
        //    for (int i = 0; i < actions.Count; i++)
        //    {
        //        AIAction action = actions[i];

        //        GUILayout.BeginHorizontal();
        //        action.ActionType = (AIAction.AIActionType)EditorGUILayout.EnumPopup(action.ActionType);

        //        switch (action.ActionType)
        //        {
        //            case AIAction.AIActionType.??????:
        //                actions[i] = new MoveAction();
        //                break;
        //        }
        //        actions[i].ActionType = action.ActionType;
        //        if (GUILayout.Button("X"))
        //        {
        //            actions.RemoveAt(i);
        //        }
        //        GUILayout.EndHorizontal();
        //        action.DrawEditor();
        //        //GUILayout.Box("123123", scrollSkin);
        //    }
        //    GUILayout.EndScrollView();
        //}
        //if (GUILayout.Button("????????"))
        //{
        //    actions.Add(new AIAction());
        //}




        serializedObject.ApplyModifiedProperties();


        if (GUI.changed)
        {
            //data.actions = actions;
            EditorUtility.SetDirty(target);
        }
    }


    private float MyFloatFieldInternal(Rect position, Rect dragHotZone, float value, [DefaultValue("EditorStyles.numberField")] GUIStyle style)
    {
        int controlID = GUIUtility.GetControlID("EditorTextField".GetHashCode(), FocusType.Keyboard, position);
        Type editorGUIType = typeof(EditorGUI);

        Type RecycledTextEditorType = Assembly.GetAssembly(editorGUIType).GetType("UnityEditor.EditorGUI+RecycledTextEditor");
        Type[] argumentTypes = new Type[] { RecycledTextEditorType, typeof(Rect), typeof(Rect), typeof(int), typeof(float), typeof(string), typeof(GUIStyle), typeof(bool) };
        MethodInfo doFloatFieldMethod = editorGUIType.GetMethod("DoFloatField", BindingFlags.NonPublic | BindingFlags.Static, null, argumentTypes, null);

        FieldInfo fieldInfo = editorGUIType.GetField("s_RecycledEditor", BindingFlags.NonPublic | BindingFlags.Static);
        object recycledEditor = fieldInfo.GetValue(null);

        object[] parameters = new object[] { recycledEditor, position, dragHotZone, controlID, value, "g7", style, true };

        return (float)doFloatFieldMethod.Invoke(null, parameters);
    }
}
