using UnityEngine;
using System.Collections;

[System.Serializable]
public class Statholder
{
    public int m_Level;
    public int m_EXP;

    public Stat m_HP;
    public Stat m_MP;
    public Stat m_SPD;
    public Stat m_STR;
    public Stat m_INT;

    public void LevelUp()
    {
        m_HP.LevelUp();
        m_MP.LevelUp();
        m_SPD.LevelUp();
        m_STR.LevelUp();
        m_INT.LevelUp();
    }

    public void EXPCheck()
    {
        if (m_EXP >= GetEXPRequirement())
        {
            LevelUp();
        }
    }

    int GetEXPRequirement()
    {
        return (m_Level * 20) * (int)((float)m_Level * 0.3f) + 50;
    }
}
