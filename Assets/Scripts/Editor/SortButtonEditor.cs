
using UnityEditor;

[CustomEditor(typeof(SortButton))]
public class SortButtonEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
    }
}
