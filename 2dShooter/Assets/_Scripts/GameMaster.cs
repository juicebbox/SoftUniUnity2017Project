using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    private GameObject player;
    // Use this for initialization
    public float damageMultiplier = 1;
    public float enemyDamageMultiplier = 1;

    private int firstSceneIndex = 3;
    private int levelsBeaten = 0;

    public bool gameStarted;
    public bool gamePaused;
    private bool firstStart;
    public Scene lastLoadedScene;
    public string difficulty;

    public bool roomLocked;

    public bool upgradeMenu;

    void Start ()
    {
        lastLoadedScene = SceneManager.GetSceneByName("working");
        Time.timeScale = 0;
        gameStarted = false;
        firstStart = true;
        firstSceneIndex = 3;
        player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update ()
    {

        if (gameStarted && (player == null))
        {
            player = GameObject.Find("Player");
        }
        if (Time.timeScale > 0)
        {
            gamePaused = false;
            gameStarted = true;
        }
        if (Input.GetAxisRaw("Cancel") != 0 && (gameStarted || !gamePaused))
        {

            if (!gamePaused)
            {
                PauseGame();
            }
        }

        if (!gameStarted && !firstStart)
        {
            RestartGame();
        }
        else if(firstStart)
        {
            FirstStartGame();
        }

        Debug.Log(firstSceneIndex);
	}

    private void FirstStartGame()
    {
        Time.timeScale = 0;
        SceneManager.LoadScene("mainMenu", LoadSceneMode.Additive);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("mainMenu"));
        gamePaused = true;
        gameStarted = true;
        firstStart = false;
    }

    public void ResumeGame()
    { 
        gamePaused = false;
        SceneManager.UnloadSceneAsync("pauseMenu");
        Time.timeScale = 1;
    }

    public void SetDifficulty(string difficulty)
    {

        if(difficulty == "Normal")
        {
            damageMultiplier = 1;
            enemyDamageMultiplier = 1;
            Debug.Log(difficulty);
        }
        else if(difficulty == "Hard")
        {
            damageMultiplier = 1.5f;
            enemyDamageMultiplier = 0.6f;
        }
    }

    public void PauseGame()
    {

        Time.timeScale = 0;

        SceneManager.LoadScene("pauseMenu", LoadSceneMode.Additive);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("pauseMenu"));
        gamePaused = true;
    }


    public void RestartGame()
    {
        Time.timeScale = 0;
        //SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(SceneManager.sceneCount - 1));
        //SceneManager.UnloadSceneAsync("pauseMenu");

        SceneManager.UnloadSceneAsync(SceneManager.GetSceneByBuildIndex(3));
        SceneManager.LoadScene("mainMenu", LoadSceneMode.Additive);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("mainMenu"));
        gamePaused = true;
        gameStarted = true;
        for(int i = 1; i< SceneManager.sceneCount; i++)
        {
            if (SceneManager.GetSceneAt(i).isLoaded)
            {
                SceneManager.UnloadSceneAsync(i);
            }
        }
    }

    public void StartGame()
    {
        Debug.Log("starting scene " + 3);
        SceneManager.LoadScene(3, LoadSceneMode.Additive);
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(3));
        SetDifficulty(difficulty);

        gameStarted = true;
        gamePaused = false;
        Time.timeScale = 1;
        SceneManager.UnloadSceneAsync("mainMenu");
 
    }

    public void LoadNextScene()
    {
        if (player == null)
        {
            player = GameObject.Find("Player");
        }
        levelsBeaten++;
        Debug.Log("Loading Next Scene");
        SceneManager.UnloadSceneAsync(firstSceneIndex);
        SceneManager.LoadScene(firstSceneIndex + levelsBeaten, LoadSceneMode.Additive);
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(firstSceneIndex + levelsBeaten));
        SetDifficulty(difficulty);
        gamePaused = false;
        Time.timeScale = 1;
        upgradeMenu = false;
        player.transform.position = Vector2.zero;
    }
}
