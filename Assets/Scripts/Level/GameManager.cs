using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    [SerializeField] GameObject[] m_platformsGO;
    [SerializeField] float m_timeBetweenPlatform;
    [SerializeField] ScrollingImage m_background;
    [SerializeField] ScrollingImage m_floor;
    [SerializeField] GameObject m_player;
    [SerializeField] Transform m_spawnPlatform;
    [SerializeField] GameObject m_deathParticleSystem;
    [SerializeField] Text m_uiPoint;
    [SerializeField] bool test;

    List<GameObject> m_platformTab;
    GameObject currentPlatform;
    int m_points;
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
        if (test && !Pause)
        {  
            if (!isPlatformInstance)
            {
                m_countTab = Random.Range(0, m_platformsGO.Length);
                currentPlatform = Instantiate(m_platformsGO[m_countTab], m_spawnPlatform.position, Quaternion.identity) as GameObject;
                currentPlatform.GetComponent<Platform>().InitPlatform(4f);
                m_platformTab.Add(currentPlatform);
                updatePoint(currentPlatform.GetComponent<Platform>().PointGived);
                isPlatformInstance = true;
            }
            else
            {
                if (currentPlatform == null)
                {
                    Debug.Log("ping currentPlatform null");
                    isPlatformInstance = false;
                }
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
        isPlatformInstance = false;
        updatePoint(m_points);
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


    public void pause(bool pause)
    {
        foreach (GameObject obj in m_platformTab)
            if (obj != null)
                obj.GetComponent<Platform>().Pause = pause;
        m_player.GetComponent<PlayerController>().Pause = pause;
        m_background.Pause = pause;
        m_floor.Pause = pause;
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
