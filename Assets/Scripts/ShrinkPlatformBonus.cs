using System.Collections;
using UnityEngine;

public class ShrinkPlatformBonus : BaseBonus
{
    private bool isShrunk = false; // Флаг для отслеживания уменьшения

    protected override void ApplyBonus(GameObject platform)
    {
        if (!isShrunk)
        {
            isShrunk = true; // Отмечаем, что платформа уже уменьшена
            Debug.Log("Бонус: Уменьшение платформы!");
            StartCoroutine(ModifyPlatformSize(platform, 0.75f));
        }
    }

    private IEnumerator ModifyPlatformSize(GameObject platform, float scaleMultiplier)
    {
        Vector3 originalScale = platform.transform.localScale;
        platform.transform.localScale = new Vector3(originalScale.x * scaleMultiplier, originalScale.y, originalScale.z);
        yield return new WaitForSeconds(effectDuration);
        platform.transform.localScale = originalScale;
        isShrunk = false; // Сбрасываем флаг после возврата к исходному размеру
    }
}
