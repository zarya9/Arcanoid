using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Для работы с UI

public class LevelManager : MonoBehaviour
{
    public GameObject winPanel; // Панель победы
    public string nextLevelName; // Название следующего уровня
    private bool levelCompleted = false; // Флаг завершения уровня
    public GameObject gamePanel;

    void Start()
    {
        Time.timeScale = 1; // Сбрасываем время при загрузке уровня
    }

    void Update()
    {
        CheckLevelState(); // Проверяем состояние уровня на каждом кадре
    }

    public void CheckLevelState()
    {
        if (!levelCompleted && AreOnlyIndestructibleBlocksLeft())
        {
            CompleteLevel();
        }
    }

    private void CompleteLevel()
    {
        levelCompleted = true;
        Time.timeScale = 0; // Останавливаем игру

        if (winPanel != null)
        {
            winPanel.SetActive(true);
            gamePanel.SetActive(false);
        }

        string currentLevel = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetInt(currentLevel + "_Completed", 1);
        Debug.Log("Сохранено: " + currentLevel + "_Completed = 1");

        if (!string.IsNullOrEmpty(nextLevelName))
        {
            PlayerPrefs.SetInt(nextLevelName + "_Unlocked", 1);
            Debug.Log("Сохранено: " + nextLevelName + "_Unlocked = 1");
        }

        PlayerPrefs.Save();
        Debug.Log("Уровень " + currentLevel + " завершен! Следующий уровень разблокирован.");
    }

    bool AreOnlyIndestructibleBlocksLeft()
    {
        GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");

        foreach (GameObject block in blocks)
        {
            BlockController blockController = block.GetComponent<BlockController>();
            if (blockController != null && blockController.health > 0)
            {
                return false; // Есть разрушаемые блоки, игра продолжается
            }
        }

        return true; // Остались только неразрушаемые блоки или блоков нет вообще
    }

    public void LoadNextLevel()
    {
        if (PlayerPrefs.GetInt(nextLevelName + "_Unlocked", 0) == 1)
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(nextLevelName);
        }
        else
        {
            Debug.LogError("Уровень " + nextLevelName + " не разблокирован!");
        }
    }

    public void RestartLevel()
    {
        Time.timeScale = 1; // Обязательно сбрасываем время перед перезапуском
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Пример метода для обработки нажатия кнопки
    public void OnNextLevelButtonClicked()
    {
        LoadNextLevel();
    }

    public void OnRestartButtonClicked()
    {
        RestartLevel();
    }
}