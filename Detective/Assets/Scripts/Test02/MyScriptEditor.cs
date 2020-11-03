using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Interactable))]
public class MyScriptEditor : Editor
{
    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();

        Interactable interactable = target as Interactable;

        //interactable.Types = EditorGUILayout.EnumPopup("Primitive to create:", interactable.Types);
        //interactable.Types = EditorGUILayout.DropdownButton(interactable.Types, FocusType.Passive,)

        if (interactable.IsZoomer)
        {
           // interactable.Item = EditorGUILayout.ObjectField(interactable.Item,typeof(Object),true);
            interactable.pos = EditorGUILayout.Vector3Field("pos:", interactable.pos);
            interactable.IsActive = EditorGUILayout.Toggle("IsActive:", interactable.IsActive);
            interactable.zoom = EditorGUILayout.FloatField("Zoom:", interactable.zoom);
            interactable.Hidden = EditorGUILayout.Toggle("Hidden:", interactable.Hidden);
            interactable.speed = EditorGUILayout.FloatField("speed:", interactable.speed);
            interactable.Value = EditorGUILayout.IntField("Value:", interactable.Value);
           // interactable.Objects = EditorGUILayout.FloatField("Objects:", interactable.Objects);
            //interactable.UiItems = EditorGUILayout.FloatField("UiItems:", interactable.UiItems);

        }
    }
}