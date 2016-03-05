using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Player))]
public class PlayerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Player t_Player = (Player)target;
        if (GUILayout.Button("Reset"))
        {
            t_Player.transform.position = Vector3.zero;
            t_Player.velocity = Vector3.zero;
        }
    }
}