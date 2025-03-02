using UnityEngine;

public abstract class BaseBonus : MonoBehaviour
{
    public float fallSpeed = 2f;
    public float effectDuration = 5f;

    void Update()
    {
        transform.Translate(Vector2.down * fallSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Platform"))
        {
            ApplyBonus(collision.gameObject);
            Destroy(gameObject);
        }
    }

    protected abstract void ApplyBonus(GameObject platform);
}
