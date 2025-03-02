using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject blockPrefab; // ������ �����
    public int gridWidth = 10; // ������ ����� ������
    public int gridHeight = 5; // ������ ����� ������
    public float spacing = 1.5f; // ���������� ����� �������
    public int currentLevel = 1; // ������� �������

    void Start()
    {
        GenerateBlocks();
    }

    void GenerateBlocks()
    {
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                // ������� ���� �� �����
                Vector3 position = new Vector3(x * spacing, y * spacing, 0);
                GameObject block = Instantiate(blockPrefab, position, Quaternion.identity);

                // ����������� ���� � ����������� �� ������
                ConfigureBlock(block, x, y);
            }
        }
    }

    void ConfigureBlock(GameObject block, int x, int y)
    {
        BlockController blockController = block.GetComponent<BlockController>();

        // �� ������ ������ ��� ����� �����������
        if (currentLevel == 1)
        {
            blockController.health = Random.Range(1, 4); // 1-3 �����
            blockController.lockIcon.SetActive(false); // �������� �������
        }
        else if (currentLevel == 2)
        {
            if (Random.Range(0, 4) == 0) // 25% ���� �������������� �����
            {
                blockController.health = -1; // ������������� ����
                blockController.lockIcon.SetActive(true); // ���������� �������
            }
            else
            {
                blockController.health = Random.Range(1, 4); // 1-3 �����
                blockController.lockIcon.SetActive(false); // �������� �������
            }
        }
        else if (currentLevel == 3)
        {
            if (Random.Range(0, 2) == 0) // 50% ���� �������������� �����
            {
                blockController.health = -1; // ������������� ����
                blockController.lockIcon.SetActive(true); // ���������� �������
            }
            else
            {
                blockController.health = Random.Range(1, 4); // 1-3 �����
                blockController.lockIcon.SetActive(false); // �������� �������
            }
        }
    }
}