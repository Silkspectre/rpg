using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEngine.UI;
using System;

public class Speech : MonoBehaviour
{
    private GameObject m_Text;
    private GameObject m_Speaker;

    void Start ()
    {
        m_Text = transform.Find("Bubble").Find("Text").gameObject;
        m_Speaker = transform.Find("Speaker").gameObject;
    }

	void Update ()
    {
	
	}

    public void SetConversation(UnfoldedDialog a_Dialog)
    {
        Vector3 t_Scale = transform.localScale;
        Vector3 t_ScaleText = m_Text.transform.localScale;
        RectTransform t_TextRect = m_Text.GetComponent<RectTransform>();
        t_Scale.x = 1.0f;
        t_ScaleText.x = 1.0f;

        if (a_Dialog.Side == UnfoldedDialog.DialogSide.Right)
        {
            t_Scale.x *= -1;
            t_ScaleText.x *= -1;

            if(m_Text.transform.localScale.x>0)
            {
                t_TextRect.offsetMin = new Vector2(20, t_TextRect.offsetMin.y);
            }
        }
        else if (m_Text.transform.localScale.x < 0)
        {
            //t_TextRect.offsetMin = new Vector2(-20, t_TextRect.offsetMin.y);
        }

        m_Text.GetComponent<Text>().text = a_Dialog.Line;

        transform.localScale = t_Scale;
        m_Text.transform.localScale = t_ScaleText;
    }

    public void EndConversation()
    {
        Vector3 t_Scale = transform.localScale;
        t_Scale.x = 0.0f;
        transform.localScale = t_Scale;
    }
}
