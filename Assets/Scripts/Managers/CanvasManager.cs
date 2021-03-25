using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    [Header("Buttons")]
    public Button startButton;
    public Button quitButton;
    public Button backButton;
    public Button settingsButton;
    public Button returnToMenuButton;
    public Button returnToGameButton;
    public Button restartGameButton;

    [Header("Menus")]
    public GameObject pauseMenu;
    public GameObject mainMenu;
    public GameObject settingsMenu;

    [Header("Text")]
    public Text livesText;
    public Text volText;
    public Text scoreText;

    [Header("Slider")]
    public Slider volSlider;

    [Header("Toggle")]
    public Toggle muteToggle;

    private bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        if (startButton)        // if we have a reference to start button
        {
            startButton.onClick.AddListener(() => GameManager.instance.StartGame());
        }

        if (quitButton)
        {
            quitButton.onClick.AddListener(() => GameManager.instance.QuitGame());
        }

        if (returnToGameButton)
        {
            returnToGameButton.onClick.AddListener(() => ReturnToGame());
        }

        if (returnToMenuButton)
        {
            returnToMenuButton.onClick.AddListener(() => GameManager.instance.ReturnToMenu());
        }

        if (backButton)
        {
            if (pauseMenu)
            {
                backButton.onClick.AddListener(() => BackToPause());
            }
            else
            {
                backButton.onClick.AddListener(() => ShowMainMenu());
            }
        }

        if (settingsButton)
        {
            settingsButton.onClick.AddListener(() => ShowSettingsMenu());
        }

        if (restartGameButton)
        {
            restartGameButton.onClick.AddListener(() => GameManager.instance.ReturnToMenu());
        }

        /* if (muteToggle)
        {
            muteToggle = GetComponent<Toggle>();
            if(AudioListener.volume == 0)
            {                
                muteToggle.isOn = false;
            }
        } */

    }

    // Update is called once per frame
    void Update()
    {
        if (pauseMenu) // if pauseMenu exists
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                pauseMenu.SetActive(!pauseMenu.activeSelf);
                isPaused = true;
                if (isPaused == true)
                    PauseActivated();
                else
                    PauseDeactivated();
            }              
        }

        if (livesText)      // if livesText exists
        {
            livesText.text = GameManager.instance.lives.ToString();     // links game lives to text of livesText
        }
        if (settingsMenu)
        {
            if (settingsMenu.activeSelf)
            {
                volText.text = volSlider.value.ToString();  // this allows the slider to link with the text value of the slider.
            }
        }

        if (scoreText)
        {
            scoreText.text = GameManager.instance.score.ToString();
        }
    }

    void PauseActivated()
    {
        Time.timeScale = 0;
        AudioListener.pause = true;       
    }

    void PauseDeactivated()
    {
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
            AudioListener.pause = false;
            isPaused = false;
    }

    public void ReturnToGame()
    {
        PauseDeactivated();
    }

    void ShowMainMenu()
    {
        settingsMenu.SetActive(false);  // will not show settings menu
        mainMenu.SetActive(true);

    }

    void ShowSettingsMenu()
    {
        if (pauseMenu)
        {
            settingsMenu.SetActive(true); 
            pauseMenu.SetActive(false);  
        }
        else
        {
            settingsMenu.SetActive(true); 
            mainMenu.SetActive(false);
        }
    }

    void BackToPause()
    {
        settingsMenu.SetActive(false);
        pauseMenu.SetActive(true);

    }

    /* void MuteAudio(bool audioIn)
    {
        if (audioIn)
        {
            AudioListener.volume = 1;
        }
        else
        {
            AudioListener.volume = 0;
        }
    } */
}
