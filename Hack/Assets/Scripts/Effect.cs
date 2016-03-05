using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Effect
{
    private Dictionary<string, float> m_Effects;

    public float this[string a_Iterator]
    {
        get { return m_Effects[a_Iterator]; }
        set { m_Effects[a_Iterator] = value; }
    }

    public bool HasStatEffect(string a_StatName)
    {
        return m_Effects.ContainsKey(a_StatName);
    }

    public void AddStatEffect(string a_StatName, float a_Change)
    {
        m_Effects.Add(a_StatName, a_Change);
    }
}
