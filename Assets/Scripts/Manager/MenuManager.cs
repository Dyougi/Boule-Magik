using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

    [SerializeField]
    Text m_scoreText;

    [SerializeField]
    Toggle m_music;

    [SerializeField]
    Toggle m_sound;

    [SerializeField]
    Slider m_speedStart;

    [SerializeField]
    Text m_speedStartValue;

    [SerializeField]
    Slider m_speedUpdate;

    [SerializeField]
    Text m_speedUpdateValue;

    [SerializeField]
    GameObject m_menuStart;

    [SerializeField]
    GameObject m_menuOption;

    [SerializeField]
    Button m_defaultValueButton;

    [SerializeField]
    OptionManager m_optionManager;

    public enum e_sliderType { SPEEDSTART, SPEEDUPDATE };
    public enum e_toggleType { MUSIC, SOUND };

    AudioSource m_audioSource;
    static float m_speedStartDefaultValue = 4;
    static float m_speedUpdateDefaultValue = 0.05f;
    bool m_isOption;

    void Start()
    {
        m_isOption = false;
        m_audioSource = GetComponent<AudioSource>();
        if (m_optionManager.Music == 1)
            m_audioSource.Play();
    }

    void Update()
    {
        if (m_scoreText.text == "Best score : ")
            m_scoreText.text += PlayerPrefs.GetInt("score").ToString();
    }

    public void launchGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void switchMenu()
    {
        if (m_isOption)
        {
            m_menuStart.SetActive(true);
            m_menuOption.SetActive(false);
            m_isOption = false;
        }
        else
        {
            m_menuStart.SetActive(false);
            m_menuOption.SetActive(true);
            m_isOption = true;
        }
    }

    public void resetValues()
    {
        m_music.isOn = true;
        m_speedStart.value = m_speedStartDefaultValue;
        m_speedUpdate.value = m_speedUpdateDefaultValue;
    }
}
