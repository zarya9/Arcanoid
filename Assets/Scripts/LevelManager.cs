using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // ��� ������ � UI

public class LevelManager : MonoBehaviour
{
    public GameObject winPanel; // ������ ������
    public string nextLevelName; // �������� ���������� ������
    private bool levelCompleted = false; // ���� ���������� ������
    public GameObject gamePanel;

    void Start()
    {
        Time.timeScale = 1; // ���������� ����� ��� �������� ������
    }

    void Update()
    {
        CheckLevelState(); // ��������� ��������� ������ �� ������ �����
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
        Time.timeScale = 0; // ������������� ����

        if (winPanel != null)
        {
            winPanel.SetActive(true);
            gamePanel.SetActive(false);
        }

        string currentLevel = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetInt(currentLevel + "_Completed", 1);
        Debug.Log("���������: " + currentLevel + "_Completed = 1");

        if (!string.IsNullOrEmpty(nextLevelName))
        {
            PlayerPrefs.SetInt(nextLevelName + "_Unlocked", 1);
            Debug.Log("���������: " + nextLevelName + "_Unlocked = 1");
        }

        PlayerPrefs.Save();
        Debug.Log("������� " + currentLevel + " ��������! ��������� ������� �������������.");
    }

    bool AreOnlyIndestructibleBlocksLeft()
    {
        GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");

        foreach (GameObject block in blocks)
        {
            BlockController blockController = block.GetComponent<BlockController>();
            if (blockController != null && blockController.health > 0)
            {
                return false; // ���� ����������� �����, ���� ������������
            }
        }

        return true; // �������� ������ ������������� ����� ��� ������ ��� ������
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
            Debug.LogError("������� " + nextLevelName + " �� �������������!");
        }
    }

    public void RestartLevel()
    {
        Time.timeScale = 1; // ����������� ���������� ����� ����� ������������
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // ������ ������ ��� ��������� ������� ������
    public void OnNextLevelButtonClicked()
    {
        LoadNextLevel();
    }

    public void OnRestartButtonClicked()
    {
        RestartLevel();
    }
}