using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_CardName, m_Description;
    [SerializeField] private Card[] m_Card;
    [SerializeField] private GameObject m_LevelUp;
    [SerializeField] private Image m_CardSprite;

    private Player_Stats m_Stats;
    private Player_Movement m_Movement;
    private Gun_Shoot m_Shoot;

    private int m_CurrentCard;

    // Start is called before the first frame update
    void Start()
    {
        m_Stats = FindObjectOfType<Player_Stats>();
        m_Movement = FindObjectOfType<Player_Movement>();
        m_Shoot = FindObjectOfType<Gun_Shoot>();
    }

    public void SetCard()
    {
        m_CurrentCard = Random.Range(0, m_Card.Length);

        m_CardName.SetText(m_Card[m_CurrentCard].m_CardName);
        m_Description.SetText(m_Card[m_CurrentCard].m_StatDesc);
        m_CardSprite.sprite = m_Card[m_CurrentCard].m_CardSprite;
    }

    public void SetUpgrade()
    {
        float statChange = m_Card[m_CurrentCard].m_StatChange;

        switch (m_Card[m_CurrentCard].m_StatType)
        {
            case StatType.Health:
                m_Stats.m_CurrentHealth += (int)statChange;
                m_Stats.m_MaxHealth += (int)statChange;
                break;
            case StatType.Damage:
                m_Shoot.m_Damage += (int)statChange;
                break;
            case StatType.FireRate:
                m_Shoot.m_FireRate += statChange;
                break;
            case StatType.Accuracy:
                m_Shoot.m_Spread -= statChange;
                break;
            case StatType.Speed:
                m_Movement.m_Speed += statChange;
                break;
        }

        Time.timeScale = 1f;
        m_LevelUp.SetActive(false);
    }
}
