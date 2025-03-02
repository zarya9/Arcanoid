using UnityEngine;

public class MultiBallBonus : BaseBonus
{
    public GameObject ballPrefab;
    public int additionalBalls = 2;
    public float spawnOffset = 0.3f;
    public float directionVariation = 30f;
    public float launchForce = 5f;

    protected override void ApplyBonus(GameObject platform)
    {
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");

        foreach (GameObject ball in balls)
        {
            BallController ballController = ball.GetComponent<BallController>();
            Rigidbody2D ballRb = ball.GetComponent<Rigidbody2D>();

            if (ballController == null || ballRb == null || !ballController.isLaunched) continue;

            Vector2 originalVelocity = ballRb.velocity; // Запоминаем скорость

            for (int i = 0; i < additionalBalls; i++)
            {
                Vector3 spawnPosition = ball.transform.position + (Vector3)Random.insideUnitCircle * spawnOffset;
                GameObject newBall = Instantiate(ballPrefab, spawnPosition, Quaternion.identity);

                BallController newBallController = newBall.GetComponent<BallController>();
                Rigidbody2D newRb = newBall.GetComponent<Rigidbody2D>();

                if (newBallController != null && newRb != null)
                {
                    newBallController.isLaunched = true;

                    float angleOffset = Random.Range(-directionVariation, directionVariation);
                    Vector2 newDirection = Quaternion.Euler(0, 0, angleOffset) * originalVelocity.normalized;

                    Debug.Log($"New ball direction: {newDirection}, Force: {launchForce}");

                    newRb.velocity = Vector2.zero; // Сбрасываем скорость
                    newRb.AddForce(newDirection * launchForce, ForceMode2D.Impulse); // Даём толчок
                    newRb.WakeUp(); // Разбудить объект, если он "уснул"
                }
            }
        }
    }
}