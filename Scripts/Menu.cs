using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_HighscoreText, m_CoinsText;
    [SerializeField] private GameObject m_MainMenu, m_Shop;
    [SerializeField] private GameObject m_HeadType, m_FaceType, m_GunType;
    [SerializeField] private GameObject m_Left, m_Right;
    [SerializeField] private Sprite m_NormalPanel, m_ChosenPanel;
    [SerializeField] private AudioSource m_Equip, m_EquipGun, m_Buy, m_Error;

    [SerializeField] private Image[] m_Heads, m_HeadLock;
    [SerializeField] private int[] m_HeadCost;

    [SerializeField] private Image[] m_Faces, m_FaceLock;
    [SerializeField] private int[] m_FaceCost;

    [SerializeField] private Image[] m_Guns, m_GunLock;
    [SerializeField] private int[] m_GunCost;

    private int m_Highscore, m_Coins;

    private int m_Head;
    private int m_Face;
    private int m_Gun;
    private int m_HasGod;

    // Start is called before the first frame update
    void Start()
    {
        m_Highscore = PlayerPrefs.GetInt("Highscore");
        m_Coins = PlayerPrefs.GetInt("Coins");
        m_HasGod = PlayerPrefs.GetInt("God");

        //---------------------------------------
        m_Head = PlayerPrefs.GetInt("Head");
        PlayerPrefs.SetInt("HasHead0", 1);

        for (int i = 0; i < m_Heads.Length; i++)
        {
            if (PlayerPrefs.GetInt("HasHead" + i) > 0)
            {
                m_HeadLock[i].gameObject.SetActive(false);
            }
        }

        m_Heads[m_Head].sprite = m_ChosenPanel;
        //---------------------------------------
        m_Face = PlayerPrefs.GetInt("Face");
        PlayerPrefs.SetInt("HasFace0", 1);

        for (int i = 0; i < m_Faces.Length; i++)
        {
            if (PlayerPrefs.GetInt("HasFace" + i) > 0)
            {
                m_FaceLock[i].gameObject.SetActive(false);
            }
        }

        m_Faces[m_Face].sprite = m_ChosenPanel;
        //---------------------------------------
        m_Gun = PlayerPrefs.GetInt("Gun");
        PlayerPrefs.SetInt("HasGun0", 1);

        for (int i = 0; i < m_Guns.Length; i++)
        {
            if (PlayerPrefs.GetInt("HasGun" + i) > 0)
            {
                m_GunLock[i].gameObject.SetActive(false);
            }
        }

        m_Guns[m_Gun].sprite = m_ChosenPanel;
        //---------------------------------------
        m_HighscoreText.SetText("Personal Best: Round " + m_Highscore);
        m_CoinsText.SetText("" + m_Coins);

        m_MainMenu.SetActive(true);
        m_Shop.SetActive(false);

        m_HeadType.SetActive(true);
        m_FaceType.SetActive(false);
        m_GunType.SetActive(false);
        m_Left.SetActive(false);
    }

    public void MainMenu()
    {
        m_MainMenu.SetActive(true);
        m_Shop.SetActive(false);
    }

    public void Shop()
    {
        m_MainMenu.SetActive(false);
        m_Shop.SetActive(true);
    }

    public void BuyHead(int head)
    {
        if (PlayerPrefs.GetInt("HasHead" + head) <= 0)
        {
            if (m_Coins >= m_HeadCost[head])
            {
                m_Coins -= m_HeadCost[head];
                PlayerPrefs.SetInt("Coins", m_Coins);
                m_CoinsText.SetText("" + m_Coins);

                PlayerPrefs.SetInt("HasHead" + head, 1);
                m_HeadLock[head].gameObject.SetActive(false);

                m_Buy.Play();
            }
            else
            {
                m_Error.Play();
            }
        }
        else
        {
            for (int i = 0; i < m_Heads.Length; i++)
            {
                m_Heads[i].sprite = m_NormalPanel;
            }

            m_Head = head;
            PlayerPrefs.SetInt("Head", head);
            m_Heads[head].sprite = m_ChosenPanel;

            m_Equip.Play();
        }
    }

    public void BuyFace(int face)
    {
        if (PlayerPrefs.GetInt("HasFace" + face) <= 0)
        {
            if (m_Coins >= m_FaceCost[face])
            {
                m_Coins -= m_FaceCost[face];
                PlayerPrefs.SetInt("Coins", m_Coins);
                m_CoinsText.SetText("" + m_Coins);

                PlayerPrefs.SetInt("HasFace" + face, 1);
                m_FaceLock[face].gameObject.SetActive(false);

                m_Buy.Play();
            }
            else
            {
                m_Error.Play();
            }
        }
        else
        {
            for (int i = 0; i < m_Faces.Length; i++)
            {
                m_Faces[i].sprite = m_NormalPanel;
            }

            m_Face = face;
            PlayerPrefs.SetInt("Face", face);
            m_Faces[face].sprite = m_ChosenPanel;

            m_Equip.Play();
        }
    }

    public void BuyGun(int gun)
    {
        if (PlayerPrefs.GetInt("HasGun" + gun) <= 0)
        {
            if (m_Coins >= m_GunCost[gun])
            {
                m_Coins -= m_GunCost[gun];
                PlayerPrefs.SetInt("Coins", m_Coins);
                m_CoinsText.SetText("" + m_Coins);

                PlayerPrefs.SetInt("HasGun" + gun, 1);
                m_GunLock[gun].gameObject.SetActive(false);

                m_Buy.Play();
            }
            else
            {
                m_Error.Play();
            }
        }
        else
        {
            for (int i = 0; i < m_Guns.Length; i++)
            {
                m_Guns[i].sprite = m_NormalPanel;
            }

            m_Gun = gun;
            PlayerPrefs.SetInt("Gun", gun);
            m_Guns[gun].sprite = m_ChosenPanel;

            m_EquipGun.Play();
        }
    }

    public void Left()
    {
        if (m_FaceType.activeSelf)
        {
            m_HeadType.SetActive(true);
            m_FaceType.SetActive(false);

            m_Left.SetActive(false);
            m_Right.SetActive(true);
        }
        else if (m_GunType.activeSelf)
        {
            m_GunType.SetActive(false);
            m_FaceType.SetActive(true);

            m_Right.SetActive(true);
        }
    }

    public void Right()
    {
        if (m_HeadType.activeSelf)
        {
            m_HeadType.SetActive(false);
            m_FaceType.SetActive(true);

            m_Left.SetActive(true);

            if (m_HasGod <= 0)
            {
                m_Right.SetActive(false);
            }
        }
        else if (m_FaceType.activeSelf)
        {
            m_GunType.SetActive(true);
            m_FaceType.SetActive(false);

            m_Right.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            m_Coins += 100;
            PlayerPrefs.SetInt("Coins", m_Coins);
            m_CoinsText.SetText("" + m_Coins);
        }
    }
}
