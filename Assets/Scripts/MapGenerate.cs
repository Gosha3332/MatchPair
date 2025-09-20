using TMPro;
using UnityEngine;

namespace MapGenerate
{
    public class MapGenerate : MonoBehaviour
    {
        [SerializeField] private Sprite mat;
        private GameObject gObject;
        public void Gener(int WH)
        {
            gObject = gameObject;
            Generate gener = new Generate(WH, mat, gObject);
            gener.GenerateMap();
        }
    }

    class Generate
    {
        private int mapWH;
        private Sprite material;
        private GameObject parent;

        public GameObject[,] Cells;
        private int[,] pair = new int[,] { { 1, 9 }, { 2, 8 }, { 3, 7 }, { 4, 6 }, { 5, 5 }, { 6, 4 }, { 7, 3 }, { 8, 2 }, { 9, 1 } };

        public Generate(int mapWH, Sprite material, GameObject parent) { this.mapWH = mapWH; this.material = material; this.parent = parent; }

        public void GenerateMap()
        {
            Cells = new GameObject[mapWH, mapWH];

            GameObject cell = new GameObject();
            cell.AddComponent<SpriteRenderer>();
            cell.GetComponent<SpriteRenderer>().sprite = material;
            cell.transform.localScale = new Vector3(1.5f, 1.5f);

            GameObject child = Object.Instantiate(new GameObject(), cell.transform);
            child.AddComponent<TextMeshPro>();
            TextMeshPro childText = child.GetComponent<TextMeshPro>();
            childText.fontSize = 4;
            childText.GetComponent<RectTransform>().sizeDelta = new Vector2(1f, 1f);
            childText.color = Color.black;
            childText.horizontalAlignment = HorizontalAlignmentOptions.Center;
            childText.verticalAlignment = VerticalAlignmentOptions.Middle;


            for (int i = 0; i < Cells.GetLength(0); i++)
            {
                for (int j = 0; j < Cells.GetLength(1); j++)
                {
                    Cells[i, j] = Object.Instantiate(cell,new Vector3( i - 1.2f, j - 1.2f), new Quaternion(0, 0, 0, 0), parent.transform);
                    if (((Cells[i, j].transform.GetChild(0).GetComponent<TextMeshPro>().text != "" && Cells[i + 1, j].transform.GetChild(0).GetComponent<TextMeshPro>().text != "") || (Cells[i, j].transform.GetChild(0).GetComponent<TextMeshPro>().text != "" && Cells[i, j + 1].transform.GetChild(0).GetComponent<TextMeshPro>().text != "")))
                    {
                        int h = Random.Range(0, 1);
                        if (h == 0)
                        {
                            int k = Random.Range(0, 8);
                            Cells[i, j].transform.GetChild(0).GetComponent<TextMeshPro>().text = $"{pair[1, k]}";
                            Cells[i + 1, j].transform.GetChild(0).GetComponent<TextMeshPro>().text = $"{pair[2, k]}";
                        }
                        else
                        {
                            int k = Random.Range(0, 8);
                            Cells[i, j].transform.GetChild(0).GetComponent<TextMeshPro>().text = $"{pair[k, 1]}";
                            Cells[i, j + 1].transform.GetChild(0).GetComponent<TextMeshPro>().text = $"{pair[k, 2]}";
                        }
                    }
                }
            }
            Object.Destroy(cell);
        }
    }
}
