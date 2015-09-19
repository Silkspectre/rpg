using UnityEngine;
using UnityEditor;
using System.IO;

public class DialogSettingNode : Node
{
    private WWW m_WWW = null;
    private string m_Path = "";

    public DialogSettingNode(Vector2 a_Position) : base(a_Position)
    {
        m_Name = "Settings";
        m_Next = CreateNext();
        m_Boundaries.height = 115;
    }

    void ReadFile()
    {
        if (m_WWW!=null && m_WWW.size > 0)
        {
            StringReader t_Reader = new StringReader(m_WWW.text);

            while(true)
            {
                string t_Character = t_Reader.ReadLine();

                if (t_Character == null) break; // no more lines

                string t_Line = "";

                string t_Text = t_Reader.ReadLine();
                bool t_First = true;
                while (t_Text!=null && t_Text!=".END")
                {
                    if (!t_First) t_Line += '\n';
                    else t_First = false;


                    t_Line += t_Text;
                    t_Text = t_Reader.ReadLine();
                }

                CreateAtEnd(t_Character, t_Line);
            }
        }
    }

    void Clear(bool a_CreateNew = true)
    {

        if (EditorUtility.DisplayDialog("Save", "Do you want to save?", "Save", "Don't save"))
        {
            Save();
        }

        m_WWW = null;
        
        if(a_CreateNew)
            m_Next = CreateNext();
        else m_Next = null;

        DialogEditor.m_Camera = Vector2.zero;
    }


    void ClearButton(string a_Text)
    {
        CreateButton(a_Text, delegate ()
        {
            Clear();
        });
    }

    protected override void OnGUI(int id)
    {
        // Open Button
        CreateButton("Open..", delegate ()
        {
            Clear(false);

            string t_Path = EditorUtility.OpenFilePanel("Open..", "Assets/Dialog", "txt");

            if (t_Path.Length != 0)
            {
                Debug.Log("Dialog editor opening " + t_Path);
                m_Path = t_Path;
                m_WWW = new WWW("file:///" + t_Path);

                ReadFile();
            }
        });

        // Whether a file is open and close button
        if (m_WWW != null)
        {
            EditorGUILayout.BeginHorizontal();

            string t_FileName = m_WWW.url.Substring(m_WWW.url.LastIndexOf('/') + 1);
            GUILayout.Label("Currently editing: " + t_FileName);

            EditorGUILayout.EndHorizontal();

            ClearButton("Close " + t_FileName);
        }
        else
        {
            EditorGUILayout.BeginHorizontal();
            
            GUILayout.Label("No file opened.");

            EditorGUILayout.EndHorizontal();

            ClearButton("Clear");
        }

        // Save button
        CreateButton("Save", delegate ()
        {
            if (m_WWW!=null)
            {
                // Save to existing file
                Save(m_Path);
            }
            else
            {
                Save();
            }
        });
    }

    void Save(string a_Path = "")
    {
        string t_Path = a_Path;
        if(t_Path.Length<=1)
            t_Path = EditorUtility.SaveFilePanel("Save as..", "Assets/Dialog", "dialog", "txt");

        if (t_Path.Length != 0)
        {
            Debug.Log("Saving to " + t_Path);
            string t_Object = RecursiveWrite();
            System.IO.File.WriteAllText(t_Path, t_Object);
        }
    }
}
