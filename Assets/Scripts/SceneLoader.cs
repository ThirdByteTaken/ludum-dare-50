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
}
