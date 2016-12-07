using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour {

    [SerializeField] AudioClip m_jumpSound; // jump sound !
    [SerializeField] AudioClip m_bonusSpeedSound; // jump sound !
    [SerializeField] AudioClip m_bonusPointSound; // jump sound !
    [SerializeField] float m_startVelocityY; // the start velocity for y axe (jump)
    [SerializeField] float m_smoothFactorDefault; // the smooth factor for the positive moving of the player on the positive axe x
    [SerializeField] Transform m_playerLimit; // the position of the limit of where the player can go
    [SerializeField] Transform m_wallCheck; // trigger to check if the player is touching a wall
    [SerializeField] Transform m_roofCheck; // trigger to check if the player is touching a wall
    [SerializeField] Transform m_raycastX; // 
    [SerializeField] Transform m_raycastY; // 
    [SerializeField] LayerMask m_whatIsWall; // A mask determining what is ground to the character
    [SerializeField] LayerMask m_whatIsRoof; // A mask determining what is ground to the character
    [SerializeField] ParticleSystem m_speedBonusPS;
    [SerializeField] ParticleSystem m_pointBonusPS;
    [SerializeField] int m_multRotationDefault;

    float m_xMaxLimit;
    float m_YMaxLimit;

    AudioSource m_managerAudio;
    Transform m_bouleMagikMesh;
    Rigidbody2D m_rigidbody;
    float m_velocityY;
    bool m_canDoubleJump;
    Vector2 m_positionDefault;
    float m_speedScroll;
    float m_smoothFactor;
    int m_multRotation;
    bool m_isPaused;


    // UNITY METHODES

    void Start () {
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_managerAudio = GetComponent<AudioSource>();
        Transform[] tabTransform =  GetComponentsInChildren<Transform>();
        m_bouleMagikMesh = tabTransform[1];
        m_positionDefault = transform.position;
        Bonus.OnBonusOn += applyBonus;
        Bonus.OnBonusOff += removeBonus;
        resetPlayer();
    }

    void LateUpdate()
    {
        RaycastHit2D hitX = Physics2D.Raycast(m_raycastX.position, Vector3.right, LayerMask.NameToLayer("Ground"));
        Debug.DrawRay(m_raycastX.position, Vector3.right, Color.green);
        if (hitX && hitX.distance < 0.5)
        {
            transform.position = new Vector3(transform.position.x - (0.5f - hitX.distance), transform.position.y, transform.position.z);
        }
    }

	void Update()
    {
        if (!m_isPaused)
        {
            if (m_canDoubleJump == false && m_rigidbody.velocity.y == 0)
                m_canDoubleJump = true;
            if (!checkIfNear(m_wallCheck.position, m_whatIsWall, 0.1f) && transform.position.x < m_playerLimit.position.x)
            {
                transform.position = Vector3.Lerp(transform.position, m_playerLimit.position, Time.deltaTime * m_smoothFactor);
                m_bouleMagikMesh.Rotate(new Vector3(0, 0, -(m_multRotation * m_speedScroll * Time.deltaTime)));
            }
            manageInputs();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "loseTrigger")
        {
            Debug.Log("Lose !");
            GameObject.Find("GameManager").GetComponent<GameManager>().lose();
        }
    }

    bool checkIfNear(Vector3 position, LayerMask whatIs, float range)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, range, whatIs);
        for (int i = 0; i < colliders.Length; i++)
            if (colliders[i].gameObject != gameObject && transform.position.x < m_playerLimit.position.x)
                return true;
        return false;
    }

    // PRIVATE METHODES

    void manageInputs()
    {
#if UNITY_ANDROID

        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began && !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                doJump();
            }
        }
#endif
#if UNITY_EDITOR
        if (Input.GetButtonDown("Jump"))
        {
            doJump();
        }
#endif
    }

    void doJump()
    {
        if (!checkIfNear(m_roofCheck.position, m_whatIsRoof, 0.1f))
        {
            if (m_rigidbody.velocity.y == 0)
            {
                Debug.Log("Jump");
                m_managerAudio.PlayOneShot(m_jumpSound);
                m_rigidbody.AddForce(new Vector2(0, m_velocityY));
            }
            else
            if (m_rigidbody.velocity.y > -5 && m_canDoubleJump)
            {
                Debug.Log("Double jump");
                m_canDoubleJump = false;
                m_managerAudio.PlayOneShot(m_jumpSound);
                m_rigidbody.velocity = Vector2.zero;
                m_rigidbody.AddForce(new Vector2(0, m_velocityY));
            }
        }
    }

    void applyBonus(GameManager.e_bonusType newBonus)
    {
        Debug.Log("applyBonus " + newBonus.ToString());
        switch (newBonus)
        {
            case GameManager.e_bonusType.SPEED:
                m_speedBonusPS.Play();
                m_multRotation = 180;
                m_smoothFactor = 0.5f;
                m_managerAudio.PlayOneShot(m_bonusSpeedSound);
                break;
            case GameManager.e_bonusType.POINT:
                m_pointBonusPS.Play();
                m_managerAudio.PlayOneShot(m_bonusPointSound);
                GameObject.Find("GameManager").GetComponent<GameManager>().updatePoint(3);
                break;
        }
    }

    void removeBonus(GameManager.e_bonusType newBonus)
    {
        Debug.Log("removeBonus " + newBonus.ToString());
        switch (newBonus)
        {
            case GameManager.e_bonusType.SPEED:
                m_speedBonusPS.Stop();
                m_multRotation = m_multRotationDefault;
                m_smoothFactor = m_smoothFactorDefault;
                break;
            case GameManager.e_bonusType.POINT:
                break;
        }
    }


    // PUBLIC METHODES


    public void resetPlayer()
    {
        Debug.Log("Reset player");
        m_smoothFactor = m_smoothFactorDefault;
        m_velocityY = m_startVelocityY;
        m_canDoubleJump = true;
        m_multRotation = m_multRotationDefault;
        transform.position = m_positionDefault;
        transform.eulerAngles = Vector3.zero;
    }

    public bool Pause
    {
        get
        {
            return m_isPaused;
        }

        set
        {
            if (value)
            {
                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
            }
            else
            {
                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            }
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
