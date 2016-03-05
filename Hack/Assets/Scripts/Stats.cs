using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;

[System.Serializable]
public class Stat
{
    public string Name;
}


[System.Serializable]
public class SkillStat : Stat
{
    public float Base;
    public List<Effect> AffectingEffects;

    public float Total
    {
        get
        {
            float t_Total = Base;

            foreach(Effect t_Effect in AffectingEffects)
            {
                t_Total += t_Effect[Name];
            }

            return t_Total;
        }
    }
}

[System.Serializable]
public class ResourceStat : Stat
{
    public float Current;
    public float Max;

    public static ResourceStat operator +(ResourceStat a_Stat, float a_Restore)
    {
        ResourceStat t_Stat = a_Stat;
        t_Stat.Current = Mathf.Clamp(t_Stat.Current + a_Restore, 0.0f, t_Stat.Max);
        return t_Stat;
    }

    public static ResourceStat operator -(ResourceStat a_Stat, float a_Restore)
    {
        ResourceStat t_Stat = a_Stat;
        t_Stat.Current = Mathf.Clamp(t_Stat.Current - a_Restore, 0.0f, t_Stat.Max);
        return t_Stat;
    }

    public static bool operator <(ResourceStat a_Stat, float a_Resource)
    {
        return a_Stat.Current < a_Resource;
    }

    public static bool operator >(ResourceStat a_Stat, float a_Resource)
    {
        return a_Stat.Current > a_Resource;
    }

    public void Restore()
    {
        Current = Max;
    }
}

public class Stats : NetworkBehaviour
{
    [SyncVar]
    public ResourceStat Health;
    [SyncVar]
    public ResourceStat Mana;

    void Start ()
    {
        Health.Restore();
        Mana.Restore();
    }
}
