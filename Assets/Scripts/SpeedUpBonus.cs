using UnityEngine;

public class SpeedUpBonus : BaseBonus
{
    private float speedMultiplier = 1.5f;

    protected override void ApplyBonus(GameObject platform)
    {
        Debug.Log("Бонус: Ускорение мяча!");
        BallController[] balls = FindObjectsOfType<BallController>();

        foreach (BallController ball in balls)
        {
            if (ball != null)
            {
                float originalSpeed = ball.GetOriginalSpeed();
                ball.SetSpeed(originalSpeed * speedMultiplier);
                Debug.Log("Ускоренная скорость мяча: " + ball.GetOriginalSpeed());

                // Запускаем возврат скорости через effectDuration секунд
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
