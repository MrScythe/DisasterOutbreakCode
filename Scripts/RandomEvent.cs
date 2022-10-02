using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cinemachine;
using Unity.VisualScripting;

enum EventType
{
    Tsunami,
    Meteor,
    Quake,
    Tornado,
    Crows,
    EventCount
};

public class RandomEvent : MonoBehaviour
{
    [SerializeField] private GameObject[] m_Event;
    [SerializeField] private TextMeshProUGUI m_Counter, m_Round;
    [SerializeField] private CinemachineVirtualCamera m_Vcam;

    [SerializeField] private GameObject m_Gun;
    [SerializeField] private GameObject m_TornadoLeft, m_TornadoRight;

    [SerializeField] private GameObject m_Double;
    [SerializeField] private TextMeshProUGUI m_DisasterText;
    [SerializeField] private Animator m_Description;

    [SerializeField] private Animator m_Light;

    private EventType m_CurrentEvent, m_SecondEvent;
    private float m_Time, m_MaxTime = 9;

    public int m_CurrentRound;
    private bool m_IsAnimating, m_IsGun, m_IsActive;

    // Start is called before the first frame update
    void Start()
    {
        m_Vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
    }

    // Update is called once per frame
    void Update()
    {
        m_Counter.SetText("" + (int)(m_Time + 1));

        if (m_IsAnimating)
        {
            if (m_Description.GetCurrentAnimatorStateInfo(0).IsName("End"))
            {
                m_IsAnimating = false;
                m_Description.gameObject.SetActive(false);
            }
        }
        else
        {
            if (m_Time > 0)
            {
                m_Time -= 1 * Time.deltaTime;

                if (!m_IsActive && !m_IsGun)
                {
                    m_IsActive = true;
                    m_Event[(int)m_CurrentEvent].SetActive(true);

                    if (m_Double.activeSelf)
                    {
                        m_Event[(int)m_SecondEvent].SetActive(true);
                    }
                }
            }
            else
            {
                SetEvent();
                m_Time = m_MaxTime;
            }
        }
    }

    void SetEvent()
    {
        EventType lastEvent = m_CurrentEvent;
        m_CurrentRound++;

        m_Round.SetText("Round " + m_CurrentRound);

        m_Vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
        m_Event[(int)m_CurrentEvent].SetActive(false);
        m_Event[(int)m_SecondEvent].SetActive(false);

        m_Gun.SetActive(false);
        m_TornadoLeft.SetActive(false);
        m_TornadoRight.SetActive(false);

        m_Double.SetActive(false);

        m_Description.gameObject.SetActive(true);
        m_IsAnimating = true;
        m_IsGun = false;
        m_IsActive = false;
        m_Light.SetBool("Fire", false);

        if (m_CurrentRound == 5)
        {
            m_Gun.SetActive(true);
            m_DisasterText.SetText("Gun??");
            m_IsGun = true;

            PlayerPrefs.SetInt("God", 1);
        }
        else
        {
            while (m_CurrentEvent == lastEvent)
            {
                m_CurrentEvent = (EventType)Random.Range(0, (int)EventType.EventCount);

                while (m_CurrentEvent == EventType.Crows && m_CurrentRound <= 5)
                {
                    m_CurrentEvent = (EventType)Random.Range(0, (int)EventType.EventCount);
                }
            }

            m_DisasterText.SetText("" + m_CurrentEvent);

            if (m_CurrentRound % 3 == 0 && m_CurrentRound > 5)
            {
                m_Double.SetActive(true);

                while (m_SecondEvent == m_CurrentEvent)
                {
                    m_SecondEvent = (EventType)Random.Range(0, (int)EventType.EventCount);
                }

                m_DisasterText.SetText(m_CurrentEvent + " + " + m_SecondEvent);
            }

            if (m_CurrentEvent == EventType.Meteor || m_SecondEvent == EventType.Meteor)
            {
                m_Light.SetBool("Fire", true);
            }

            if (m_CurrentEvent == EventType.Quake || m_SecondEvent == EventType.Quake)
            {
                m_Vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 3;
            }

            if (m_CurrentEvent == EventType.Tornado || m_SecondEvent == EventType.Tornado)
            {
                int random = Random.Range(0, 2);

                if (random == 0)
                {
                    m_TornadoLeft.SetActive(true);
                    m_TornadoRight.SetActive(false);
                }
                else
                {
                    m_TornadoLeft.SetActive(false);
                    m_TornadoRight.SetActive(true);
                }
            }
        }
    }
}
