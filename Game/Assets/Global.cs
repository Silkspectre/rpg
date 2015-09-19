using UnityEngine;
using System.Collections;

public class Global : MonoBehaviour
{
    public Character[] m_Characters;
    public static Global Singleton = null;

    void Start()
    {
        Singleton = this;
    }

    public Character GetCharacter(string a_Name)
    {
        if (m_Characters.Length == 0) return null;
        
        for(int i = 0; i<m_Characters.Length; i++)
        {
            if (m_Characters[i].Name == a_Name)
                return m_Characters[i];
        }

        return null;
    }
}
