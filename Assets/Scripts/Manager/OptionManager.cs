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
        if (PlayerPrefs.HasKey("music"))
            m_isMusic = PlayerPrefs.GetInt("music");
        else
        {
            PlayerPrefs.SetInt("music", 1);
            m_isMusic = 1;
        }
        if (PlayerPrefs.HasKey("sound"))
            m_isSound = PlayerPrefs.GetInt("sound");
        else
        {
            PlayerPrefs.SetInt("sound", 1);
            m_isSound = 1;
        }
        if (PlayerPrefs.HasKey("speedStart"))
            m_speedStart = PlayerPrefs.GetFloat("speedStart");
        else
        {
            PlayerPrefs.SetFloat("speedStart", 4);
            m_speedStart = 4;
        }
        if (PlayerPrefs.HasKey("speedUpdate"))
            m_speedUpdate = PlayerPrefs.GetFloat("speedUpdate");
        else
        {
            PlayerPrefs.SetFloat("speedUpdate", 0.05f);
            m_speedUpdate = 0.05f;
        }
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
