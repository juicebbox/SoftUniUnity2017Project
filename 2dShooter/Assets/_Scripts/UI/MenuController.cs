using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public Button startGameButton;
    //public Button settingsButton;
    //public Button settingsBackButton;

    public Dropdown difficultyDropdown;

    public GameObject mainMenuView;
    public GameObject settingsMenuView;

    public AudioClip buttonClick;
    public AudioSource audioSource;
    public GameMaster gameMaster;
    // Use this for initialization

    private bool mainMenuShown;

    void Start ()
    {
        mainMenuView.SetActive(true);
        if(settingsMenuView != null)
        {
            settingsMenuView.SetActive(false);
        }
    }

    private void Update()
    {
        SetDifficulty();
        if(!gameMaster.gameStarted && !mainMenuShown)
        {
            mainMenuShown = true;
        }
    }

    public void SetDifficulty()
    {
        if(difficultyDropdown != null)
        {
            gameMaster.difficulty = difficultyDropdown.options[difficultyDropdown.value].text;
            gameMaster.SetDifficulty(gameMaster.difficulty);
            // gameMaster.SetDifficulty(gameMaster.difficulty);
        }

    }
    public void GoToMainMenu()
    {
        settingsMenuView.SetActive(false);
        mainMenuView.SetActive(true);
        //audioSource.PlayOneShot(buttonClick);
    }

    public void GoToSettingsMenu()
    {
        if (settingsMenuView != null)
        {
            settingsMenuView.SetActive(true);
        }
        mainMenuView.SetActive(false);
        audioSource.PlayOneShot(buttonClick);
    }

    public void StartGame()
    {
        
        gameMaster.StartGame();
        Destroy(gameObject);
    }

    public void ResumeGame()
    {
        gameMaster.ResumeGame();
        Destroy(gameObject);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
}
