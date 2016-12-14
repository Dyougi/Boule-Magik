using UnityEngine;
using System.Collections;

public class OptionManager : MonoBehaviour {

    public delegate void OptionAction(bool isOn);
    public static event OptionAction OnMusic;
    public static event OptionAction OnSound;

    int m_isMusic;
    int m_isSound;
    float m_speedStart;
    float m_speedUpdate;

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    void Start()
    {
        m_isMusic = PlayerPrefs.GetInt("music");
        m_isSound = PlayerPrefs.GetInt("sound");
        m_speedStart = PlayerPrefs.GetFloat("speedStart");
        m_speedUpdate = PlayerPrefs.GetFloat("speedUpdate");
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
            if (OnMusic != null)
                OnMusic(m_isMusic == 1 ? true : false);
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
            if (OnSound != null)
                OnSound(m_isSound == 1 ? true : false);
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
        }
    }
}
