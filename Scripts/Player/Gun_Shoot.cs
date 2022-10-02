using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{ 
    SMG,
    Silencer,
    Revolver,
    Sniper
};

public class Gun_Shoot : MonoBehaviour
{
    [SerializeField] private GameObject m_Bullet;
    [SerializeField] private GameObject m_SMGSound, m_SilencerSound, m_RevolverSound, m_SniperSound;
    [SerializeField] private Transform m_Emitter;
    [SerializeField] public float m_FireRate;
    [SerializeField] public float m_Spread;
    [SerializeField] private Animator m_Shoot;

    public int m_Damage;
    public int m_BulletSpeed;
    public WeaponType m_WeaponType;

    private float m_Delay, m_MaxDelay = 1;

    // Start is called before the first frame update
    void Start()
    {
        m_Delay = m_MaxDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Delay < m_MaxDelay)
        {
            m_Delay += m_FireRate * Time.deltaTime;
        }
        else if (Input.GetMouseButton(0))
        {
            Quaternion rotation = Quaternion.Euler(m_Emitter.eulerAngles.x, m_Emitter.eulerAngles.y, m_Emitter.eulerAngles.z + Random.Range(-m_Spread, m_Spread));
            GameObject bullet = Instantiate(m_Bullet, m_Emitter.position, rotation);

            Bullet comp = bullet.GetComponent<Bullet>();
            comp.m_Damage = m_Damage;
            comp.m_Speed = m_BulletSpeed;
            Destroy(bullet, 1f);


            switch (m_WeaponType)
            {
                case WeaponType.SMG:
                    GameObject sound = Instantiate(m_SMGSound, m_Emitter.position, rotation);
                    Destroy(sound, 1f);
                    break;
                case WeaponType.Silencer:
                    GameObject sound2 = Instantiate(m_SilencerSound, m_Emitter.position, rotation);
                    Destroy(sound2, 1f);
                    break;
                case WeaponType.Revolver:
                    GameObject sound3 = Instantiate(m_RevolverSound, m_Emitter.position, rotation);
                    Destroy(sound3, 1f);
                    break;
                case WeaponType.Sniper:
                    GameObject sound4 = Instantiate(m_SniperSound, m_Emitter.position, rotation);
                    Destroy(sound4, 1f);
                    break;
            }

            m_Shoot.SetTrigger("Shoot");

            m_Delay = 0;
        }
    }
}
