using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Stats : MonoBehaviour
{
    [SerializeField] private int m_ExpAmount;
    [SerializeField] private int m_Health;
    [SerializeField] private Animator m_Animation;
    [SerializeField] private GameObject m_Particle;
    [SerializeField] private bool m_ParticleOnHit;
    [SerializeField] private GameObject m_Coin;

    private Player_Stats m_Stats;
    private int m_DropChance;

    // Start is called before the first frame update
    void Start()
    {
        m_Stats = FindObjectOfType<Player_Stats>();
        m_DropChance = Random.Range(0, 100);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            m_Health -= collision.GetComponent<Bullet>().m_Damage;
            m_Animation.SetTrigger("Hurt");

            if (m_ParticleOnHit)
            {
                GameObject particle = Instantiate(m_Particle, transform.position, Quaternion.identity);
                Destroy(particle, 1f);
            }

            if (m_Health <= 0)
            {
                transform.parent.gameObject.SetActive(false);
                m_Health = 5;

                if (!m_ParticleOnHit)
                {
                    GameObject particle = Instantiate(m_Particle, transform.position, Quaternion.identity);
                    Destroy(particle, 2f);
                }

                if (m_DropChance <= 70)
                {
                    GameObject coin = Instantiate(m_Coin, transform.position, Quaternion.identity);
                    Destroy(coin, 2f);
                }

                m_Stats.AddExp(m_ExpAmount);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
