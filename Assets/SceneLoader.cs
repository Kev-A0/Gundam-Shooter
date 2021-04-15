using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Name: Saksham Bhardwaj
/// StudentNo: A01185352
/// </summary>
public class SceneLoader : MonoBehaviour
{
    [SerializeField] Slider Volume;

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ChangeVolume()
    {
        AudioListener.volume = Volume.value;
    }

    public void QuitGame()
    {
        Application.Quit();

        Debug.Log("Quit");
    }
}
