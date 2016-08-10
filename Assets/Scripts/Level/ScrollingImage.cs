using UnityEngine;
using System.Collections;

public class ScrollingImage : MonoBehaviour {

    [SerializeField] float speedScrolling;
    [SerializeField] bool doScroll;

    bool m_isPaused;
    float pos;

    Renderer GOrenderer;

    void Start()
    {
        GOrenderer = GetComponent<Renderer>();
        m_isPaused = false;
        pos = 0;
    }

	void FixedUpdate ()
    {
        if (doScroll)
            if (!m_isPaused)
            {
                pos = pos >= 1 ? 0 : pos + Time.deltaTime * speedScrolling;
                GOrenderer.material.mainTextureOffset = new Vector2(pos, 0);
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
}
