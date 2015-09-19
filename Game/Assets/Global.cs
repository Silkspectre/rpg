using UnityEngine;
using System.Collections;

public class Global : MonoBehaviour
{
    public Character[] m_Characters;
    public Global m_Global = null;

    void Start()
    {
        m_Global = this;
    }
}
