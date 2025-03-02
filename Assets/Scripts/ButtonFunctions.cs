using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonFunctions : MonoBehaviour
{
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }
    public void NextLevel()
    {
        // Замените "NextLevelScene" на название вашей следующей сцены
        SceneManager.LoadScene("Level2");
    }
    public void GoToHome()
    {
        // Замените "MainScene" на название вашей главной сцены
        SceneManager.LoadScene("MainMenu");
    }
}
