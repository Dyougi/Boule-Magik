using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour
{

    [SerializeField] Transform m_endEntity;
    [SerializeField] Transform m_unspawnEntity;
    [SerializeField] int widthSize;
    [SerializeField] int m_pointGived;

    float m_speedTranslate;
    bool m_isPaused;

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
                GameObject.Find("GameManager").GetComponent<GameManager>().updatePoint(PointGived);
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

    public void initPlatform(float newSpeed)
    {
        m_speedTranslate = newSpeed;
    }
}
