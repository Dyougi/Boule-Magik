using UnityEngine;
using System.Collections;

public class OptionManager : MonoBehaviour {

    int m_isMusic;
    int m_isSound;
    float m_speedStart;
    float m_speedUpdate;
    bool isLoaded = false;

    private static OptionManager instance;

    public static OptionManager Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        if (instance == null)
            instance = this;
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        m_isMusic = PlayerPrefs.GetInt("music");
        m_isSound = PlayerPrefs.GetInt("sound");
        m_speedStart = PlayerPrefs.GetFloat("speedStart");
        m_speedUpdate = PlayerPrefs.GetFloat("speedUpdate");
        isLoaded = true;
    }

    public void saveOption()
    {
        PlayerPrefs.Save();
    }

    public int Music
    {
        get
        {
            return m_isMusic;
        }

        set
        {
            m_isMusic = value;
            PlayerPrefs.SetInt("music", m_isMusic);
            Debug.Log("OptionManager music set to " + m_isMusic);
        }
    }

    public int Sound
    {
        get
        {
            return m_isSound;
        }

        set
        {
            m_isSound = value;
            PlayerPrefs.SetInt("sound", m_isSound);
            Debug.Log("OptionManager sound set to " + m_isSound);
        }
    }

    public float SpeedStart
    {
        get
        {
            return m_speedStart;
        }

        set
        {
            m_speedStart = value;
            PlayerPrefs.SetFloat("speedStart", m_speedStart);
            Debug.Log("OptionManager speedStart set to " + m_speedStart);
        }
    }

    public float Speedupdate
    {
        get
        {
            return m_speedUpdate;
        }

        set
        {
            m_speedUpdate = value;
            PlayerPrefs.SetFloat("speedUpdate", m_speedUpdate);
            Debug.Log("OptionManager speedUpdate set to " + m_speedUpdate);
        }
    }

    public bool IsLoaded
    {
        get
        {
            return isLoaded;
        }
    }
}
