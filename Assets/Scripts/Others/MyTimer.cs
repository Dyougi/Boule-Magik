using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTimer : MonoBehaviour {

    private static MyTimer instance;

    float m_totalTime;

    bool m_isPaused;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }

    public static MyTimer Instance
    {
        get { return instance; }
    }

    // Use this for initialization
    void Start () {
        m_totalTime = 0;
        m_isPaused = false;
    }
	
	// Update is called once per frame
	void Update () {
		if (!m_isPaused)
        {
            m_totalTime += Time.deltaTime;
        }
	}
    
    public void resetTimer()
    {
        m_totalTime = 0;
    }

    public float TotalTime
    {
        get
        {
            return m_totalTime;
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
