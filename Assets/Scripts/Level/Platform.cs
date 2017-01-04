using UnityEngine;

public class Platform : MonoBehaviour
{

    [SerializeField] Transform m_endEntity; // Landamrk of where the platform have to be destroyed on himself
    [SerializeField] Transform m_unspawnEntity; // Landmark of where the platform have to be destroyed in the world
    [SerializeField] int widthSize; // Width size of the platform
    [SerializeField] int m_pointGived; // How much point gived when passed

    float m_speedTranslate; // Speed of the translate for this platform (for the scrolling purpose)
    bool m_isPaused; // Is game paused ? Then stop translate

    // UNITY METHODES

    void Start()
    {
        m_isPaused = false;
    }

    void Update()
    {
        if (!m_isPaused)
        {
            if (m_endEntity.position.x <= m_unspawnEntity.position.x)
            {
                GameObject.Find("GameManager").GetComponent<GameManager>().UpdatePoint(PointGived);
                Destroy(gameObject);
            }
            transform.Translate(new Vector3(-m_speedTranslate * Time.deltaTime, 0, 0));
        }
    }

    // PUBLIC METHODES

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

    public int WidthSize
    {
        get
        {
            return widthSize;
        }
    }

    public int PointGived
    {
        get
        {
            return m_pointGived;
        }
    }

    public float Speed
    {
        get
        {
            return m_speedTranslate;
        }

        set
        {
            m_speedTranslate = value;
        }
    }

    public void InitPlatform(float newSpeed)
    {
        m_speedTranslate = newSpeed;
    }
}
