using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    public int levelNumber; // Номер уровня, на который ведет кнопка
    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);

        // Проверяем, доступен ли уровень
        if (!IsLevelUnlocked(levelNumber))
        {
            button.interactable = false; // Блокируем кнопку, если уровень не открыт
        }
    }

    void OnClick()
    {
        if (IsLevelUnlocked(levelNumber))
        {
            SceneManager.LoadScene("Level" + levelNumber);
        }
        else
        {
            Debug.Log("Уровень " + levelNumber + " еще не разблокирован!");
        }
    }

    bool IsLevelUnlocked(int level)
    {
        if (level == 1 || level == 2) return true; // Первый уровень всегда доступен

        return PlayerPrefs.GetInt("Level" + level + "Unlocked", 0) == 1;
    }

    public static void CompleteLevel(int level)
    {
        PlayerPrefs.SetInt("Level" + level + "Completed", 1);
        PlayerPrefs.SetInt("Level" + (level + 1) + "Unlocked", 1);
        PlayerPrefs.Save(); // Сохраняем изменения
        Debug.Log("Уровень " + level + " завершен! Следующий уровень разблокирован.");
    }
}
