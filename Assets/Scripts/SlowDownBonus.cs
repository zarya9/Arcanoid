using UnityEngine;

public class SlowDownBonus : BaseBonus
{
    private float slowMultiplier = 0.5f;

    protected override void ApplyBonus(GameObject platform)
    {
        Debug.Log("Бонус: Замедление мяча!");
        BallController[] balls = FindObjectsOfType<BallController>();

        foreach (BallController ball in balls)
        {
            if (ball != null)
            {
                float originalSpeed = ball.GetOriginalSpeed();
                ball.SetSpeed(originalSpeed * slowMultiplier);

                // Запускаем таймер для возврата скорости
                Invoke("ResetSpeed", effectDuration);
            }
        }
    }

    private void ResetSpeed()
    {
        BallController[] balls = FindObjectsOfType<BallController>();

        foreach (BallController ball in balls)
        {
            if (ball != null && ball.gameObject.activeInHierarchy)
            {
                ball.SetSpeed(ball.GetOriginalSpeed());
                Debug.Log("Оригинальная скорость мяча вернулась: " + ball.GetOriginalSpeed());
            }
        }
    }
}
