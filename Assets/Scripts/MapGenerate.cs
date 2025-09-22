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
            GameObject child = Object.Instantiate(cell, cell.transform);

            cell.AddComponent<SpriteRenderer>();
            cell.GetComponent<SpriteRenderer>().sprite = material;
            cell.transform.localScale = new Vector3(1.7f, 1.7f);
            cell.AddComponent<BoxCollider2D>();
            cell.AddComponent<TachControler>();
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
                    Cells[i, j] = Object.Instantiate(cell, new Vector3(i - 1.2f, j - 1.2f), new Quaternion(0, 0, 0, 0), parent.transform);
                }
            }
            Object.Destroy(child);
            Object.Destroy(cell);
            for (int i = 0; i < Cells.GetLength(0); i++)
            {
                for (int j = 0; j < Cells.GetLength(1); j++)
                {
                    if (Cells[i, j].transform.GetChild(0).GetComponent<TextMeshPro>().text != "") { continue; }

                    int VerticalOrHorizontal = Random.Range(0, 2);
                    int pairs = Random.Range(0, pair.GetLength(0));

                    if (VerticalOrHorizontal == 0)
                    {
                        if ((i + 1 < Cells.GetLength(0)) && (Cells[i + 1, j].transform.GetChild(0).GetComponent<TextMeshPro>().text == ""))
                        {
                            Cells[i, j].transform.GetChild(0).GetComponent<TextMeshPro>().text = $"{pair[pairs, 0]}";
                            Cells[i + 1, j].transform.GetChild(0).GetComponent<TextMeshPro>().text = $"{pair[pairs, 1]}";
                        }
                        else if (j + 1 < Cells.GetLength(1) && (Cells[i, j + 1].transform.GetChild(0).GetComponent<TextMeshPro>().text == ""))
                        {
                            Cells[i, j].transform.GetChild(0).GetComponent<TextMeshPro>().text = $"{pair[pairs, 0]}";
                            Cells[i, j + 1].transform.GetChild(0).GetComponent<TextMeshPro>().text = $"{pair[pairs, 1]}";
                        }
                    }
                    else
                    {
                        if ((j + 1 < Cells.GetLength(1)) && (Cells[i, j + 1].transform.GetChild(0).GetComponent<TextMeshPro>().text == ""))
                        {
                            Cells[i, j].transform.GetChild(0).GetComponent<TextMeshPro>().text = $"{pair[pairs, 0]}";
                            Cells[i, j + 1].transform.GetChild(0).GetComponent<TextMeshPro>().text = $"{pair[pairs, 1]}";
                        }
                        else if (i + 1 < Cells.GetLength(0) && (Cells[i + 1, j].transform.GetChild(0).GetComponent<TextMeshPro>().text == ""))
                        {
                            Cells[i, j].transform.GetChild(0).GetComponent<TextMeshPro>().text = $"{pair[pairs, 0]}";
                            Cells[i + 1, j].transform.GetChild(0).GetComponent<TextMeshPro>().text = $"{pair[pairs, 1]}";
                        }
                    }
                }
            }
        }

        private int?[,] Cash = new int?[2, 2];
        public void TachCell()
        {
            for (int i = 0; i < Cells.GetLength(0); i++)
            {
                for (int j = 0; j < Cells.GetLength(1); j++)
                {
                    if (Cells[i, j].transform.GetComponent<TachControler>().isTach == true)
                    {
                        if (Cash[0, 0] != null && Cash[0, 1] != null)
                        {
                            Cash[0, 0] = i;
                            Cash[0, 1] = j;
                        }
                        else
                        {
                            Cash[1, 0] = i;
                            Cash[1, 1] = j;
                        }
                    }
                }
            }
        }
    }
}