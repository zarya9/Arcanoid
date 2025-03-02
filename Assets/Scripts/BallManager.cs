using UnityEngine;

public class BallManager : MonoBehaviour
{
    public static BallManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void DestroyAllClones()
    {
        // ������� ��� ������� � ����� "BallClone"
        GameObject[] clones = GameObject.FindGameObjectsWithTag("BallClone");

        // ���������� ������ ����
        foreach (var clone in clones)
        {
            if (clone != null)
            {
                Destroy(clone);
            }
        }
    }
}