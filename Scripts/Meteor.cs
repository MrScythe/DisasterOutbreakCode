using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    [SerializeField] private float m_FallingSpeed, m_HorizontalSpeed;
    [SerializeField] private GameObject m_Shard;
    private float m_Speed;

    // Start is called before the first frame update
    void Start()
    {
        m_Speed = Random.Range(m_HorizontalSpeed - 2, m_HorizontalSpeed + 2);
        Destroy(gameObject, 1f);
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
        transform.Translate(Vector3.down * m_FallingSpeed * Time.deltaTime, Space.World);
        transform.Translate(Vector3.left * m_Speed * Time.deltaTime, Space.World);
    }
}
