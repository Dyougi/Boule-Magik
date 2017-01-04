using System.Collections;
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

    OptionManager m_optionManager;
    int m_hightScore;

    public enum e_sliderType { SPEEDSTART, SPEEDUPDATE };
    public enum e_toggleType { MUSIC, SOUND };

    AudioSource m_audioSource;
    static float m_speedStartDefaultValue = 4;
    static float m_speedUpdateDefaultValue = 0.05f;
    bool m_isOption;
    float m_scorePrinted;

    void Start()
    {
        m_isOption = false;
        m_audioSource = GetComponent<AudioSource>();
        m_optionManager = OptionManager.Instance;
        m_hightScore = PlayerPrefs.GetInt("score");
        m_scorePrinted = -1;
    }

    void Update()
    {
        if (m_optionManager != null && m_optionManager.Score != m_scorePrinted)
            m_scoreText.text = "Best score : " + m_hightScore.ToString();
        if (m_optionManager != null && m_optionManager.Music == 1 && !m_audioSource.isPlaying)
            m_audioSource.Play();
        if (m_optionManager != null && m_optionManager.Music == 0 && m_audioSource.isPlaying)
            m_audioSource.Stop();
    }

    public void LaunchGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void SwitchMenu()
    {
        if (m_isOption) // Main menu
        {
            m_optionManager.saveOption();
            m_menuStart.SetActive(true);
            m_menuOption.SetActive(false);
            m_isOption = false;
        }
        else // Option menu
        {
            m_menuStart.SetActive(false);
            m_menuOption.SetActive(true);
            m_isOption = true;
        }
    }

    public void ResetValues()
    {
        m_music.isOn = true;
        m_sound.isOn = true;
        m_speedStart.value = m_speedStartDefaultValue;
        m_speedUpdate.value = m_speedUpdateDefaultValue;
    }
}
