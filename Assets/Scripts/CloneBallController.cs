using UnityEngine;

public class CloneBallController : MonoBehaviour
{
    public float speed = 5f; // Базовая скорость (может быть переопределена)
    public Vector2 direction;

    private AudioSource audioSource;
    public AudioClip platformHitSound;
    public AudioClip boundaryHitSound;
    public AudioClip blockHitSound;

    private Rigidbody2D rb;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();

        // Задаем случайное начальное направление для клона
        direction = Random.insideUnitCircle.normalized;
        rb.velocity = direction * speed; // Используем заданную скорость
    }

    // Метод для установки скорости клона
    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
        rb.velocity = direction * speed; // Обновляем скорость
    }

    void Update()
    {
        // Проверка границ экрана
        CheckScreenBounds();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            float hitPosition = (transform.position.x - collision.transform.position.x) / collision.collider.bounds.size.x;
            direction = new Vector2(hitPosition, 1).normalized;
            rb.velocity = direction * speed;
            PlaySound(platformHitSound);
        }
        else if (collision.gameObject.CompareTag("Block"))
        {
            direction = Vector2.Reflect(direction, collision.contacts[0].normal);
            rb.velocity = direction * speed;
            PlaySound(blockHitSound);
        }
        else
        {
            direction = Vector2.Reflect(direction, collision.contacts[0].normal);
            rb.velocity = direction * speed;
            PlaySound(boundaryHitSound);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BottomSquare"))
        {
            // Уничтожаем клон, если он падает вниз
            Destroy(gameObject);
        }
    }

    void CheckScreenBounds()
    {
        Vector2 ballPosition = transform.position;
        Vector2 minBounds = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 maxBounds = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        bool isOutOfBounds = false;

        // Проверка левой и правой границ
        if (ballPosition.x < minBounds.x || ballPosition.x > maxBounds.x)
        {
            direction.x = -direction.x;
            isOutOfBounds = true;
        }

        // Проверка верхней границы
        if (ballPosition.y > maxBounds.y)
        {
            direction.y = -direction.y;
            isOutOfBounds = true;
        }

        // Если шар вышел за границы, обновляем его скорость
        if (isOutOfBounds)
        {
            rb.velocity = direction * speed;
            PlaySound(boundaryHitSound);
        }

        // Ограничиваем позицию шара в пределах экрана
        transform.position = new Vector2(
            Mathf.Clamp(ballPosition.x, minBounds.x, maxBounds.x),
            Mathf.Clamp(ballPosition.y, minBounds.y, maxBounds.y)
        );
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}