using UnityEngine;

public class ScrollingEntity : MonoBehaviour
{
    [SerializeField] Transform m_endEntity; // Landamrk of where the platform have to be destroyed on himself
    [SerializeField] Transform m_unspawnEntity; // Landmark of where the platform have to be destroyed in the world
    [SerializeField] Transform m_spawnEntity; // Where the platform have to be instanciated

    float m_speedTranslate; // Speed of the platform in -x (for the scrolling)
    bool m_isPaused; // Is game paused ?

    // UNITY METHODES

    void Start()
    {
        m_isPaused = false;
    }

    bool test = false;

    void FixedUpdate()
    {
        if (!m_isPaused)
        {
            test = false;
                if (m_endEntity.position.x <= m_unspawnEntity.position.x)
            {
                test = true;
                if (gameObject.name == "FirstBackgroundPivot")
                {
                    Debug.Log("##################################################");
                    Vector3 tmp = m_spawnEntity.position - new Vector3(Mathf.Abs(m_endEntity.position.x) - 20, 0, 0);
                    Debug.Log("diff quand supêrieur a 20 : " + new Vector3(Mathf.Abs(m_endEntity.position.x) - 20, 0, 0));
                    Debug.Log("le vecteur : " + tmp);
                }
                m_endEntity.position = m_spawnEntity.position - new Vector3(Mathf.Abs(m_endEntity.position.x) - 20, 0, 0);
                if (gameObject.name == "FirstBackgroundPivot")
                    Debug.Log("position : " + m_endEntity.position);
            }
            if (gameObject.name == "FirstBackgroundPivot" && test)
                Debug.Log("le translate : " + -m_speedTranslate * Time.deltaTime);
            transform.Translate(new Vector3(-m_speedTranslate * Time.deltaTime, 0, 0));
            if (gameObject.name == "FirstBackgroundPivot" && test)
            {
                Debug.Log("transform = " + transform.position);
                Debug.Log("##################################################");
            }
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
    public void initEntity(float newSpeed)
    {
        m_speedTranslate = newSpeed;
    }
}
