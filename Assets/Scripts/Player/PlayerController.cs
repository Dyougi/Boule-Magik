using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    [SerializeField] float m_startVelocityY;
    [SerializeField] float m_smoothFactor;
    [SerializeField] Transform m_playerLimit;


    Rigidbody2D m_rigidbody;
    float m_velocityY;
    bool m_canDoubleJump;
    Vector2 m_savePosition;
    float m_savePosX;
    bool m_isPaused;

    // UNITY METHODES

    void Start () {
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_velocityY = m_startVelocityY;
        m_canDoubleJump = true;
        m_savePosition = transform.position;
    }
	
	void Update () {
        if (!m_isPaused)
        {
            ManageInputs();
            if (m_canDoubleJump == false && m_rigidbody.velocity.y == 0)
                m_canDoubleJump = true;

            //Avance auto du joueur
            if (transform.position.x < m_playerLimit.position.x && transform.position.x == m_savePosX)
            {
                transform.position = Vector3.Lerp(transform.position, m_playerLimit.position, Time.deltaTime * m_smoothFactor);
            }
            m_savePosX = transform.position.x;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "loseTrigger")
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().lose();
        }
    }

    // PRIVATE METHODES

    void ManageInputs()
    {
        if (Input.GetButtonDown("Jump"))
        {
            doJump();
        }
        if (Input.GetButton("Power"))
        {

        }
    }

    void doJump()
    {
        if (m_rigidbody.velocity.y == 0)
        {
            m_rigidbody.AddForce(new Vector2(0, m_velocityY));
        }
        else if (m_rigidbody.velocity.y > -5 && m_canDoubleJump == true)
        {
            m_canDoubleJump = false;
            m_rigidbody.AddForce(new Vector2(0, m_velocityY));
        }
    }

    // PUBLIC METHODES

    public void resetPlayer()
    {
        transform.position = m_savePosition;
    }

    public bool Pause
    {
        get
        {
            return m_isPaused;
        }

        set
        {
            m_isPaused = value;
        }

    }
}
