using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    public int levelNumber; // ����� ������, �� ������� ����� ������
    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);

        // ���������, �������� �� �������
        if (!IsLevelUnlocked(levelNumber))
        {
            button.interactable = false; // ��������� ������, ���� ������� �� ������
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
            Debug.Log("������� " + levelNumber + " ��� �� �������������!");
        }
    }

    bool IsLevelUnlocked(int level)
    {
        if (level == 1 || level == 2) return true; // ������ ������� ������ ��������

        return PlayerPrefs.GetInt("Level" + level + "Unlocked", 0) == 1;
    }

    public static void CompleteLevel(int level)
    {
        PlayerPrefs.SetInt("Level" + level + "Completed", 1);
        PlayerPrefs.SetInt("Level" + (level + 1) + "Unlocked", 1);
        PlayerPrefs.Save(); // ��������� ���������
        Debug.Log("������� " + level + " ��������! ��������� ������� �������������.");
    }
}
