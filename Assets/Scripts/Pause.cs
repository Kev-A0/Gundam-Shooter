using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// This is the scrip connexted to pause menu and its functions
/// 
/// Author: Saksham Bhardwaj
/// Date: April 13, 2021; Revision: 1.0
/// </summary>
public class Pause : MonoBehaviour
{
    public static bool isGamePaused = false;

    [SerializeField] GameObject pause;

    //pause function having sub functions set as public
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGamePaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }
    //resume button function
    public void ResumeGame()
    {
        pause.SetActive(false);
        Time.timeScale = 1f;
        isGamePaused = false;
    }
    //pause button function
    public void PauseGame()
    {
        pause.SetActive(true);
        Time.timeScale = 0f;
        isGamePaused = true;
    }
    //restart button function
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }
    //menu button function
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }
    public void Exit()
    {
        Application.Quit();

        Debug.Log("Quit");
    }

}
