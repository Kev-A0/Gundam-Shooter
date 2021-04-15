using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// This is the scrip connexted to scene loader foer main menu and other sub menus
/// 
/// Author: Saksham Bhardwaj
/// Date: April 10, 2021; Revision: 2.0
/// </summary>
public class SceneLoader : MonoBehaviour
{
    [SerializeField] Slider Volume;
    //play game function
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    //volume slider function value
    public void ChangeVolume()
    {
        AudioListener.volume = Volume.value;
    }
    //quit button log and function
    public void QuitGame()
    {
        Application.Quit();

        Debug.Log("Quit");
    }
}
