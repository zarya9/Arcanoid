using UnityEngine;

public class BallController : MonoBehaviour
{
    public float speed = 5f;
    public Vector2 direction;

    public bool isLaunched = false;

    private AudioSource audioSource;
    public AudioClip platformHitSound;
    public AudioClip boundaryHitSound;
    public AudioClip blockHitSound;

    private Vector3 initialPosition;
    private LifeManager lifeManager;
    private float originalSpeed;
    private Rigidbody2D rb;

    void Start()
    {
        direction = Vector2.zero;
        audioSource = GetComponent<AudioSource>();
        initialPosition = transform.position;
        lifeManager = FindObjectOfType<LifeManager>();
        originalSpeed = speed;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!isLaunched && Input.GetMouseButtonDown(0))
        {
            LaunchBall();
        }

        // Проверка границ экрана
        CheckScreenBounds();
    }

    public void LaunchBall()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = (mousePosition - (Vector2)transform.position).normalized;
        isLaunched = true;
        rb.velocity = direction * speed;
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
            lifeManager.LoseLife();
            ResetBall();
        }
    }

    void CheckScreenBounds()
    {
        if (!isLaunched) return; // Не проверяем границы, если шар не запущен

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

    public void ResetBall()
    {
        transform.position = initialPosition;
        isLaunched = false;
        direction = Vector2.zero;
        rb.velocity = Vector2.zero;
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
        if (isLaunched)
        {
            rb.velocity = direction * speed;
        }
    }

    public float GetOriginalSpeed()
    {
        return originalSpeed;
    }

    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection.normalized;
        if (isLaunched)
        {
            rb.velocity = direction * speed;
        }
    }
    private void OnDestroy()
    {
        BallManager.Instance.DestroyAllClones();
    }
}