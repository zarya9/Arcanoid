using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectiveBlockScript : MonoBehaviour
{
    public int health = 3;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball") || collision.gameObject.CompareTag("BallClone"))
        {
            Vector2 normal = collision.contacts[0].normal;
            Vector2 newDirection = Vector2.Reflect(collision.gameObject.GetComponent<BallController>().direction, normal);
            collision.gameObject.GetComponent<BallController>().direction = newDirection;

            health--;
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}