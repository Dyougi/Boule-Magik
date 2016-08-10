using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour {

    [SerializeField] int widthSize;
    [SerializeField] int m_pointGived;
    [SerializeField] Transform m_endPlatform;
    [SerializeField] Transform m_unspawnPlatform;

    float m_speedTranslate;
    bool m_isPaused;

    // UNITY METHODES
	
	void Update () {
        if (!m_isPaused)
        {
            transform.Translate(new Vector3(-m_speedTranslate * Time.deltaTime, 0, 0));
            if (m_endPlatform.position.x < m_unspawnPlatform.position.x)
            {
                Debug.Log("destroy platform");
                Destroy(gameObject);
            }
        }
	}

    // PUBLIC METHODES

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

    public void InitPlatform(float newSpeedTranslate)
    {
        m_speedTranslate = newSpeedTranslate;
        m_isPaused = false;
    }

}
