using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] GameObject spaceship;
    [SerializeField] Text levelText;
    [SerializeField] Text helpText;
    [SerializeField] GameObject gameInfoConvas;
    [SerializeField] GameObject pauseMenuConvas;

    public void Start()
    {
        gameInfoConvas.SetActive(true);
        pauseMenuConvas.SetActive(false);
        helpText.gameObject.SetActive(false);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        spaceship.GetComponent<Rocket>().gameIsPaused = true;
        gameInfoConvas.SetActive(false);
        pauseMenuConvas.SetActive(true);
        helpText.gameObject.SetActive(false);

    }
    public void PlayGame()
    {
        Time.timeScale = 1;
        spaceship.GetComponent<Rocket>().gameIsPaused = false;
        gameInfoConvas.SetActive(true);
        pauseMenuConvas.SetActive(false);
    }
    public void PlayAgain()
    {
        Time.timeScale = 1;
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
        gameInfoConvas.SetActive(true);
        pauseMenuConvas.SetActive(false);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void Help()
    {
        helpText.gameObject.SetActive(!helpText.gameObject.active);
    }
    void Update()
    {
        levelText.text = "Level : " + (SceneManager.GetActiveScene().buildIndex + 1).ToString();
    }
}
