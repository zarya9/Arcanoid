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
        // �������� "NextLevelScene" �� �������� ����� ��������� �����
        SceneManager.LoadScene("Level2");
    }
    public void GoToHome()
    {
        // �������� "MainScene" �� �������� ����� ������� �����
        SceneManager.LoadScene("MainMenu");
    }
}
