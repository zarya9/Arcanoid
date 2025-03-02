using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    
    private bool isPaused = false;

    // ���������� ������
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // ���������/���������� �����
    public void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
    }

    // ����� � ������� ����
    public void GoToMainMenu()
    {
        Time.timeScale = 1; // ����������, ��� ����� ���� ���������
        SceneManager.LoadScene("MainMenu"); // �������, ��� � ����� "MainMenu" ���������� �������� � Build Settings
    }

    // ����� �� ����
    public void QuitGame()
    {
        Application.Quit();
    }
}
