using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crow_Behaviour : MonoBehaviour
{
    [SerializeField] private float m_Speed, m_SteerSpeed, m_AttackSpeed;
    [SerializeField] private AudioSource m_AttackSound;

    private Transform m_PlayerPos;
    private float m_CurrentSpeed, m_TargetSpeed;

    private float m_Delay, m_MaxDelay;
    private float m_AttackD, m_MaxAttackD = 1.5f;
    private Vector2 m_TargetPos, m_OldPos;
    private bool m_HasReached, m_HasPlayedSound;

    // Start is called before the first frame update
    void Start()
    {
        m_PlayerPos = GameObject.FindGameObjectWithTag("Player").transform;

        m_MaxDelay = Random.Range(1f, 5f);

        int random = Random.Range(0, 2);

        if (random == 0)
        {
            m_CurrentSpeed = -m_Speed;
            m_TargetSpeed = -m_Speed;
        }
        else
        {
            m_CurrentSpeed = m_Speed;
            m_TargetSpeed = m_Speed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Delay < m_MaxDelay)
        {
            m_Delay += 1 * Time.deltaTime;

            if (transform.position.x <= -10)
            {
                m_TargetSpeed = -m_Speed;
            }
            else if (transform.position.x >= 10)
            {
                m_TargetSpeed = m_Speed;
            }

            if (m_CurrentSpeed > m_TargetSpeed)
            {
                m_CurrentSpeed -= m_SteerSpeed * Time.deltaTime;
            }
            else if (m_CurrentSpeed < m_TargetSpeed)
            {
                m_CurrentSpeed += m_SteerSpeed * Time.deltaTime;
            }
            else
            {
                m_CurrentSpeed = m_TargetSpeed;
            }

            transform.Translate(Vector3.left * m_CurrentSpeed * Time.deltaTime, Space.World);
        }
        else
        {
            if (m_AttackD < m_MaxAttackD)
            {
                m_AttackD += 1 * Time.deltaTime;
                m_TargetPos = m_PlayerPos.position;
                m_OldPos = transform.position;
            }
            else
            {
                if (!m_HasPlayedSound)
                {
                    m_AttackSound.Play();
                    m_HasPlayedSound = true;
                }

                if ((Vector2)transform.position != m_TargetPos && !m_HasReached)
                {
                    transform.position = Vector2.MoveTowards(transform.position, m_TargetPos, m_AttackSpeed * Time.deltaTime);
                }
                else
                {
                    m_HasReached = true;
                }

                if (m_HasReached)
                {
                    if ((Vector2)transform.position != m_OldPos)
                    {
                        transform.position = Vector2.MoveTowards(transform.position, m_OldPos, m_AttackSpeed * Time.deltaTime);
                    }
                    else
                    {
                        m_HasReached = false;
                        m_HasPlayedSound = false;

                        m_Delay = 0;
                        m_AttackD = 0;
                        m_MaxDelay = Random.Range(1f, 5f);
                    }
                }
            }
        }
    }
}
