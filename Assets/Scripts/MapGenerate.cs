using UnityEngine;

public class MapGenerate : MonoBehaviour
{
    [SerializeField] private int mapCount;

    public GameObject[,] mapObject;
    private int[,] pair = new int[,] { { 1, 9 }, { 2, 8 }, { 3, 7 }, { 4, 6 }, { 5, 5 }, { 6, 4 }, { 7, 3 }, { 8, 2 }, { 9, 1 } };


    private void GenerateMap()
    {
        mapObject = new GameObject[mapCount, mapCount];
        for (int i = 0; i < mapObject.GetLength(0); i++)
        {
            for (int j = 0; j < mapObject.GetLength(1); j++)
            {
                bool next = false;
                if(mapObject[i-1, j]) { next = true; }
                if (next) { mapObject[i, j] = Instantiate(gameObject, mapObject[i - 1, j].gameObject.transform.position, new Quaternion(0, 0, 0, 0)); }
                mapObject[i, j].gameObject.AddComponent<SpriteRenderer>();
                mapObject[i, j].gameObject.AddComponent<TextMesh>();
                if (((mapObject[i, j].gameObject.GetComponent<TextMesh>().text != "" && mapObject[i + 1, j].gameObject.GetComponent<TextMesh>().text != "") || (mapObject[i, j].gameObject.GetComponent<TextMesh>().text != "" && mapObject[i, j + 1].gameObject.GetComponent<TextMesh>().text != "")))
                {
                    int h = Random.Range(0, 1);
                    if (h == 0)
                    {
                        int k = Random.Range(0, 8);
                        mapObject[i, j].GetComponent<TextMesh>().text = $"{pair[1, k]}";
                        mapObject[i + 1, j].GetComponent<TextMesh>().text = $"{pair[2, k]}";
                    }
                    else
                    {
                        int k = Random.Range(0, 8);
                        mapObject[i, j].GetComponent<TextMesh>().text = $"{pair[k, 1]}";
                        mapObject[i, j + 1].GetComponent<TextMesh>().text = $"{pair[k, 2]}";
                    }
                }
            }
        }
    }
}
