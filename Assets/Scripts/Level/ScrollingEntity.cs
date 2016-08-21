using UnityEngine;
using System.Collections;

public class ScrollingEntity : MonoBehaviour
{
    [SerializeField] Transform m_endEntity;
    [SerializeField] Transform m_unspawnEntity;
    [SerializeField] Transform m_spawnEntity;

    float m_speedTranslate;
    bool m_isPaused;

    // UNITY METHODES

    void Start()
    {
        m_isPaused = false;
    }

    void FixedUpdate()
    {
        if (!m_isPaused)
        {

            if (m_endEntity.position.x <= m_unspawnEntity.position.x)
            {
                transform.position = m_spawnEntity.position - new Vector3(Mathf.Abs(m_endEntity.position.x) - 20, 0, 0);
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
