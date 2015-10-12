using UnityEngine;
using System.Collections;

[System.Serializable]
public class Stat
{
    public float m_Base = 0.0f; // Base of state
    private float m_Current = 0.0f; // Current stat
    private float m_Bonus = 0.0f; // Current stat
    public float m_Max = 0.0f;

    public float m_Regen = 0.0f;
    public float m_PerLevel = 1.0f;

    private Statholder m_Statholder = null;

    Stat(Statholder a_Statholder, int a_Level)
    {
        m_Statholder = a_Statholder;
    }

    ~Stat()
    {

    }

    public float GetCurrent()
    {
        return m_Current;
    }

    void CalculateBonuses()
    {
        
    }

    public void LevelUp()
    {
        m_Max = m_Max + m_PerLevel;
        m_Current = m_Max;
    }
}
