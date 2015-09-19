using UnityEngine;
using UnityEditor;

public class DialogNode : Node
{
    private string m_Character;
    private string m_Lines;

    public DialogNode(Vector2 a_Position, string a_Character, string a_Lines) : base(a_Position)
    {
        m_Name = "Dialog";
        m_Character = a_Character;
        m_Lines = a_Lines;

        m_Boundaries.width = 500;
        m_Boundaries.height = 200;
    }

    public override string RecursiveWrite(string a_String = "")
    {
        a_String += m_Character + "\n";
        a_String += m_Lines + "\n.END";

        if (m_Next != null) return m_Next.RecursiveWrite(a_String+ "\n");
        return a_String;
    }

    protected override void OnGUI(int id)
    { 
        m_Character = EditorGUILayout.TextField("Name: ", m_Character);
        EditorGUILayout.LabelField("Lines: (Press enter for next line)");
        m_Lines = EditorGUILayout.TextArea(m_Lines);


        if (m_Next == null)
            CreateButton("Add Dialog", delegate ()
            {
                m_Next = CreateNext();
            });
    }
}
