using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellGenerator : MonoBehaviour
{

    public Transform pool;
    public GameObject[] cells;
    public int maxCells;
    public int maxSuperiorCells;
    public float timerLimit;
    public float xPosMin, xPosMax, yPosMin, yPosMax;

    private float timer = 0;

    void Update()
    {
        if (pool.childCount < maxCells) {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            } else
            {
                AddCell();
                timer = timerLimit;
            }
        }
    }

    private void AddCell()
    {
        var arrayOfChildren = pool.transform
                .Cast<Transform>()
                .Where(c=>c.gameObject.tag == "Superior Cells")
                .ToArray();

        int index = arrayOfChildren.Length >= maxSuperiorCells ? 0 : (int) Random.Range(0, cells.Length);
        float x = Random.Range(xPosMin, xPosMax);
        float y = Random.Range(yPosMin, yPosMax);

        GameObject clone = Instantiate(cells[index], new Vector2(x, y), Quaternion.identity);
        clone.transform.parent = pool;
    }

}
