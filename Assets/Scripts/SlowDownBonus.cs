using UnityEngine;

public class SlowDownBonus : BaseBonus
{
    private float slowMultiplier = 0.5f;

    protected override void ApplyBonus(GameObject platform)
    {
        Debug.Log("�����: ���������� ����!");
        BallController[] balls = FindObjectsOfType<BallController>();

        foreach (BallController ball in balls)
        {
            if (ball != null)
            {
                float originalSpeed = ball.GetOriginalSpeed();
                ball.SetSpeed(originalSpeed * slowMultiplier);

                // ��������� ������ ��� �������� ��������
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
