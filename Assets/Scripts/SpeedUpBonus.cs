using UnityEngine;

public class SpeedUpBonus : BaseBonus
{
    private float speedMultiplier = 1.5f;

    protected override void ApplyBonus(GameObject platform)
    {
        Debug.Log("�����: ��������� ����!");
        BallController[] balls = FindObjectsOfType<BallController>();

        foreach (BallController ball in balls)
        {
            if (ball != null)
            {
                float originalSpeed = ball.GetOriginalSpeed();
                ball.SetSpeed(originalSpeed * speedMultiplier);
                Debug.Log("���������� �������� ����: " + ball.GetOriginalSpeed());

                // ��������� ������� �������� ����� effectDuration ������
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
                Debug.Log("������������ �������� ���� ���������: " + ball.GetOriginalSpeed());
            }
        }
    }
}
