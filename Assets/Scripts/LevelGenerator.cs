using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject blockPrefab; // Префаб блока
    public int gridWidth = 10; // Ширина сетки блоков
    public int gridHeight = 5; // Высота сетки блоков
    public float spacing = 1.5f; // Расстояние между блоками
    public int currentLevel = 1; // Текущий уровень

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
                // Создаем блок на сцене
                Vector3 position = new Vector3(x * spacing, y * spacing, 0);
                GameObject block = Instantiate(blockPrefab, position, Quaternion.identity);

                // Настраиваем блок в зависимости от уровня
                ConfigureBlock(block, x, y);
            }
        }
    }

    void ConfigureBlock(GameObject block, int x, int y)
    {
        BlockController blockController = block.GetComponent<BlockController>();

        // На первом уровне все блоки разрушаемые
        if (currentLevel == 1)
        {
            blockController.health = Random.Range(1, 4); // 1-3 жизни
            blockController.lockIcon.SetActive(false); // Скрываем замочек
        }
        else if (currentLevel == 2)
        {
            if (Random.Range(0, 4) == 0) // 25% шанс неразрушаемого блока
            {
                blockController.health = -1; // Неразрушаемый блок
                blockController.lockIcon.SetActive(true); // Показываем замочек
            }
            else
            {
                blockController.health = Random.Range(1, 4); // 1-3 жизни
                blockController.lockIcon.SetActive(false); // Скрываем замочек
            }
        }
        else if (currentLevel == 3)
        {
            if (Random.Range(0, 2) == 0) // 50% шанс неразрушаемого блока
            {
                blockController.health = -1; // Неразрушаемый блок
                blockController.lockIcon.SetActive(true); // Показываем замочек
            }
            else
            {
                blockController.health = Random.Range(1, 4); // 1-3 жизни
                blockController.lockIcon.SetActive(false); // Скрываем замочек
            }
        }
    }
}