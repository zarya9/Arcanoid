using UnityEngine;

public class MovingBlockScript : MonoBehaviour
{
    public float speed = 2f;
    public float distance = 3f;
    private Vector3 startPosition;
    public int health = 1;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float newX = startPosition.x + Mathf.PingPong(Time.time * speed, distance);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball") || collision.gameObject.CompareTag("BallClone"))
        {
            health--;
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}