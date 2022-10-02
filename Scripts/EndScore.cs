using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_Rounds, m_Highscore, m_Coins;
    [SerializeField] private GameObject m_HasHighscore;
    [SerializeField] private Color m_Normal, m_Earning;

    private int m_HighscoreRounds;
    private int m_TotalCoins, m_NewCoins;

    private Player_Stats m_Stats;
    private RandomEvent m_Event;

    private bool m_AddCoins;
    private float m_Delay, m_MaxDelay = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        m_Stats = FindObjectOfType<Player_Stats>();
        m_Event = FindObjectOfType<RandomEvent>();

        m_Rounds.SetText("" + m_Event.m_CurrentRound);

        m_HighscoreRounds = PlayerPrefs.GetInt("Highscore");

        m_TotalCoins = PlayerPrefs.GetInt("Coins");
        m_NewCoins = m_TotalCoins += m_Stats.m_Coins;
        PlayerPrefs.SetInt("Coins", m_NewCoins);
        m_Coins.SetText("" + m_TotalCoins);
        m_Coins.color = m_Normal;

        if (m_Event.m_CurrentRound > m_HighscoreRounds)
        {
            m_HighscoreRounds = m_Event.m_CurrentRound;
            PlayerPrefs.SetInt("Highscore", m_Event.m_CurrentRound);

            m_HasHighscore.SetActive(true);
        }

        m_Highscore.SetText("" + m_HighscoreRounds);
    }

    public void AddCoins()
    {
        if (m_Stats.m_Coins > 0)
        {
            m_AddCoins = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (m_AddCoins)
        {
            m_Coins.color = m_Earning;

            if (m_Delay < m_MaxDelay)
            {
                m_Delay += 1 * Time.deltaTime;
            }
            else
            {
                if (m_TotalCoins < m_NewCoins)
                {
                    m_TotalCoins++;
                    m_Coins.SetText("" + m_TotalCoins);

                    m_Delay = 0;
                }
                else
                {
                    m_AddCoins = false;
                    m_Coins.color = m_Normal;
                }
            }
        }
    }
}
