using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetGun : MonoBehaviour
{
    [SerializeField] private Sprite[] m_Gun;
    [SerializeField] private bool m_IsUI, m_IsShooter;

    private SpriteRenderer m_Sprite;
    private Image m_Image;
    private Gun_Shoot m_Shoot;

    private int m_CurrentGun;

    // Start is called before the first frame update
    void Start()
    {
        if (m_IsUI)
        {
            m_Image = GetComponent<Image>();
            m_Image.sprite = m_Gun[PlayerPrefs.GetInt("Gun")];
        }
        else
        {
            m_Sprite = GetComponent<SpriteRenderer>();
            m_Sprite.sprite = m_Gun[PlayerPrefs.GetInt("Gun")];
        }

        if (m_IsShooter)
        {
            m_Shoot = FindObjectOfType<Gun_Shoot>();
            m_CurrentGun = PlayerPrefs.GetInt("Gun");

            switch (m_CurrentGun)
            {
                case 0: //SMG
                    m_Shoot.m_Damage = 1;
                    m_Shoot.m_BulletSpeed = 50;
                    m_Shoot.m_FireRate = 5;
                    m_Shoot.m_Spread = 7;
                    m_Shoot.m_WeaponType = WeaponType.SMG;
                    break;
                case 1: //Silencer
                    m_Shoot.m_Damage = 2;
                    m_Shoot.m_BulletSpeed = 75;
                    m_Shoot.m_FireRate = 3;
                    m_Shoot.m_Spread = 1;
                    m_Shoot.m_WeaponType = WeaponType.Silencer;
                    break;
                case 2: //Revolver
                    m_Shoot.m_Damage = 4;
                    m_Shoot.m_BulletSpeed = 50;
                    m_Shoot.m_FireRate = 2;
                    m_Shoot.m_Spread = 3;
                    m_Shoot.m_WeaponType = WeaponType.Revolver;
                    break;
                case 3: //Sniper
                    m_Shoot.m_Damage = 5;
                    m_Shoot.m_BulletSpeed = 100;
                    m_Shoot.m_FireRate = 1;
                    m_Shoot.m_Spread = 0;
                    m_Shoot.m_WeaponType = WeaponType.Sniper;
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
