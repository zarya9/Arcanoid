using UnityEngine;

public class BottomSquare : MonoBehaviour
{
    public LifeManager lifeManager;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            lifeManager.LoseLife(); 
        }
        if (collision.CompareTag("Untagged"))
        {
            Destroy(collision.gameObject);
        }
    }
}