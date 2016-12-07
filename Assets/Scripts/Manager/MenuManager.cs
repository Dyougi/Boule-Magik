using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

    [SerializeField]
    Text m_scoreText;

    [SerializeField]
    Canvas m_menuStart;

    [SerializeField]
    Canvas m_menuOption;

    void Update()
    {
        if (m_scoreText.text == "Best score : ")
            m_scoreText.text += PlayerPrefs.GetInt("score").ToString();
    }

    public void launchGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void optionsMenu()
    {

    }
}
