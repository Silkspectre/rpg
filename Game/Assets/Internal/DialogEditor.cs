using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;

public class DialogEditor : EditorWindow
{
    public static Vector2 m_Camera = Vector2.zero;
    DialogSettingNode m_DialogSettingNode;

    [MenuItem("Liane's RPG/Dialog editor")]
    static void ShowEditor()
    {
        EditorWindow.GetWindow<DialogEditor>();
    }

    public DialogEditor()
    {
        m_DialogSettingNode = new DialogSettingNode(new Vector2(20, 20));
    }
    
    private Vector2 m_MousePosition;

    void OnGUI()
    {
        GUI.BeginGroup(new Rect(m_Camera.x, m_Camera.y, 100000, 100000));

        BeginWindows();

        m_DialogSettingNode.Draw();

        EndWindows();

        GUI.EndGroup();

        // Mouse dragging in field
        Event evt = Event.current;
        switch(evt.type)
        {
            case EventType.ScrollWheel:
                m_Camera -= evt.delta*15;
                Repaint();
                break;

            case EventType.MouseDown:
                m_MousePosition = evt.mousePosition;
                break;

            case EventType.MouseDrag:
                Vector2 t_Dist = m_MousePosition - evt.mousePosition;
                m_Camera -= t_Dist;

                m_MousePosition = evt.mousePosition;
                Repaint();
                break;
        }
    }

}
