using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player_Stats : MonoBehaviour
{
    [SerializeField] public int m_MaxHealth;
    [SerializeField] private GameObject m_Weapon, m_ArmR, m_Events;
    [SerializeField] private Slider m_ExpBar;
    [SerializeField] private float m_ExpSpeed;
    [SerializeField] private GameObject m_LevelUp;
    [SerializeField] private CardManager[] m_Upgrade;

    [SerializeField] private AudioSource m_Reload, m_Coin, m_Splash;
    [SerializeField] private RandomEvent m_Manager;

    [SerializeField] private TextMeshProUGUI m_TotalCoins;
    [SerializeField] private GameObject m_EndScreen;

    public int m_CurrentHealth;
    private bool m_HasWeapon;

    private int m_CurrentEXP, m_MaxEXP = 10;
    private float m_Exp;
    private int m_EXPIncrement = 2;

    private Rigidbody2D m_Rigidbody;
    private Animator m_Animation;
    private Player_Movement m_Movement;

    public int m_Coins;

    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_Animation = GetComponent<Animator>();
        m_Movement = GetComponent<Player_Movement>();

        m_CurrentHealth = m_MaxHealth;
        m_ExpBar.maxValue = m_MaxEXP;
        m_ExpBar.value = 0;

        m_ExpBar.transform.parent.gameObject.SetActive(false);

        m_Weapon.SetActive(false);

        m_TotalCoins.SetText("0");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("PickUp") && !collision.CompareTag("Harmless") && !collision.CompareTag("Coin") && !collision.CompareTag("Range"))
        {
            m_CurrentHealth--;
            m_Animation.SetTrigger("Hurt");

            Vector2 direction = (transform.position - collision.gameObject.transform.position).normalized;
            m_Rigidbody.AddRelativeForce(direction * 1000);

            if (m_CurrentHealth <= 0)
            {
                m_Animation.SetTrigger("Dead");
                m_Movement.enabled = false;
                m_Weapon.SetActive(false);

                m_Events.SetActive(false);
                m_Manager.enabled = false;

                m_Rigidbody.simulated = false;
                m_EndScreen.SetActive(true);
            }

            if (collision.CompareTag("Dead"))
            {
                m_Splash.Play();
                m_Movement.enabled = false;
                m_Weapon.SetActive(false);

                m_Events.SetActive(false);
                m_Manager.enabled = false;

                m_Rigidbody.simulated = false;
                m_EndScreen.SetActive(true);
            }
        }
        else if (collision.CompareTag("PickUp"))
        {
            m_Weapon.SetActive(true);
            m_Reload.Play();

            m_ArmR.SetActive(false);
            m_ExpBar.transform.parent.gameObject.SetActive(true);
            m_HasWeapon = true;
        }
        else if (collision.CompareTag("Coin"))
        {
            m_Coins++;
            m_Coin.Play();

            m_TotalCoins.SetText("" + m_Coins);

            Destroy(collision.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Exp < m_CurrentEXP)
        {
            m_Exp += m_ExpSpeed * Time.deltaTime;

            if (m_Exp >= m_MaxEXP)
            {
                m_Exp -= m_MaxEXP;
                m_CurrentEXP -= m_MaxEXP;
                m_MaxEXP += m_EXPIncrement;

                m_ExpBar.maxValue = m_MaxEXP;

                for (int i = 0; i < m_Upgrade.Length; i++)
                {
                    m_Upgrade[i].SetCard();
                }
                
                m_LevelUp.SetActive(true);
                Time.timeScale = 0;
            }

            m_ExpBar.value = m_Exp;
        }
        else
        {
            m_Exp = m_CurrentEXP;
            m_ExpBar.value = m_Exp;
        }
    }

    public void AddExp(int amount)
    {
        if (m_HasWeapon)
        {
            m_CurrentEXP += amount;
        }
    }
}
