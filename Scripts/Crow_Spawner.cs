using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crow_Spawner : MonoBehaviour
{
    [SerializeField] private GameObject m_Crow;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SpawnCrows()
    {
        int randomCount = Random.Range(3, 8);

        for (int i = 0; i < randomCount; i++)
        {
            Vector2 randomPos = new Vector2(Random.Range(-5f, 5f), Random.Range(8f, 9f));

            Instantiate(m_Crow, randomPos, Quaternion.identity);
        }
    }
}
