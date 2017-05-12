using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

    [SerializeField]
    Text m_scoreText;

    [SerializeField]
    Text m_speedText;

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
    GameObject m_menuCredit;

    [SerializeField]
    Button m_defaultValueButton;

    OptionManager m_optionManager;

    public enum e_sliderType { SPEEDSTART, SPEEDUPDATE };
    public enum e_toggleType { MUSIC, SOUND };
    public enum e_menuType { MAINMENU, OPTIONS, CREDIT };

    AudioSource m_audioSource;
    const float m_speedStartDefaultValue = 4;
    const float m_speedUpdateDefaultValue = 0.1f;
    float m_scorePrinted;
    float m_speedPrinted;
    e_menuType m_lastMenu;

    void Start()
    {
        m_audioSource = GetComponent<AudioSource>();
        m_optionManager = OptionManager.Instance;
        m_scorePrinted = -1;
        m_speedPrinted = -1;
        m_lastMenu = e_menuType.MAINMENU;
    }

    void Update()
    {
        if (m_optionManager != null && m_optionManager.Score != m_scorePrinted)
        {
            m_scorePrinted = m_optionManager.Score;
            m_scoreText.text = "Best score : " + m_optionManager.Score.ToString();
        }
        if (m_optionManager != null && m_optionManager.SpeedScore != m_speedPrinted)
        {
            m_speedPrinted = m_optionManager.SpeedScore;
            m_speedText.text = "Best speed : " + m_optionManager.SpeedScore.ToString("0.0"); ;
        }
        if (m_optionManager != null && m_optionManager.Music == 1 && !m_audioSource.isPlaying)
            m_audioSource.Play();
        if (m_optionManager != null && m_optionManager.Music == 0 && m_audioSource.isPlaying)
            m_audioSource.Stop();
    }

    public void LaunchGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void SwitchMenu(int menuType)
    {
        switch ((e_menuType)menuType)
        {
            case e_menuType.MAINMENU:
                m_menuStart.SetActive(true);
                if (m_lastMenu == e_menuType.OPTIONS)
                {
                    m_menuOption.SetActive(false);
                    m_optionManager.saveOption();
                }
                if (m_lastMenu == e_menuType.CREDIT)
                {
                    m_menuCredit.SetActive(false);
                }
                break;
            case e_menuType.OPTIONS:
                m_menuStart.SetActive(false);
                m_menuOption.SetActive(true);
                m_lastMenu = e_menuType.OPTIONS;
                break;
            case e_menuType.CREDIT:
                m_menuStart.SetActive(false);
                m_menuCredit.SetActive(true);
                m_lastMenu = e_menuType.CREDIT;
                break;
        }
    }

    public void ResetValues()
    {
        m_music.isOn = true;
        m_sound.isOn = true;
        m_speedStart.value = m_speedStartDefaultValue;
        //m_speedUpdate.value = m_speedUpdateDefaultValue;
    }
}
