using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinDrop : MonoBehaviour
{
    [SerializeField] private float m_Speed;

    private Transform m_PlayerPos;
    private bool m_InRange;
    private float m_CurrentSpeed;

    // Start is called before the first frame update
    void Start()
    {
        m_PlayerPos = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            m_InRange = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        m_CurrentSpeed += m_Speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, m_PlayerPos.position, m_CurrentSpeed * Time.deltaTime);
    }
}
