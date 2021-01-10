using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;



[CustomEditor(typeof(Interactable))]
public class Editor_Interactable : Editor
{

    public override void OnInspectorGUI()
    {
        Interactable interactable = target as Interactable;
        interactable.TypesCount = EditorGUILayout.IntField("TypesCount", interactable.TypesCount);

        GUILayout.Space(6f);
        EditorGUI.DrawRect(EditorGUILayout.GetControlRect(false, 2f), Color.green);
        GUILayout.Space(6f);


        for (int i = 0; i < interactable.TypesCount; i++)
        {     
            interactable.Types.Add(TypeofItem.Collectable);
            interactable.Types[i] = (TypeofItem)EditorGUILayout.EnumPopup("Type" + i + " :", interactable.Types[i]);
            GUILayout.Space(5f);

            switch (interactable.Types[i])
            {
                #region Zoomer
                case TypeofItem.Zoomer:
                    interactable.Item = (GameObject)EditorGUILayout.ObjectField("Item", interactable.Item, typeof(GameObject), true);
                    interactable.pos = EditorGUILayout.Vector3Field("pos:", interactable.pos);
                    GUILayout.Space(5f);
                    interactable.zoom = EditorGUILayout.FloatField("Zoom:", interactable.zoom);
                    break;
                #endregion

                #region Collectable
                case TypeofItem.Collectable:
                    interactable.Hidden = EditorGUILayout.Toggle("Hidden:", interactable.Hidden);
                    GUILayout.Space(5f);
                    interactable.AffectedByZoom = EditorGUILayout.Toggle("AffectedByZoom:", interactable.AffectedByZoom);
                    GUILayout.Space(5f);
                    interactable.UiItems = (GameObject)EditorGUILayout.ObjectField("UiItems", interactable.UiItems, typeof(GameObject), true);
                    break;
                #endregion

                #region UiSelectable
                case TypeofItem.UiSelectable:
                    break;
                #endregion

                #region OpenClose
                case TypeofItem.OpenClose:
                    interactable.pos = EditorGUILayout.Vector3Field("pos:", interactable.pos);
                    GUILayout.Space(5f);
                    interactable.IsActive = EditorGUILayout.Toggle("IsActive:", interactable.IsActive);
                    GUILayout.Space(5f);
                    interactable.AffectedByZoom = EditorGUILayout.Toggle("AffectedByZoom:", interactable.AffectedByZoom);
                    GUILayout.Space(5f);
                    interactable.Item = (GameObject)EditorGUILayout.ObjectField("Item", interactable.Item, typeof(GameObject), true);
                    GUILayout.Space(5f);
                    interactable.Locked = EditorGUILayout.Toggle("Locked", interactable.Locked);
                    break;
                #endregion

                #region Spin
                case TypeofItem.Spin:
                    interactable.speed = EditorGUILayout.FloatField("speed:", interactable.speed);
                    GUILayout.Space(5f);
                    interactable.Value = EditorGUILayout.IntField("Value:", interactable.Value);
                    GUILayout.Space(5f);
                    interactable.AffectedByZoom = EditorGUILayout.Toggle("AffectedByZoom:", interactable.AffectedByZoom);
                    break;
                #endregion

                #region Navigation
                case TypeofItem.Navigation:
                    interactable.Objects = (GameObject)EditorGUILayout.ObjectField("Objects:", interactable.Objects, typeof(GameObject), true);
                    GUILayout.Space(10f);
                    interactable.step= EditorGUILayout.IntField("step:", interactable.step);
                    GUILayout.Space(5f);
                  //  interactable.DestinationCounts = EditorGUILayout.IntField("DestinationCounts:", interactable.DestinationCounts);
                    GUILayout.Space(5f);

                    SerializedProperty CameraPosition = serializedObject.FindProperty("Destinations");
                    EditorGUILayout.PropertyField( CameraPosition, new GUIContent("My Camera Positions"));
                    serializedObject.ApplyModifiedProperties();
                    GUILayout.Space(5f);
                    interactable.AffectedByZoom = EditorGUILayout.Toggle("AffectedByZoom:", interactable.AffectedByZoom);
                    break;
                #endregion

                #region PlaceHolder
                case TypeofItem.PlaceHolder:
                    interactable.UiItems = (GameObject)EditorGUILayout.ObjectField("UiItems", interactable.UiItems, typeof(GameObject), true);
                    GUILayout.Space(5f);
                    interactable.AffectedByZoom = EditorGUILayout.Toggle("AffectedByZoom:", interactable.AffectedByZoom);
                    break;
                #endregion

                #region search
                case TypeofItem.Search:
                    interactable.pos = EditorGUILayout.Vector3Field("pos:", interactable.pos);
                    GUILayout.Space(5f);
                    interactable.AffectedByZoom = EditorGUILayout.Toggle("AffectedByZoom:", interactable.AffectedByZoom);
                    GUILayout.Space(5f);
                    interactable.Item = (GameObject)EditorGUILayout.ObjectField("Item", interactable.Item, typeof(GameObject), true);
                    break;
                #endregion

                #region coloring
                case TypeofItem.Coloring:
                     interactable.Item = (GameObject)EditorGUILayout.ObjectField("Item", interactable.Item, typeof(GameObject), true);
                    GUILayout.Space(5f);
                    interactable.ColorTool = (Sprite)EditorGUILayout.ObjectField("ColorToolSprite", interactable.ColorTool, typeof(Sprite),true);
                    GUILayout.Space(5f);
                    interactable.colorChoice = EditorGUILayout.ColorField("wall color", interactable.colorChoice);
                    break;
                #endregion

                #region Input
                case TypeofItem.InputPuzzle:
                    interactable.RightAnswer = EditorGUILayout.TextField("right answer ", interactable.RightAnswer);
                    interactable.MyInput = (InputField)EditorGUILayout.ObjectField("player Answer", interactable.MyInput, typeof(InputField), true);
                    interactable.Item = (GameObject)EditorGUILayout.ObjectField("ItemHidden", interactable.Item, typeof(GameObject), true);
                    interactable.CanvasIn = (GameObject)EditorGUILayout.ObjectField("CanvasIn", interactable.CanvasIn, typeof(GameObject), true);
                    break;
                #endregion

                #region Switcher
                case TypeofItem.Switcher:
                    //DrawDefaultInspector();
                    break;
                    #endregion
            }

            GUILayout.Space(10f);
            EditorGUI.DrawRect(EditorGUILayout.GetControlRect(false, 2f), Color.red);
            GUILayout.Space(10f);
        }
    }
}