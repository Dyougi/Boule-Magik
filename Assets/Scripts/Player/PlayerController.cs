using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System.IO;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    AudioClip m_jumpSound; // jump sound !

    [SerializeField]
    AudioClip m_bonusSpeedUpSound; // bonus speed up sound !

    [SerializeField]
    AudioClip m_bonusScrollDownSound; // bonus point down sound !

    [SerializeField]
    AudioClip m_bonusScrollUpSound; // bonus point up sound !

    [SerializeField]
    AudioClip m_bonusPointSound; // bonus point sound !

    [SerializeField]
    AudioClip m_bonusSuperPowerSound; // bonus super power sound !

    [SerializeField]
    AudioClip m_bonusSuperPowerActivatedSound; // bonus super power sound when used !

    [SerializeField]
    float m_startVelocityY; // the start velocity for y axe (jump)

    [SerializeField]
    float m_smoothFactorDefault; // the smooth factor for the positive moving of the player on the positive axe x

    [SerializeField]
    Transform m_playerLimit; // the position of the limit of where the player can go

    [SerializeField]
    Transform m_wallCheck; // trigger to check if the player is touching a wall

    [SerializeField]
    Transform m_roofCheck; // trigger to check if the player is touching a wall

    [SerializeField]
    Transform m_groundCheck; // trigger to check if the player is grounded

    [SerializeField]
    Transform m_raycastX; // Position of the origine of the raycast to see collider on the x axe

    [SerializeField]
    Transform m_raycastY; // Position of the origine of the raycast to see collider on the y axe

    [SerializeField]
    Transform m_raycastY2; // Position of the origine of the raycast to see collider on the y axe for top

    [SerializeField]
    LayerMask m_whatIsWall; // A mask determining what is wall to the character

    [SerializeField]
    LayerMask m_whatIsRoof; // A mask determining what is roof to the character

    [SerializeField]
    LayerMask m_whatIsGround; // A mask determining what is ground to the character

    [SerializeField]
    ParticleSystem m_speedUpBonusPS; // Particle for the speed up bonus

    [SerializeField]
    ParticleSystem m_scrollDownBonusPS; // Particle for the scroll down bonus

    [SerializeField]
    ParticleSystem m_scrollUpBonusPS; // Particle for the scroll up bonus

    [SerializeField]
    ParticleSystem m_pointBonusPS; // Particle for the point bonus

    [SerializeField]
    ParticleSystem m_powerBonusPS; // Particle for the power bonus

    [SerializeField]
    int m_multRotationDefault;

    OptionManager m_optionManager;

    List<BonusApplied> m_bonusList;

    AudioSource m_managerAudio;
    Transform m_bouleMagikMesh;
    Rigidbody2D m_rigidbody;
    ParticleSystem m_currentSpeedUpParticleSystem;
    ParticleSystem m_currentScrollDownParticleSystem;
    ParticleSystem m_currentScrollUpParticleSystem;
    ParticleSystem m_currentPointParticleSystem;
    float m_velocityY;
    bool m_canDoubleJump;
    Vector3 m_positionDefault;
    float m_speedScroll;
    public float m_smoothFactor;
    int m_multRotation;
    bool m_isPaused;
    bool m_isSuperPowerUp;


    // UNITY METHODES


    void Start () {
        m_bonusList = new List<BonusApplied>();
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_managerAudio = GetComponent<AudioSource>();
        m_optionManager = OptionManager.Instance;
        Transform[] tabTransform =  GetComponentsInChildren<Transform>();
        m_bouleMagikMesh = tabTransform[1];
        m_positionDefault = transform.position;
        ResetPlayer();
    }

    void LateUpdate()
    {
        RaycastHit2D hitX = Physics2D.Raycast(m_raycastX.position, Vector3.right, 0.7f, m_whatIsWall);
        if (hitX && hitX.distance < 0.6)
        {
#if UNITY_EDITOR
            LogManager.LogMessageToFile("hitX.distance     :\t" + hitX.distance);
            LogManager.LogMessageToFile("hitX.point        :\t" + hitX.point);
            LogManager.LogMessageToFile("pos player before :\t" + transform.position.ToString());
#endif
            transform.position = new Vector3(transform.position.x - (0.6f - hitX.distance), transform.position.y, transform.position.z);
#if UNITY_EDITOR
            LogManager.LogMessageToFile("pos player after  :\t" + transform.position.ToString() + "\n");
#endif
            return;
        }

        RaycastHit2D hitY = Physics2D.Raycast(m_raycastY.position, Vector3.down, 0.7f, m_whatIsGround);
        if (hitY && hitY.distance < 0.6 && hitY.distance != 0)
        {
#if UNITY_EDITOR
            LogManager.LogMessageToFile("hitY.distance     :\t" + hitY.distance);
            LogManager.LogMessageToFile("hitY.point        :\t" + hitY.point);
            LogManager.LogMessageToFile("pos player before :\t" + transform.position.ToString());
#endif
            transform.position = new Vector3(transform.position.x, transform.position.y + (0.6f - hitY.distance), transform.position.z);
#if UNITY_EDITOR
            LogManager.LogMessageToFile("pos player after  :\t" + transform.position.ToString() + "\n");
#endif
            return;
        }

        RaycastHit2D hitY2 = Physics2D.Raycast(m_raycastY2.position, Vector3.up, 0.7f, m_whatIsGround);
        if (hitY2 && hitY2.distance < 0.6 && hitY2.distance != 0)
        {
#if UNITY_EDITOR
            LogManager.LogMessageToFile("hitY2.distance    :\t" + hitY2.distance);
            LogManager.LogMessageToFile("hitY2.point       :\t" + hitY2.point);
            LogManager.LogMessageToFile("pos player before :\t" + transform.position.ToString());
#endif
            transform.position = new Vector3(transform.position.x, transform.position.y - (0.6f - hitY2.distance), transform.position.z);
#if UNITY_EDITOR
            LogManager.LogMessageToFile("pos player after  :\t" + transform.position.ToString() + "\n");
#endif
        }
    }

	void Update()
    {
        if (!m_isPaused)
        {
            ManageInputs();

            // Manage double jump conditions
            if (m_canDoubleJump == false && CheckIfNear(m_groundCheck.position, m_whatIsGround, 0.1f))
                m_canDoubleJump = true;

            // Manage the movement and rotation of the player
            if (!CheckIfNear(m_wallCheck.position, m_whatIsWall, 0.1f) && transform.position.x < m_playerLimit.position.x)
            {
                transform.position = Vector3.Lerp(transform.position, m_playerLimit.position, Time.deltaTime * m_smoothFactor);
                m_bouleMagikMesh.Rotate(new Vector3(0, 0, -(m_multRotation * m_speedScroll * Time.deltaTime)));
            }

            // Manage the bonus
            foreach (BonusApplied item in m_bonusList.ToArray())
            {
                if ((item.m_startBonusTime + item.m_bonusTime) < MyTimer.Instance.TotalTime)
                {
                    RemoveBonus(item.m_bonusType);
                    m_bonusList.Remove(item);
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "loseTrigger")
        {
            Debug.Log("Lose !");
            GameObject.Find("GameManager").GetComponent<GameManager>().Lose();
        }
        if (other.tag == "bonus")
        {
            Destroy(other.gameObject);
            ApplyBonus(other.gameObject.GetComponent<Bonus>().BonusType);
            BonusApplied newBonus = new BonusApplied(other.gameObject.GetComponent<Bonus>().BonusTime, other.gameObject.GetComponent<Bonus>().BonusType);
            m_bonusList.Add(newBonus);
        }
    }

    bool CheckIfNear(Vector3 position, LayerMask whatIs, float range)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, range, whatIs);
        for (int i = 0; i < colliders.Length; i++)
            if (colliders[i].gameObject != gameObject && transform.position.x < m_playerLimit.position.x)
                return true;
        return false;
    }

    // PRIVATE METHODES

    void ManageInputs()
    {
#if UNITY_ANDROID

        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began && !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                DoJump();
            }
        }
