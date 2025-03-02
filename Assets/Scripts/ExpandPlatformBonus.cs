using System.Collections;
using UnityEngine;

public class ExpandPlatformBonus : BaseBonus
{
    private bool isExpanded = false;
    private Vector3 originalScale;
    public float effectDurationpl = 2f; // ������� ��������� ���������� ��� ����������������� �������

    protected override void ApplyBonus(GameObject platform)
    {
        if (platform == null)
        {
            Debug.LogError("������: ��������� �� �������� � �����!");
            return;
        }

        if (!isExpanded)
        {
            isExpanded = true;
            originalScale = platform.transform.localScale; // ���������� �������� ������
            Debug.Log("�����: ���������� ���������! ����� ������: " + originalScale.x * 1.5f);
            StartCoroutine(ModifyPlatformSize(platform, 1.5f));
        }
        else
        {
            Debug.Log("��������� ��� ���������, ��������� ���������� ����������.");
        }
    }

    private IEnumerator ModifyPlatformSize(GameObject platform, float scaleMultiplier)
    {
        if (platform == null)
        {
            Debug.LogError("������: ��������� ���� ���������� �� ���������� �������!");
            yield break;
        }

        // ����������� ���������
        platform.transform.localScale = new Vector3(originalScale.x * scaleMultiplier, originalScale.y, originalScale.z);
        Debug.Log("��������� ���������, ���� " + effectDuration + " ������.");

        // ���� ����������������� �������
        yield return new WaitForSeconds(effectDuration);

        // ���������� ��������� � ��������� �������
        platform.transform.localScale = originalScale;
        Debug.Log("������ ��������� �������� � ���������: " + originalScale.x);

        isExpanded = false;
        Debug.Log("����� ����� ������������ �����.");
    }
}
