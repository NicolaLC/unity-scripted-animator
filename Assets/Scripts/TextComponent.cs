using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextComponent : MonoBehaviour
{
    private Text m_text;
    [SerializeField]
    private string textPlaceholder = "";

    private void Awake()
    {
        m_text = GetComponent<Text>();
        m_text.text = textPlaceholder;
    }

    public void SetText(string next)
    {
        m_text.text = next;
    }

    public void SetText(float next)
    {
        m_text.text = next.ToString();
    }
}
