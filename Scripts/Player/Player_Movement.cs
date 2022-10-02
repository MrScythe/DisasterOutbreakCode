using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Player_Movement : MonoBehaviour
{
    [SerializeField] public float m_Speed, m_JumpHeight;
    [SerializeField] private float m_Acceleration, m_Drag;
    [SerializeField] private float m_TornadoDrag;

    [SerializeField] private GameObject m_TLeft, m_TRight;
    [SerializeField] private AudioSource m_JumpSound;

    private int m_TotalJumps = 2;

    private Animator m_Animation;

    private float m_CurrentSpeed;
    private bool m_Right;

    private Rigidbody2D m_Rigidbody;
    private bool m_Jump;

    private float m_Fall = 2.5f, m_Low = 20f;

    // Use this for initialization
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_Animation = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        m_Jump = true;
        m_TotalJumps = 2;

        m_Animation.SetBool("Grounded", true);
    }

    private void Update()
    {
        // Cast a ray straight down.
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f);

        // If it hits something...
        if (hit.collider.CompareTag("Ground"))
        {
            m_Jump = true;
        }

        if (Input.GetKeyDown(KeyCode.Space) && m_Jump)
        {
            m_Rigidbody.velocity = Vector2.up * m_JumpHeight;
            m_Animation.SetTrigger("Jump");
            m_JumpSound.Play();

            m_TotalJumps--;

            if (m_TotalJumps <= 0)
            {
                m_Jump = false;
            }
        }

        if (m_Rigidbody.velocity.y < 0)
        {
            m_Rigidbody.velocity += Vector2.up * Physics2D.gravity.y * (m_Fall - 1) * Time.deltaTime;
        }

        else if (m_Rigidbody.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            m_Rigidbody.velocity += Vector2.up * Physics2D.gravity.y * (m_Low - 1) * Time.deltaTime;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (m_TRight.transform.parent.gameObject.activeSelf)
        {
            if (m_TRight.activeSelf)
            {
                transform.Translate(Vector3.right * m_TornadoDrag * Time.deltaTime, Space.World);
            }
            else if (m_TLeft.activeSelf)
            {
                transform.Translate(Vector3.left * m_TornadoDrag * Time.deltaTime, Space.World);
            }
        }

        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            m_Animation.SetBool("Walking", true);

            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);

                if (m_CurrentSpeed < m_Speed)
                {
                    m_CurrentSpeed += m_Acceleration * Time.deltaTime;
                }
                else
                {
                    m_CurrentSpeed = m_Speed;
                }

                transform.Translate(Vector3.right * m_CurrentSpeed * Time.deltaTime, Space.World);
                m_Right = true;
            }

            else if (Input.GetAxisRaw("Horizontal") < 0)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);

                if (m_CurrentSpeed > -m_Speed)
                {
                    m_CurrentSpeed -= m_Acceleration * Time.deltaTime;
                }
                else
                {
                    m_CurrentSpeed = -m_Speed;
                }

                transform.Translate(Vector3.right * m_CurrentSpeed * Time.deltaTime, Space.World);
                m_Right = false;
            }
        }
        else if (m_CurrentSpeed < -0.5f || m_CurrentSpeed > 0.5f)
        {
            m_Animation.SetBool("Walking", false);

            if (m_CurrentSpeed > 0)
            {
                m_CurrentSpeed -= m_Drag * Time.deltaTime;
            }
            else if (m_CurrentSpeed < 0)
            {
                m_CurrentSpeed += m_Drag * Time.deltaTime;
            }

            if (m_Right)
            {
                transform.Translate(Vector3.right * m_CurrentSpeed * Time.deltaTime, Space.World);
            }
            else
            {
                transform.Translate(Vector3.right * m_CurrentSpeed * Time.deltaTime, Space.World);
            }
        }
        else
        {
            m_CurrentSpeed = 0;
            m_Animation.SetBool("Walking", false);
        }
    }

    public void HasJumped()
    {
        m_Animation.SetBool("Grounded", false);
    }
}
