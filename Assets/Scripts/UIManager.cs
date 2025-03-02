using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    
    private bool isPaused = false;

    // Перезапуск уровня
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Включение/выключение паузы
    public void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
    }

    // Выход в главное меню
    public void GoToMainMenu()
    {
        Time.timeScale = 1; // Убеждаемся, что время идет нормально
        SceneManager.LoadScene("MainMenu"); // Убедись, что у сцены "MainMenu" правильное название в Build Settings
    }

    // Выход из игры
    public void QuitGame()
    {
        Application.Quit();
    }
}
