using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public float speed = 7f;
    private Vector3 initialPosition;
    private void Start()
    {
        initialPosition = transform.position;
    }
    void Update()
    {
        Vector3 targetPosition = transform.position;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            touchPosition.z = 0;
            targetPosition.x = touchPosition.x;
        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            targetPosition.x = mousePosition.x;
        }

        float halfPlatformWidth = transform.localScale.x / 2f;
        float minX = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + halfPlatformWidth;
        float maxX = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - halfPlatformWidth;
        targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);

        transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);
    }
    public void ResetPlatform()
    {
        transform.position = initialPosition; // Возвращаем платформу в начальную позицию
    }
}