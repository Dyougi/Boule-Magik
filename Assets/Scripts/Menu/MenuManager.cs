using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

    [SerializeField] Text scoreText;

    void Update()
    {
        if (scoreText.text == "Best score : ")
            scoreText.text += PlayerPrefs.GetInt("Score").ToString();
    }

    public void launchGame()
    {
        SceneManager.LoadScene("MainScene");
    }
}
