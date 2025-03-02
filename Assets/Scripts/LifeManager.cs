using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LifeManager : MonoBehaviour
{
    public GameObject[] hearts; // UI-иконки жизней
    public AudioClip gameOverSound;
    public GameObject gameOverPanel;
    public GameObject gamePanel;
    public Button restartButton;
    public Button mainMenuButton;

    private int health;
    private AudioSource gameOverAudioSource;
    private BallController ballController;
    private PlatformController platformController;
    private bool isLosingLife = false; // Флаг для защиты от двойного вызова

    void Start()
    {
        health = hearts.Length;
        gameOverAudioSource = gameObject.AddComponent<AudioSource>();
        ballController = FindObjectOfType<BallController>();
        platformController = FindObjectOfType<PlatformController>();

        restartButton.onClick.AddListener(RestartGame);
        mainMenuButton.onClick.AddListener(GoToMainMenu);

        UpdateLifeSprites();
        Time.timeScale = 1; // Сбрасываем замедленное время после поражения
    }

    void UpdateLifeSprites()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].SetActive(i < health); // Включаем/выключаем сердца на UI
        }
    }

    public void LoseLife()
    {
        if (isLosingLife) return; // Если уже идет потеря жизни, выходим
        isLosingLife = true;

        if (health > 0)
        {
            health--;
            UpdateLifeSprites();

            if (health == 0)
            {
                GameOver();
            }
            else
            {
                StartCoroutine(ResetAfterLifeLoss());
            }
        }
    }

    private IEnumerator ResetAfterLifeLoss()
    {
        yield return new WaitForSeconds(0.5f);
        ballController.ResetBall();
        platformController.ResetPlatform();
        isLosingLife = false; // Сбрасываем флаг потери жизни
    }

    private void GameOver()
    {
        gameOverPanel.SetActive(true);
        gamePanel.SetActive(false);

        if (gameOverSound != null)
        {
            gameOverAudioSource.PlayOneShot(gameOverSound);
        }

        Time.timeScale = 0; // Останавливаем игру сразу, без задержки
    }

    void RestartGame()
    {
        Time.timeScale = 1; // Перед рестартом сбрасываем скорость времени
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void GoToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}
