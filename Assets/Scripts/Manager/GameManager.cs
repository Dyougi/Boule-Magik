using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    GameObject[] m_platformsGO; // tab for all platforms

    [SerializeField]
    float m_spaceBetweenPlatform; // space between platform

    [SerializeField]
    float m_startSpeedScroll; // the default speed of the level translate

    [SerializeField]
    float m_updateSpeedScroll; // the speed added at each tick

    [SerializeField]
    GameObject m_firstBackground; // first background

    [SerializeField]
    GameObject m_secondBackground; // second background

    [SerializeField]
    GameObject m_firstFloor; // first floor

    [SerializeField]
    GameObject m_secondFloor; // second floor

    [SerializeField]
    GameObject m_player; // GO of the player

    [SerializeField]
    Transform m_spawnPlatform; // the position of where platform are instanciated

    [SerializeField]
    GameObject m_deathParticleSystem; // the GO with the particle system played when player is dead

    [SerializeField]
    Text m_uiPoint; // the Text from the GUI to show points

    [SerializeField]
    Text m_uiSpeed; // the Text from the GUI to show points

    [SerializeField]
    Text m_uiRestart; // the Text from the GUI to show counter when restart after background

    [SerializeField]
    AudioSource m_musicManager; // audio source for music

    [SerializeField]
    AudioSource m_soundManager; // audio source for sound

    [SerializeField]
    AudioClip m_musicDefault; // the Text from the GUI to show points

    [SerializeField]
    AudioClip m_loseSound; // the Text from the GUI to show points

    [SerializeField]
    Sprite m_playSprite; // the Text from the GUI to show points

    [SerializeField]
    Sprite m_pauseSprite; // the Text from the GUI to show points

    [SerializeField]
    GameObject m_retryButton; // the retry button when the player lose

    [SerializeField]
    GameObject m_pauseButton; // the pause button

    [SerializeField]
    GameObject m_menuButton; // the menu button to get back to the menu scene when the player lose

    [SerializeField]
    GameObject m_superPowerButton; // the button for the super power

    public enum e_bonusType { SPEEDUP, SCROLLDOWN, SCROLLUP, POINT, SUPERPOWER }

    OptionManager m_optionManager; // Reference to the singleton Option Manager

    List<GameObject> m_platformTab; // list of platforms instanciated
    GameObject currentPlatform; // the current platform handled
    int m_points; // current points of the player
    float m_speedScroll; // the current speed of the level translate
    float m_timeBetweenTwoInstancePlatform; // variable to save the time passed to know when to make another instance of a new platform
    float m_sizePlatformSave; // variable to save the size of the last platform instanciated
    int m_countTab; // count set randomly to instance a random platform
    bool m_isPaused; // is game paused ?
    bool m_isLose; // is the game lost ?
    float m_timeWhenLose; // the moment the player lost

    // UNITY METHODES

    void Start()
    {
        m_platformTab = new List<GameObject>();
        m_optionManager = OptionManager.Instance;
        m_musicManager.clip = m_musicDefault;
        if (m_optionManager.Music == 1)
            m_musicManager.Play();
        m_startSpeedScroll = m_optionManager.SpeedStart;
        //m_updateSpeedScroll = m_optionManager.Speedupdate;
        InitGame();
    }

    void OnApplicationFocus(bool pauseStatus)
    {
        if (pauseStatus)
        {
            m_uiRestart.gameObject.SetActive(true);
            StartCoroutine(RestartAfterbackground());
        }
        else
        {
            if (m_player.GetComponent<PlayerController>().IsSuperPowerUp)
                DesactivateSuperPowerButton();
            m_pauseButton.SetActive(false);
            UpdatePause(true);
        }
    }

    void Update()
    {
        if (!Pause)
        {
            if (m_sizePlatformSave <= (m_timeBetweenTwoInstancePlatform - m_spaceBetweenPlatform))
            {
                m_timeBetweenTwoInstancePlatform = 0;
                m_countTab = Random.Range(0, m_platformsGO.Length);
                currentPlatform = Instantiate(m_platformsGO[m_countTab], m_spawnPlatform.position, Quaternion.identity) as GameObject;
                currentPlatform.GetComponent<Platform>().InitPlatform(m_speedScroll);
                m_sizePlatformSave = currentPlatform.GetComponent<Platform>().WidthSize;
                m_platformTab.Add(currentPlatform);
                UpdateSpeedScroll(m_updateSpeedScroll);
                Debug.Log("Instance platform : " + currentPlatform.name + ", speed scroll : " + m_speedScroll);
            }
            m_timeBetweenTwoInstancePlatform += Time.deltaTime * m_speedScroll;
        }
        if (m_isLose)
        {
            if (m_timeWhenLose + 1 < Time.time)
            {
                m_retryButton.SetActive(true);
                m_menuButton.SetActive(true);
            }
        }
    }

    // PRIVATE METHODES

    IEnumerator RestartAfterbackground()
    {
        for (float count = 4f; count > 1; count -= Time.deltaTime)
        {
            if (count.ToString("0") == "4")
                m_uiRestart.text = "3";
            else
                m_uiRestart.text = count.ToString("0");
            yield return null;
        }
        if (m_player.GetComponent<PlayerController>().IsSuperPowerUp)
            ActivateSuperPowerButton();
        m_uiRestart.gameObject.SetActive(false);
        m_pauseButton.SetActive(true);
        UpdatePause(false);
    }

    void InitGame()
    {
        m_countTab = 0; // tab platform count;
        m_isPaused = false; // game paused ?
        m_isLose = false; // lose state
        m_points = 0; // point number
        currentPlatform = null; // instance variable for current platform
        UpdateSpeedScroll(m_startSpeedScroll); // set default speed scroll
        UpdatePoint(m_points); // set default point
        m_firstFloor.GetComponent<ScrollingEntity>().InitEntity(m_startSpeedScroll); // set default speed for level transtaltion
        m_secondFloor.GetComponent<ScrollingEntity>().InitEntity(m_startSpeedScroll); // set default speed for level transtaltion
        m_firstBackground.GetComponent<ScrollingEntity>().InitEntity(m_startSpeedScroll); // set default speed for level transtaltion
        m_secondBackground.GetComponent<ScrollingEntity>().InitEntity(m_startSpeedScroll); // set default speed for level transtaltion
        m_speedScroll = m_startSpeedScroll; // set default speed for level transtaltion
        m_timeBetweenTwoInstancePlatform = 0; // set the default value for the time passed between two instance of platform
        m_sizePlatformSave = m_timeBetweenTwoInstancePlatform - m_spaceBetweenPlatform; // set default value for variable of the save of size of current platform
        /*Image tempImage = m_superPowerButton.GetComponent<Image>();
        Color tempColor = new Color(tempImage.color.r, tempImage.color.g, tempImage.color.b, 50);
        m_superPowerButton.GetComponent<Image>().color = tempColor;*/
        DesactivateSuperPowerButton();
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

    public void UpdatePoint(int addedPoint)
    {
        m_points += addedPoint;
        m_uiPoint.text = "Point: " + m_points.ToString();
    }

    public void UpdateSpeedScroll(float newSpeed)
    {
        m_speedScroll += newSpeed;
        m_uiSpeed.text = "Speed: " + m_speedScroll.ToString("0.0");
        foreach (GameObject obj in m_platformTab)
            if (obj != null)
                obj.GetComponent<Platform>().Speed = m_speedScroll;
        m_player.GetComponent<PlayerController>().Speed = m_speedScroll;
        m_firstBackground.GetComponent<ScrollingEntity>().Speed = m_speedScroll;
        m_secondBackground.GetComponent<ScrollingEntity>().Speed = m_speedScroll;
        m_firstFloor.GetComponent<ScrollingEntity>().Speed = m_speedScroll;
        m_secondFloor.GetComponent<ScrollingEntity>().Speed = m_speedScroll;
    }

    public void UpdatePause(bool pause)
    {
        foreach (GameObject obj in m_platformTab)
            if (obj != null)
            {
                obj.GetComponent<Platform>().Pause = pause;
                Animation[] anims = obj.GetComponentsInChildren<Animation>();
                foreach (Animation item in anims)
                {
                    if (pause)
                        item["Bonus"].speed = 0;
                    else
                        item["Bonus"].speed = 1;
                }
            }
        if (m_optionManager.Music == 1)
        {
            if (m_musicManager.isPlaying)
                m_musicManager.Pause();
            else
                m_musicManager.UnPause();
        }
        m_player.GetComponent<PlayerController>().Pause = pause;
        m_firstBackground.GetComponent<ScrollingEntity>().Pause = pause;
        m_secondBackground.GetComponent<ScrollingEntity>().Pause = pause;
        m_firstFloor.GetComponent<ScrollingEntity>().Pause = pause;
        m_secondFloor.GetComponent<ScrollingEntity>().Pause = pause;
        MyTimer.Instance.Pause = pause;
        Pause = pause;
    }

    public void PauseButtonPressed(Image buttonImage)
    {
        buttonImage.sprite = m_isPaused == false ? m_playSprite : m_pauseSprite;
        UpdatePause(!m_isPaused);
    }

    public void ActivateSuperPowerButton()
    {
        m_superPowerButton.SetActive(true);
        /*Image tempImage = m_superPowerButton.GetComponent<Image>();
        Color tempColor = new Color(tempImage.color.r, tempImage.color.g, tempImage.color.b, 255);
        m_superPowerButton.GetComponent<Image>().color = tempColor;*/
    }

    public void DesactivateSuperPowerButton()
    {
        m_superPowerButton.SetActive(false);
        /*Image tempImage = m_superPowerButton.GetComponent<Image>();
        Color tempColor = new Color(tempImage.color.r, tempImage.color.g, tempImage.color.b, 50);
        m_superPowerButton.GetComponent<Image>().color = tempColor;*/
    }

    public void SuperPowerButtonPressed()
    {
        foreach (GameObject obj in m_platformTab)
            if (obj != null)
                StartCoroutine(obj.GetComponent<Platform>().Fade());
        DesactivateSuperPowerButton();
        m_player.GetComponent<PlayerController>().SuperPowerUsed();
    }

    public void Lose()
    {
        DesactivateSuperPowerButton();
        if (PlayerPrefs.GetInt("pointScore") < m_points)
        {
            m_optionManager.Score = m_points;
            PlayerPrefs.SetInt("pointScore", m_points);
            PlayerPrefs.Save();
        }
        if (PlayerPrefs.GetFloat("speedScore") < m_speedScroll && m_startSpeedScroll == 4)
        {
            m_optionManager.SpeedScore = m_speedScroll;
            PlayerPrefs.SetFloat("speedScore", m_speedScroll);
            PlayerPrefs.Save();
        }
        m_pauseButton.SetActive(false);
        UpdatePause(true);
        m_isLose = true;
        if (m_optionManager.Sound == 1)
            m_soundManager.PlayOneShot(m_loseSound);
        if (m_optionManager.Music == 1)
            m_musicManager.Stop();
        foreach (ParticleSystem particl in m_player.GetComponentsInChildren<ParticleSystem>())
        {
            if (particl.isPlaying)
                particl.Stop();
        }
        Instantiate(m_deathParticleSystem, m_player.transform.position, m_deathParticleSystem.transform.rotation);
        m_timeWhenLose = Time.time;
    }

    public void RestartGame()
    {
        foreach (GameObject obj in m_platformTab)
            Destroy(obj);
        m_player.GetComponent<PlayerController>().ResetPlayer();
        InitGame();
        UpdatePause(false);
        m_retryButton.SetActive(false);
        m_menuButton.SetActive(false);
        m_pauseButton.SetActive(true);
        if (m_optionManager.Music == 1)
        {
            m_musicManager.Stop();
            m_musicManager.Play();
        }
    }

    public void GoMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
