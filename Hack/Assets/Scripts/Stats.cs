using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;
using System;

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
    [SerializeField]
    public float m_Current;

    [SerializeField]
    private float m_Max;

    public float Current
    {
        get { return m_Current; }
        set { MaxChanged(); m_Current = value; }
    }

    public float Max
    {
        get { return m_Max; }
        set { MaxChanged(); m_Max = value; }
    }

    public void Restore()
    {
        Current = Max;
    }

    #region OPERATORS
    public static ResourceStat operator +(ResourceStat a_Stat, float a_Restore)
    {
        ResourceStat t_Stat = a_Stat;
        t_Stat.Current = Mathf.Clamp(t_Stat.Current + a_Restore, 0.0f, t_Stat.Max);

        a_Stat.CheckEvents();

        return t_Stat;
    }

    public static ResourceStat operator -(ResourceStat a_Stat, float a_Restore)
    {
        ResourceStat t_Stat = a_Stat;
        t_Stat.Current = Mathf.Clamp(t_Stat.Current - a_Restore, 0.0f, t_Stat.Max);

        a_Stat.CheckEvents();

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
    #endregion

    #region EVENTS
    public class MaxChangedEventArgs : EventArgs
    {
        public float OldMax { get; set; }
        public float NewMax { get; set; }
    }

    protected void MaxChanged()
    {
        Debug.Log(Name + "'s max value changed to " + Max);

        EventHandler<MaxChangedEventArgs> t_Handler = OnMaxChange;

        if (t_Handler != null)
        {
            t_Handler(this, null);
        }
    }

    protected void BecameZero()
    {
        Debug.Log(Name + "'s current value is now 0");

        EventHandler t_Handler = OnZero;
        if (t_Handler != null)
        {
            t_Handler(this, new EventArgs());
        }
    }

    protected void BecameFull()
    {
        Debug.Log(Name + "'s current value is now Max (" + Max + ")");

        EventHandler t_Handler = OnFull;
        
        if (t_Handler != null)
        {
            t_Handler(this, new EventArgs());
        }
    }

    public event EventHandler OnZero;
    public event EventHandler OnFull;
    public event EventHandler<MaxChangedEventArgs> OnMaxChange;

    private void CheckEvents()
    {
        if (Current == 0.0f)
            BecameZero();
        else if (Current == Max)
            BecameFull();
    }
    #endregion
}

public class Stats : NetworkBehaviour
{
    [SyncVar]
    public ResourceStat Health;
    [SyncVar]
    public ResourceStat Mana;

    void Start ()
    {
        Player t_Player = GetComponent<Player>();

        if (t_Player != null)
            Health.OnZero += t_Player.HasDied;
    }
}
