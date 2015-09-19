using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

[System.Serializable]
public class UnfoldedDialog
{
    public enum DialogSide
    {
        Left,
        Right
    };

    public DialogSide Side;
    public string Name;
    public string Line;
};

public class Dialog : MonoBehaviour
{
    public TextAsset m_DialogFile;

    private int m_Iterator = 0;

    Speech m_Speech = null;

    private List<string> m_Characters = new List<string>();
    private List<UnfoldedDialog> m_Dialog = new List<UnfoldedDialog>();

    void Start()
    {
        m_Speech = GameObject.FindGameObjectWithTag("SpeechUI").GetComponent<Speech>();
        EndConversation();

        StringReader t_Reader = new StringReader(m_DialogFile.text);

        while (true)
        {
            string t_Name = t_Reader.ReadLine();
            if (t_Name == null) break; // no more lines


            int t_Found = m_Characters.IndexOf(t_Name);
            if (t_Found == -1)
            {
                m_Characters.Add(t_Name);
                t_Found = m_Characters.Count - 1;
            }

            UnfoldedDialog.DialogSide t_Side = (t_Found % 2 == 0) ? (UnfoldedDialog.DialogSide.Left) : (UnfoldedDialog.DialogSide.Right);

            string t_Text = t_Reader.ReadLine();
            while (t_Text != null && t_Text != ".END")
            {
                UnfoldedDialog t_Dialog = new UnfoldedDialog();
                t_Dialog.Name = t_Name;
                t_Dialog.Side = t_Side;
                t_Dialog.Line = t_Text;

                m_Dialog.Add(t_Dialog);

                t_Text = t_Reader.ReadLine();
            }
            
        }
    }

    void Reset()
    {
        m_Iterator = 0;
    }

    public void Speak()
    {
        if (m_Speech == null)
        {
            Debug.LogError("Tried to talk to an NPC without a speech UI!");
            return;
        }

        if (m_Iterator<m_Dialog.Count)
        {
            m_Speech.SetConversation(m_Dialog[m_Iterator]);
            m_Iterator++;
        }
        else EndConversation();
    }

    public void EndConversation()
    {
        Reset();
        m_Speech.EndConversation();
    }
}
