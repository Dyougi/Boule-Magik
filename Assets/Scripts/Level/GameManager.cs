using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class GameManager : MonoBehaviour {

    [SerializeField] GameObject[] m_platformsGO;
    [SerializeField] float m_timeBetweenPlatform;
    [SerializeField] float m_startSpeedScroll;
    [SerializeField] float m_updateSpeedScroll;
    [SerializeField] GameObject m_firstBackground;
    [SerializeField] GameObject m_secondBackground;
    [SerializeField] GameObject m_firstFloor;
    [SerializeField] GameObject m_secondFloor;
    [SerializeField] GameObject m_player;
    [SerializeField] Transform m_spawnPlatform;
    [SerializeField] GameObject m_deathParticleSystem;
    [SerializeField] Text m_uiPoint;

    List<GameObject> m_platformTab;
    GameObject currentPlatform;
    Time m_saveAccelerationTranslate;
    int m_points;
    float m_speedScroll;
    int m_countTab;
    bool m_isPaused;
    bool m_isLose;

    bool isPlatformInstance;

    // UNITY METHODES

    void Start () {
        m_platformTab = new List<GameObject>();
        initGame();
    }
	
	void Update () {
        manageInput();
        if (!Pause)
        {
            if (currentPlatform == null)
            {
                Debug.Log("ping currentPlatform null");
                isPlatformInstance = false;
            }
            if (!isPlatformInstance)
            {
                m_countTab = Random.Range(0, m_platformsGO.Length);
                currentPlatform = Instantiate(m_platformsGO[m_countTab], m_spawnPlatform.position, Quaternion.identity) as GameObject;
                currentPlatform.GetComponent<Platform>().initPlatform(m_speedScroll);
                m_platformTab.Add(currentPlatform);
                updatePoint(currentPlatform.GetComponent<Platform>().PointGived);
                isPlatformInstance = true;
                updateSpeedScroll(m_updateSpeedScroll);
            }
            
        }
    }

    // PRIVATE METHODES

    void initGame()
    {
        m_countTab = 0;
        m_isPaused = false;
        m_isLose = false;
        m_points = 0;
        currentPlatform = null;
        updateSpeedScroll(m_startSpeedScroll);
        updatePoint(m_points);
        m_firstFloor.GetComponent<ScrollingEntity>().initEntity(m_startSpeedScroll);
        m_secondFloor.GetComponent<ScrollingEntity>().initEntity(m_startSpeedScroll);
        m_firstBackground.GetComponent<ScrollingEntity>().initEntity(m_startSpeedScroll);
        m_secondBackground.GetComponent<ScrollingEntity>().initEntity(m_startSpeedScroll);
        m_speedScroll = m_startSpeedScroll;
    }

    void manageInput()
    {
        if (Input.GetButtonDown("Submit"))
        {
            Debug.Log("Submit");
            if (m_isLose)
            {
                restartGame();
                Debug.Log("restartGame");
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

    public void updatePoint(int addedPoint)
    {
        m_points += addedPoint;
        m_uiPoint.text = m_points.ToString();
    }

    public void updateSpeedScroll(float newSpeed)
    {
        m_speedScroll += newSpeed;
        foreach (GameObject obj in m_platformTab)
            if (obj != null)
                obj.GetComponent<Platform>().Speed = m_speedScroll;
        m_firstBackground.GetComponent<ScrollingEntity>().Speed = m_speedScroll;
        m_secondBackground.GetComponent<ScrollingEntity>().Speed = m_speedScroll;
        m_firstFloor.GetComponent<ScrollingEntity>().Speed = m_speedScroll;
        m_secondFloor.GetComponent<ScrollingEntity>().Speed = m_speedScroll;
    }

    public void pause(bool pause)
    {
        foreach (GameObject obj in m_platformTab)
            if (obj != null)
                obj.GetComponent<Platform>().Pause = pause;
        m_player.GetComponent<PlayerController>().Pause = pause;
        m_firstBackground.GetComponent<ScrollingEntity>().Pause = pause;
        m_secondBackground.GetComponent<ScrollingEntity>().Pause = pause;
        m_firstFloor.GetComponent<ScrollingEntity>().Pause = pause;
        m_secondFloor.GetComponent<ScrollingEntity>().Pause = pause;
        Pause = pause;
    }

    public void lose()
    {
        pause(true);
        m_isLose = true;
        Instantiate(m_deathParticleSystem, m_player.transform.position, m_deathParticleSystem.transform.rotation);
    }

    public void restartGame()
    {
        foreach (GameObject obj in m_platformTab)
            Destroy(obj);
        m_player.GetComponent<PlayerController>().resetPlayer();
        initGame();
        pause(false);
    }
}
