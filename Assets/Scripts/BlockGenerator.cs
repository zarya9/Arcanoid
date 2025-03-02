//using UnityEngine;

//public class BlockGenerator : MonoBehaviour
//{
//    public GameObject[] blockPrefabs; // Префабы блоков
//    public int gridWidth = 10; // Ширина сетки блоков
//    public int gridHeight = 5; // Высота сетки блоков
//    public float spacing = 1.5f; // Расстояние между блоками

//    void Start()
//    {
//        GenerateBlocks();
//    }

//    void GenerateBlocks()
//    {
//        for (int x = 0; x < gridWidth; x++)
//        {
//            for (int y = 0; y < gridHeight; y++)
//            {
//                // Выбираем случайный префаб блока
//                GameObject blockPrefab = blockPrefabs[Random.Range(0, blockPrefabs.Length)];

//                // Создаем блок на сцене
//                Vector3 position = new Vector3(x * spacing, y * spacing, 0);
//                GameObject block = Instantiate(blockPrefab, position, Quaternion.identity);

//                // Настраиваем блок в зависимости от его типа
//                if (block.CompareTag("Indestructible"))
//                {
//                    // Неразрушаемый блок
//                    block.GetComponent<BlockController>().health = -1; // -1 означает, что блок не разрушается
//                }
//                else if (block.CompareTag("Moving"))
//                {
//                    // Движущийся блок
//                    block.AddComponent<MovingBlock>();
//                }
//                else if (block.CompareTag("Reflective"))
//                {
//                    // Отражающий блок
//                    block.AddComponent<ReflectiveBlock>();
//                }
//                else
//                {
//                    // Обычный блок с случайным количеством здоровья
//                    block.GetComponent<BlockController>().health = Random.Range(1, 4);
//                }
//            }
//        }
//    }
//}