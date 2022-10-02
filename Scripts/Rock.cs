using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField] private float m_FallingSpeed, m_Acceleration;
    [SerializeField] private GameObject m_Shard;
    private float m_Speed;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 2f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject shard = Instantiate(m_Shard, transform.position, Quaternion.identity);
        Destroy(shard, 1f);

        Destroy(gameObject);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (m_Speed < m_FallingSpeed)
        {
            m_Speed += m_Acceleration * Time.deltaTime;
        }
        else
        {
            m_Speed = m_FallingSpeed;
        }

        transform.Translate(Vector3.down * m_Speed * Time.deltaTime, Space.World);
    }
}