#endif
#if UNITY_EDITOR
        if (Input.GetButtonDown("Jump"))
        {
            DoJump();
        }
#endif
    }

    void DoJump()
    {
        if (!CheckIfNear(m_roofCheck.position, m_whatIsRoof, 0.1f))
        {
            if (m_rigidbody.velocity.y == 0)
            {
                //Debug.Log("Jump");
                if (m_optionManager.Sound == 1)
                    m_managerAudio.PlayOneShot(m_jumpSound);
                m_rigidbody.AddForce(new Vector2(0, m_velocityY));
            }
            else
            if (m_rigidbody.velocity.y > -5 && m_canDoubleJump)
            {
                //Debug.Log("Double jump");
                m_canDoubleJump = false;
                if (m_optionManager.Sound == 1)
                    m_managerAudio.PlayOneShot(m_jumpSound);
                m_rigidbody.velocity = Vector2.zero;
                m_rigidbody.AddForce(new Vector2(0, m_velocityY + 50));
            }
        }
    }

    void ApplyBonus(GameManager.e_bonusType newBonus)
    {
        Debug.Log("applyBonus " + newBonus.ToString());
        switch (newBonus)
        {
            case GameManager.e_bonusType.SPEEDUP:
                m_currentSpeedUpParticleSystem = Instantiate(m_speedUpBonusPS, transform.position, m_speedUpBonusPS.transform.rotation) as ParticleSystem;
                m_currentSpeedUpParticleSystem.transform.parent = gameObject.transform;
                m_multRotation = 180;
                m_smoothFactor = 0.5f;
                if (m_optionManager.Sound == 1)
                    m_managerAudio.PlayOneShot(m_bonusSpeedUpSound);
                break;
            case GameManager.e_bonusType.SCROLLDOWN:
                m_currentScrollDownParticleSystem = Instantiate(m_scrollDownBonusPS, new Vector3(transform.position.x, transform.position.y, -1f), m_scrollDownBonusPS.transform.rotation) as ParticleSystem;
                m_currentScrollDownParticleSystem.transform.parent = gameObject.transform;
                if (m_speedScroll - 0.2f > m_optionManager.SpeedStart)
                    GameObject.Find("GameManager").GetComponent<GameManager>().UpdateSpeedScroll(-0.2f);
                if (m_optionManager.Sound == 1)
                    m_managerAudio.PlayOneShot(m_bonusScrollDownSound);
                break;
            case GameManager.e_bonusType.SCROLLUP:
                m_currentScrollUpParticleSystem = Instantiate(m_scrollUpBonusPS, new Vector3(transform.position.x, transform.position.y, -1f), m_scrollUpBonusPS.transform.rotation) as ParticleSystem;
                m_currentScrollUpParticleSystem.transform.parent = gameObject.transform;
                GameObject.Find("GameManager").GetComponent<GameManager>().UpdateSpeedScroll(0.2f);
                if (m_optionManager.Sound == 1)
                    m_managerAudio.PlayOneShot(m_bonusScrollUpSound);
                break;
            case GameManager.e_bonusType.POINT:
                m_currentPointParticleSystem = Instantiate(m_pointBonusPS, new Vector3(transform.position.x, transform.position.y, -1f), m_pointBonusPS.transform.rotation) as ParticleSystem;
                m_currentPointParticleSystem.transform.parent = gameObject.transform;
                if (m_optionManager.Sound == 1)
                    m_managerAudio.PlayOneShot(m_bonusPointSound);
                GameObject.Find("GameManager").GetComponent<GameManager>().UpdatePoint(3);
                break;
            case GameManager.e_bonusType.SUPERPOWER:
                m_isSuperPowerUp = true;
                if (m_optionManager.Sound == 1)
                    m_managerAudio.PlayOneShot(m_bonusSuperPowerSound);
                GameObject.Find("GameManager").GetComponent<GameManager>().ActivateSuperPowerButton();
                break;
        }
    }

    void RemoveBonus(GameManager.e_bonusType newBonus)
    {
        Debug.Log("removeBonus " + newBonus.ToString());
        switch (newBonus)
        {
            case GameManager.e_bonusType.SPEEDUP:
                m_multRotation = m_multRotationDefault;
                m_smoothFactor = m_smoothFactorDefault;
                break;
            case GameManager.e_bonusType.SCROLLDOWN:
                break;
            case GameManager.e_bonusType.SCROLLUP:
                break;
            case GameManager.e_bonusType.POINT:
                break;
            case GameManager.e_bonusType.SUPERPOWER:
                break;
        }
    }


    // PUBLIC METHODES

    public void SuperPowerUsed()
    {
        if (m_optionManager.Sound == 1)
            m_managerAudio.PlayOneShot(m_bonusSuperPowerActivatedSound);
        m_isSuperPowerUp = false;
        m_powerBonusPS.Play();
    }

    public void ResetPlayer()
    {
        Debug.Log("Reset player");
        m_smoothFactor = m_smoothFactorDefault;
        m_velocityY = m_startVelocityY;
        m_canDoubleJump = true;
        m_multRotation = m_multRotationDefault;
        transform.position = m_positionDefault;
        transform.eulerAngles = Vector3.zero;
        m_bonusList.Clear();
        if (m_currentSpeedUpParticleSystem != null)
            Destroy(m_currentSpeedUpParticleSystem.gameObject);
        if (m_currentScrollDownParticleSystem != null)
            Destroy(m_currentScrollDownParticleSystem.gameObject);
        if (m_currentPointParticleSystem != null)
            Destroy(m_currentPointParticleSystem.gameObject);
        m_isSuperPowerUp = false;
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
                if (m_currentSpeedUpParticleSystem != null)
                    m_currentSpeedUpParticleSystem.Pause();
                if (m_currentScrollDownParticleSystem != null)
                    m_currentScrollDownParticleSystem.Pause();
                if (m_currentPointParticleSystem != null)
                    m_currentPointParticleSystem.Pause();
            }
            else
            {
                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                if (m_currentSpeedUpParticleSystem != null)
                    m_currentSpeedUpParticleSystem.Play();
                if (m_currentScrollDownParticleSystem != null)
                    m_currentScrollDownParticleSystem.Play();
                if (m_currentPointParticleSystem != null)
                    m_currentPointParticleSystem.Play();
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

    public bool IsSuperPowerUp
    {
        get
        {
            return m_isSuperPowerUp;
        }
    }
}
