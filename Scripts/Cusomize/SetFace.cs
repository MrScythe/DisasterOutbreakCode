using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFace : MonoBehaviour
{
    [SerializeField] private Sprite[] m_Face;
    [SerializeField] private bool m_Menu;
    private SpriteRenderer m_Sprite;

    // Start is called before the first frame update
    void Start()
    {
        m_Sprite = GetComponent<SpriteRenderer>();
        m_Sprite.sprite = m_Face[PlayerPrefs.GetInt("Face")];
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Menu)
        {
            m_Sprite = GetComponent<SpriteRenderer>();
            m_Sprite.sprite = m_Face[PlayerPrefs.GetInt("Face")];
        }
    }
}
