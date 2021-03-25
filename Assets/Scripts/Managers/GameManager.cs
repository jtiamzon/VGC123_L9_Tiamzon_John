using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement; // Added in

public class GameManager : MonoBehaviour
{
    static GameManager _instance = null;
    // what will happen is that the object (_instance) is a one-time use. Once it has been set, it will stay set.
    public static GameManager instance
    {
        get { return _instance; }
        set { _instance = value; }
    }

    int _score = 0;
    public int score
    {
        get { return _score; }
        set
        {
            _score = value;
            Debug.Log("Current Score Is " + _score);
        }
    }
    AudioSource playerDeathAudio;
    public AudioClip playerDeathSFX;

    public int maxLives = 3;
    int _lives;   
    public int lives
    {
        get { return _lives; }
        set
        {
            if (_lives > value) // if the lives is greater than the value that's being set (basically if we're losing a life)
            {
                // respawn code goes here using Instantiate or reposition
                // SpawnPlayer(currentLevel.spawnLocation);
                if (!playerDeathAudio)
                {
                    playerDeathAudio = gameObject.AddComponent<AudioSource>();
                    playerDeathAudio.clip = playerDeathSFX;
                    playerDeathAudio.loop = false;
                    playerDeathAudio.Play();
                }
                else
                {
                    playerDeathAudio.Play();
                }
                Respawn();
                // _lives = maxLives;
            }
            _lives = value;
            if (_lives > maxLives)
            {
                _lives = maxLives;
            }
            
            else if (_lives < 0)
            {
                // Game Over code will go here.
            }
            Debug.Log("Current Lives Are: " + _lives);
        }
    }

    public GameObject playerInstance;
    public GameObject playerPrefab;
    public LevelManager currentLevel;

    // Start is called before the first frame update
    void Start()
    {
        if (instance)      // if instance exists
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this); // when it loads, don't destroy the GameManager
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        // Switching between scenes
        if (Input.GetKeyDown(KeyCode.Escape))       // if Escape is pressed down
        {
            if (SceneManager.GetActiveScene().name == "Level")
            {
                SceneManager.LoadScene("TitleScreen");
                // Note: LoadSceneAsync will open a secondary thread and run from there; will need to incorporate a loading screen with a LoadSceneAsync
                // Aka when a cut scene is happening, LoadSceneAsync will be loading the level design in the background
            }
            else if (SceneManager.GetActiveScene().name == "TitleScreen")
            {
                SceneManager.LoadScene("Level");
            }
            
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            QuitGame();
        }

        //Lab7
        /*if (lives <= 0) // if lives reach or equal to 0
        {
            if (SceneManager.GetActiveScene().name == "Level")  // if the active scene playing is Level
            {
                SceneManager.LoadScene("GameOver"); // scene will load GameOver scene                    
         
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene().name == "GameOver")
            {
                SceneManager.LoadScene("TitleScreen");
            }

        }*/
    }

    // Week9
    public void SpawnPlayer(Transform spawnLocation)
    {
        // CameraFollow mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>(); <<--- this can work too
        CameraFollow mainCamera = FindObjectOfType<CameraFollow>();
        // EnemyTurret[] turretEnemy = FindObjectOfType<EnemyTurret>(); <<--- to have turretEnemy find player

        if (mainCamera) // if mainCamera is found
        {
            mainCamera.player = Instantiate(playerPrefab, spawnLocation.position, spawnLocation.rotation);
            playerInstance = mainCamera.player;
            /*
            for (int i = 0; i < turretEnemy.Length; i++)
            {
                turretEnemy.player = mainCamera.player;
            }
            */
        }
        else
        {
            SpawnPlayer(spawnLocation); // 
        }
       
    }

    public void Respawn()   // this will save the spawn location
    {
        playerInstance.transform.position = currentLevel.spawnLocation.position;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level");
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
        #else
            Application.Quit(); // will exit the game
        #endif
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("TitleScreen");
    }
}
