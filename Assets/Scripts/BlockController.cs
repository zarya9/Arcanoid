using UnityEngine;

public class BlockController : MonoBehaviour
{
    public int health = 1;
    public GameObject lockIcon;
    public GameObject[] bonusPrefabs;
    public float bonusDropChance = 1f;

    private LevelManager levelManager;

    void Start()
    {
        health = Random.Range(0, 4) == 0 ? -1 : Random.Range(1, 4);
        if (lockIcon != null)
        {
            lockIcon.SetActive(health < 0);
        }

        levelManager = FindObjectOfType<LevelManager>();
    }

    public void TakeDamage()
    {
        if (health < 0) return;

        health--;
        if (health <= 0)
        {
            TrySpawnBonus();
            Destroy(gameObject);

            levelManager?.CheckLevelState();
        }
    }

    void TrySpawnBonus()
    {
        if (bonusPrefabs == null || bonusPrefabs.Length == 0)
        {
            Debug.LogError("Массив bonusPrefabs пуст или не назначен!");
            return;
        }

        if (Random.value <= bonusDropChance)
        {
            int randomIndex = Random.Range(0, bonusPrefabs.Length);
            GameObject bonusPrefab = bonusPrefabs[randomIndex];

            if (bonusPrefab != null)
            {
                Instantiate(bonusPrefab, transform.position, Quaternion.identity);
                Debug.Log("Бонус создан: " + bonusPrefab.name);
            }
            else
            {
                Debug.LogError("Префаб бонуса не назначен!");
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball") || collision.gameObject.CompareTag("BallClone"))
        {
            TakeDamage();
        }
    }
}
