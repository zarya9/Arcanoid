using System.Collections;
using UnityEngine;

public class ExpandPlatformBonus : BaseBonus
{
    private bool isExpanded = false;
    private Vector3 originalScale;
    public float effectDurationpl = 2f; // Добавил публичную переменную для продолжительности эффекта

    protected override void ApplyBonus(GameObject platform)
    {
        if (platform == null)
        {
            Debug.LogError("Ошибка: платформа не передана в бонус!");
            return;
        }

        if (!isExpanded)
        {
            isExpanded = true;
            originalScale = platform.transform.localScale; // Запоминаем исходный размер
            Debug.Log("Бонус: Увеличение платформы! Новый размер: " + originalScale.x * 1.5f);
            StartCoroutine(ModifyPlatformSize(platform, 1.5f));
        }
        else
        {
            Debug.Log("Платформа уже увеличена, повторное применение невозможно.");
        }
    }

    private IEnumerator ModifyPlatformSize(GameObject platform, float scaleMultiplier)
    {
        if (platform == null)
        {
            Debug.LogError("Ошибка: платформа была уничтожена до завершения эффекта!");
            yield break;
        }

        // Увеличиваем платформу
        platform.transform.localScale = new Vector3(originalScale.x * scaleMultiplier, originalScale.y, originalScale.z);
        Debug.Log("Платформа увеличена, ждем " + effectDuration + " секунд.");

        // Ждем продолжительность эффекта
        yield return new WaitForSeconds(effectDuration);

        // Возвращаем платформу к исходному размеру
        platform.transform.localScale = originalScale;
        Debug.Log("Размер платформы вернулся к исходному: " + originalScale.x);

        isExpanded = false;
        Debug.Log("Бонус можно использовать снова.");
    }
}
