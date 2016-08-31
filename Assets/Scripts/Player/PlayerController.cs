using UnityEngine;
using System.Collections;
using System.Linq;

public class PlayerController : MonoBehaviour {

    [SerializeField] AudioClip m_jumpSound; // jump sound !
    [SerializeField] float m_startVelocityY; // the start velocity for y axe (jump)
    [SerializeField] float m_smoothFactor; // the smooth factor for the positive moving of the player on the positive axe x
    [SerializeField] Transform m_playerLimit; // the position of the limit of where the player can go

    AudioSource m_managerAudio;
    Transform m_bouleMagikMesh;
    Rigidbody2D m_rigidbody;
    float m_velocityY;
    bool m_canDoubleJump;
    Vector2 m_savePosition;
    float m_savePosX;
    float m_speedScroll;
    bool m_isPaused;

    // UNITY METHODES

    void Start () {
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_managerAudio = GetComponent<AudioSource>();
        m_velocityY = m_startVelocityY;
        m_canDoubleJump = true;
        m_savePosition = transform.position;
        Transform[] tabTransform =  GetComponentsInChildren<Transform>();
        m_bouleMagikMesh = tabTransform[1];
    }

	void Update () {
        if (!m_isPaused)
        {
            manageInputs();
            if (m_canDoubleJump == false && m_rigidbody.velocity.y == 0)
                m_canDoubleJump = true;
            //Avance auto du joueur
            if (transform.position.x < m_playerLimit.position.x && transform.position.x == m_savePosX)
            {
                transform.position = Vector3.Lerp(transform.position, m_playerLimit.position, Time.deltaTime * m_smoothFactor);
                m_bouleMagikMesh.Rotate(new Vector3(0, 0, -(90 * m_speedScroll * Time.deltaTime)));
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
        if (other.tag == "bonus")
        {
            switch (other.GetComponent<Bonus>().BonusType)
            {
                case GameManager.e_bonusType.SPEED:
                    applySpeedBonus();
                    break;
            }
            Destroy(other.gameObject);
        }
    }

    // PRIVATE METHODES

    void manageInputs()
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
            m_managerAudio.PlayOneShot(m_jumpSound);
            m_rigidbody.AddForce(new Vector2(0, m_velocityY));
        }
        else
            if (m_rigidbody.velocity.y > -5 && m_canDoubleJump)
            {
                m_canDoubleJump = false;
                m_managerAudio.PlayOneShot(m_jumpSound);
                m_rigidbody.AddForce(new Vector2(0, m_velocityY));
            }
    }

    void applySpeedBonus()
    {

    }

    void removeSpeedBonus()
    {

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

    public float Speed
    {
        get
        {
            return m_speedScroll;
        }

        set
        {
            m_speedScroll = value;
        }
    }
}
