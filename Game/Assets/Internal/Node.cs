using UnityEngine;
using UnityEditor;
using System;

public class Node
{
    protected Rect m_Boundaries;
    protected Node m_Next = null;

    protected string m_Name = "Node";

    public Node(Rect a_Rect)
    {
        m_Boundaries = a_Rect;
    }

    public Node(Vector2 a_Position)
    {
        m_Boundaries.width = 200;
        m_Boundaries.height = 100;
        m_Boundaries.x = a_Position.x;
        m_Boundaries.y = a_Position.y;
    }

    public virtual string RecursiveWrite(string a_String = "")
    {
        if (m_Next == null)
            return a_String;
        else return m_Next.RecursiveWrite(a_String);
    }


    public void Draw(int i = 1)
    {
        m_Boundaries = GUI.Window(i, m_Boundaries, OnGUIInternal, m_Name);
       
        if (m_Next != null)
        {
            DrawNodeCurve(m_Next.m_Boundaries);
            m_Next.Draw(i + 1);
        }
    }

    void OnGUIInternal(int id)
    {
        GUI.DragWindow(new Rect(0,0, m_Boundaries.width, 20));

        OnGUI(id);
    }

    protected virtual void OnGUI(int id)
    {

    }

    public void AddToEnd(Node a_End)
    {
        if (m_Next == null)
        {
            m_Next = a_End;
        }
        else m_Next.AddToEnd(a_End);
    }

    public void CreateAtEnd(string a_Character = "", string a_Lines = "")
    {
        if (m_Next == null)
        {
            m_Next = CreateNext(a_Character, a_Lines);
        }
        else m_Next.CreateAtEnd(a_Character, a_Lines);
    }


    protected void CreateButton(string a_Text, Action a_Function)
    {
        Rect r = EditorGUILayout.BeginHorizontal("Button");

        if (GUI.Button(r, GUIContent.none))
            a_Function();

        GUILayout.Label(a_Text);

        EditorGUILayout.EndHorizontal();
    }

    protected Node CreateNext(string a_Character = "", string a_Lines = "")
    {
        return new DialogNode(new Vector2(m_Boundaries.x, m_Boundaries.yMax + 30), a_Character, a_Lines);
    }

    public Node GetNext()
    {
        return m_Next;
    }

    void DrawNodeCurve(Rect a_Target)
    {
        if (a_Target.x > m_Boundaries.xMax)
        {
            Vector3 startPos = new Vector3(m_Boundaries.x + m_Boundaries.width, m_Boundaries.y + m_Boundaries.height / 2, 0);
            Vector3 endPos = new Vector3(a_Target.x, a_Target.y + a_Target.height / 2, 0);
            Vector3 startTan = startPos + Vector3.right * 50;
            Vector3 endTan = endPos + Vector3.left * 50;

            Handles.DrawBezier(startPos, endPos, startTan, endTan, Color.black, null, 1.5f);
        }
        else if (a_Target.y > m_Boundaries.yMax)
        {
            Vector3 startPos = new Vector3(m_Boundaries.x + m_Boundaries.width / 2, m_Boundaries.y + m_Boundaries.height, 0);
            Vector3 endPos = new Vector3(a_Target.x + a_Target.width / 2, a_Target.y, 0);

            Vector3 startTan = startPos + Vector3.up * 50;
            Vector3 endTan = endPos + Vector3.down * 50;

            Handles.DrawBezier(startPos, endPos, startTan, endTan, Color.black, null, 1.5f);
        }
        else if (a_Target.xMax > m_Boundaries.x)
        {
            Vector3 startPos = new Vector3(m_Boundaries.x + m_Boundaries.width / 2, m_Boundaries.y, 0);
            Vector3 endPos = new Vector3(a_Target.x + a_Target.width / 2, a_Target.y + a_Target.height, 0);

            Vector3 startTan = startPos + Vector3.down * 50;
            Vector3 endTan = endPos + Vector3.up * 50;

            Handles.DrawBezier(startPos, endPos, startTan, endTan, Color.black, null, 1.5f);
        }
        else 
        {
            Vector3 startPos = new Vector3(m_Boundaries.x, m_Boundaries.y + m_Boundaries.height /2, 0);
            Vector3 endPos = new Vector3(a_Target.x + a_Target.width, a_Target.y + a_Target.height/2, 0);

            Vector3 startTan = startPos + Vector3.left * 50;
            Vector3 endTan = endPos + Vector3.right * 50;

            Handles.DrawBezier(startPos, endPos, startTan, endTan, Color.black, null, 1.5f);
        }
    }
}
