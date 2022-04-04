using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    public void LoadMenu()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(0);
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        isPaused = false;
        SceneManager.LoadScene(1);
    }

    public void LoadCredits()
    {
        SceneManager.LoadScene(2, LoadSceneMode.Additive);
    }

    public void UnloadCredits()
    {
        SceneManager.UnloadSceneAsync(2);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(0);
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        isPaused = true;
        SceneManager.LoadScene(3, LoadSceneMode.Additive);
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1;
        isPaused = false;
        SceneManager.UnloadSceneAsync(3);
    }

    public static void PlayerDeath()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(4);
    }

    public static bool isPaused = false;
    public static void TogglePause()
    {
        if (isPaused)
        {
            Time.timeScale = 1;
            isPaused = false;
            SceneManager.UnloadSceneAsync(3);
        }
        else
        {
            Time.timeScale = 0;
            isPaused = true;
            SceneManager.LoadScene(3, LoadSceneMode.Additive);
        }
    }
}
