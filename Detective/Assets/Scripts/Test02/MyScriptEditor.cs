using UnityEditor;

[CustomEditor(typeof(Interactable))]
public class MyScriptEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Interactable interactable = target as Interactable;

        interactable.Types = (TypeofItem)EditorGUILayout.EnumPopup("Type: ", interactable.Types);

        switch (interactable.Types)
        {
            case TypeofItem.Zoomer:
                //interactable.Item = EditorGUILayout.ObjectField(interactable.Item, typeof(Object), true);
                interactable.pos = EditorGUILayout.Vector3Field("pos:", interactable.pos);
                interactable.zoom = EditorGUILayout.FloatField("Zoom:", interactable.zoom);
                break;
            case TypeofItem.Collectable:
                interactable.Hidden = EditorGUILayout.Toggle("Hidden:", interactable.Hidden);
                break;
            case TypeofItem.UiSelectable:
                break;
            case TypeofItem.OpenClose:
                interactable.pos = EditorGUILayout.Vector3Field("pos:", interactable.pos);
                interactable.IsActive = EditorGUILayout.Toggle("IsActive:", interactable.IsActive);
                break;
            case TypeofItem.Spin:
                interactable.speed = EditorGUILayout.FloatField("speed:", interactable.speed);
                interactable.Value = EditorGUILayout.IntField("Value:", interactable.Value);
                break;
            case TypeofItem.Navigation:
                //interactable.Objects = EditorGUILayout.FloatField("Objects:", interactable.Objects);
                break;
            case TypeofItem.PlaceHolder:
                //interactable.UiItems = EditorGUILayout.FloatField("UiItems:", interactable.UiItems);
                break;
        }           
    }
}