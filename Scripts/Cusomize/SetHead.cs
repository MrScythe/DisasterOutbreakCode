using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SetHead : MonoBehaviour
{
    [SerializeField] private Sprite[] m_Head;
    [SerializeField] private bool m_Menu;
    private SpriteRenderer m_Sprite;

    // Start is called before the first frame update
    void Start()
    {
        m_Sprite = GetComponent<SpriteRenderer>();
        m_Sprite.sprite = m_Head[PlayerPrefs.GetInt("Head")];
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Menu)
        {
            m_Sprite = GetComponent<SpriteRenderer>();
            m_Sprite.sprite = m_Head[PlayerPrefs.GetInt("Head")];
        }
    }
}
