//using UnityEngine;

//public class BlockGenerator : MonoBehaviour
//{
//    public GameObject[] blockPrefabs; // ������� ������
//    public int gridWidth = 10; // ������ ����� ������
//    public int gridHeight = 5; // ������ ����� ������
//    public float spacing = 1.5f; // ���������� ����� �������

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
//                // �������� ��������� ������ �����
//                GameObject blockPrefab = blockPrefabs[Random.Range(0, blockPrefabs.Length)];

//                // ������� ���� �� �����
//                Vector3 position = new Vector3(x * spacing, y * spacing, 0);
//                GameObject block = Instantiate(blockPrefab, position, Quaternion.identity);

//                // ����������� ���� � ����������� �� ��� ����
//                if (block.CompareTag("Indestructible"))
//                {
//                    // ������������� ����
//                    block.GetComponent<BlockController>().health = -1; // -1 ��������, ��� ���� �� �����������
//                }
//                else if (block.CompareTag("Moving"))
//                {
//                    // ���������� ����
//                    block.AddComponent<MovingBlock>();
//                }
//                else if (block.CompareTag("Reflective"))
//                {
//                    // ���������� ����
//                    block.AddComponent<ReflectiveBlock>();
//                }
//                else
//                {
//                    // ������� ���� � ��������� ����������� ��������
//                    block.GetComponent<BlockController>().health = Random.Range(1, 4);
//                }
//            }
//        }
//    }
//}