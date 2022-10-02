using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor_Spawner : MonoBehaviour
{
    [SerializeField] private GameObject m_Meteor;

    private float m_Delay, m_MaxDelay;

    // Start is called before the first frame update
    void Start()
    {
        m_MaxDelay = Random.Range(0f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Delay < m_MaxDelay)
        {
            m_Delay += 1 * Time.deltaTime;
        }
        else
        {
            Vector2 randomPos = new Vector2(Random.Range(transform.position.x - transform.localScale.x / 2, transform.position.x + transform.localScale.x / 2), transform.position.y);
            Instantiate(m_Meteor, randomPos, Quaternion.identity);

            m_Delay = 0;
            m_MaxDelay = Random.Range(0.4f, 1f);
        }
    }
}
