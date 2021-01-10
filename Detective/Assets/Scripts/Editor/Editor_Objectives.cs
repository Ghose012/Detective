using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


[CustomEditor(typeof(Objectives))]
public class Editor_Objectives : Editor
{
    public override void OnInspectorGUI()
    {
        Objectives objective = target as Objectives;
        objective.TypesCkeckCount = EditorGUILayout.IntField("TypesCount", objective.TypesCkeckCount);

        GUILayout.Space(6f);
        EditorGUI.DrawRect(EditorGUILayout.GetControlRect(false, 2f), Color.green);
        GUILayout.Space(6f);


        for (int i = 0; i < objective.TypesCkeckCount; i++)
        {
            objective.Types.Add(TypeofObjective.SpinCheck);
            objective.Types[i] = (TypeofObjective)EditorGUILayout.EnumPopup("TypeCheck" + i + " :", objective.Types[i]);
            GUILayout.Space(5f);

            switch (objective.Types[i])
            {
                #region SpinCheck
                case TypeofObjective.SpinCheck:
                    objective.Item = (GameObject)EditorGUILayout.ObjectField("Item", objective.Item, typeof(GameObject), true);
                    GUILayout.Space(5f);
                    SerializedProperty hands = serializedObject.FindProperty("Hands");
                    EditorGUILayout.PropertyField(hands, new GUIContent("hands spin count"));
                    serializedObject.ApplyModifiedProperties();
                    GUILayout.Space(5f);
                    SerializedProperty time = serializedObject.FindProperty("Time");
                    EditorGUILayout.PropertyField(time, new GUIContent("Time Check Spin"));
                    serializedObject.ApplyModifiedProperties();
                    GUILayout.Space(5f);
                    objective.IsComplete = EditorGUILayout.Toggle("objective done or not", objective.IsComplete);
                    GUILayout.Space(5f);

                    break;
                #endregion

                #region SlotPuzzle
                case TypeofObjective.SlotPuzzle:
                    SerializedProperty placeholder = serializedObject.FindProperty("PlaceHolders");
                    EditorGUILayout.PropertyField(placeholder, new GUIContent("PlaceHolders Check"));
                    serializedObject.ApplyModifiedProperties();
                    GUILayout.Space(5f);
                    objective.IsComplete = EditorGUILayout.Toggle("objective done or not", objective.IsComplete);
                    GUILayout.Space(5f);

                    break;
                #endregion

                #region Coloring
                case TypeofObjective.ColoringPuzzle:
                    objective.DesiredColor = EditorGUILayout.ColorField("Desired color", objective.DesiredColor);
                    objective.Item = (GameObject)EditorGUILayout.ObjectField("Item", objective.Item, typeof(GameObject), true);
                    GUILayout.Space(5f);
                    objective.IsComplete = EditorGUILayout.Toggle("objective done or not", objective.IsComplete);
                    objective.KeyOpen = (GameObject)EditorGUILayout.ObjectField("KeyItem", objective.KeyOpen, typeof(GameObject), true);


                    break;
                #endregion

              
                #region Key
                case TypeofObjective.Key:
                    SerializedProperty Allobjectives = serializedObject.FindProperty("AllObjectives");
                    EditorGUILayout.PropertyField(Allobjectives, new GUIContent("AllObjectives"));
                    serializedObject.ApplyModifiedProperties();
                    GUILayout.Space(5f);
                    objective.Open = (Sprite)EditorGUILayout.ObjectField("Key Sprite", objective.Open, typeof(Sprite), true);
                    objective.UiItems = (GameObject)EditorGUILayout.ObjectField("UiItems", objective.UiItems, typeof(GameObject), true);
                    objective.KeyOpen = (GameObject)EditorGUILayout.ObjectField("KeyItem", objective.KeyOpen, typeof(GameObject), true);
                    objective.indexScene = EditorGUILayout.IntField("index scene: ", objective.indexScene);
                    GUILayout.Space(5f);


                    break;
                #endregion

            }

        }
    }


}
